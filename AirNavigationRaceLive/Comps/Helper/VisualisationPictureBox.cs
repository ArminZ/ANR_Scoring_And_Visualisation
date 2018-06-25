using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Comps
{
    public class VisualisationPictureBox : PictureBox
    {
        private ParcourSet Parcour;
        private Converter c;
        private List<FlightSet> flights;
        //private List<IntersectionPoint> intersectionPoints;
        private Pen Pen = new Pen(new SolidBrush(Color.Red), 2f);
        private Pen PenHover = new Pen(new SolidBrush(Color.White), 4f);
        private Pen PenSelected = new Pen(new SolidBrush(Color.Blue), 6f);
        private SolidBrush Brush = new SolidBrush(Color.FromArgb(100, 255, 0, 0));
        private Pen PenFlight = new Pen(new SolidBrush(Color.Black), 7f);
        private Pen PenIntersection = new Pen(new SolidBrush(Color.Black), 7f);
        public void SetParcour(ParcourSet iParcour)
        {
            Parcour = iParcour;
            Brush = new SolidBrush(Color.FromArgb((255 * iParcour.Alpha) / 100, iParcour.ColorPROH));
            Pen.Width = (float)iParcour.PenWidthGates;
            Pen.Color = iParcour.ColorGates;
        }
        public void SetConverter(Converter iConverter)
        {
            c = iConverter;
        }
        public void SetData(List<FlightSet> flights)
        {
            this.flights = flights;
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            PaintParcourAndData(pe, true);
        }

        private void PaintParcourAndData(PaintEventArgs pe, bool rescale)
        {
            if (Image == null)
            {
                return;
            }
            //float lineThickness = 2f;
            //if (pe != null && pe.ClipRectangle.Bottom == -4)
            //{
            //    lineThickness = 7f;
            //}
            // Pen.Width = lineThickness;

            #region parcour
            if (Parcour != null && c != null)
            {
                int y0 = 0;
                int x0 = 0;
                double factor = 1;
                PenFlight.Width = 7f;
                PenIntersection.Width = 7f;
                Pen.Width = (float)Parcour.PenWidthGates;
                if (rescale)
                {
                    double widthFactor = (double)Width / Image.Width;
                    double heightFactor = (double)Height / Image.Height;
                    factor = Math.Min(widthFactor, heightFactor);
                    Pen.Width = Pen.Width * (float)factor;
                    double factorDiff = Math.Abs(widthFactor - heightFactor);

                    if (widthFactor < heightFactor)
                    {
                        y0 = (int)((Height - (Image.Height * factor)) / 2);
                    }
                    else
                    {
                        x0 = (int)((Width - (Image.Width * factor)) / 2);
                    }
                }
                lock (Parcour)
                {
                    ICollection<Line> lines = Parcour.Line;
                    List<Line> linespenalty = lines.Where(p => p.Type == (int)LineType.PENALTYZONE).ToList();
                    foreach (Line l in linespenalty)
                    {
                        int startXp = x0 + (int)(c.getStartX(l) * factor);
                        int startYp = y0 + (int)(c.getStartY(l) * factor);
                        int endXp = x0 + (int)(c.getEndX(l) * factor);
                        int endYp = y0 + (int)(c.getEndY(l) * factor);
                        int orientationXp = x0 + (int)(c.getOrientationX(l) * factor);
                        int orientationYp = y0 + (int)(c.getOrientationY(l) * factor);
                        try
                        {
                            pe.Graphics.FillPolygon(Brush, new System.Drawing.Point[] { new System.Drawing.Point(startXp, startYp), new System.Drawing.Point(endXp, endYp), new System.Drawing.Point(orientationXp, orientationYp) });
                        }
                        catch
                        {
                            //TODO
                        }
                    }

                    // SP and FP lines
                    lines = lines.Where(l => l.Type >= (int)LineType.START_A && l.Type <= (int)LineType.END_D).ToList();
                    foreach (Line l in lines)
                    {
                        if (l.A != null && l.B != null & l.O != null)
                        {
                            int startX = x0 + (int)(c.getStartX(l) * factor);
                            int startY = y0 + (int)(c.getStartY(l) * factor);
                            int endX = x0 + (int)(c.getEndX(l) * factor);
                            int endY = y0 + (int)(c.getEndY(l) * factor);
                            int orientationX = x0 + (int)(c.getOrientationX(l) * factor);
                            int orientationY = y0 + (int)(c.getOrientationY(l) * factor);

                            Model.Point CenterPoint = new Model.Point();
                            CenterPoint.latitude = (l.A.latitude + l.B.latitude) / 2.0;
                            CenterPoint.longitude = (l.A.longitude + l.B.longitude) / 2.0;
                            // create a dedicated point on the same latitude as the Center point
                            Model.Point RadiusPoint = c.PointForRadius(CenterPoint);

                            float LongCorrFactor = (float)c.LongitudeCorrFactor(l);

                            int midX = startX + (endX - startX) / 2;
                            int midY = startY + (endY - startY) / 2;
                            int radX = (int)(c.LongitudeToX(RadiusPoint.longitude) * factor);
                            int radY = (int)(c.LatitudeToY(RadiusPoint.latitude) * factor);
                            //int orientationX = c.getOrientationX(l);
                            //int orientationY = c.getOrientationY(l);
                            double tmp = (double)midY + (orientationY - midY) * c.LongitudeCorrFactor(CenterPoint);
                            orientationY = (int)tmp;
                            Vector start = new Vector(startX, startY, 0);
                            Vector radv = new Vector(radX, radY, 0);
                            //float radius = (float)Vector.Abs(midv - start)
                            float radius;
                            if (rescale)
                            {
                                radius = Math.Abs(midY - radY) * LongCorrFactor * (float)factor;  
                            }
                            else
                            {
                                radius = Math.Abs(midY - radY) / LongCorrFactor / (float)factor;
                            }

                            try
                            {
                                pe.Graphics.DrawLine(Pen, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                pe.Graphics.TranslateTransform(midX - radius, midY - radius * LongCorrFactor);
                                pe.Graphics.DrawEllipse(Pen, 0, 0, radius * 2, radius * 2 * LongCorrFactor);
                                pe.Graphics.ResetTransform();

                                int orientationYCorr = midY + (int)(LongCorrFactor * (orientationY - midY));
                                pe.Graphics.DrawLine(Pen, new System.Drawing.Point(midX, midY), new System.Drawing.Point(orientationX, orientationYCorr));
                            }
                            catch
                            {
                                //TODO
                            }
                        }
                    }
                }
            }
            #endregion

            if (flights != null)
            {
                int y0 = 0;
                int x0 = 0;
                double factor = 1;
                PenFlight.Width = 7f;
                PenIntersection.Width = 7f;
                float radiusIntersection = 30;

                if (rescale)
                {
                    double widthFactor = (double)Width / Image.Width;
                    double heightFactor = (double)Height / Image.Height;
                    factor = Math.Min(widthFactor, heightFactor);
                    double factorDiff = Math.Abs(widthFactor - heightFactor);

                    PenFlight.Width = PenFlight.Width * (float)factor;
                    PenIntersection.Width = PenIntersection.Width * (float)factor;
                    radiusIntersection = 30 * (float)factor;

                    if (widthFactor < heightFactor)
                    {
                        y0 = (int)((Height - (Image.Height * factor)) / 2);
                    }
                    else
                    {
                        x0 = (int)((Width - (Image.Width * factor)) / 2);
                    }
                }

                foreach (FlightSet flight in flights)
                {
                    List<System.Drawing.Point> points = new List<System.Drawing.Point>();
                    foreach (AirNavigationRaceLive.Model.Point gd in flight.Point)
                    {
                        int startXp = x0 + (int)(c.LongitudeToX(gd.longitude) * factor);
                        int startYp = y0 + (int)(c.LatitudeToY(gd.latitude) * factor);
                        points.Add(new System.Drawing.Point(startXp, startYp));
                    }
                    if (points.Count > 2)
                    {
                        pe.Graphics.DrawLines(PenFlight, points.ToArray());
                    }

                    foreach (IntersectionPoint gd in flight.IntersectionPointSet)
                    {
                        int startXp = x0 + (int)(c.LongitudeToX(gd.longitude) * factor);
                        int startYp = y0 + (int)(c.LatitudeToY(gd.latitude) * factor);
                        Model.Point p = new Model.Point();
                        p.latitude = gd.latitude;
                        float corrFact = (float)c.LongitudeCorrFactor(p);
                        int r = (int)radiusIntersection;
                        pe.Graphics.DrawEllipse(PenIntersection, startXp - r, startYp - r * corrFact, 2 * r, 2 * r * corrFact);
                    }
                }
            }
        }

        public System.Drawing.Image PrintOutImage { get { return GeneratePrintOut(); } }

        protected Image GeneratePrintOut()
        {
            if (Parcour != null && c != null)
            {
                Bitmap bt = new Bitmap(Image.Width, Image.Height);
                Graphics gr = Graphics.FromImage(bt);
                Rectangle rc = new Rectangle(0, 0, Image.Width, Image.Height);
                gr.DrawImage(Image, rc);
                PaintEventArgs pe = new PaintEventArgs(gr, new Rectangle(-2, -2, -2, -2));
                PaintParcourAndData(pe, false);
                return bt;
            }
            return null;
        }

        public double GetXDistanceKM()
        {
            double topLat = c.TopLeftLatitudeY;
            double leftLong = c.TopLeftLongitudeX;
            double rightLong = c.TopLeftLongitudeX + Image.Width * c.SizeLongitudeX;
            return Converter.Distance(leftLong, topLat, rightLong, topLat);
        }
        public double GetYDistanceKM()
        {
            double topLat = c.TopLeftLatitudeY;
            double leftLong = c.TopLeftLongitudeX;
            double bottomLat = c.TopLeftLatitudeY + Image.Height * c.SizeLatitudeY;
            return Converter.Distance(leftLong, topLat, leftLong, bottomLat);
        }
    }
}
