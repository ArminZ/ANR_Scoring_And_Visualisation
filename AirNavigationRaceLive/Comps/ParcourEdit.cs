using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Model;
using AirNavigationRaceLive.ModelExtensions;

namespace AirNavigationRaceLive.Comps
{
    public partial class ParcourEdit : UserControl
    {
        Cursor select = new Cursor(@"Resources\GPSCursor.cur");
        Cursor move = new Cursor(@"Resources\GPSCursorModify.cur");
        private Client.DataAccess Client;
        Converter c = null;
        private ParcourSet activeParcour;
        private PointTempExtension dragPoint = null;
        private readonly List<PointTempExtension> gluePoints = new List<PointTempExtension>();
        private readonly List<PointTempExtension> connectedPoints = new List<PointTempExtension>();
        private PointTempExtension hoverPoint = null;
        private PointTempExtension selectedPoint = null;
        ParcourGenerator pc;
        Timer t;
        private MapSet CurrentMap = null;
        private volatile bool drag = false;

        public ParcourEdit(Client.DataAccess iClient)
        {
            Client = iClient;
            InitializeComponent();
            PictureBox1.Cursor = select;
            activeParcour = new ParcourSet();
            PictureBox1.SetParcour(activeParcour);
        }
        #region load
        private void ParcourGen_Load(object sender, EventArgs e)
        {
            loadMaps();
            comboBoxPoint.Items.Clear();
            foreach (int i in new int[]{3,4,5,6,7,8,9,10,14})
            {
                comboBoxPoint.Items.Add((LineType)i);
            }
        }
        private void loadMaps()
        {
            comboBoxParcours.Items.Clear();
            List<ParcourSet> parcours = Client.SelectedCompetition.ParcourSet.ToList();
            foreach (ParcourSet p in parcours)
            {
                comboBoxParcours.Items.Add(new ListItem(p));
            }
        }

        class ListItem
        {
            private ParcourSet parcour;
            public ListItem(ParcourSet iparcour)
            {
                parcour = iparcour;
            }

