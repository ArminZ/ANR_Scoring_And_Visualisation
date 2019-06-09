using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Comps
{
    public class PictureBoxExt : PictureBox
    {
        private ParcourSet Parcour;
        private Converter c;
        private Line selectedLine;
        private Line hoverLine;
        private Pen Pen = new Pen(new SolidBrush(Color.Red), 2f);
        private Pen PenHover = new Pen(new SolidBrush(Color.White), 4f);
        private Pen PenSelected = new Pen(new SolidBrush(Color.Blue), 6f);
        private SolidBrush Brush = new SolidBrush(Color.FromArgb(100, 255, 0, 0));

        private bool rescale = false;

        // -- parcour picture box --
        private volatile bool pdf = false;
        public Color ColorPROH;// = Color.FromArgb(255, 0, 0);
        public Color ColorGates;// = Color.FromArgb(255, 0, 0);
        public float PenWidthGates; // = 6f;
        private Pen PenGates = new Pen(new SolidBrush(Color.Red), 1f);
        public bool HasCircleOnGates;// = false;
        public int ParcourT;

        // -- visualisation picture box --       
        private List<FlightSet> flights;
        private Pen PenFlight = new Pen(new SolidBrush(Color.Black), 7f);
        private Pen PenIntersection = new Pen(new SolidBrush(Color.Black), 7f);
        private Pen PenChannel = new Pen(new SolidBrush(Color.Black), 7f);

        private float IntersectionCircleRadius = 20;
        private bool ShowIntersectionCircle = false;
        private bool FillChannel = false;

        const double GateOrientationArrowLength = 0.3;   // gatewidth in NM, used when/if plotting the SP/FP orientation
        public void SetParcour(ParcourSet iParcour)
        {
            Parcour = iParcour;
            Brush = new SolidBrush(Color.FromArgb((255 * iParcour.Alpha) / 100, iParcour.ColorPROH));

            PenGates.Width = (float)iParcour.PenWidthGates;
            PenGates.Color = iParcour.ColorGates;
            HasCircleOnGates = iParcour.HasCircleOnGates;

            PenIntersection.Color = iParcour.ColorIntersection == Color.FromArgb(0, 0, 0, 0) ? Properties.Settings.Default.IntersectionColor : iParcour.ColorIntersection;
            PenIntersection.Width = iParcour.PenWidthIntersection == 0 ? (float)Properties.Settings.Default.IntersectionPenWidth : (float)iParcour.PenWidthIntersection;
            //           IntersectionCircleRadius = iParcour.IntersectionCircleRadius == 0 ? (float)20.0 : (float)iParcour.IntersectionCircleRadius;
            IntersectionCircleRadius = iParcour.IntersectionCircleRadius == 0 ? (float)20.0 : (float)iParcour.IntersectionCircleRadius;
            ShowIntersectionCircle = iParcour.HasIntersectionCircles;

            Color c = iParcour.ColorChannel;
            PenChannel.Color = iParcour.ColorChannel == Color.FromArgb(0, 0, 0, 0) ? Properties.Settings.Default.ChannelColor : iParcour.ColorChannel;
            PenChannel.Width = iParcour.PenWidthChannel == 0 ? (float)Properties.Settings.Default.ChannelPenWidth : (float)iParcour.PenWidthChannel;


            Pen.Width = (float)iParcour.PenWidthGates;
            Pen.Color = iParcour.ColorGates;

            ParcourT = Parcour.PenaltyCalcType;

        }
        public void SetConverter(Converter iConverter)
        {
            c = iConverter;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            PaintParcourAndData(pe, true);
        }

        public System.Drawing.Image PrintOutImage { get { return GeneratePrintOut(); } }

        protected Image GeneratePrintOut()
        {
            if (Parcour != null && c != null)
            {
                pdf = true;
                Bitmap bt = new Bitmap(Image.Width, Image.Height);
                Graphics gr = Graphics.FromImage(bt);
                Rectangle rc = new Rectangle(0, 0, Image.Width, Image.Height);
                gr.DrawImage(Image, rc);
                PaintEventArgs pe = new PaintEventArgs(gr, new Rectangle(-2, -2, -2, -2));
                PaintParcourAndData(pe, rescale);
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

        public void SetData(List<FlightSet> flights)
        {
            this.flights = flights;
        }

        public void SetSelectedLine(Line iLine)
        {
            selectedLine = iLine;
        }
        public void SetHoverLine(Line iLine)
        {
            hoverLine = iLine;
        }
        private void PaintParcourAndData(PaintEventArgs pe, bool rescale)
        {
            if (Image == null)
            {
                return;
            }



            #region parcour
            if (Parcour != null && c != null)
            {
                //PenFlight.Width = 7f;
                //PenIntersection.Width = (float)Properties.Settings.Default.IntersectionPenWidth;
                Pen.Width = (float)Parcour.PenWidthGates;

                lock (Parcour)
                {
                    ICollection<Line> lines = Parcour.Line;
                    List<Line> linesClosedPolygon = new List<Line>();
                    switch (ParcourT)
                    {
                        case (int)ParcourType.PROHZONES:
                            linesClosedPolygon = lines.Where(p => (p.Type == (int)LineType.PENALTYZONE) || (p.Type >= (int)LineType.PROH_A && p.Type <= (int)LineType.PROH_D)).ToList();
                            PolygonFiller(linesClosedPolygon, Pen, Brush, pe);
                            break;
                        case (int)ParcourType.CHANNELS:
                            if (FillChannel)
                            {
                                linesClosedPolygon = lines.Where(p => p.Type >= (int)LineType.CHANNEL_A && p.Type <= (int)LineType.CHANNEL_D).ToList();
                                PolygonFiller(linesClosedPolygon, Pen, Brush, pe);
                            }
                            break;
                        default:
                            break;
                    }
                    // Polygons filling:  PROH zone, channel-specific PROH zones
                    PolygonFiller(linesClosedPolygon, Pen, Brush, pe);


                    //List<Line> linesL = lines.Where(p => (p.Type >= (int)LineType.CHANNEL_A && p.Type <= (int)LineType.CHANNEL_A) || (p.Type >= (int)LineType.CHANNEL_A && p.Type <= (int)LineType.CHANNEL_A)).ToList();
                    //foreach (Line l in linesL)

                    #region Draw line objects: basically SP line, FP line, Channel borders
                    foreach (Line l in lines)
                    {
                        if (l.A != null && l.B != null & l.O != null)
                        {
                            int startX = c.getStartX(l);
                            int startY = c.getStartY(l);
                            float LongCorrFactor = (float)c.LongitudeCorrFactor(l);
                            int endX = c.getEndX(l);
                            int endY = c.getEndY(l);

                            Model.Point CenterPoint = new Model.Point();
                            CenterPoint.latitude = (l.A.latitude + l.B.latitude) / 2.0;
                            CenterPoint.longitude = (l.A.longitude + l.B.longitude) / 2.0;

                            // create a dedicated point on the same latitude as the Center point
                            Model.Point RadiusPoint = c.PointForRadius(CenterPoint);


                            int midX = startX + (endX - startX) / 2;
                            int midY = startY + (endY - startY) / 2;
                            int radX = c.LongitudeToX(RadiusPoint.longitude);
                            int radY = c.LatitudeToY(RadiusPoint.latitude);

                            Vector start = new Vector(startX, startY, 0);
                            Vector radv = new Vector(radX, radY, 0);
                            float radius = Math.Abs(midY - radY) / LongCorrFactor;

                            try
                            {
                                //int orientationYCorr = pdf == true ? midY + (int)(LongCorrFactor * (orientationY - midY)) : orientationY;

                                if (l.Type == (int)LineType.PENALTYZONE)
                                {
                                    // common penaltyzone is handled above 
                                }
                                if (l.Type >= (int)LineType.PROH_A && l.Type <= (int)LineType.PROH_D)
                                {
                                    // channel-specific penaltyzones are handled above
                                }
                                if (l.Type == (int)LineType.TKOF)
                                {
                                    // TKOF line is not shown/ for purpose not implemented
                                }
                                if (l.Type == (int)LineType.NOBACKTRACKLINE)
                                {
                                    // NBL is deprecated
                                }

                                if (l.Type >= (int)LineType.CENTERLINE_A && l.Type <= (int)LineType.CENTERLINE_D)
                                {
                                    // Centerline is not yet implemented on Route import, and therefore not yet implemented here
                                }

                                if (l.Type >= (int)LineType.CHANNEL_A && l.Type <= (int)LineType.CHANNEL_D && ParcourT == (int)ParcourType.CHANNELS)
                                {
                                    pe.Graphics.DrawLine(PenChannel, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                    pe.Graphics.ResetTransform();
                                }

                                if (l.Type >= (int)LineType.START_A && l.Type <= (int)LineType.END_D)
                                {
                                    pe.Graphics.DrawLine(PenGates, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                    pe.Graphics.ResetTransform();


                                    if (HasCircleOnGates)
                                    {
                                        // draw ellipse
                                        pe.Graphics.TranslateTransform(midX - radius, midY - radius * LongCorrFactor);
                                        pe.Graphics.DrawEllipse(PenGates, 0, 0, radius * 2, radius * 2 * LongCorrFactor);
                                        pe.Graphics.ResetTransform();
                                        // draw orientation line 
                                        // http://www.movable-type.co.uk/scripts/latlong.html?from=48.619,-120.412&to=48.59617,-120.4020

                                        Vector a = new Vector(l.A.longitude, l.A.latitude, 0);
                                        Vector b = new Vector(l.B.longitude, l.B.latitude, 0);
                                        Vector o = Vector.ArrowPointFromGivenLine(a, b, GateOrientationArrowLength);
                                        int HeadingX = c.LongitudeToX(o.X);
                                        int HeadingY = c.LatitudeToY(o.Y);
                                        pe.Graphics.DrawLine(PenGates, new System.Drawing.Point(midX, midY), new System.Drawing.Point(HeadingX, HeadingY));
                                        //  pe.Graphics.DrawLine(PenGates, new System.Drawing.Point(midX, midY), new System.Drawing.Point(orientationX, orientationYCorr));
                                        pe.Graphics.ResetTransform();
                                    }

                                    #region Legacy behaviour 

                                    if (!pdf)
                                    {
                                        // LEGACY behaviour - used when editing points
                                        if (selectedLine == l)
                                        {
                                            pe.Graphics.DrawLine(PenSelected, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                            //   pe.Graphics.DrawLine(PenSelected, new System.Drawing.Point(midX, midY), new System.Drawing.Point(oriX, oriY));
                                        }

                                        if (hoverLine == l)
                                        {
                                            pe.Graphics.DrawLine(PenHover, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                            //  pe.Graphics.DrawLine(PenHover, new System.Drawing.Point(midX, midY), new System.Drawing.Point(oriX, oriY));
                                        }
                                    }
                                    #endregion
                                }
                            }
                            catch
                            {
                                //TODO
                            }
                        }
                    }

                    #endregion
                }
            }
            #endregion

            if (flights != null)
            {
                //PenFlight.Width = 7f;
                //PenIntersection.Width = (float)Properties.Settings.Default.IntersectionPenWidth;
                int r = (int)(IntersectionCircleRadius*30.0);  //45.0

                foreach (FlightSet flight in flights)
                {
                    List<System.Drawing.Point> points = new List<System.Drawing.Point>();
                    foreach (AirNavigationRaceLive.Model.Point gd in flight.Point)
                    {
                        int startXp = (int)c.LongitudeToX(gd.longitude);
                        int startYp = (int)c.LatitudeToY(gd.latitude);
                        points.Add(new System.Drawing.Point(startXp, startYp));
                    }
                    if (points.Count > 2)
                    {
                        pe.Graphics.DrawLines(PenFlight, points.ToArray());
                    }
                    if (flight.IntersectionPointSet != null && ShowIntersectionCircle)
                    {
                        foreach (IntersectionPoint gd in flight.IntersectionPointSet)
                        {
                            int startXp = (int)c.LongitudeToX(gd.longitude);
                            int startYp = (int)c.LatitudeToY(gd.latitude);
                            Model.Point p = new Model.Point();
                            p.latitude = gd.latitude;
                            float corrFact = (float)c.LongitudeCorrFactor(p);
                            pe.Graphics.DrawEllipse(PenIntersection, startXp - r, startYp - r * corrFact, 2 * r, 2 * r * corrFact);
                        }
                    }
                }
            }
        }

        private void PolygonFiller(List<Line> linesClosedPolygon, Pen pen, SolidBrush brush, PaintEventArgs pe)
        {
            // new code:
            // we may have several polygons, but they are saved as small line pieces in the database
            // how to distinct different polygons: a polygons end point is identical with the first point

            List<System.Drawing.Point> pts = new List<System.Drawing.Point>();
            List<System.Drawing.Point> ptsF = new List<System.Drawing.Point>();
            bool isPolygonStart = true;
            foreach (Line l in linesClosedPolygon)
            {
                if (isPolygonStart)
                {
                    //reset
                    pts = new List<System.Drawing.Point>();
                    ptsF = new List<System.Drawing.Point>();
                    isPolygonStart = false;
                }
                System.Drawing.Point startPt = new System.Drawing.Point();
                System.Drawing.Point endPt = new System.Drawing.Point();
                startPt.X = c.getStartX(l);
                startPt.Y = c.getStartY(l);
                endPt.X = c.getEndX(l);
                endPt.Y = c.getEndY(l);
                pts.Add(startPt);
                ptsF.Add(startPt);

                if (endPt != pts[0]) // line' end point identical with first point?
                {
                    pts.Add(endPt);
                    ptsF.Add(endPt);
                }
                else
                {
                    // handle Graphics
                    pe.Graphics.FillPolygon(brush, pts.ToArray<System.Drawing.Point>());
                    // draw border of polygon --NOT YET activated
                    //pe.Graphics.DrawPolygon(pen, ptsF.ToArray<System.Drawing.Point>());
                    // start a new polygon
                    isPolygonStart = true;
                }

            }
        }
    }
}
