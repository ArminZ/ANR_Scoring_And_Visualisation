using System;
using System.Collections.Generic;
using System.Linq;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Comps.Helper
{
    public class ParcourModelSingle
    {
        private ParcourChannelSingle Channel = null;
        private List<ParcourPolygon> Polygons = new List<ParcourPolygon>();
        private double desiredLength;
        private double weight = double.MinValue;
        private double channelWidth;
        private Converter c;
        public double straightLength = 0;
        public double lenght = 0;

        public ParcourModelSingle(ParcourSet parcour, Converter c, double channel, double channelLength)
        {
            this.desiredLength = Converter.NMtoM(channelLength);
            this.channelWidth = Converter.NMtoM(channel);
            this.c = c;
            List<Line> lines = new List<Line>(parcour.Line);
            AddLineAsCorridor(c, lines.Single(p => p.Type == (int)LineType.START_A), lines.Single(p => p.Type == (int)LineType.END_A));
        }

        public ParcourModelSingle(ParcourSet parcour, Converter c, double channel, double channelLength, bool regenerate)
        {
            this.desiredLength = Converter.NMtoM(channelLength);
            this.channelWidth = Converter.NMtoM(channel);
            this.c = c;
            List<Line> lines = new List<Line>(parcour.Line);
            AddLineAsCorridor(c, lines.Single(p => p.Type == (int)LineType.START_A), lines.Single(p => p.Type == (int)LineType.END_A), lines, LineType.START_A);
        }


        public ParcourModelSingle(ParcourModelSingle pm, double firstWeight)
        {
            this.desiredLength = pm.desiredLength;
            this.channelWidth = pm.channelWidth;
            this.c = pm.c;
            AddCorridor(pm.Channel);
            Randomize(firstWeight);
        }

        public void addPolygons()
        {
            Point Start = new Point();
            Start.longitude = c.XtoLongitude(Channel.Start.X);
            Start.latitude = c.YtoLatitude(Channel.Start.Y);
            //TODO

            double latitude = Start.latitude;
            double longitude = Start.longitude;

            double channelMeterKm = channelWidth;
            double latitudePlus1 = latitude + 1;
            double longitudePlus1 = longitude + 1;

            double distLat1 = Converter.Distance(longitude, latitude, longitude, latitudePlus1);
            double distLong1 = Converter.Distance(longitude, latitude, longitudePlus1, latitude);

            double corridorDiffInLat = channelMeterKm / distLat1;
            double corridorDiffInLong = channelMeterKm / distLong1;

            double channelLatitude = corridorDiffInLat / 2;
            double channelLongitude = corridorDiffInLong / 2;

            double longitudePlusChannel = longitude + channelLongitude;
            double latitudePlusChannel = latitude + channelLatitude;

            double channelx = c.LongitudeToX(longitudePlusChannel) - Channel.Start.X;
            double channely = c.LatitudeToY(latitudePlusChannel) - Channel.Start.Y;

            Vector channel = new Vector(channelx, channely, 0);

            Vector startEnd = new Vector(Channel.End.X, Channel.End.Y, 0) - new Vector(Channel.Start.X, Channel.Start.Y, 0);
            Vector startEndOrtho = Vector.Orthogonal(startEnd) * (-1);
            Vector startEndOrthoNormalized = (startEndOrtho / Vector.Abs(startEndOrtho)) * Vector.Abs(channel);
            Vector startEndNormalized = (startEnd / Vector.Abs(startEnd)) * Vector.Abs(channel);

            for (int i = 1; i < 8; i++)
            {
                Vector localChannel = Channel.LinearCombinations[i + 1] - Channel.LinearCombinations[i];
                Vector localChannelOrtho = Vector.Orthogonal(localChannel);
                Vector localChannelOrthoNormalized = (localChannelOrtho / Vector.Abs(localChannelOrtho)) * -Vector.Abs(channel);

                Vector a1_intersection = Vector.InterceptionLine(Channel.LinearCombinations[i], startEndOrtho, Channel.LinearCombinations[i] + localChannelOrthoNormalized - localChannel * 10, Channel.LinearCombinations[i + 1] + localChannelOrthoNormalized + localChannel * 10);

                {
                    Vector localChannel2 = Channel.LinearCombinations[i] - Channel.LinearCombinations[i - 1];
                    Vector localChannelOrtho2 = Vector.Orthogonal(localChannel2);
                    Vector localChannelOrthoNormalized2 = (localChannelOrtho2 / Vector.Abs(localChannelOrtho2)) * -Vector.Abs(channel);
                    Vector a1_intersection2 = Vector.InterceptionLine(Channel.LinearCombinations[i], startEndOrtho, Channel.LinearCombinations[i] + localChannelOrthoNormalized2 - localChannel2 * 10, Channel.LinearCombinations[i] + localChannelOrthoNormalized2 + localChannel2 * 10);
                    a1_intersection = new Vector(a1_intersection.X, Math.Min(a1_intersection.Y, a1_intersection2.Y), 0);
                }

                Vector a2_intersection = Vector.InterceptionLine(Channel.LinearCombinations[i + 1], startEndOrtho, Channel.LinearCombinations[i] + localChannelOrthoNormalized - localChannel * 10, Channel.LinearCombinations[i + 1] + localChannelOrthoNormalized + localChannel * 10);

                {
                    Vector localChannel2 = Channel.LinearCombinations[i + 2] - Channel.LinearCombinations[i + 1];
                    Vector localChannelOrtho2 = Vector.Orthogonal(localChannel2);
                    Vector localChannelOrthoNormalized2 = (localChannelOrtho2 / Vector.Abs(localChannelOrtho2)) * -Vector.Abs(channel);
                    Vector a2_intersection2 = Vector.InterceptionLine(Channel.LinearCombinations[i + 1], startEndOrtho, Channel.LinearCombinations[i + 1] + localChannelOrthoNormalized2 - localChannel2 * 10, Channel.LinearCombinations[i + 1] + localChannelOrthoNormalized2 + localChannel2 * 10);
                    a2_intersection = new Vector(a2_intersection.X, Math.Min(a2_intersection.Y, a2_intersection2.Y), 0);
                }

                Vector a1 = a1_intersection;
                Vector a2 = a2_intersection;
                Vector a3 = a1 + startEndOrtho / 3;
                Vector a4 = a2 + startEndOrtho / 3;
                Polygons.Add(new ParcourPolygon(a1, a2, a4, a3));



                Vector b1_intersection = Vector.InterceptionLine(Channel.LinearCombinations[i], startEndOrtho, Channel.LinearCombinations[i] - localChannelOrthoNormalized - localChannel * 10, Channel.LinearCombinations[i + 1] - localChannelOrthoNormalized + localChannel * 10);
                {
                    Vector localChannel2 = Channel.LinearCombinations[i] - Channel.LinearCombinations[i - 1];
                    Vector localChannelOrtho2 = Vector.Orthogonal(localChannel2);
                    Vector localChannelOrthoNormalized2 = (localChannelOrtho2 / Vector.Abs(localChannelOrtho2)) * -Vector.Abs(channel);
                    Vector b1_intersection2 = Vector.InterceptionLine(Channel.LinearCombinations[i], startEndOrtho, Channel.LinearCombinations[i] - localChannelOrthoNormalized2 - localChannel2 * 10, Channel.LinearCombinations[i] - localChannelOrthoNormalized2 + localChannel2 * 10);
                    b1_intersection = new Vector(b1_intersection.X, Math.Max(b1_intersection.Y, b1_intersection2.Y), 0);
                }

                Vector b2_intersection = Vector.InterceptionLine(Channel.LinearCombinations[i + 1], startEndOrtho, Channel.LinearCombinations[i] - localChannelOrthoNormalized - localChannel * 10, Channel.LinearCombinations[i + 1] - localChannelOrthoNormalized + localChannel * 10);

                {
                    Vector localChannel2 = Channel.LinearCombinations[i + 2] - Channel.LinearCombinations[i + 1];
                    Vector localChannelOrtho2 = Vector.Orthogonal(localChannel2);
                    Vector localChannelOrthoNormalized2 = (localChannelOrtho2 / Vector.Abs(localChannelOrtho2)) * -Vector.Abs(channel);
                    Vector b2_intersection2 = Vector.InterceptionLine(Channel.LinearCombinations[i + 1], startEndOrtho, Channel.LinearCombinations[i + 1] - localChannelOrthoNormalized2 - localChannel2 * 10, Channel.LinearCombinations[i + 1] - localChannelOrthoNormalized2 + localChannel2 * 10);
                    b2_intersection = new Vector(b2_intersection.X, Math.Max(b2_intersection.Y, b2_intersection2.Y), 0);
                }

                Vector b1 = b1_intersection;
                Vector b2 = b2_intersection;
                Vector b3 = b1 - startEndOrtho / 3;
                Vector b4 = b2 - startEndOrtho / 3;
                Polygons.Add(new ParcourPolygon(b1, b2, b4, b3));
            }
        }

        public ParcourChannelSingle getChannel()
        {
            return Channel;
        }
        public List<ParcourPolygon> getPolygons()
        {
            return Polygons;
        }
        public double Weight(Converter c)
        {
            if (weight == double.MinValue)
            {
                straightLength = Channel.getDistanceStraight(c);
                lenght = Channel.getDistance(c);
                weight = Math.Abs(lenght - desiredLength);
            }
            return weight;
        }

        private void Randomize(double factor)
        {
            Channel.Randomize(factor);
        }

        private void AddLineAsCorridor(Converter c, Line start, Line end)
        {
            Vector MiddleStart = Vector.Middle(getVector(c, start.A), getVector(c, start.B));
            Vector MiddleEnd = Vector.Middle(getVector(c, end.A), getVector(c, end.B));
            Channel = new ParcourChannelSingle(MiddleStart, MiddleEnd, c);
        }

        private void AddLineAsCorridor(Converter c, Line start, Line end, List<Line> lines, LineType lineType)
        {
            Vector MiddleStart = Vector.Middle(getVector(c, start.A), getVector(c, start.B));
            Vector MiddleEnd = Vector.Middle(getVector(c, end.A), getVector(c, end.B));
            Channel = new ParcourChannelSingle(MiddleStart, MiddleEnd, lineType, lines, c);
        }

        private void AddCorridor(ParcourChannelSingle c)
        {
            Channel = new ParcourChannelSingle(c);
        }

        public static Vector getVector(Converter c, Point point)
        {
            return new Vector(c.LongitudeToX(point.longitude), c.LatitudeToY(point.latitude), 0);
        }
    }

    public class ParcourChannelSingle
    {
        internal Vector Start;
        internal Vector End;
        internal List<Vector> LinearCombinations = new List<Vector>(10);
        internal List<Vector> ImmutablePoints = new List<Vector>();

        public List<Vector> getLinearCombinations()
        {
            return LinearCombinations;
        }

        public ParcourChannelSingle(Vector Start, Vector End, Converter c)
        {
            this.Start = Start;
            this.End = End;
            Vector StartEnd = Vector.Direction(Start, End);
            for (int i = 0; i < 10; i++)
            {
                Vector linComb = Start + (StartEnd * (i / 9.0));
                LinearCombinations.Add(linComb);
            }
            double dist = getDistance(c);
            double straightDist = getDistanceStraight(c);
            if (dist - straightDist > 0.1)
            {
                System.Console.Out.WriteLine("ERROR");
            }
        }

        public ParcourChannelSingle(Vector Start, Vector End, LineType type, List<Line> lines, Converter c)
        {
            this.Start = Start;
            this.End = End;
            List<Line> pointLine = lines.Where(p => p.Type == (int)LineType.Point).ToList();
            int i = 0;
            List<Line> corridorPoints = new List<Line>();
            for (int j = 0; j < 9; j++)
            {
                corridorPoints.Add(pointLine[i + j]);
            }
            foreach (Line l in corridorPoints)
            {
                Vector v = ParcourModel.getVector(c, l.A);

                if (isEdited(l))
                {
                    ImmutablePoints.Add(v);
                }
                LinearCombinations.Add(v);
            }
            LinearCombinations.Add(End);
        }
        private bool isEdited(Line l)
        {
            return false;//TODO l.A.edited || l.B.edited;
        }
        private bool samePos(Point a, Point b)
        {
            return Vector.Abs(new Vector(a.longitude, a.latitude, a.altitude) - new Vector(b.longitude, b.latitude, b.altitude)) < 0.00001;
        }

        public ParcourChannelSingle(ParcourChannelSingle pc)
        {
            this.Start = pc.Start;
            this.End = pc.End;
            foreach (Vector v in pc.LinearCombinations)
            {
                Vector vec = new Vector(v);
                LinearCombinations.Add(vec);
                if (pc.ImmutablePoints.Contains(v))
                {
                    ImmutablePoints.Add(vec);
                }
            }
        }

        internal void Randomize(double factor)
        {
            for (int i = 2; i < 8; i++)
            {
                Vector v = LinearCombinations.ElementAt(i);
                if (!ImmutablePoints.Contains(v))
                {
                    int sign = (i % 2 == 1) ? 1 : -1;
                    Vector orth = Vector.Orthogonal(End - Start);
                    orth = (orth / Vector.Abs(orth)) * (Utils.getNextDouble() * factor * sign);
                    v.X += orth.X;
                    v.Y += orth.Y;
                }
            }
        }
        public double getDistance(Converter c)
        {
            double result = 0;
            Vector last = Start;
            foreach (Vector v in LinearCombinations)
            {
                Point Ende = new Point();
                Ende.longitude = c.XtoLongitude(last.X);
                Ende.latitude = c.YtoLatitude(last.Y);
                Point ss = new Point();
                ss.longitude = c.XtoLongitude(v.X);
                ss.latitude = c.YtoLatitude(v.Y);
                double dist = Converter.Distance(Ende, ss);

                result += dist;
                last = v;
            }
            return result;
        }

        public double getDistanceStraight(Converter c)
        {
            Point Ende = new Point();
            Ende.longitude = c.XtoLongitude(Start.X);
            Ende.latitude = c.YtoLatitude(Start.Y);
            Point ss = new Point();
            ss.longitude = c.XtoLongitude(End.X);
            ss.latitude = c.YtoLatitude(End.Y);
            double dist = Converter.Distance(Ende, ss);

            return dist;
        }
    }

    public class ParcourPolygonSingle
    {
        List<Vector> Edges = new List<Vector>();
        public ParcourPolygonSingle(Vector p1, Vector p2, Vector p3, Vector p4)
        {
            Edges.Add(p1);
            Edges.Add(p2);
            Edges.Add(p3);
            Edges.Add(p4);
        }
        public List<Vector> getEdges()
        {
            return Edges;
        }
    }
}
