using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Model;
using System.Drawing.Drawing2D;
using System;

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
        // user may modify prohibited zone color
        private volatile bool pdf = false;
        public Color ProhZoneColor = Color.FromArgb(255, 0, 0);

        public Color UserPenColor = Color.FromArgb(255, 0, 0);
        public float UserLineWidth = 6f;
        public float UserCircleWidth = 2f;
        public bool HasCircle =false;
        private Pen UserPenLine = new Pen(new SolidBrush(Color.Red), 2f);
        private Pen UserPenCircle = new Pen(new SolidBrush(Color.Red), 6f);

        public void SetParcour(ParcourSet iParcour)
        {
            Parcour = iParcour;
            Brush = new SolidBrush(Color.FromArgb(255*iParcour.Alpha/100, ProhZoneColor));
            UserPenLine.Width = UserLineWidth;
            UserPenCircle.Width = UserCircleWidth;
            UserPenLine.Color = UserPenColor;
            UserPenCircle.Color = UserPenColor;
        }
        public void SetConverter(Converter iConverter)
        {
            c = iConverter;
        }
        public void SetSelectedLine(Line iLine)
        {
            selectedLine = iLine;
        }
        public void SetHoverLine(Line iLine)
        {
            hoverLine = iLine;
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (Parcour != null && c != null)
            {
                lock (Parcour)
                {
                    ICollection<Line> lines = Parcour.Line;
                    List<Line> linespenalty = lines.Where(p => p.Type == (int)LineType.PENALTYZONE).ToList();
                    foreach (Line l in linespenalty)
                    {
                        int startXp = c.getStartX(l);
                        int startYp = c.getStartY(l);
                        int endXp = c.getEndX(l);
                        int endYp = c.getEndY(l);
                        int orientationXp = c.getOrientationX(l);
                        int orientationYp = c.getOrientationY(l);
                        try
                        {
                            pe.Graphics.FillPolygon(Brush, new System.Drawing.Point[] { new System.Drawing.Point(startXp, startYp), new System.Drawing.Point(endXp, endYp), new System.Drawing.Point(orientationXp, orientationYp) });
                        }
                        catch
                        {
                            //TODO
                        }
                    }
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
                            double tmp = (double)midY + (orientationY - midY) *c.LongitudeCorrFactor(CenterPoint);
                            orientationY = (int)tmp;
                            Vector start = new Vector(startX, startY, 0);
                            Vector radv = new Vector(radX, radY, 0);
                            //float radius = (float)Vector.Abs(midv - start)
                            float radius = Math.Abs(midY-radY)/LongCorrFactor;
                            try
                            {
                                if (l.Type != (int)LineType.PENALTYZONE)
                                {
                                    //Start_X/End_X
                                    if (l.Type >= 3 && l.Type <= 10 && !pdf && HasCircle)
                                    {
                                        pe.Graphics.DrawLine(UserPenLine, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                        //pe.Graphics.ResetTransform();
                                        pe.Graphics.TranslateTransform(midX - radius, midY - radius * LongCorrFactor);
                                        pe.Graphics.DrawEllipse(UserPenCircle, 0, 0, radius * 2, radius * 2 * LongCorrFactor);
                                        pe.Graphics.ResetTransform();
                                    }
                                    if (selectedLine == l && !pdf)
                                    {
                                        pe.Graphics.DrawLine(PenSelected, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                        if (!pdf)
                                        {
                                            pe.Graphics.DrawLine(PenSelected, new System.Drawing.Point(midX, midY), new System.Drawing.Point(orientationX, orientationY));
                                            pe.Graphics.DrawEllipse(PenSelected, orientationX - 3, orientationY - 3, 6, 6);
                                        }
                                    }
                                    if (hoverLine == l && !pdf)
                                    {
                                        pe.Graphics.DrawLine(PenHover, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                        if (!pdf)
                                        {
                                            pe.Graphics.DrawLine(PenHover, new System.Drawing.Point(midX, midY), new System.Drawing.Point(orientationX, orientationY));
                                            pe.Graphics.DrawEllipse(PenHover, orientationX - 2, orientationY - 2, 4, 4);
                                        }
                                    }
                                    pe.Graphics.DrawLine(UserPenLine, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                    if (!pdf)
                                    {
                                        pe.Graphics.DrawLine(UserPenLine, new System.Drawing.Point(midX, midY), new System.Drawing.Point(orientationX, orientationY));
                                        pe.Graphics.DrawEllipse(UserPenLine, orientationX - 1, orientationY - 1, 2, 2);
                                    }
                                    if (pdf)
                                    {
                                        pe.Graphics.ResetTransform();
                                        pe.Graphics.TranslateTransform(midX - radius, midY - radius * LongCorrFactor);
                                        pe.Graphics.DrawEllipse(UserPenCircle, 0, 0, radius * 2, radius * 2 * LongCorrFactor);
                                        pe.Graphics.ResetTransform();

                                        int orientationYCorr = midY + (int)(LongCorrFactor * (orientationY - midY));
                                        pe.Graphics.DrawLine(UserPenLine, new System.Drawing.Point(midX, midY), new System.Drawing.Point(orientationX, orientationYCorr));
                                        pe.Graphics.DrawEllipse(PenSelected, orientationX - 3, orientationYCorr - 3, 6, 6);
                                    }
                                }
                            }
                            catch
                            {
                                //TODO
                            }
                        }
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
    }
}
