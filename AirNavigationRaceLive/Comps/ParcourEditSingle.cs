using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AirNavigationRaceLive.Comps.Model;
using AirNavigationRaceLive.Comps.Helper;
using NetworkObjects;

namespace AirNavigationRaceLive.Comps
{
    public partial class ParcourEditSingle : UserControl
    {
        Cursor select = new Cursor(@"Resources\GPSCursor.cur");
        Cursor move = new Cursor(@"Resources\GPSCursorModify.cur");
        private Client.DataAccess Client;
        Converter c = null;
        private Parcour activeParcour;
        private Point dragPoint = null;
        private readonly List<Point> gluePoints = new List<Point>();
        private readonly List<Point> connectedPoints = new List<Point>();
        private Point hoverPoint = null;
        private Point selectedPoint = null;
        ParcourGeneratorSingle pc;
        Timer t;
        private Map CurrentMap = null;
        private volatile bool drag = false;
        private volatile bool setStartPoint = false;
        private volatile bool setEndPoint = false;
        private volatile bool generatedParcour;

        public ParcourEditSingle(Client.DataAccess iClient)
        {
            Client = iClient;
            InitializeComponent();
            PictureBox1.Cursor = select;
            activeParcour = new Parcour();
            PictureBox1.SetParcour(activeParcour);
        }
        #region load
        private void ParcourGen_Load(object sender, EventArgs e)
        {
            loadMaps();
            comboBoxPoint.Items.Clear();
            foreach (int i in new int[] { 3, 7 })
            {
                comboBoxPoint.Items.Add((LineType)i);
            }
        }
        private void loadMaps()
        {
            comboBoxParcours.Items.Clear();
            List<Parcour> parcours = Client.SelectedCompetition.Parcour.ToList();
            Parcour newParcour = new Parcour();
            newParcour.Name = "New Parcour";
            newParcour.Line.Clear();
            newParcour.Line.Add(new Line());
            comboBoxParcours.Items.Add(new ListItem(newParcour));
            foreach (Parcour p in parcours)
            {
                if (p.Line.Count(pp => pp.Type == (int)LineType.START_B || pp.Type == (int)LineType.START_C || pp.Type == (int)LineType.START_D) == 0)
                {
                    comboBoxParcours.Items.Add(new ListItem(p));
                }
            }
            comboBoxMaps.Items.Clear();
            List<Map> maps = Client.SelectedCompetition.Map.ToList();
            foreach (Map m in maps)
            {
                comboBoxMaps.Items.Add(new ListItemMap(m));
            }
        }

        class ListItemMap
        {
            private Map map;
            public ListItemMap(Map imap)
            {
                map = imap;
            }

            public override String ToString()
            {
                return map.Name;
            }
            public Map getMap()
            {
                return map;
            }
        }

        class ListItem
        {
            private Parcour parcour;
            public ListItem(Parcour iparcour)
            {
                parcour = iparcour;
            }

