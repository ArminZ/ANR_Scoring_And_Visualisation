using AirNavigationRaceLive.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirNavigationRaceLive.Comps.Helper
{
    public class GeneratePenalty
    {
        private const long tickOfSecond = 10000000;
        private const long tickOfMinute = tickOfSecond * 60;

        private const int C_PointsPerSec = 3; // PenaltyPoints per second
        private const int C_TKOF_TimeLower = 0; // lower time limit (sec)n allowed at TKOF
        private const int C_TKOF_TimeUpper = 60; // upper time limit (sec)n allowed at TKOF
        private const int C_SPFP_TimeTolerance = 1; // time tolerance (sec) allowed when passing SF, FP
        private const int C_SPFP_MaxPenalty = 200; // max penalty for not observed/ exceeding time limits on SP/FP 
        private const int C_TKOF_MaxPenalty = 200; // max penalty for not observed/ exceeding time limits on TKOF
        private const int C_PROH_TimeTolerance = 5; // time tolerance (sec) allowed inside PROH area without penalty 
        private const int C_PROH_MaxPenalty = 300; // max penalty for flying inside PROH area

        public static void CalculateAndPersistPenaltyPoints(Client.DataAccess c, FlightSet f)
        {
            // TODO: implement IntersectionPointSet in DBContext
            // implement IntersectionPointSet as output parameter in method CalculatePenaltyPoints
            // persist points to DBContext, will be retrieved for printing
            List<IntersectionPoint> intersectionPoints = new List<IntersectionPoint>();
            List<PenaltySet> penalties = CalculatePenaltyPoints(f, out intersectionPoints);

            c.DBContext.PenaltySet.RemoveRange(f.PenaltySet);
            c.DBContext.IntersectionPointSet.RemoveRange(f.IntersectionPointSet);
            f.PenaltySet.Clear();
            f.IntersectionPointSet.Clear();
            f.PenaltySet = penalties;
            foreach (IntersectionPoint p in intersectionPoints)
            {
                p.Flight_Id = f.Id; // must add the Flight Id
                f.IntersectionPointSet.Add(p);
            }

            c.DBContext.SaveChanges();
        }

        public static List<PenaltySet> CalculatePenaltyPoints(FlightSet flight, out List<IntersectionPoint> lstIntersectionPoints)
        {
            Point last = null;
            List<PenaltySet> result = new List<PenaltySet>();
            List<LineP> PenaltyZoneLines = new List<LineP>();
            // intersectionPoints will contain the intersection poinst of the flight path with SP, FP, PROH areas
            List<IntersectionPoint> intersectionPoints = new List<IntersectionPoint>();

            QualificationRoundSet qr = flight.QualificationRoundSet;
            ParcourSet parcour = flight.QualificationRoundSet.ParcourSet;

            foreach (Line nl in parcour.Line.Where(p => p.Type == (int)LineType.PENALTYZONE))
            {
                PenaltyZoneLines.Add(getLine(nl));
            }
            // add also all channel-specific prohibited zones assigned channel
            PenaltyZoneLines.AddRange(getAssignedProhZonelLines(parcour, (Route)flight.Route));

            List<LineP> dataLines = new List<LineP>();
            foreach (Point g in flight.Point)
            {
                if (last != null)
                {
                    LineP l = new LineP();
                    l.end = new Vector(g.longitude, g.latitude, 0);
                    l.TimestamEnd = g.Timestamp;
                    l.start = new Vector(last.longitude, last.latitude, 0);
                    l.TimestamStart = last.Timestamp;
                    dataLines.Add(l);
                }
                last = g;
            }
            LineP startLine = getStartLine(parcour, (Route)flight.Route);
            LineP endLine = getEndLine(parcour, (Route)flight.Route);
            if (startLine == null || endLine == null)
            {
                lstIntersectionPoints = intersectionPoints;
                return result;
            }
            LineP takeOffLine = new LineP();
            takeOffLine.start = new Vector(qr.TakeOffLine.A.longitude, qr.TakeOffLine.A.latitude, 0);
            takeOffLine.end = new Vector(qr.TakeOffLine.B.longitude, qr.TakeOffLine.B.latitude, 0);
            takeOffLine.orientation = Vector.Orthogonal(takeOffLine.end - takeOffLine.start);

            long maxTimestamp = 0;
            maxTimestamp = flight.Point.Max(x => x.Timestamp);

            // For TakeOff-, Start- and End Line, we obviously(?) assume that these lines should have been passed two minutes after expected passing time 
            // used in case they are not passed
            bool shouldHaveCrossedTakeOff = (maxTimestamp - 2 * tickOfMinute) > flight.TimeTakeOff;
            bool shouldHaveCrossedStart = (maxTimestamp - 2 * tickOfMinute) > flight.TimeStartLine;
            bool shouldHaveCrossedEnd = (maxTimestamp - 2 * tickOfMinute) > flight.TimeEndLine;

            bool haveCrossedTakeOff = false;
            bool haveCrossedStart = false;
            bool haveCrossedEnd = false;
            bool insidePenalty = false;
            long timeSinceInsidePenalty = 0;

            foreach (LineP l in dataLines)
            {
                double intersectionTakeOff = getIntersection(l, takeOffLine);
                double intersectionStart = getIntersection(l, startLine);
                double intersectionEnd = getIntersection(l, endLine);

                IntersectionPoint ipTakeOff = new IntersectionPoint();
                IntersectionPoint ipStart = new IntersectionPoint();
                IntersectionPoint ipEnd = new IntersectionPoint();

                #region crossing takeOff line
                if (getIntersection(l, takeOffLine, out ipTakeOff))
                {
                    haveCrossedTakeOff = true;
                    intersectionPoints.Add(ipTakeOff);
                    long crossTime = ipTakeOff.Timestamp;
                    long diff = crossTime - flight.TimeTakeOff;
                    if (diff > C_TKOF_TimeUpper* tickOfSecond || diff < C_TKOF_TimeLower* tickOfSecond)
                    {
                        crossTime = ((crossTime + (tickOfSecond / 2) + 1) / tickOfSecond) * tickOfSecond; // round
                        PenaltySet penalty = new PenaltySet();
                        penalty.Points = C_TKOF_MaxPenalty;
                        penalty.Reason = string.Format("Take-off Line planned: {1}, actual: {0}", new DateTime((Int64)crossTime).ToLongTimeString(), new DateTime((Int64)flight.TimeTakeOff).ToLongTimeString());
                        result.Add(penalty);
                    }
                }
                #endregion

                #region crossing start line
                if (getIntersection(l, startLine, out ipStart))
                {
                    haveCrossedStart = true;
                    intersectionPoints.Add(ipStart);
                    long crossTime = ipStart.Timestamp;
                    long diff = crossTime - flight.TimeStartLine;
                    if (diff > C_SPFP_TimeTolerance * tickOfSecond)
                    {
                        crossTime = ((crossTime + (tickOfSecond / 2) + 1) / tickOfSecond) * tickOfSecond; // round
                        diff = crossTime - flight.TimeStartLine;
                        int sec = (int)((diff + (tickOfSecond / 2) + 1) / tickOfSecond);
                        PenaltySet penalty = new PenaltySet();
                        penalty.Points = Math.Min((sec - C_SPFP_TimeTolerance) * C_PointsPerSec, C_SPFP_MaxPenalty);
                        penalty.Reason = string.Format("SP Line planned: {1}, actual: {0}", new DateTime((Int64)crossTime).ToLongTimeString(), new DateTime((Int64)flight.TimeStartLine).ToLongTimeString());
                        result.Add(penalty);
                    }
                }
                #endregion

                #region crossing end line
                if (getIntersection(l, endLine, out ipEnd))
                {
                    haveCrossedEnd = true;
                    intersectionPoints.Add(ipEnd);
                    long crossTime = ipEnd.Timestamp;
                    long diff = crossTime - flight.TimeEndLine;
                    if (diff > C_SPFP_TimeTolerance * tickOfSecond)
                    {
                        crossTime = ((crossTime + (tickOfSecond / 2) + 1) / tickOfSecond) * tickOfSecond; // round
                        int sec = (int)((diff + (tickOfSecond / 2) + 1) / tickOfSecond);
                        PenaltySet penalty = new PenaltySet();
                        penalty.Points = Math.Min((sec - C_SPFP_TimeTolerance) * C_PointsPerSec, C_SPFP_MaxPenalty);
                        penalty.Reason = string.Format("FP Line planned: {1}, actual: {0}", new DateTime((Int64)crossTime).ToLongTimeString(), new DateTime((Int64)flight.TimeEndLine).ToLongTimeString());
                        result.Add(penalty);
                    }
                }
                #endregion

                #region entering or leaving prohibited zone

                bool stateChanged = false;
                IntersectionPoint ip = new IntersectionPoint();
                if (intersectsProhAreaPoint(PenaltyZoneLines, l, out stateChanged, out ip))
                {
                    if (stateChanged)
                    {
                        intersectionPoints.Add(ip);
                        if (!insidePenalty)
                        {
                            insidePenalty = true;
                            timeSinceInsidePenalty = ip.Timestamp;
                        }
                        else
                        {
                            insidePenalty = false;
                            long diff = ip.Timestamp - timeSinceInsidePenalty;
                            if (diff > tickOfSecond *C_PROH_TimeTolerance)
                            {
                                PenaltySet penalty = new PenaltySet();
                                // round times for entering/leaving penalty zone to nearest full second
                                long pstart =((timeSinceInsidePenalty + (tickOfSecond / 2) + 1) / tickOfSecond)* tickOfSecond; // rounded
                                long pend = ((ip.Timestamp + (tickOfSecond / 2) + 1) / tickOfSecond) * tickOfSecond; // rounded
                                int sec = (int)((pend-pstart) / tickOfSecond);
                                penalty.Points = (sec - C_PROH_TimeTolerance) * C_PointsPerSec; 
                                // Max penalty inside PROH zone: ======>> NOTE: disabled after NOV 2017
                                // penalty.Points = Math.Min((sec - 5) * 3, 300);
                                penalty.Reason = string.Format("Penalty zone for {0} sec, [{1} - {2}]", sec, new DateTime(pstart).ToLongTimeString(), new DateTime(pend).ToLongTimeString());
                                result.Add(penalty);
                            }
                        }
                    }
                }
                #endregion
            }

            #region handling cases where Takeoff-, Start- or End line are not observed/never crossed
            if (shouldHaveCrossedTakeOff && !haveCrossedTakeOff)
            {
                PenaltySet penalty = new PenaltySet();
                penalty.Points = C_TKOF_MaxPenalty;
                penalty.Reason = "Takeoff Line not passed";
                result.Add(penalty);

            };
            if (shouldHaveCrossedStart && !haveCrossedStart)
            {
                PenaltySet penalty = new PenaltySet();
                penalty.Points = C_SPFP_MaxPenalty;
                penalty.Reason = "SP Line not passed";
                result.Add(penalty);

            };
            if (shouldHaveCrossedEnd && !haveCrossedEnd)
            {
                PenaltySet penalty = new PenaltySet();
                penalty.Points = C_SPFP_MaxPenalty;
                penalty.Reason = "FP Line not passed";
                result.Add(penalty);
            };
            #endregion
            lstIntersectionPoints = intersectionPoints;
            return result;
        }
        /// <summary>
        /// Return timestamp in ticks of relevant intersection
        /// </summary>
        /// <param name="penaltyZones"></param>
        /// <param name="line"></param>
        /// <param name="changedState"></param>
        /// <returns></returns>
        //private static double intersectsProhAreaTimeStamp(List<LineP> penaltyZones, LineP line, out bool changedState)
        //{
        //    changedState = false;
        //    double result = -1;
        //    foreach (LineP nl in penaltyZones)
        //    {
        //        double intersection = getIntersection(line, nl);
        //        if (intersection != -1)
        //        {
        //            changedState = !changedState;
        //            result = line.TimestamStart + (line.TimestamEnd - line.TimestamStart) * intersection;
        //        }
        //    }
        //    return result;
        //}

        /// <summary>
        /// Get interpolated coordinates and timestamp for of the intersection point
        /// </summary>
        /// <param name="penaltyZones"></param>
        /// <param name="line"></param>
        /// <param name="changedState"></param>
        /// <returns></returns>
        //private static IntersectionPoint intersectsProhAreaPoint(List<LineP> penaltyZones, LineP line, out bool changedState)
        //{
        //    changedState = false;
        //    IntersectionPoint result = new IntersectionPoint();
        //    foreach (LineP nl in penaltyZones)
        //    {
        //        double intersection = getIntersection(line, nl);
        //        if (intersection != -1)
        //        {
        //            long timestmp = line.TimestamStart + (long)Math.Truncate((line.TimestamEnd - line.TimestamStart) * intersection);
        //            double x = line.start.X + (line.end.X - line.start.X) * intersection;
        //            double y = line.start.Y + (line.end.Y - line.start.Y) * intersection;
        //            double z = 0;
        //            result.longitude = x;
        //            result.latitude = y;
        //            result.altitude = z;
        //            result.Timestamp = timestmp;

        //            changedState = !changedState;
        //        }
        //    }
        //    return result;
        //}

        private static bool intersectsProhAreaPoint(List<LineP> penaltyZones, LineP data, out bool changedState, out IntersectionPoint ip)
        {
            changedState = false;
            bool result = false;
            ip = new IntersectionPoint();
            foreach (LineP nl in penaltyZones)
            {
                IntersectionPoint ip1 = new IntersectionPoint();
                if (getIntersection(data, nl, out ip1))
                {
                    ip = ip1;
                    result = true;
                    changedState = !changedState;
                }
            }
            return result;
        }

        private static LineP getLine(Line nline)
        {
            LineP line = new LineP();
            line.start = new Vector(nline.A.longitude, nline.A.latitude, 0);
            line.end = new Vector(nline.B.longitude, nline.B.latitude, 0);
            line.orientation = new Vector(nline.O.longitude, nline.O.latitude, 0);
            return line;
        }

        /// <summary>
        /// Find the intersection of data with fix
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fix"></param>
        /// <returns></returns>
        private static double getIntersection(LineP data, LineP fix)
        {
            Vector intersection = Vector.Interception(data.start, data.end, fix.start, fix.end);
            if (intersection != null)
            {
                return Vector.Abs(intersection - data.start) / Vector.Abs(data.end - data.start);
            }
            return -1;
        }

        /// <summary>
        /// Find the intersection of data with fix. Insersection point ip is output parameter
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fix"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        private static bool getIntersection(LineP data, LineP fix, out IntersectionPoint ip)
        {
            ip = new IntersectionPoint();
            bool res = false;
            Vector intersectionV = Vector.Interception(data.start, data.end, fix.start, fix.end);
            if (intersectionV != null)
            {
                double intersection = Vector.Abs(intersectionV - data.start) / Vector.Abs(data.end - data.start);
                long timestmp = data.TimestamStart + (long)Math.Truncate((data.TimestamEnd - data.TimestamStart) * intersection);
                double x = data.start.X + (data.end.X - data.start.X) * intersection;
                double y = data.start.Y + (data.end.Y - data.start.Y) * intersection;
                double z = data.start.Z + (data.end.Z - data.start.Z) * intersection;
                ip.longitude = x;
                ip.latitude = y;
                ip.altitude = z;
                ip.Timestamp = timestmp;
                res = true;
                return res;
            }
            return res;
        }
        private static LineP getStartLine(ParcourSet parcour, Route type)
        {
            Line nl = null;
            try
            {
                switch (type)
                {
                    case Route.A:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.START_A); break;
                        }
                    case Route.B:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.START_B); break;
                        }
                    case Route.C:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.START_C); break;
                        }
                    case Route.D:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.START_D); break;
                        }
                }
            }
            catch { }
            LineP l = null;
            if (nl != null)
            {
                l = new LineP();
                l.start = new Vector(nl.A.longitude, nl.A.latitude, 0);
                l.end = new Vector(nl.B.longitude, nl.B.latitude, 0);
                l.orientation = new Vector(nl.O.longitude, nl.O.latitude, 0);
            }
            return l;
        }
        private static LineP getEndLine(ParcourSet parcour, Route type)
        {
            Line nl = null;
            try
            {
                switch (type)
                {
                    case Route.A:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.END_A); break;
                        }
                    case Route.B:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.END_B); break;
                        }
                    case Route.C:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.END_C); break;
                        }
                    case Route.D:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.END_D); break;
                        }
                }
            }
            catch { }
            LineP l = null;
            if (nl != null)
            {
                l = new LineP();
                l.start = new Vector(nl.A.longitude, nl.A.latitude, 0);
                l.end = new Vector(nl.B.longitude, nl.B.latitude, 0);
                l.orientation = new Vector(nl.O.longitude, nl.O.latitude, 0);
            }
            return l;
        }

        /// <summary>
        /// retrieve channel data for the route that is assigned to the competitor
        /// </summary>
        /// <param name="parcour"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static List<LineP> getAssignedChannelLines(ParcourSet parcour, Route type)
        {
            List<LineP> lst = new List<LineP>();
            switch (type)
            {
                case Route.A:
                    foreach (Line nl in parcour.Line.Where(p => p.Type == (int)LineType.CHANNEL_A))
                    {
                        lst.Add(getLine(nl));
                    }
                    break;
                case Route.B:
                    foreach (Line nl in parcour.Line.Where(p => p.Type == (int)LineType.CHANNEL_B))
                    {
                        lst.Add(getLine(nl));
                    }
                    break;
                case Route.C:
                    foreach (Line nl in parcour.Line.Where(p => p.Type == (int)LineType.CHANNEL_C))
                    {
                        lst.Add(getLine(nl));
                    }
                    break;
                case Route.D:
                    foreach (Line nl in parcour.Line.Where(p => p.Type == (int)LineType.CHANNEL_D))
                    {
                        lst.Add(getLine(nl));
                    }
                    break;
                default:
                    break;
            }
            return lst;
        }

        /// <summary>
        /// retrieve prohibited zones for the route that is assigned to the competitor
        /// </summary>
        /// <param name="parcour"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static List<LineP> getAssignedProhZonelLines(ParcourSet parcour, Route type)
        {
            List<LineP> lst = new List<LineP>();
            switch (type)
            {
                case Route.A:
                    foreach (Line nl in parcour.Line.Where(p => p.Type == (int)LineType.PROH_B || p.Type == (int)LineType.PROH_C || p.Type == (int)LineType.PROH_D))
                    {
                        lst.Add(getLine(nl));
                    }
                    break;
                case Route.B:
                    foreach (Line nl in parcour.Line.Where(p => p.Type == (int)LineType.PROH_A || p.Type == (int)LineType.PROH_C || p.Type == (int)LineType.PROH_D))
                    {
                        lst.Add(getLine(nl));
                    }
                    break;
                case Route.C:
                    foreach (Line nl in parcour.Line.Where(p => p.Type == (int)LineType.PROH_A || p.Type == (int)LineType.PROH_B || p.Type == (int)LineType.PROH_D))
                    {
                        lst.Add(getLine(nl));
                    }
                    break;
                case Route.D:
                    foreach (Line nl in parcour.Line.Where(p => p.Type == (int)LineType.PROH_A || p.Type == (int)LineType.PROH_B || p.Type == (int)LineType.PROH_C))
                    {
                        lst.Add(getLine(nl));
                    }
                    break;
                default:
                    break;
            }
            return lst;
        }

        /// <summary>
        /// Determines if the given point is inside the polygon
        /// </summary>
        /// <param name="polygon">the vertices of polygon</param>
        /// <param name="testPoint">the given point</param>
        /// <returns>true if the point is inside the polygon; otherwise, false</returns>
        public static bool IsPointInPolygon4(Vector[] polygon, Vector testPoint)
        {
            // see https://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon
            // NOTE: not yet in use
            bool result = false;
            int j = polygon.Length - 1;
            for (int i = 0; i < polygon.Length; i++)
            {
                if (polygon[i].Y < testPoint.Y && polygon[j].Y >= testPoint.Y || polygon[j].Y < testPoint.Y && polygon[i].Y >= testPoint.Y)
                {
                    if (polygon[i].X + (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
    }
    class LineP
    {
        public Vector start;
        public Vector end;
        public Vector orientation;
        public long TimestamStart;
        public long TimestamEnd;
    }

    class PointP
    {
        public double X = 0;
        public double Y = 0;
        public double Z = 0;
        public long Timestamp = 0;

        public PointP(double X, double Y, double Z, long Timestamp)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.Timestamp = Timestamp;
        }
    }
}
