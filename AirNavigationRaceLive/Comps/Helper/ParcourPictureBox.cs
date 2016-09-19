using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Comps.Model;
using NetworkObjects;

namespace AirNavigationRaceLive.Comps
{
    public class ParcourPictureBox : PictureBox
    {
        private Parcour Parcour;
        private Converter c;
        private Line selectedLine;
        private Line hoverLine;
        private System.Drawing.Pen Pen = new Pen(new SolidBrush(Color.Red), 2f);
        private System.Drawing.Pen PenHover = new Pen(new SolidBrush(Color.White), 4f);
        private System.Drawing.Pen PenSelected = new Pen(new SolidBrush(Color.Blue), 6f);
        private SolidBrush Brush = new SolidBrush(Color.FromArgb(70, 255, 0, 0));
        private volatile bool pdf = false;

        public void SetParcour(Parcour iParcour)
        {
            Parcour = iParcour;
            Brush = new SolidBrush(Color.FromArgb((255*iParcour.Alpha)/100, 255, 0, 0));
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
                            int endX = c.getEndX(l);
                            int endY = c.getEndY(l);
                            int midX = startX + (endX - startX) / 2;
                            int midY = startY + (endY - startY) / 2;
                            int orientationX = c.getOrientationX(l);
                            int orientationY = c.getOrientationY(l);
                            Vector start = new Vector(startX, startY, 0);
                            Vector midv = new Vector(midX, midY, 0);
                            float radius = (float)Vector.Abs(midv - start);
                            try
                            {
                                if (l.Type != (int)LineType.PENALTYZONE)
                                {
                                    //Start_X/End_X
                                    if (((int)l.Type) >= 3 && ((int)l.Type) <= 10 && !pdf)
                                    {
                                        pe.Graphics.DrawEllipse(Pen, midX - radius, midY - radius, radius * 2, radius * 2);
                                    }
                                    if (selectedLine == l)
                                    {
                                        pe.Graphics.DrawLine(PenSelected, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                        if (!pdf)
                                        {
                                            pe.Graphics.DrawLine(PenSelected, new System.Drawing.Point(midX, midY), new System.Drawing.Point(orientationX, orientationY));
                                            pe.Graphics.DrawEllipse(PenSelected, orientationX - 3, orientationY - 3, 6, 6);
                                        }
                                    }
                                    if (hoverLine == l)
                                    {
                                        pe.Graphics.DrawLine(PenHover, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                        if (!pdf)
                                        {
                                            pe.Graphics.DrawLine(PenHover, new System.Drawing.Point(midX, midY), new System.Drawing.Point(orientationX, orientationY));
                                            pe.Graphics.DrawEllipse(PenHover, orientationX - 2, orientationY - 2, 4, 4);
                                        }
                                    }
                                    pe.Graphics.DrawLine(Pen, new System.Drawing.Point(startX, startY), new System.Drawing.Point(endX, endY));
                                    if (!pdf)
                                    {
                                        pe.Graphics.DrawLine(Pen, new System.Drawing.Point(midX, midY), new System.Drawing.Point(orientationX, orientationY));
                                        pe.Graphics.DrawEllipse(Pen, orientationX - 1, orientationY - 1, 2, 2);
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
