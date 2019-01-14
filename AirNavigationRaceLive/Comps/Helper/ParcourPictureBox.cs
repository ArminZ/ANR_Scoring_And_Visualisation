using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Comps
{
    public class ParcourPictureBox : PictureBox
    {
        private ParcourSet Parcour;
        private Converter c;
        private Line selectedLine;
        private Line hoverLine;
        private Pen Pen = new Pen(new SolidBrush(Color.Red), 2f);
        private Pen PenHover = new Pen(new SolidBrush(Color.White), 4f);
        private Pen PenSelected = new Pen(new SolidBrush(Color.Blue), 6f);
        private SolidBrush Brush = new SolidBrush(Color.FromArgb(70, 255, 0, 0));

        private bool rescale = false;

        // -- parcour picture box --
        private volatile bool pdf = false;
        public Color ColorPROH;// = Color.FromArgb(255, 0, 0);
        public Color ColorGates;// = Color.FromArgb(255, 0, 0);
        public float PenWidthGates; // = 6f;
        public bool HasCircleOnGates;// = false;
        private Pen UserPenGates = new Pen(new SolidBrush(Color.Red), 1f);

        // -- visualisation picture box --       
        private List<FlightSet> flights;
        private Pen PenFlight = new Pen(new SolidBrush(Color.Black), 7f);
        private Pen PenIntersection = new Pen(new SolidBrush(Color.Black), 7f);
        bool fillChannel = false;  //TODO
        bool showChannel = true;  //TODO
        bool showProh = false;  //TODO


        public void SetParcour(ParcourSet iParcour)
        {
            Parcour = iParcour;
            Brush = new SolidBrush(Color.FromArgb(255 * iParcour.Alpha / 100, iParcour.ColorPROH));
            UserPenGates.Width = (float)iParcour.PenWidthGates;
            UserPenGates.Color = iParcour.ColorGates;

            PenIntersection.Color = Properties.Settings.Default.IntersectionColor;
            PenIntersection.Width = (float)Properties.Settings.Default.IntersectionPenWidth;

            Pen.Width = (float)iParcour.PenWidthGates;
            Pen.Color = iParcour.ColorGates;
            HasCircleOnGates = iParcour.HasCircleOnGates;

        }
        public void SetConverter(Converter iConverter)
        {
            c = iConverter;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            PaintParcourAndData(pe, rescale);
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
                PaintEventArgs pe = new PaintEventArgs(gr, new Rectangle());
                pdf = true;
                OnPaint(pe);
                pdf = false;
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

            if (Parcour != null && c != null)
            {
                lock (Parcour)
                {
                    ICollection<Line> lines = Parcour.Line;

                    List<Line> linesClosedPolygon = new List<Line>();
                    List<System.Drawing.Point> pts = new List<System.Drawing.Point>();
                    List<System.Drawing.Point> ptsF = new List<System.Drawing.Point>();
                    bool isPolygonStart = true;

                    if (showProh)
                    {

                        #region Polygons filling:  PROH zone, channel-specific PROH zones (new code)

                        // new code:
                        // we may have aseveral polygons, but they are saved as small line pieces in the database
                        // how to distinct different polygons: a polygons end point is identical with the first point
                        // consider the common type PENALRTZONE but also channel-specific Penalty zones PROH_A yo PROH_D
                        linesClosedPolygon = lines.Where(p => (p.Type == (int)LineType.PENALTYZONE) || (p.Type >= (int)LineType.PROH_A && p.Type <= (int)LineType.PROH_D)).ToList();

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
                                pe.Graphics.FillPolygon(Brush, pts.ToArray<System.Drawing.Point>());
                                // draw border of polygon --NOT YET activated
                                //pe.Graphics.DrawPolygon(UserPenLine, ptsF.ToArray<System.Drawing.Point>());
                                // start a new polygon
                                isPolygonStart = true;
                            }
                        }

                        #endregion

                    }

                    if (fillChannel)
                    {

                        #region Polygons filling: channels

                        // new code:
                        // we may have several polygons, but they are saved as small line pieces in the database
                        // how to distinct different polygons: a polygons end point is identical with the first point
                        linesClosedPolygon = lines.Where(p => p.Type >= (int)LineType.CHANNEL_A && p.Type <= (int)LineType.CHANNEL_D).ToList();
                        pts = new List<System.Drawing.Point>();
                        ptsF = new List<System.Drawing.Point>();
                        isPolygonStart = true;
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
                                pe.Graphics.FillPolygon(Brush, pts.ToArray<System.Drawing.Point>());
                                // draw border of polygon --NOT YET activated
                                //pe.Graphics.DrawPolygon(UserPenLine, ptsF.ToArray<System.Drawing.Point>());
                                // start a new polygon
                                isPolygonStart = true;
                            }
                        }

                        #endregion
                    }

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
                            int orientationX = c.getOrientationX(l);
                            int orientationY = c.getOrientationY(l);
                            double tmp = (double)midY + (orientationY - midY) * c.LongitudeCorrFactor(CenterPoint);
                            orientationY = (int)tmp;
                            Vector start = new Vector(startX, startY, 0);
                            Vector radv = new Vector(radX, radY, 0);
                            //float radius = (float)Vector.Abs(midv - start)
                            float radius = Math.Abs(midY - radY) / LongCorrFactor;
                            try
                            {
                                int orientationYCorr = pdf == true ? midY + (int)(LongCorrFactor * (orientationY - midY)) : orientationY;

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

                                if (l.Type >= (int)LineType.CHANNEL_A && l.Type <= (int)LineType.CHANNEL_D && showChannel)
                                {
                                    pe.Graphics.DrawLine(UserPenGates, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                    pe.Graphics.ResetTransform();
                                }

                                if (l.Type >= (int)LineType.START_A && l.Type <= (int)LineType.END_D)
                                {
                                    pe.Graphics.DrawLine(UserPenGates, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                    pe.Graphics.ResetTransform();

                                    if (HasCircleOnGates)
                                    { // draw ellipse
                                        pe.Graphics.TranslateTransform(midX - radius, midY - radius * LongCorrFactor);
                                        pe.Graphics.DrawEllipse(UserPenGates, 0, 0, radius * 2, radius * 2 * LongCorrFactor);
                                        pe.Graphics.ResetTransform();
                                        // draw orientation line 
                                        pe.Graphics.DrawLine(UserPenGates, new System.Drawing.Point(midX, midY), new System.Drawing.Point(orientationX, orientationYCorr));
                                        pe.Graphics.ResetTransform();
                                    }

                                    if (!pdf)
                                    {
                                        // LEGACY behaviour - used when editing points
                                        if (selectedLine == l)
                                        {
                                            pe.Graphics.DrawLine(PenSelected, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                            pe.Graphics.DrawLine(PenSelected, new System.Drawing.Point(midX, midY), new System.Drawing.Point(orientationX, orientationY));
                                        }

                                        if (hoverLine == l)
                                        {
                                            pe.Graphics.DrawLine(PenHover, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                            pe.Graphics.DrawLine(PenHover, new System.Drawing.Point(midX, midY), new System.Drawing.Point(orientationX, orientationY));
                                        }
                                    }
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

            if (flights != null)
            {

                double factor = 1;
                //PenFlight.Width = 7f;
                //PenIntersection.Width = (float)Properties.Settings.Default.IntersectionPenWidth;
                float radiusIntersection = 20;

                foreach (FlightSet flight in flights)
                {
                    List<System.Drawing.Point> points = new List<System.Drawing.Point>();
                    foreach (AirNavigationRaceLive.Model.Point gd in flight.Point)
                    {
                        int startXp = (int)(c.LongitudeToX(gd.longitude) * factor);
                        int startYp = (int)(c.LatitudeToY(gd.latitude) * factor);
                        points.Add(new System.Drawing.Point(startXp, startYp));
                    }
                    if (points.Count > 2)
                    {
                        pe.Graphics.DrawLines(PenFlight, points.ToArray());
                    }

                    if (flight.IntersectionPointSet != null)
                    {

                        foreach (IntersectionPoint gd in flight.IntersectionPointSet)
                        {
                            int startXp = (int)(c.LongitudeToX(gd.longitude) * factor);
                            int startYp = (int)(c.LatitudeToY(gd.latitude) * factor);
                            Model.Point p = new Model.Point();
                            p.latitude = gd.latitude;
                            float corrFact = (float)c.LongitudeCorrFactor(p);
                            int r = (int)radiusIntersection;
                            pe.Graphics.DrawEllipse(PenIntersection, startXp - r, startYp - r * corrFact, 2 * r, 2 * r * corrFact);
                        }
                    }
                }
            }

        }

    }
}
