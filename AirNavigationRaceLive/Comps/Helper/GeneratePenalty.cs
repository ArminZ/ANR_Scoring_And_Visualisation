using NetworkObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirNavigationRaceLive.Comps.Helper
{
 public class GeneratePenalty
    {
        private const long tickOfSecond = 10000000;
        private const long tickOfMinute = tickOfSecond * 60;

        public static void CalculateAndPersistPenaltyPoints(Client.DataAccess c, Flight f)
        {
            List<Penalty> penalties = CalculatePenaltyPoints(f);
            c.DBContext.PenaltySet.RemoveRange(f.Penalty);
            f.Penalty.Clear();
            foreach (Penalty p in penalties)
            {
                f.Penalty.Add(p);
            }
            c.DBContext.SaveChanges();
        }

        public static List<Penalty> CalculatePenaltyPoints(Flight flight)
        {
            List<Penalty> result = new List<Penalty>();
            Point last = null;
            List<LineP> PenaltyZoneLines = new List<LineP>();
            QualificationRound qr = flight.QualificationRound;
            Parcour parcour = flight.QualificationRound.Parcour;
            foreach (Line nl in parcour.Line.Where(p => p.Type == ((int)LineType.PENALTYZONE)))
            {
                PenaltyZoneLines.Add(getLine(nl));
            }

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
            foreach (Point d in flight.Point)
            {
                maxTimestamp = Math.Max(d.Timestamp, maxTimestamp);
            }
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
                if (intersectionTakeOff != -1)
                {
                    haveCrossedTakeOff = true;
                    long crossTime = (long) Math.Floor(l.TimestamStart + (l.TimestamEnd - l.TimestamStart) * intersectionStart);
                    long diff = crossTime - flight.TimeTakeOff;
                    int seconds = (int)(diff / tickOfSecond);
                    if (diff < 0)
                    {
                        seconds++;
                    }
                    if (seconds > 60 || seconds < 0)
                    {
                        Penalty penalty = new Penalty();
                        penalty.Points = 200;
                        penalty.Reason = "Crossed Takeoff-Line at " + new DateTime((Int64)crossTime).ToLongTimeString() + " instead of expected " + new DateTime((Int64)flight.TimeTakeOff).ToLongTimeString();
                        result.Add(penalty);
                    }
                }
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
                    if (seconds > 1)
                    {
                        Penalty penalty = new Penalty();
                        penalty.Points = Math.Min((seconds-1) * 3, 200);
                        penalty.Reason = "Crossed Start-Line at " + new DateTime((Int64)crossTime).ToLongTimeString() + " instead of expected " + new DateTime((Int64)flight.TimeStartLine).ToLongTimeString();
                        result.Add(penalty);
                    }
                }
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
                    if (seconds>1)
                    {
                        Penalty penalty = new Penalty();
                        penalty.Points = Math.Min((seconds-1) * 3, 200);
                        penalty.Reason = "Crossed End-Line at " + new DateTime((Int64)crossTime).ToLongTimeString() + " instead of expected " + new DateTime((Int64)flight.TimeEndLine).ToLongTimeString();
                        result.Add(penalty);
                    }
                }
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
                            int sec = (int)Math.Floor(((intersectionPenalty - timeSinceInsidePenalty) / tickOfSecond));
                            if (sec > 5)
                            {
                                Penalty penalty = new Penalty();
                                penalty.Points = Math.Min((sec-5) * 3, 300);
                                penalty.Reason = "Inside Penaltyzone for " + sec + " sec from " + new DateTime((Int64)timeSinceInsidePenalty).ToLongTimeString() + " to " + new DateTime((Int64)intersectionPenalty).ToLongTimeString();
                                result.Add(penalty);
                            }
                        }
                    }
                }
            }
            if (shouldHaveCrossedTakeOff && !haveCrossedTakeOff)
            {
                Penalty penalty = new Penalty();
                penalty.Points = 200;
                penalty.Reason = "Takeoff not passed";
                result.Add(penalty);

            }; 
            if (shouldHaveCrossedStart && !haveCrossedStart)
            {
                Penalty penalty = new Penalty();
                penalty.Points = 200;
                penalty.Reason = "Start not passed";
                result.Add(penalty);

            };
            if (shouldHaveCrossedEnd && !haveCrossedEnd)
            {
                Penalty penalty = new Penalty();
                penalty.Points = 200;
                penalty.Reason = "End not passed";
                result.Add(penalty);
            };

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
        private static LineP getStartLine(Parcour parcour, NetworkObjects.Route type)
        {
            Line nl = null;
            try
            {
                switch (type)
                {
                    case NetworkObjects.Route.A:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.START_A); break;
                        }
                    case NetworkObjects.Route.B:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.START_B); break;
                        }
                    case NetworkObjects.Route.C:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.START_C); break;
                        }
                    case NetworkObjects.Route.D:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.START_D); break;
                        }
                }
            }
            catch { }
            LineP l=null;
            if (nl != null)
            {
                l = new LineP();
                l.start = new Vector(nl.A.longitude, nl.A.latitude, 0);
                l.end = new Vector(nl.B.longitude, nl.B.latitude, 0);
                l.orientation = new Vector(nl.O.longitude, nl.O.latitude, 0);
            }
            return l;
        }
        private static LineP getEndLine(Parcour parcour, NetworkObjects.Route type)
        {
            Line nl = null;
            try
            {
                switch (type)
                {
                    case NetworkObjects.Route.A:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.END_A); break;
                        }
                    case NetworkObjects.Route.B:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.END_B); break;
                        }
                    case NetworkObjects.Route.C:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.END_C); break;
                        }
                    case NetworkObjects.Route.D:
                        {
                            nl = parcour.Line.Single(p => p.Type == (int)LineType.END_D); break;
                        }
                }
            }
            catch { }
            LineP l=null;
            if (nl != null)
            {
                l = new LineP();
                l.start = new Vector(nl.A.longitude, nl.A.latitude, 0);
                l.end = new Vector(nl.B.longitude, nl.B.latitude, 0);
                l.orientation = new Vector(nl.O.longitude, nl.O.latitude, 0);
            }
            return l;
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