            public override String ToString()
            {
                return parcour.Name;
            }
            public ParcourSet getParcour()
            {
                return parcour;
            }
        }
        #endregion

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            fldCursorX.Text = e.X.ToString();
            fldCursorY.Text = e.Y.ToString();
            if (c != null)
            {
                double latitude = c.YtoLatitude(e.Y);
                double longitude = c.XtoLongitude(e.X);
                fldLatitude.Text = latitude.ToString();
                fldLongitude.Text = longitude.ToString();
                if (drag && (hoverPoint != null || dragPoint != null))
                {
                    if (dragPoint == null)
                    {
                        dragPoint = hoverPoint;
                    }
                    double newLatitude = c.YtoLatitude(e.Y);
                    double newLongitude = c.XtoLongitude(e.X);
                    double oldLat = dragPoint.latitude;
                    double oldLong = dragPoint.longitude;
                    dragPoint.latitude = newLatitude;
                    dragPoint.longitude = newLongitude;
                    dragPoint.edited = true;
                    foreach (PointTempExtension p in gluePoints)
                    {
                        p.latitude = newLatitude;
                        p.longitude = newLongitude;
                        p.edited = true;
                    }
                    if (checkBoxConnected.Checked)
                    {
                        foreach (PointTempExtension p in connectedPoints)
                        {
                            if (p != dragPoint)
                            {
                                p.latitude = dragPoint.latitude + (p.latitude - oldLat);
                                p.longitude = dragPoint.longitude + (p.longitude - oldLong);
                                p.edited = true;
                            }
                        }
                    }
                    PictureBox1.Invalidate();
                }
                else
                {
                    dragPoint = null;
                    bool Point = false;
                    lock (activeParcour)
                    {
                        foreach (Line l in activeParcour.Line)
                        {
                            int startX = c.getStartX(l);
                            int startY = c.getStartY(l);
                            int endX = c.getEndX(l);
                            int endY = c.getEndY(l);
                            int midX = startX + (endX - startX) / 2;
                            int midY = startY + (endY - startY) / 2;
                            int orientationX = c.getOrientationX(l);
                            int orientationY = c.getOrientationY(l);
                            Vector mousePos = new Vector(e.X, e.Y, 0);
                            if (Vector.Abs(mousePos - new Vector(startX, startY, 0)) < 3)
                            {
                                SetHoverPoint(new PointTempExtension(l.A), l);
                                gluePoints.Clear();
                                gluePoints.AddRange(findGluePoints(activeParcour.Line, new PointTempExtension(l.A)));
                                connectedPoints.Clear();
                                connectedPoints.AddRange(findConnectedPoints(activeParcour.Line, new PointTempExtension(l.A), l));
                                Point = true;
                                PictureBox1.Cursor = move;
                                break;
                            }
                            else if (Vector.Abs(mousePos - new Vector(endX, endY, 0)) < 3)
                            {
                                SetHoverPoint(new PointTempExtension(l.B), l);
                                gluePoints.Clear();
                                gluePoints.AddRange(findGluePoints(activeParcour.Line, new PointTempExtension(l.B)));
                                connectedPoints.Clear();
                                connectedPoints.AddRange(findConnectedPoints(activeParcour.Line,new PointTempExtension(l.B), l));
                                Point = true;
                                PictureBox1.Cursor = move;
                                break;

                            }
                            else if (Vector.Abs(mousePos - new Vector(orientationX, orientationY, 0)) < 3)
                            {
                                SetHoverPoint(new PointTempExtension(l.O), l);
                                gluePoints.Clear();
                                gluePoints.AddRange(findGluePoints(activeParcour.Line, new PointTempExtension(l.O)));
                                connectedPoints.Clear();
                                connectedPoints.AddRange(findConnectedPoints(activeParcour.Line, new PointTempExtension(l.O), l));
                                Point = true;
                                PictureBox1.Cursor = move;
                                break;
                            }
                        }

                    }
                    if (!Point)
                    {
                        SetHoverPoint(null, null);
                        PictureBox1.Cursor = select;
                    }
                }
            }
        }
        private List<PointTempExtension> findGluePoints(ICollection<Line> Line, PointTempExtension original)
        {
            List<PointTempExtension> result = new List<PointTempExtension>();
            foreach (Line l in Line)
            {
                if (samePos(l.A, original) && !(original.Id == l.A.Id))
                {
                    result.Add(new PointTempExtension(l.A));
                }
                if (samePos(l.B, original) && !(original.Id == l.B.Id))
                {
                    result.Add(new PointTempExtension(l.B));
                }
                if (samePos(l.O, original) && !(original.Id == l.O.Id))
                {
                    result.Add(new PointTempExtension(l.O));
                }
            }
            return result;
        }
        private List<PointTempExtension> findConnectedPoints(ICollection<Line> Line, Point original, Line originalLine)
        {
            List<PointTempExtension> result = new List<PointTempExtension>();
            foreach (Line l in Line)
            {
                if (l.Type >= 3 && l.Type <= 10 && (l.A.Id == original.Id || l.B.Id == original.Id || l.O.Id == original.Id))
                {
                    result.Add(new PointTempExtension(l.A));
                    result.Add(new PointTempExtension(l.B));
                    result.Add(new PointTempExtension(l.O));
                }
                else if (l.Type == (int)LineType.Point && originalLine.Type >= 3 && originalLine.Type <= 6)
                {
                    Vector mid = Vector.Middle(new Vector(originalLine.A.longitude, originalLine.A.latitude, originalLine.A.altitude), new Vector(originalLine.B.longitude, originalLine.B.latitude, originalLine.B.altitude));
                    if (Vector.Abs(mid - new Vector(l.A.longitude, l.A.latitude, l.A.altitude)) < 0.01)
                    {
                        result.Add(new PointTempExtension(l.A));
                        result.Add(new PointTempExtension(l.B));
                        result.Add(new PointTempExtension(l.O));
                        result.AddRange(findGluePoints(Line, new PointTempExtension(l.O)));
                    }
                }
            }
            return result;
        }
        private bool samePos(Point a, Point b)
        {
            return Vector.Abs(new Vector(a.longitude, a.latitude, a.altitude) - new Vector(b.longitude, b.latitude, b.altitude)) == 0;
        }
        private void SetHoverPoint(PointTempExtension p, Line l)
        {
            bool change = hoverPoint != p;
            if (change)
            {
                hoverPoint = p;
                PictureBox1.SetHoverLine(l);
                PictureBox1.Invalidate();
            }
        }
        private void comboBoxParcours_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem li = comboBoxParcours.SelectedItem as ListItem;
            if (li != null)
            {
                ParcourSet p = li.getParcour();
                MapSet m = p.MapSet;
                MemoryStream ms = new MemoryStream(m.PictureSet.Data);
                PictureBox1.Image = System.Drawing.Image.FromStream(ms);
                c = new Converter(m);
                PictureBox1.SetConverter(c);
                CurrentMap = m;
                List<Line> toDelete =  p.Line.Where(pp=> pp.Type == (int)LineType.START || pp.Type == (int)LineType.END).ToList();
                foreach(Line l in toDelete)
                {
                    p.Line.Remove(l);
                }
                activeParcour = p;
                PictureBox1.SetParcour(activeParcour);
                bool generatedParcour = activeParcour.Line.Count(pp => pp.Type == (int)LineType.Point) > 0;
                btnRecalc.Enabled = generatedParcour;
                chkAutocalc.Enabled = generatedParcour;
                chkAutocalc.Checked = generatedParcour;
            }
        }

        private void btnRecalc_Click(object sender, EventArgs e)
        {
            if (btnRecalc.Enabled)
            {
                btnRecalc.Enabled = false;
                try
                {
                    double channel = Decimal.ToDouble(channelWide.Value);
                    t = new Timer();
                    t.Tick += new EventHandler(t_Tick);
                    t.Interval = 100;
                    t.Start();
                    pc = new ParcourGenerator();
                    lock (activeParcour)
                    {
                        pc.RecalcParcour(activeParcour, c, channel);
                    }
                    PictureBox1.Invalidate();
                }
                catch (Exception ex)
                {
                    btnRecalc.Enabled = true;
                    MessageBox.Show(ex.Message, "Error while generating Parcour");
                }
            }
        }

        void t_Tick(object sender, EventArgs e)
        {
            try
            {
                PictureBox1.Invalidate();
                if (pc.finished)
                {
                    t.Stop();
                    btnRecalc.Enabled = true;
                }
            }
            catch
            {
                t.Stop();
                btnRecalc.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CurrentMap == null)
            {
                MessageBox.Show("No MapSet selected", "Incomplete Data");
            }
            else
            {
                ParcourSet p = new ParcourSet();
                p.CompetitionSet = Client.SelectedCompetition;
                p.Name = fldName.Text;
                foreach(Line l in activeParcour.Line)
                {
                    p.Line.Add(l);
                }
                p.MapSet = CurrentMap;
                Client.DBContext.ParcourSet.Add(p);
                Client.DBContext.SaveChanges();
                MessageBox.Show("Successfully saved");
            }
        }

        private void ParcourGen_VisibleChanged(object sender, EventArgs e)
        {
            loadMaps();
        }
        #region NumUpDown
        private void numLatA_ValueChanged(object sender, EventArgs e)
        {
            if (selectedPoint != null)
            {
                selectedPoint.latitude = Decimal.ToDouble(numLatA.Value);
                PictureBox1.Invalidate();
            }
        }

        private void numLongA_ValueChanged(object sender, EventArgs e)
        {
            if (selectedPoint != null)
            {
                selectedPoint.longitude = Decimal.ToDouble(numLongA.Value);
                PictureBox1.Invalidate();
            }
        }
        #endregion

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            if (activeParcour != null && hoverPoint != null)
            {
                foreach (Line l in activeParcour.Line)
                {
                    if (l.Type <= 10 && l.Type >= 3 && (l.O == hoverPoint || l.A == hoverPoint || l.B == hoverPoint))
                    {
                        comboBoxPoint.SelectedItem = (LineType)l.Type;
                    }
                }
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
            if (chkAutocalc.Checked)
            {
                btnRecalc_Click(null, null);
            }
            PictureBox1_MouseMove(PictureBox1, e);
            if (hoverPoint != null)
            {
                selectedPoint = hoverPoint;
                numLatA.Value = (decimal)selectedPoint.latitude;
                numLongA.Value = (decimal)selectedPoint.longitude;
            }
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            drag = false;
        }

        private void channelWide_ValueChanged(object sender, EventArgs e)
        {
            btnRecalc_Click(null, null);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (activeParcour != null && comboBoxPoint.SelectedItem != null)
            {
                if (activeParcour.Line.Count(p => p.Type == (int)comboBoxPoint.SelectedItem) == 1)
                {
                    Line l = activeParcour.Line.First(p => p.Type == (int)comboBoxPoint.SelectedItem);
                    Vector a = new Vector(l.A.longitude, l.A.latitude, 0);
                    Vector b = new Vector(l.B.longitude, l.B.latitude, 0);
                    Vector m = Vector.Middle(a, b);
                    Vector neu = new Vector((double)manualPointLongitude.Value, (double)manualPointLatitude.Value, 0);
                    Vector diff = neu-m;
                    l.A.longitude += diff.X;
                    l.A.latitude += diff.Y;
                    l.O.longitude += diff.X;
                    l.O.latitude += diff.Y;
                    l.B.longitude += diff.X;
                    l.B.latitude += diff.Y;
                    List<PointTempExtension> connectedPoints = findConnectedPoints(activeParcour.Line, l.A, l);
                    foreach (Point p in connectedPoints)
                    {
                        if (p != l.A && p != l.B && p != l.O)
                        {
                            p.longitude += diff.X;
                            p.latitude += diff.Y;
                        }
                    }
                    PictureBox1.Invalidate();
                }
            }
        }

        private void comboBoxPoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (activeParcour != null && comboBoxPoint.SelectedItem != null)
            {
                if (activeParcour.Line.Count(p => p.Type == (int)comboBoxPoint.SelectedItem) == 1)
                {
                    Line l = activeParcour.Line.First(p => p.Type == (int)comboBoxPoint.SelectedItem);
                    Vector a = new Vector(l.A.longitude, l.A.latitude, 0);
                    Vector b = new Vector(l.B.longitude, l.B.latitude, 0);
                    Vector m = Vector.Middle(a, b);
                    manualPointLatitude.Value = (decimal)m.Y;
                    manualPointLongitude.Value = (decimal)m.X;
                }
                else
                {
                    manualPointLatitude.Value = 0;
                    manualPointLongitude.Value = 0;
                }
            }
        }
    }
}
