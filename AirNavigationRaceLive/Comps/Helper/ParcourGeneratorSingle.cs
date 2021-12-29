using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Comps.Helper
{
    public class ParcourGeneratorSingle
    {
        private const double LineOfNoReturnDist = 1.5;
        private double best = double.MaxValue;
        private volatile ParcourModelSingle bestModel = null;
        private ParcourSet parcour;
        private Converter c;
        private ComparerSingle comparer;
        public volatile bool finished = false;
        public double bestStraightLength = 0;
        public double bestLegLength = 0;
        private volatile bool regenerate = false;

        private void ProcessList(object o)
        {
            List<ParcourModelSingle> list = o as List<ParcourModelSingle>;
            bool switcher = true;
            double epsilon = 0.001;
            double factor = 0.1f;
            if (regenerate)
            {
                factor = list[0].Weight(c) / 10;
            }
            while (best > epsilon && Math.Abs(factor) * 10 > epsilon)
            {
                System.Console.Out.WriteLine(best + "," + epsilon + " " + factor);
                if (regenerate)
                {
                    switcher = !switcher;
                    factor = switcher ? factor : -factor;
                }
                list.Sort(comparer);
                ParcourModelSingle first = list[0];
                double firstWeight = first.Weight(c);
                if (first.Weight(c) < best)
                {
                    bestModel = first;
                    best = first.Weight(c);
                    AddBestModel();
                }
                list.Clear();
                for (int j = 0; j < 300; j++)
                {
                    list.Add(new ParcourModelSingle(first, factor));
                }
                epsilon += 0.0001;
                if (regenerate)
                {
                    factor = factor - Math.Sign(factor) * ((Math.Abs(Math.Abs(factor) - epsilon)) / 500);
                }
            }
            finished = true;
        }

        private void AddBestModel()
        {
            bestModel.addPolygons();
            lock (parcour)
            {
                bestLegLength = Converter.KmToNM(bestModel.lenght);
                bestStraightLength = Converter.KmToNM(bestModel.straightLength);
                
                foreach (Line line in parcour.Line.ToList<Line>().Where(p => p.Type == (int)LineType.Point))
                {
                    parcour.Line.Remove(line);
                }
                ParcourChannelSingle pc = bestModel.getChannel();

                Vector last = null;
                foreach (Vector v in pc.getLinearCombinations())
                {
                    if (last != null)
                    {
                        Line l = new Line();
                        l.Type = (int)LineType.Point;
                        l.A = Factory.newGPSPoint(c.XtoLongitude(last.X), c.YtoLatitude(last.Y), 0);
                        l.B = Factory.newGPSPoint(c.XtoLongitude(last.X), c.YtoLatitude(last.Y), 0);
                        if (pc.ImmutablePoints.Contains(last))
                        {
                            //TODO l.A.edited = true;
                            //TODO l.B.edited = true;
                        }
                        l.O = Factory.newGPSPoint(c.XtoLongitude(v.X), c.YtoLatitude(v.Y), 0);
                        if (pc.ImmutablePoints.Contains(v))
                        {
                            //TODO l.O.edited = true;
                        }
                        parcour.Line.Add(l);
                    }
                    last = v;
                }
                
                foreach (Line line in parcour.Line.ToList<Line>().Where(p => p.Type == (int)LineType.PENALTYZONE))
                {
                    parcour.Line.Remove(line);
                }
                foreach (ParcourPolygon pg in bestModel.getPolygons())
                {
                    Vector mid = new Vector(0, 0, 0);
                    foreach (Vector v in pg.getEdges())
                    {
                        mid = mid + v;
                    }
                    int count = pg.getEdges().Count;
                    mid = mid / count;

                    for (int i = 0; i < count; i++)
                    {
                        Line l = new Line();
                        l.Type = (int)LineType.PENALTYZONE;
                        l.A = Factory.newGPSPoint(c.XtoLongitude(pg.getEdges()[i].X), c.YtoLatitude(pg.getEdges()[i].Y), 0);
                        l.B = Factory.newGPSPoint(c.XtoLongitude(pg.getEdges()[(i + 1) % count].X), c.YtoLatitude(pg.getEdges()[(i + 1) % count].Y), 0);
                        l.O = Factory.newGPSPoint(c.XtoLongitude(mid.X), c.YtoLatitude(mid.Y), 0);
                        parcour.Line.Add(l);
                    }
                }
            }
        }



        internal void RecalcParcour(ParcourSet parcour, Converter c, double channel, double channelLength)
        {
            this.parcour = parcour;
            this.c = c;
            comparer = new ComparerSingle(c);
            this.regenerate = true;
            ParcourModelSingle pm;
            if (parcour.Line.Count > 2)
            {
                pm = new ParcourModelSingle(parcour, c, channel, channelLength, true);
            }
            else
            {
                pm = new ParcourModelSingle(parcour, c, channel, channelLength);
            }
            List<List<ParcourModelSingle>> modelList = new List<List<ParcourModelSingle>>();
            //System.Diagnostics.Process.GetCurrentProcess().
            for (int i = 0; i < 1; i++)
            {
                List<ParcourModelSingle> list = new List<ParcourModelSingle>();
                modelList.Add(list);
                list.Add(pm);
                for (int j = 0; j < 300; j++)
                {
                    list.Add(new ParcourModelSingle(pm, 1));
                }
            }
            best = double.MaxValue;
            bestModel = null;

            foreach (List<ParcourModelSingle> list in modelList)
            {
                Thread t = new Thread(new ParameterizedThreadStart(ProcessList));
                t.Start(list);
            }
        }
    }
    class ComparerSingle : Comparer<ParcourModelSingle>
    {
        private Converter c;
        public ComparerSingle(Converter c)
        {
            this.c = c;
        }
        public override int Compare(ParcourModelSingle x, ParcourModelSingle y)
        {
            return x.Weight(c).CompareTo(y.Weight(c));
        }
    }
}
