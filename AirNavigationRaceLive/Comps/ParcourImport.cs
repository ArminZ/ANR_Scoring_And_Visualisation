using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Comps
{
    public partial class ParcourImport : UserControl
    {
        private Client.DataAccess Client;
        Converter c = null;
        private ParcourSet activeParcour;
        private Line activeLine;
        private ActivePoint ap = ActivePoint.NONE;
        private Line selectedLine = null;
        private Line hoverLine = null;
        private MapSet CurrentMap = null;

        private enum ActivePoint
        {
            A, B, O, NONE
        }

        public ParcourImport(Client.DataAccess iClient)
        {
            Client = iClient;
            InitializeComponent();
            lblCompetition.Text = Client.SelectedCompetition.Name + " - parcours";
            PictureBox1.Cursor = new Cursor(@"Resources\GPSCursor.cur");
            activeParcour = new ParcourSet();
            numericUpDownAlpha.Value = activeParcour.Alpha;
            PictureBox1.SetParcour(activeParcour);
            checkValidationErrors();
        }
        #region load
        private void ParcourGen_Load(object sender, EventArgs e)
        {
            loadMaps();
        }
        private void loadMaps()
        {
            comboBoxMaps.Items.Clear();
            List<MapSet> maps = Client.SelectedCompetition.MapSet.ToList();
            foreach (MapSet m in maps)
            {
                comboBoxMaps.Items.Add(new ListItem(m));
            }
        }

        class ListItem
        {
            private MapSet map;
            public ListItem(MapSet imap)
            {
                map = imap;
            }

            public override String ToString()
            {
                return map.Name;
            }
            public MapSet getMap()
            {
                return map;
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
                if (activeLine != null)
                {
                    PictureBox1.SetSelectedLine(null);
                    #region activeLine != null
                    switch (ap)
                    {
                        case ActivePoint.A:
                            {
                                Point a = Factory.newGPSPoint(longitude, latitude, 0);
                                Point b = Factory.newGPSPoint(a.longitude, a.latitude, a.altitude);
                                Point o = Factory.newGPSPoint(a.longitude, a.latitude, a.altitude);
                                activeLine.A = a;
                                activeLine.B = b;
                                activeLine.O = o;
                                PictureBox1.Invalidate();
                                break;
                            }
                        case ActivePoint.B:
                            {
                                Point b = Factory.newGPSPoint(longitude, latitude, 0);
                                Point o = Factory.newGPSPoint(b.longitude, b.latitude, b.altitude);
                                activeLine.B = b;
                                activeLine.O = o;
                                PictureBox1.Invalidate();
                                break;
                            }
                        case ActivePoint.O:
                            {
                                Point o = Factory.newGPSPoint(longitude, latitude, 0);
                                activeLine.O = o;
                                PictureBox1.Invalidate();
                                break;
                            }
                        case ActivePoint.NONE:
                            {

                                break;
                            }
                    }
                    #endregion
                }
                else
                {
                    bool Line = false;
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
                            if (Vector.Abs(Vector.MinDistance(new Vector(startX, startY, 0), new Vector(endX, endY, 0), mousePos)) < 3 ||
                                Vector.Abs(Vector.MinDistance(new Vector(midX, midY, 0), new Vector(orientationX, orientationY, 0), mousePos)) < 3)
                            {
                                SetHoverLine(l);
                                Line = true;
                                break;
                            }
                        }
                    }
                    if (!Line)
                    {
                        SetHoverLine(null);
                    }
                }
            }
        }
        private void SetSelectedLine(Line l)
        {
            bool change = selectedLine != l;
            if (change)
            {
                selectedLine = l;
                PictureBox1.SetSelectedLine(l);
                PictureBox1.Invalidate();
                lineBox.Enabled = l != null;
                if (l != null)
                {
                    numLatA.Value = (decimal)l.A.latitude;
                    numLatB.Value = (decimal)l.B.latitude;
                    numLatO.Value = (decimal)l.O.latitude;
                    numLongA.Value = (decimal)l.A.longitude;
                    numLongB.Value = (decimal)l.B.longitude;
                    numLongO.Value = (decimal)l.O.longitude;
                    fldLineTyp.Text = ((LineType)l.Type).ToString();
                }
                else
                {
                    numLatA.Value = 0;
                    numLatB.Value = 0;
                    numLatO.Value = 0;
                    numLongA.Value = 0;
                    numLongB.Value = 0;
                    numLongO.Value = 0;
                    fldLineTyp.Text = "";
                }
            }
        }
        private void SetHoverLine(Line l)
        {
            bool change = hoverLine != l;
            if (change)
            {
                hoverLine = l;
                PictureBox1.SetHoverLine(l);
                PictureBox1.Invalidate();
                if (selectedLine == null)
                {
                    lineBox.Enabled = l != null;
                    if (l != null)
                    {
                        numLatA.Value = (decimal)l.A.latitude;
                        numLatB.Value = (decimal)l.B.latitude;
                        numLatO.Value = (decimal)l.O.latitude;
                        numLongA.Value = (decimal)l.A.longitude;
                        numLongB.Value = (decimal)l.B.longitude;
                        numLongO.Value = (decimal)l.O.longitude;
                        fldLineTyp.Text = ((LineType)l.Type).ToString();
                    }
                    else
                    {
                        numLatA.Value = 0;
                        numLatB.Value = 0;
                        numLatO.Value = 0;
                        numLongA.Value = 0;
                        numLongB.Value = 0;
                        numLongO.Value = 0;
                        fldLineTyp.Text = "";
                    }
                }
            }
        }
        private void comboBoxMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem li = comboBoxMaps.SelectedItem as ListItem;
            if (li != null)
            {
                MemoryStream ms = new MemoryStream(li.getMap().PictureSet.Data);
                PictureBox1.Image = System.Drawing.Image.FromStream(ms);
                c = new Converter(li.getMap());
                PictureBox1.SetConverter(c);
                activeParcour = new ParcourSet();
                PictureBox1.SetParcour(activeParcour);
                SetHoverLine(null);
                SetSelectedLine(null);
                CurrentMap = li.getMap();
                PictureBox1.Invalidate();
                checkValidationErrors();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ParcourSet p = new ParcourSet();
            p.Name = fldName.Text;
            foreach (Line l in activeParcour.Line)
            {
                p.Line.Add(l);
            }
            p.MapSet = CurrentMap;
            p.Alpha = activeParcour.Alpha;
            p.CompetitionSet = Client.SelectedCompetition;
            Client.DBContext.ParcourSet.Add(p);
            Client.DBContext.SaveChanges();
            MessageBox.Show("Successfully saved", "Parcour", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PictureBox1_Click(object sender, MouseEventArgs e)
        {
            if (activeLine != null)
            {
                switch (ap)
                {
                    case ActivePoint.A:
                        {
                            ap = ActivePoint.B;
                            break;
                        }
                    case ActivePoint.B:
                        {
                            ap = ActivePoint.O;
                            break;
                        }
                    case ActivePoint.O:
                        {
                            ap = ActivePoint.NONE;
                            activeLine = null;
                            break;
                        }
                }
            }
            else
            {
                SetSelectedLine(hoverLine);
            }
        }
        private void ParcourGen_VisibleChanged(object sender, EventArgs e)
        {
            loadMaps();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            activeParcour = new ParcourSet();
            PictureBox1.SetParcour(activeParcour);
            SetHoverLine(null);
            SetSelectedLine(null);
            fldName.Text = "";
            PictureBox1.Invalidate();
            checkValidationErrors();
        }


        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string FileFilter = "AutoCAD Files (*.dxf)|*.dxf|All Files (*.*)|*.*";
            ofd.Title = "DXF File Import (CH1904)";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.Filter = FileFilter;
            ofd.FileOk += new CancelEventHandler(ofd_FileOk);
            ofd.ShowDialog();
        }

        void ofd_FileOk(object sender, CancelEventArgs e)
        {
            OpenFileDialog ofd = sender as OpenFileDialog;
            try
            {
                activeParcour = Importer.importFromDxfCH(ofd.FileName);
                PictureBox1.SetParcour(activeParcour);
                PictureBox1.Invalidate();
                fldName.Text = Path.GetFileNameWithoutExtension(ofd.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error while Parsing File");
            }
        }

        #region NumUpDown
        private void numLatA_ValueChanged(object sender, EventArgs e)
        {
            if (selectedLine != null)
            {
                (selectedLine.A as Point).latitude = Decimal.ToDouble(numLatA.Value);
                PictureBox1.Invalidate();
            }
        }

        private void numLongA_ValueChanged(object sender, EventArgs e)
        {
            if (selectedLine != null)
            {
                (selectedLine.A as Point).longitude = Decimal.ToDouble(numLongA.Value);
                PictureBox1.Invalidate();
            }
        }

        private void numLatB_ValueChanged(object sender, EventArgs e)
        {
            if (selectedLine != null)
            {
                (selectedLine.B as Point).latitude = Decimal.ToDouble(numLatB.Value);
                PictureBox1.Invalidate();
            }
        }

        private void numLongB_ValueChanged(object sender, EventArgs e)
        {
            if (selectedLine != null)
            {
                (selectedLine.B as Point).longitude = Decimal.ToDouble(numLongB.Value);
                PictureBox1.Invalidate();
            }

        }

        private void numLatO_ValueChanged(object sender, EventArgs e)
        {
            if (selectedLine != null)
            {
                (selectedLine.O as Point).latitude = Decimal.ToDouble(numLatO.Value);
                PictureBox1.Invalidate();
            }

        }

        private void numLongO_ValueChanged(object sender, EventArgs e)
        {
            if (selectedLine != null)
            {
                (selectedLine.O as Point).longitude = Decimal.ToDouble(numLongO.Value);
                PictureBox1.Invalidate();
            }

        }
        #endregion


        private void btnImportDxfWGS_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string FileFilter = "AutoCAD Files (*.dxf)|*.dxf|All Files (*.*)|*.*";
            ofd.Title = "DXF File Import (WGS84)";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.Filter = FileFilter;
            ofd.FilterIndex = 1;
            ofd.FileOk += new CancelEventHandler(ofd_FileOkWGS);
            ofd.ShowDialog();
        }
        void ofd_FileOkWGS(object sender, CancelEventArgs e)
        {
            OpenFileDialog ofd = sender as OpenFileDialog;
            try
            {
                activeParcour = Importer.importFromDxfWGS(ofd.FileName);
                PictureBox1.SetParcour(activeParcour);
                PictureBox1.Invalidate();
                PictureBox1.Refresh();
                fldName.Text = Path.GetFileNameWithoutExtension(ofd.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error while Parsing File");
            }
        }

        private void btnImportSwitched_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string FileFilter = "AutoCAD Files (*.dxf)|*.dxf|All Files (*.*)|*.*";
            ofd.Title = "DXF File Import (WGS84), Switched";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.Filter = FileFilter;
            ofd.FilterIndex = 1;
            ofd.FileOk += new CancelEventHandler(ofd_FileOkWGSSwitched);
            ofd.ShowDialog();
        }
        void ofd_FileOkWGSSwitched(object sender, CancelEventArgs e)
        {
            OpenFileDialog ofd = sender as OpenFileDialog;
            try
            {
                activeParcour = Importer.importFromDxfWGSSwitched(ofd.FileName);
                PictureBox1.SetParcour(activeParcour);
                PictureBox1.Invalidate();
                PictureBox1.Refresh();
                fldName.Text = Path.GetFileNameWithoutExtension(ofd.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error while Parsing File");
            }
        }

        private void btnImportLayerKML_Click(object sender, EventArgs e)
        {
            //not yet implemented
            OpenFileDialog ofd = new OpenFileDialog();
            string FileFilter = "GoogleEarth KML (*.kml)|*.kml|All Files (*.*)|*.*";
            ofd.Title = "Layer Import from KML file";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.Filter = FileFilter;
            ofd.FilterIndex = 1;
            ofd.FileOk += new CancelEventHandler(ofd_FileOkKMLLayer);
            ofd.ShowDialog();
        }
        void ofd_FileOkKMLLayer(object sender, CancelEventArgs e)
        {
            OpenFileDialog ofd = sender as OpenFileDialog;
            try
            {
                activeParcour = Importer.importFromKMLLayer(ofd.FileName);
                PictureBox1.SetParcour(activeParcour);
                PictureBox1.Invalidate();
                PictureBox1.Refresh();
                fldName.Text = Path.GetFileNameWithoutExtension(ofd.FileName);
            }
            catch (ApplicationException ex1)
            {
                MessageBox.Show(ex1.Message, "Parcour import from *.kml", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error while Parsing File");
            }
        }

        private void fldName_TextChanged(object sender, EventArgs e)
        {
            checkValidationErrors();
        }

        private void checkValidationErrors()
        {
            bool hasErrors = false;
            errorProviderParcourImport.Clear();
            if (string.IsNullOrWhiteSpace(fldName.Text))
            {
                errorProviderParcourImport.SetError(fldName, "Fill a name");
                hasErrors = true;
            }
            if (CurrentMap == null)
            {
                errorProviderParcourImport.SetError(fldName, "No MapSet selected");
                hasErrors = true;
            }
            btnSave.Enabled = !hasErrors && !(CurrentMap == null);
        }

        private void numericUpDownAlpha_ValueChanged(object sender, EventArgs e)
        {
            if (activeParcour != null)
            {
                ParcourSet p = activeParcour;
                p.Alpha = (int)numericUpDownAlpha.Value;
                PictureBox1.SetParcour(p);
                PictureBox1.Invalidate();
            }
        }

        private void numericUpDownPen_ValueChanged(object sender, EventArgs e)
        {
            if (activeParcour != null)
            {
                ParcourSet p = activeParcour;
                PictureBox1.UserLineWidth = (float)numericUpDownPen.Value;
                PictureBox1.SetParcour(p);
                PictureBox1.Invalidate();
            }
        }

        private void btnColorLayer_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = false;
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            btnColorLayer.BackColor = cd.Color;
            ParcourSet p = activeParcour;
            PictureBox1.ProhZoneColor = cd.Color;
            PictureBox1.SetParcour(p);
            PictureBox1.Invalidate();
        }

        private void btnColorPen_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = false;
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            btnColorPen.BackColor = cd.Color;
            ParcourSet p = activeParcour;
            PictureBox1.UserPenColor = cd.Color;
            PictureBox1.SetParcour(p);
            PictureBox1.Invalidate();
        }

        private void checkBoxCircle_CheckedChanged(object sender, EventArgs e)
        {
            ParcourSet p = activeParcour;
            PictureBox1.UserCircleWidth = checkBoxCircle.Checked ? 2f : 0f;
            PictureBox1.SetParcour(p);
            PictureBox1.Invalidate();
        }
    }
}
