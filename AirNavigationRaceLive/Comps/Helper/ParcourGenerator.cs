using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Comps.Helper
{
    public class ParcourGenerator
    {
        private const double EndLineDist = 0.9;
        private const double LineOfNoReturnDist = 1.5;
        private double best = double.MaxValue;
        private volatile ParcourModel bestModel = null;
        private ParcourSet parcour;
        private Converter c;
        private Comparer comparer = new Comparer();
        public volatile bool finished = false;
        private volatile bool regenerate = false;
        private int count;

        public void GenerateParcour(ParcourSet parcour, Converter c, double lenght, double channel, int count)
        {
            this.parcour = parcour;
            this.c = c;
            this.count = count;
            Line Start = parcour.Line.Single(p => p.Type == (int)LineType.START) as Line;
            if (Start == null) return;
            #region StartVektoren
            Vector StartAV = new Vector(c.LongitudeToX(Start.A.longitude), c.LatitudeToY(Start.A.latitude), 0);
            Vector StartBV = new Vector(c.LongitudeToX(Start.B.longitude), c.LatitudeToY(Start.B.latitude), 0);
            Vector StartOV = new Vector(c.LongitudeToX(Start.O.longitude), c.LatitudeToY(Start.O.latitude), 0);
            Vector StartAB = Vector.Direction(StartAV, StartBV);
            Vector StartMV = Vector.Middle(StartAV, StartBV);
            Vector StartLotOrientation = Vector.MinDistance(StartAV, StartBV, StartOV);

            double GateRadiusKm = Converter.NMtoM(0.3);
            Vector StartABNormalized = StartAB / Vector.Abs(StartAB);
            double StartABLength = Converter.Distance(c.XtoLongitude(StartAV.X), c.YtoLatitude(StartAV.Y), c.XtoLongitude((StartAV + StartABNormalized).X), c.YtoLatitude((StartAV + StartABNormalized).Y));
            Vector StartAB1KM = StartABNormalized / StartABLength;

            StartLotOrientation = StartLotOrientation * (Vector.Abs(StartAB1KM * GateRadiusKm) / Vector.Abs(StartLotOrientation));
            StartOV = StartMV + (StartLotOrientation * 2);

            Vector Start_A_M = StartAV + (StartAB1KM * GateRadiusKm);
            Vector Start_A_A = StartAV;
            Vector Start_A_B = Start_A_M + (StartAB1KM * GateRadiusKm);
            Vector Start_A_O = Start_A_M + StartLotOrientation;

            Line Start_A;
            if (parcour.Line.Any(p => p.Type == (int)LineType.START_A))
            {
                Start_A = parcour.Line.Single(p => p.Type == (int)LineType.START_A);
            }
            else
            {
                Start_A = new Line();
                Start_A.Type = (int)LineType.START_A;
                parcour.Line.Add(Start_A);
            }
            Start_A.A = Factory.newGPSPoint(c.XtoLongitude(Start_A_A.X), c.YtoLatitude(Start_A_A.Y), 0);
            Start_A.B = Factory.newGPSPoint(c.XtoLongitude(Start_A_B.X), c.YtoLatitude(Start_A_B.Y), 0);
            Start_A.O = Factory.newGPSPoint(c.XtoLongitude(Start_A_O.X), c.YtoLatitude(Start_A_O.Y), 0);

            Vector Start_D_M = StartBV - (StartAB1KM * GateRadiusKm);
            Vector Start_D_B = StartBV;
            Vector Start_D_A = Start_D_M - (StartAB1KM * GateRadiusKm);
            Vector Start_D_O = Start_D_M + StartLotOrientation;
            Line Start_D;
            if (parcour.Line.Any(p => p.Type == (int)LineType.START_D))
            {
                Start_D = parcour.Line.Single(p => p.Type == (int)LineType.START_D);
            }
            else
            {
                Start_D = new Line();
                Start_D.Type = (int)LineType.START_D;
                parcour.Line.Add(Start_D);
            }
            Start_D.A = Factory.newGPSPoint(c.XtoLongitude(Start_D_A.X), c.YtoLatitude(Start_D_A.Y), 0);
            Start_D.B = Factory.newGPSPoint(c.XtoLongitude(Start_D_B.X), c.YtoLatitude(Start_D_B.Y), 0);
            Start_D.O = Factory.newGPSPoint(c.XtoLongitude(Start_D_O.X), c.YtoLatitude(Start_D_O.Y), 0);


            Vector Start_B_M = Start_A_M + (Vector.Direction(Start_A_M, Start_D_M) / 3.0);
            Vector Start_B_A = Start_B_M - (StartAB1KM * GateRadiusKm);
            Vector Start_B_B = Start_B_M + (StartAB1KM * GateRadiusKm);
            Vector Start_B_O = Start_B_M + StartLotOrientation;

            Line Start_B;
            if (parcour.Line.Any(p => p.Type == (int)LineType.START_B))
            {
                Start_B = parcour.Line.Single(p => p.Type == (int)LineType.START_B);
            }
            else
            {
                Start_B = new Line();
                Start_B.Type = (int)LineType.START_B;
                parcour.Line.Add(Start_B);
            }
            Start_B.A = Factory.newGPSPoint(c.XtoLongitude(Start_B_A.X), c.YtoLatitude(Start_B_A.Y), 0);
            Start_B.B = Factory.newGPSPoint(c.XtoLongitude(Start_B_B.X), c.YtoLatitude(Start_B_B.Y), 0);
            Start_B.O = Factory.newGPSPoint(c.XtoLongitude(Start_B_O.X), c.YtoLatitude(Start_B_O.Y), 0);

            Vector Start_C_M = Start_A_M + ((Vector.Direction(Start_A_M, Start_D_M) * 2) / 3.0);
            Vector Start_C_B = Start_C_M - (StartAB1KM * GateRadiusKm);
            Vector Start_C_A = Start_C_M + (StartAB1KM * GateRadiusKm);
            Vector Start_C_O = Start_C_M + StartLotOrientation;
            Line Start_C;
            if (parcour.Line.Any(p => p.Type == (int)LineType.START_C))
            {
                Start_C = parcour.Line.Single(p => p.Type == (int)LineType.START_C);
            }
            else
            {
                Start_C = new Line();
                Start_C.Type = (int)LineType.START_C;
                parcour.Line.Add(Start_C);
            }
            Start_C.A = Factory.newGPSPoint(c.XtoLongitude(Start_C_A.X), c.YtoLatitude(Start_C_A.Y), 0);
            Start_C.B = Factory.newGPSPoint(c.XtoLongitude(Start_C_B.X), c.YtoLatitude(Start_C_B.Y), 0);
            Start_C.O = Factory.newGPSPoint(c.XtoLongitude(Start_C_O.X), c.YtoLatitude(Start_C_O.Y), 0);
            #endregion

            #region EndeVektoren
            double lengthKm = Converter.NMtoM(lenght);
            Vector lotPoint = StartMV + StartLotOrientation;
            double lotLenght = Converter.Distance(c.XtoLongitude(StartMV.X), c.YtoLatitude(StartMV.Y), c.XtoLongitude(lotPoint.X), c.YtoLatitude(lotPoint.Y));
            Vector StartEnd = (StartLotOrientation / lotLenght) * lengthKm * EndLineDist; //Shorten to make linearcombinations of vectors ... factor to be definded

            Vector EndeAV = StartAV + StartEnd;
            Vector EndeBV = StartBV + StartEnd;
            Vector EndeMV = Vector.Middle(EndeAV, EndeBV);
            Vector EndeOV = EndeMV + StartLotOrientation;
            Vector EndeAB = Vector.Direction(EndeAV, EndeBV);

            Vector Ende_A_A = Start_A_A + StartEnd;
            Vector Ende_A_B = Start_A_B + StartEnd;
            Vector Ende_A_M = Start_A_M + StartEnd;
            Vector Ende_A_O = Start_A_O + StartEnd;

            Line END_A;
            if (parcour.Line.Any(p => p.Type == (int)LineType.END_A))
            {
                END_A = parcour.Line.Single(p => p.Type == (int)LineType.END_A);
            }
            else
            {
                END_A = new Line();
                END_A.Type = (int)LineType.END_A;
                parcour.Line.Add(END_A);
            }
            END_A.A = Factory.newGPSPoint(c.XtoLongitude(Ende_A_A.X), c.YtoLatitude(Ende_A_A.Y), 0);
            END_A.B = Factory.newGPSPoint(c.XtoLongitude(Ende_A_B.X), c.YtoLatitude(Ende_A_B.Y), 0);
            END_A.O = Factory.newGPSPoint(c.XtoLongitude(Ende_A_O.X), c.YtoLatitude(Ende_A_O.Y), 0);

            Vector Ende_B_A = Start_B_A + StartEnd;
            Vector Ende_B_B = Start_B_B + StartEnd;
            Vector Ende_B_M = Start_B_M + StartEnd;
            Vector Ende_B_O = Start_B_O + StartEnd;

            Line END_B;
            if (parcour.Line.Any(p => p.Type == (int)LineType.END_B))
            {
                END_B = parcour.Line.Single(p => p.Type == (int)LineType.END_B);
            }
            else
            {
                END_B = new Line();
                END_B.Type = (int)LineType.END_B;
                parcour.Line.Add(END_B);
            }
            END_B.A = Factory.newGPSPoint(c.XtoLongitude(Ende_B_A.X), c.YtoLatitude(Ende_B_A.Y), 0);
            END_B.B = Factory.newGPSPoint(c.XtoLongitude(Ende_B_B.X), c.YtoLatitude(Ende_B_B.Y), 0);
            END_B.O = Factory.newGPSPoint(c.XtoLongitude(Ende_B_O.X), c.YtoLatitude(Ende_B_O.Y), 0);

            Vector Ende_C_A = Start_C_A + StartEnd;
            Vector Ende_C_B = Start_C_B + StartEnd;
            Vector Ende_C_M = Start_C_M + StartEnd;
            Vector Ende_C_O = Start_C_O + StartEnd;
            Line END_C;
            if (parcour.Line.Any(p => p.Type == (int)LineType.END_C))
            {
                END_C = parcour.Line.Single(p => p.Type == (int)LineType.END_C);
            }
            else
            {
                END_C = new Line();
                END_C.Type = (int)LineType.END_C;
                parcour.Line.Add(END_C);
            }
            END_C.A = Factory.newGPSPoint(c.XtoLongitude(Ende_C_A.X), c.YtoLatitude(Ende_C_A.Y), 0);
            END_C.B = Factory.newGPSPoint(c.XtoLongitude(Ende_C_B.X), c.YtoLatitude(Ende_C_B.Y), 0);
            END_C.O = Factory.newGPSPoint(c.XtoLongitude(Ende_C_O.X), c.YtoLatitude(Ende_C_O.Y), 0);

            Vector Ende_D_A = Start_D_A + StartEnd;
            Vector Ende_D_B = Start_D_B + StartEnd;
            Vector Ende_D_M = Start_D_M + StartEnd;
            Vector Ende_D_O = Start_D_O + StartEnd;
            Line END_D;
            if (parcour.Line.Any(p => p.Type == (int)LineType.END_D))
            {
                END_D = parcour.Line.Single(p => p.Type == (int)LineType.END_D);
            }
            else
            {
                END_D = new Line();
                END_D.Type = (int)LineType.END_D;
                parcour.Line.Add(END_D);
            }
            END_D.A = Factory.newGPSPoint(c.XtoLongitude(Ende_D_A.X), c.YtoLatitude(Ende_D_A.Y), 0);
            END_D.B = Factory.newGPSPoint(c.XtoLongitude(Ende_D_B.X), c.YtoLatitude(Ende_D_B.Y), 0);
            END_D.O = Factory.newGPSPoint(c.XtoLongitude(Ende_D_O.X), c.YtoLatitude(Ende_D_O.Y), 0);
            #endregion

            #region LineOfNoReturn
            double lengthLONRKm = Converter.NMtoM(LineOfNoReturnDist);
            Vector StartToLONR = (StartLotOrientation / lotLenght) * ((lengthKm * EndLineDist) - lengthLONRKm);

            Vector LONR_A = StartAV + StartToLONR;
            Vector LONR_B = StartBV + StartToLONR;
            Vector LONR_O = StartOV + StartToLONR;
            Line LONR;
            if (parcour.Line.Any(p => p.Type == (int)LineType.LINEOFNORETURN))
            {
                LONR = parcour.Line.Single(p => p.Type == (int)LineType.LINEOFNORETURN);
            }
            else
            {
                LONR = new Line();
                LONR.Type = (int)LineType.LINEOFNORETURN;
                parcour.Line.Add(LONR);
            }
            LONR.A = Factory.newGPSPoint(c.XtoLongitude(LONR_A.X), c.YtoLatitude(LONR_A.Y), 0);
            LONR.B = Factory.newGPSPoint(c.XtoLongitude(LONR_B.X), c.YtoLatitude(LONR_B.Y), 0);
            LONR.O = Factory.newGPSPoint(c.XtoLongitude(LONR_O.X), c.YtoLatitude(LONR_O.Y), 0);
            #endregion
            CalculateParcour(parcour, c, channel);

        }

        private void CalculateParcour(ParcourSet parcour, Converter c, double channel)
        {
            ParcourModel pm = new ParcourModel(parcour, c, EndLineDist, channel);
            List<List<ParcourModel>> modelList = new List<List<ParcourModel>>();
            //System.Diagnostics.Process.GetCurrentProcess().
            for (int i = 0; i < 1; i++)
            {
                List<ParcourModel> list = new List<ParcourModel>();
                modelList.Add(list);
                list.Add(pm);
                for (int j = 0; j < 300; j++)
                {
                    list.Add(new ParcourModel(pm, 1));
                }
            }
            best = double.MaxValue;
            bestModel = null;

            foreach (List<ParcourModel> list in modelList)
            {
                Thread t = new Thread(new ParameterizedThreadStart(ProcessList));
                t.Start(list);
            }
        }

        private void ProcessList(object o)
        {
            List<ParcourModel> list = o as List<ParcourModel>;
            bool switcher = true;
            double epsilon = 0.01;
            double factor = 1f;
            if (regenerate)
            {
                factor = list[0].Weight()/10;
            }
            while (best > epsilon && Math.Abs(factor)*10 > epsilon)
            {
                if (regenerate)
                {
                    switcher = !switcher;
                    factor = switcher ? factor : -factor;
                }
                list.Sort(comparer);
                ParcourModel first = list[0];
                double firstWeight = first.Weight();
                if (first.Weight() < best)
                {
                    bestModel = first;
                    best = first.Weight();
                    AddBestModel();
                }
                list.Clear();
                //list.Add(first);
                for (int j = 0; j < 300; j++)
                {
                    list.Add(new ParcourModel(first, factor));
                }
                epsilon += 0.01;
                if (regenerate)
                {
                    factor = factor - Math.Sign(factor)* ((Math.Abs(Math.Abs(factor) - epsilon))/500);
                }
            }
            finished = true;
        }

        private void AddBestModel()
        {
            bestModel.addPolygons();
            lock (parcour)
            {
                foreach( Line line in parcour.Line.ToList<Line>().Where(p => p.Type == (int)LineType.Point))
                {
                    parcour.Line.Remove(line);
                }
                foreach (ParcourChannel pc in bestModel.getChannels())
                {
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

        internal void RecalcParcour(ParcourSet parcour, Converter c,double channel)
        {
            this.parcour = parcour;
            this.c = c;
            this.regenerate = true;
            ParcourModel pm = new ParcourModel(parcour, c, EndLineDist, channel,true);
            List<List<ParcourModel>> modelList = new List<List<ParcourModel>>();
            //System.Diagnostics.Process.GetCurrentProcess().
            for (int i = 0; i < 1; i++)
            {
                List<ParcourModel> list = new List<ParcourModel>();
                modelList.Add(list);
                list.Add(pm);
                for (int j = 0; j < 300; j++)
                {
                    list.Add(new ParcourModel(pm, 1));
                }
            }
            best = double.MaxValue;
            bestModel = null;

            foreach (List<ParcourModel> list in modelList)
            {
                Thread t = new Thread(new ParameterizedThreadStart(ProcessList));
                t.Start(list);
            }
        }
    }
    class Comparer : Comparer<ParcourModel>
    {
        public override int Compare(ParcourModel x, ParcourModel y)
        {
            return x.Weight().CompareTo(y.Weight());
        }
    }
}