            public override String ToString()
            {
                return parcour.Name;
            }
            public Parcour getParcour()
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
                    //TODO dragPoint.edited = true;
                    foreach (Point p in gluePoints)
                    {
                        p.latitude = newLatitude;
                        p.longitude = newLongitude;
                        //TODO     p.edited = true;
                    }
                    foreach (Point p in connectedPoints)
                    {
                        if (p != dragPoint)
                        {
                            p.latitude = dragPoint.latitude + (p.latitude - oldLat);
                            p.longitude = dragPoint.longitude + (p.longitude - oldLong);
                            //TODO p.edited = true;
                        }
                    }
                    PictureBox1.Invalidate();
                }
                else
                {
                    dragPoint = null;
                    bool pointSet = false;
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
                                SetHoverPoint(l.A, l);
                                gluePoints.Clear();
                                gluePoints.AddRange(findGluePoints(activeParcour.Line, l.A));
                                connectedPoints.Clear();
                                connectedPoints.AddRange(findConnectedPoints(activeParcour.Line, l.A, l));
                                pointSet = true;
                                PictureBox1.Cursor = move;
                                break;
                            }
                            else if (Vector.Abs(mousePos - new Vector(endX, endY, 0)) < 3)
                            {
                                SetHoverPoint(l.B, l);
                                gluePoints.Clear();
                                gluePoints.AddRange(findGluePoints(activeParcour.Line, l.B));
                                connectedPoints.Clear();
                                connectedPoints.AddRange(findConnectedPoints(activeParcour.Line, l.B, l));
                                pointSet = true;
                                PictureBox1.Cursor = move;
                                break;

                            }
                            else if (Vector.Abs(mousePos - new Vector(orientationX, orientationY, 0)) < 3)
                            {
                                SetHoverPoint(l.O, l);
                                gluePoints.Clear();
                                gluePoints.AddRange(findGluePoints(activeParcour.Line, l.O));
                                connectedPoints.Clear();
                                connectedPoints.AddRange(findConnectedPoints(activeParcour.Line, l.O, l));
                                pointSet = true;
                                PictureBox1.Cursor = move;
                                break;
                            }
                        }

                    }
                    if (!pointSet)
                    {
                        SetHoverPoint(null, null);
                        PictureBox1.Cursor = select;
                    }
                }
            }
        }
        private List<Point> findGluePoints(ICollection<Line> Line, Point original)
        {
            List<Point> result = new List<Point>();
            foreach (Line l in Line)
            {
                if (samePos(l.A, original) && !(original == l.A))
                {
                    result.Add(l.A);
                }
                if (samePos(l.B, original) && !(original == l.B))
                {
                    result.Add(l.B);
                }
                if (samePos(l.O, original) && !(original == l.O))
                {
                    result.Add(l.O);
                }
            }
            return result;
        }
        private List<Point> findConnectedPoints(ICollection<Line> Line, Point original, Line originalLine)
        {
            List<Point> result = new List<Point>();
            foreach (Line l in Line)
            {
                if (l.Type >= 3 && l.Type <= 10 && (l.A == original || l.B == original || l.O == original))
                {
                    result.Add(l.A);
                    result.Add(l.B);
                    result.Add(l.O);
                }
                else if (l.Type == (int)LineType.Point && originalLine.Type >= 3 && originalLine.Type <= 6)
                {
                    Vector mid = Vector.Middle(new Vector(originalLine.A.longitude, originalLine.A.latitude, originalLine.A.altitude), new Vector(originalLine.B.longitude, originalLine.B.latitude, originalLine.B.altitude));
                    if (Vector.Abs(mid - new Vector(l.A.longitude, l.A.latitude, l.A.altitude)) < 0.01)
                    {
                        result.Add(l.A);
                        result.Add(l.B);
                        result.Add(l.O);
                        result.AddRange(findGluePoints(Line, l.O));
                    }
                }
            }
            return result;
        }
        private bool samePos(Point a, Point b)
        {
            return Vector.Abs(new Vector(a.longitude, a.latitude, a.altitude) - new Vector(b.longitude, b.latitude, b.altitude)) == 0;
        }
        private void SetHoverPoint(Point p, Line l)
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
                Parcour p = li.getParcour();
                Map m = null;
                comboBoxMaps.SelectedItem = null;
                if (p.Map!=null)
                {
                    m = p.Map;
                    MemoryStream ms = new MemoryStream(m.Picture.Data);
                    PictureBox1.Image = System.Drawing.Image.FromStream(ms);
                    c = new Converter(m);
                    PictureBox1.SetConverter(c);
                    CurrentMap = m;
                    foreach (Object lim in comboBoxMaps.Items)
                    {
                        ListItemMap limm = lim as ListItemMap;
                        if (limm.getMap() == m)
                        {
                            comboBoxMaps.SelectedItem = lim;
                            break;
                        }
                    }
                }
                else
                {
                    p = new Parcour();
                }

                List<Line> toDelete = p.Line.Where(pp => pp.Type == (int)LineType.START || pp.Type == (int)LineType.END).ToList();
                foreach (Line l in toDelete)
                {
                    p.Line.Remove(l);
                }
                activeParcour = p;
                PictureBox1.SetParcour(activeParcour);
                generatedParcour = activeParcour.Line.Count(pp => pp.Type == (int)LineType.Point) > 0;
            }
        }


        private void comboBoxMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItemMap limm = comboBoxMaps.SelectedItem as ListItemMap;
            if (limm != null)
            {
                activeParcour = new Parcour();
                CurrentMap = limm.getMap();
                MemoryStream ms = new MemoryStream(CurrentMap.Picture.Data);
                PictureBox1.Image = System.Drawing.Image.FromStream(ms);
                c = new Converter(CurrentMap);
                PictureBox1.SetParcour(activeParcour);
                PictureBox1.SetConverter(c);
            }
        }


        private void btnRecalc_Click(object sender, EventArgs e)
        {
            if (!(activeParcour.Line.Count(p => p.Type == (int)LineType.START_A) == 1 && activeParcour.Line.Count(p => p.Type == (int)LineType.END_A) == 1))
            {
                return;
            }
            double channelWidec = Decimal.ToDouble(channelWide.Value);
            double channelLength = Decimal.ToDouble(fldChannelLength.Value);
            t = new Timer();
            t.Tick += new EventHandler(t_Tick);
            t.Interval = 100;
            t.Start();
            pc = new ParcourGeneratorSingle();
            lock (activeParcour)
            {
                pc.RecalcParcour(activeParcour, c, channelWidec, channelLength);

            }
            PictureBox1.Invalidate();
        }

        void t_Tick(object sender, EventArgs e)
        {
            try
            {
                PictureBox1.Invalidate();
                fldLengthDirect.Text = pc.bestStraightLength + "";
                fldLengthSummed.Text = pc.bestLegLength + "";
                if (pc.finished)
                {
                    t.Stop();
                }
            }
            catch
            {
                t.Stop();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CurrentMap == null)
            {
                MessageBox.Show("No Map selected", "Incomplete Data");
            }
            else
            {
                Parcour p = new Parcour();
                p.Name = fldName.Text;
                foreach(Line l in activeParcour.Line)
                {
                    p.Line.Add(l);
                }
                p.Map = CurrentMap;
                p.Competition = Client.SelectedCompetition;
                Client.DBContext.ParcourSet.Add(p);
                Client.DBContext.SaveChanges();
                MessageBox.Show("Successfully saved");
            }
        }

        private void ParcourGen_VisibleChanged(object sender, EventArgs e)
        {
            loadMaps();
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (setEndPoint || setStartPoint)
            {
                return;
            }

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
            if (setEndPoint || setStartPoint)
            {
                double latitude = c.YtoLatitude(e.Y);
                double longitude = c.XtoLongitude(e.X);
                double channel = Decimal.ToDouble(channelWide.Value);
                double channelMeterKm = Converter.NMtoM(channel);
                double latitudePlus1 = latitude + 1;
                double longitudePlus1 = longitude + 1;

                double distLat1 = Converter.Distance(longitude, latitude, longitude, latitudePlus1);
                double distLong1 = Converter.Distance(longitude, latitude, longitudePlus1, latitude);

                double corridorDiffInLat = channelMeterKm / distLat1;
                double corridorDiffInLong = channelMeterKm / distLong1;


                double channelLatitude = corridorDiffInLat / 2;
                double channelLongitude = corridorDiffInLong / 2;

                if (setStartPoint)
                {
                    List<Line> toDelete = activeParcour.Line.Where(pp => pp.Type == (int)LineType.START_A).ToList();
                    foreach (Line ll in toDelete)
                    {
                        activeParcour.Line.Remove(ll);
                    }
                    Line l = new Line();
                    l.Type = (int)LineType.START_A;

                    l.A = Factory.newGPSPoint(longitude, latitude - channelLatitude, 0);
                    l.B = Factory.newGPSPoint(longitude, latitude + channelLatitude, 0);
                    l.O = Factory.newGPSPoint(longitude + channelLongitude, latitude, 0);
                    activeParcour.Line.Add(l);
                }
                if (setEndPoint)
                {
                    List<Line> toDelete = activeParcour.Line.Where(pp => pp.Type == (int)LineType.END_A).ToList();
                    foreach (Line ll in toDelete)
                    {
                        activeParcour.Line.Remove(ll);
                    }
                    Line l = new Line();
                    l.Type = (int)LineType.END_A;

                    l.A = Factory.newGPSPoint(longitude, latitude - channelLatitude, 0);
                    l.B = Factory.newGPSPoint(longitude, latitude + channelLatitude, 0);
                    l.O = Factory.newGPSPoint(longitude + channelLongitude, latitude, 0);

                    activeParcour.Line.Add(l);
                }

                PictureBox1.Invalidate();

                setEndPoint = false;
                setStartPoint = false;
                btnRecalc_Click(null, null);
                return;
            }
            drag = false;
            btnRecalc_Click(null, null);
            PictureBox1_MouseMove(PictureBox1, e);
            if (hoverPoint != null)
            {
                selectedPoint = hoverPoint;
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
                    Vector diff = neu - m;
                    l.A.longitude += diff.X;
                    l.A.latitude += diff.Y;
                    l.O.longitude += diff.X;
                    l.O.latitude += diff.Y;
                    l.B.longitude += diff.X;
                    l.B.latitude += diff.Y;
                    List<Point> connectedPoints = findConnectedPoints(activeParcour.Line, l.A, l);
                    foreach (Point p in connectedPoints)
                    {
                        if (p != l.A && p != l.B && p != l.O)
                        {
                            p.longitude += diff.X;
                            p.latitude += diff.Y;
                        }
                    }
                    PictureBox1.Invalidate();
                    btnRecalc_Click(null, null);
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

        private void btnSetStartPoint_Click(object sender, EventArgs e)
        {
            setStartPoint = true;
            setEndPoint = false;
        }

        private void btnSetEndPoint_Click(object sender, EventArgs e)
        {
            setStartPoint = false;
            setEndPoint = true;
        }

        private void fldChannelLength_ValueChanged(object sender, EventArgs e)
        {
            btnRecalc_Click(null, null);
        }
    }
}
