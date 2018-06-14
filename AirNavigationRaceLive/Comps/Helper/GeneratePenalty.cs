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
            List<PenaltySet> penalties = CalculatePenaltyPoints(f);
            c.DBContext.PenaltySet.RemoveRange(f.PenaltySet);
            f.PenaltySet.Clear();
            foreach (PenaltySet p in penalties)
            {
                f.PenaltySet.Add(p);
            }
            c.DBContext.SaveChanges();
        }

        public static List<PenaltySet> CalculatePenaltyPoints(FlightSet flight)
        {
            Point last = null;
            List<PenaltySet> result = new List<PenaltySet>();
            List<LineP> PenaltyZoneLines = new List<LineP>();

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
                return result;
            }
            LineP takeOffLine = new LineP();
            takeOffLine.start = new Vector(qr.TakeOffLine.A.longitude, qr.TakeOffLine.A.latitude, 0);
            takeOffLine.end = new Vector(qr.TakeOffLine.B.longitude, qr.TakeOffLine.B.latitude, 0);
            takeOffLine.orientation = Vector.Orthogonal(takeOffLine.end - takeOffLine.start);

            long maxTimestamp = 0;
            //foreach (Point d in flight.Point)
            //{
            //    maxTimestamp = Math.Max(d.Timestamp, maxTimestamp);
            //}
            maxTimestamp = flight.Point.Max(x => x.Timestamp);

            // For TakeOff-, Start- and End Line, assume that these lines should have been passed two minutes after expected passing time 
            // used in case they are not passed
            bool shouldHaveCrossedTakeOff = (maxTimestamp - 2 * tickOfMinute) > flight.TimeTakeOff;
            bool shouldHaveCrossedStart = (maxTimestamp - 2 * tickOfMinute) > flight.TimeStartLine;
            bool shouldHaveCrossedEnd = (maxTimestamp - 2 * tickOfMinute) > flight.TimeEndLine;

            bool haveCrossedTakeOff = false;
            bool haveCrossedStart = false;
            bool haveCrossedEnd = false;
            bool insidePenalty = false;
            double timeSinceInsidePenalty = 0;

            foreach (LineP l in dataLines)
            {
                double intersectionTakeOff = getIntersection(l, takeOffLine);
                double intersectionStart = getIntersection(l, startLine);
                double intersectionEnd = getIntersection(l, endLine);

                #region crossing takeOff line
                if (intersectionTakeOff != -1)
                {
                    haveCrossedTakeOff = true;
                    long crossTime = (long)Math.Floor(l.TimestamStart + (l.TimestamEnd - l.TimestamStart) * intersectionStart);
                    long diff = crossTime - flight.TimeTakeOff;
                    int seconds = (int)(diff / tickOfSecond);
                    if (diff < 0)
                    {
                        seconds++;
                    }
                    if (seconds > C_TKOF_TimeUpper || seconds < C_TKOF_TimeLower)
                    {
                        PenaltySet penalty = new PenaltySet();
                        penalty.Points = C_TKOF_MaxPenalty;
                        penalty.Reason = string.Format("Take-off Line planned: {1}, actual: {0}", new DateTime((Int64)crossTime).ToLongTimeString(), new DateTime((Int64)flight.TimeTakeOff).ToLongTimeString());
                        result.Add(penalty);
                    }
                }
                #endregion

                #region crossing start line
                if (intersectionStart != -1)
                {
                    haveCrossedStart = true;
                    long crossTime = (long)Math.Floor(l.TimestamStart + (l.TimestamEnd - l.TimestamStart) * intersectionStart);
                    long diff = crossTime - flight.TimeStartLine;
                    int seconds = (int)Math.Abs(diff / tickOfSecond);
                    if (diff < 0)
                    {
                        seconds++;
                    }
                    if (seconds > C_SPFP_TimeTolerance)
                    {
                        PenaltySet penalty = new PenaltySet();
                        penalty.Points = Math.Min((seconds - C_SPFP_TimeTolerance) * C_PointsPerSec, C_SPFP_MaxPenalty);
                        penalty.Reason = string.Format("SP Line planned: {1}, actual: {0}", new DateTime((Int64)crossTime).ToLongTimeString(), new DateTime((Int64)flight.TimeStartLine).ToLongTimeString());
                        result.Add(penalty);
                    }
                }
                #endregion

                #region crossing end line
                if (intersectionEnd != -1)
                {
                    haveCrossedEnd = true;
                    long crossTime = (long)Math.Floor(l.TimestamStart + (l.TimestamEnd - l.TimestamStart) * intersectionEnd);
                    long diff = crossTime - flight.TimeEndLine;
                    int seconds = (int)(Math.Abs(diff / tickOfSecond));
                    if (diff < 0)
                    {
                        seconds++;
                    }
                    if (seconds > 1)
                    {
                        PenaltySet penalty = new PenaltySet();
                        penalty.Points = Math.Min((seconds - C_SPFP_TimeTolerance) * C_PointsPerSec, C_SPFP_MaxPenalty);
                        penalty.Reason = string.Format("FP Line planned: {1}, actual: {0}", new DateTime((Int64)crossTime).ToLongTimeString(), new DateTime((Int64)flight.TimeEndLine).ToLongTimeString());
                        result.Add(penalty);
                    }
                }
                #endregion

                #region entering or leaving prohibited zone

                bool stateChanged = false;
                double intersectionPenalty = intersectsPenaltyPoint(PenaltyZoneLines, l, out stateChanged);
                if (intersectionPenalty != -1)
                {
                    if (stateChanged)
                    {
                        if (!insidePenalty)
                        {
                            insidePenalty = true;
                            timeSinceInsidePenalty = intersectionPenalty;
                        }
                        else
                        {
                            insidePenalty = false;
                           // int sec = (int)Math.Floor((intersectionPenalty - timeSinceInsidePenalty) / tickOfSecond);
                           // round time period to nearest full second
                            int sec = (int)Math.Round((intersectionPenalty - timeSinceInsidePenalty) / tickOfSecond,MidpointRounding.AwayFromZero);
                            if (sec > C_PROH_TimeTolerance)
                            {
                                PenaltySet penalty = new PenaltySet();
                                // Max penalty inside PROH zone: disabled after NOV 2017
                                // penalty.Points = Math.Min((sec - 5) * 3, 300);
                                penalty.Points = (sec - C_PROH_TimeTolerance) * C_PointsPerSec;
                                // round times entering/leaving penalty zone) to nearest full second
                                long pstart = (long)Math.Round(timeSinceInsidePenalty / tickOfSecond, MidpointRounding.AwayFromZero) * tickOfSecond;
                                long pend = (long)Math.Round(intersectionPenalty/ tickOfSecond,MidpointRounding.AwayFromZero) * tickOfSecond;
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

            return result;
        }
        /// <summary>
        /// Return timestamp in ticks of relevant intersection
        /// </summary>
        /// <param name="penaltyZones"></param>
        /// <param name="line"></param>
        /// <param name="changedState"></param>
        /// <returns></returns>
        private static double intersectsPenaltyPoint(List<LineP> penaltyZones, LineP line, out bool changedState)
        {
            changedState = false;
            double result = -1;
            foreach (LineP nl in penaltyZones)
            {
                double intersection = getIntersection(line, nl);
                if (intersection != -1)
                {
                    changedState = !changedState;
                    result = (line.TimestamStart + (line.TimestamEnd - line.TimestamStart) * intersection);
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
        private static double getIntersection(LineP data, LineP fix)
        {
            Vector intersection = Vector.Interception(data.start, data.end, fix.start, fix.end);
            if (intersection != null)
            {
                return Vector.Abs(intersection - data.start) / Vector.Abs(data.end - data.start);
            }
            return -1;
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
        /// selecting the prohibited zones for the channel that is assigned to the competitor
        /// 
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
}
