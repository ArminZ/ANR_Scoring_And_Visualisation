using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Dialogs;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Comps
{
    public partial class ParcourOverview : UserControl
    {
        private Client.DataAccess Client;
        Converter c = null;
        private Line activeLine;
        private ActivePoint ap = ActivePoint.NONE;
        private Line selectedLine = null;
        private Line hoverLine = null;
        private ParcourSet activeParcour = new ParcourSet();
        private bool mustPromptForAdditionalText = Properties.Settings.Default.parcourPDFAdditionalText;

        private enum ActivePoint
        {
            A, B, O, NONE
        }

        public ParcourOverview(Client.DataAccess iClient)
        {
            Client = iClient;
            InitializeComponent();
            lblCompetition.Text = Client.SelectedCompetition.Name + " - parcours";
            PictureBox1.Cursor = new Cursor(@"Resources\GPSCursor.cur");
        }
        #region load

        class ListItem : ListViewItem
        {
            private ParcourSet parcour;
            public ListItem(ParcourSet iParcour) : base(iParcour.Name)
            {
                parcour = iParcour;
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

        private void loadParcours()
        {
            lblCompetition.Text = Client.SelectedCompetition.Name + " - parcours";
            deleteToolStripMenuItem.Enabled = false;
            PictureBox1.SetConverter(c);
            PictureBox1.Image = null;
            activeParcour = new ParcourSet();
            PictureBox1.SetParcour(activeParcour);
            SetHoverLine(null);
            SetSelectedLine(null);
            PictureBox1.Invalidate();

            listBox1.Items.Clear();
            //CompetitionSet cs = Client.SelectedCompetition;
            //List<ParcourSet> ods = cs.ParcourSet.ToList<ParcourSet>();
            List<ParcourSet> parcours = Client.SelectedCompetition.ParcourSet.ToList<ParcourSet>();
            foreach (ParcourSet p in parcours)
            {
                listBox1.Items.Add(new ListItem(p));
            }
        }
        #endregion
        private void ParcourGen_VisibleChanged(object sender, EventArgs e)
        {
            loadParcours();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadParcours();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 1)
            {
                ListItem li = listBox1.SelectedItems[0] as ListItem;
                ParcourSet parcour = li.getParcour();
                if (parcour.Id != 0)
                {
                    Client.DBContext.ParcourSet.Remove(parcour);
                }
                Client.DBContext.SaveChanges();
                loadParcours();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 1)
            {
                ListItem li = listBox1.SelectedItems[0] as ListItem;
                activeParcour = li.getParcour();
                deleteToolStripMenuItem.Enabled = true;
                MapSet map = li.getParcour().MapSet;

                MemoryStream ms = new MemoryStream(map.PictureSet.Data);
                PictureBox1.Image = System.Drawing.Image.FromStream(ms);
                c = new Converter(map);
                PictureBox1.SetConverter(c);


                //PictureBox1.Invalidate();

                numericUpDownAlpha.Value = activeParcour.Alpha;
                btnColorGates.BackColor = activeParcour.ColorGates;
                btnColorPROH.BackColor = activeParcour.ColorPROH;
                checkBoxCircle.Checked = activeParcour.HasCircleOnGates;
                numericUpDownPenGates.Value = activeParcour.PenWidthGates;

                //PictureBox1.ColorGates = activeParcour.ColorGates;
                //PictureBox1.PenWidthGates = (float)activeParcour.PenWidthGates;
                PictureBox1.HasCircleOnGates = activeParcour.HasCircleOnGates;

                radioButtonPenaltyCalcTypePROH.Checked = (activeParcour.PenaltyCalcType == 0) ? true : false;
                radioButtonPenaltyCalcTypeChannel.Checked = (!radioButtonPenaltyCalcTypePROH.Checked);
                btnChannelColor.BackColor = activeParcour.ColorChannel;
                btnChannelFillColor.BackColor = activeParcour.ColorChannelFill;
                numericUpDownChannelAlpha.Value = activeParcour.Alpha;
                numericUpDownChannelPen.Value = activeParcour.PenWidthChannel;

                chkIntersectionPointsShow.Checked = activeParcour.HasIntersectionCircles;
                btnIntersectColor.BackColor = activeParcour.ColorIntersection;
                numericUpDownIntersectionCircleRadius.Value = activeParcour.IntersectionCircleRadius;
                numericUpDownPenWidthIntersect.Value = activeParcour.PenWidthIntersection;

                PictureBox1.SetParcour(activeParcour);
                SetHoverLine(null);
                SetSelectedLine(null);
                PictureBox1.Invalidate();
            }
        }

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

        #region add Lines
        private void btnAddStartLine_Click(object sender, EventArgs e)
        {
            SetSelectedLine(null);
            if (activeParcour.Line.Any(p => p.Type == (int)LineType.START))
            {
                activeLine = activeParcour.Line.Single(p => p.Type == (int)LineType.START);
            }
            else
            {
                activeLine = new Line();
                activeLine.Type = (int)LineType.START;
                activeParcour.Line.Add(activeLine);
            }
            ap = ActivePoint.A;
        }
        private void btnAddEnd_Click(object sender, EventArgs e)
        {
            SetSelectedLine(null);
            if (activeParcour.Line.Any(p => p.Type == (int)LineType.END))
            {
                activeLine = activeParcour.Line.Single(p => p.Type == (int)LineType.END);
            }
            else
            {
                activeLine = new Line();
                activeLine.Type = (int)LineType.END;
                activeParcour.Line.Add(activeLine);
            }
            ap = ActivePoint.A;
        }
        private void btnAddLineOfNoReturn_Click(object sender, EventArgs e)
        {
            SetSelectedLine(null);
            if (activeParcour.Line.Any(p => p.Type == (int)LineType.NOBACKTRACKLINE))
            {
                activeLine = activeParcour.Line.Single(p => p.Type == (int)LineType.NOBACKTRACKLINE);
            }
            else
            {
                activeLine = new Line();
                activeLine.Type = (int)LineType.NOBACKTRACKLINE;
                activeParcour.Line.Add(activeLine);
            }
            ap = ActivePoint.A;

        }
        #endregion
        private void PictureBox1_Click(object sender, MouseEventArgs e)
        {
            if (activeLine != null)
            {
                switch (ap)
                {
                    case ParcourOverview.ActivePoint.A:
                        {
                            ap = ParcourOverview.ActivePoint.B;
                            break;
                        }
                    case ParcourOverview.ActivePoint.B:
                        {
                            ap = ParcourOverview.ActivePoint.O;
                            break;
                        }
                    case ParcourOverview.ActivePoint.O:
                        {
                            ap = ParcourOverview.ActivePoint.NONE;
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            double scaleFactor = 2.0;
            string mapScale = "";
            PDFSize size = PDFSize.A4;

            if (radioButton200.Checked) { scaleFactor = 2.0; mapScale = "1:200 000"; };
            if (radioButton250.Checked) { scaleFactor = 2.5; mapScale = "1:250 000"; };
            if (radioButtonOtherScale.Checked)
            {
                scaleFactor = Double.Parse(maskedTextBoxOtherScale.Text.Replace(" ", "")) / 100000.0;
                mapScale = string.Format("1:{0}", string.Format("{0:### ### ###}", maskedTextBoxOtherScale.Text).TrimStart(' '));
                size = PDFSize.A3;
            };

            string defaultText = string.Format("Map scale = {0}" + Environment.NewLine + "Parcour length = 00.00 NM" + Environment.NewLine + "Time = 00:00 Min (@80 kt)", mapScale);
            String freitext = string.Empty;

            if (listBox1.SelectedItems.Count == 1 && PictureBox1.PrintOutImage != null)
            {
                ListItem li = listBox1.SelectedItems[0] as ListItem;
                String dirPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\AirNavigationRace\";
                DirectoryInfo di = Directory.CreateDirectory(dirPath);
                if (!di.Exists)
                {
                    di.Create();
                }
                if (mustPromptForAdditionalText)
                {
                    TextOverlayDialog dialog = new TextOverlayDialog(defaultText);
                    dialog.ShowDialog();
                    freitext = dialog.text;
                }
                else
                {
                    freitext = Environment.NewLine + Environment.NewLine + string.Format("Map scale = {0}", mapScale);
                }

                if (radioButtonOtherScale.Checked)
                {
                    PDFCreator.CreateParcourPDF(size, chkShowCalcTable.Checked, scaleFactor, PictureBox1, Client, li.getParcour().Name, dirPath +
                        @"\Parcour_" + li.getParcour().Id + "_" + li.getParcour().Name + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf", freitext);
                }
                else
                {
                    PDFCreator.CreateParcourPDF(size, chkShowCalcTable.Checked, scaleFactor, PictureBox1, Client, li.getParcour().Name, dirPath +
                        @"\Parcour_" + li.getParcour().Id + "_" + li.getParcour().Name + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf", freitext);
                }
            }
        }

        private void listBox1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string newName = e.Label;
            if (newName != null && newName != "")
            {
                ListItem item = listBox1.Items[e.Item] as ListItem;
                ParcourSet p = item.getParcour();
                p.Name = newName;
                Client.DBContext.SaveChanges();
            }
        }


        private void numericUpDownAlpha_ValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 1)
            {
                ListItem li = listBox1.SelectedItems[0] as ListItem;
                ParcourSet p = li.getParcour();
                p.Alpha = (int)numericUpDownAlpha.Value;
                Client.DBContext.SaveChanges();
                PictureBox1.SetParcour(p);
                PictureBox1.Invalidate();
            }
        }

        private void numericUpDownPen_ValueChanged(object sender, EventArgs e)
        {
            if (activeParcour != null)
            {
                ParcourSet p = activeParcour;
                PictureBox1.PenWidthGates = (float)numericUpDownPenGates.Value;
                p.PenWidthGates = numericUpDownPenGates.Value;
                Client.DBContext.SaveChanges();
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
            btnColorPROH.BackColor = cd.Color;
            ParcourSet p = activeParcour;
            p.ColorPROH = cd.Color;
            Client.DBContext.SaveChanges();
            PictureBox1.ColorPROH = cd.Color;
            PictureBox1.SetParcour(p);
            PictureBox1.Invalidate();
        }

        private void btnColorPen_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = false;
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            btnColorGates.BackColor = cd.Color;
            ParcourSet p = activeParcour;
            p.ColorGates = cd.Color;
            Client.DBContext.SaveChanges();
            PictureBox1.ColorGates = cd.Color;
            PictureBox1.SetParcour(p);
            PictureBox1.Invalidate();
        }

        private void checkBoxCircle_CheckedChanged(object sender, EventArgs e)
        {
            ParcourSet p = activeParcour;
            PictureBox1.HasCircleOnGates = checkBoxCircle.Checked;
            p.HasCircleOnGates = checkBoxCircle.Checked;
            Client.DBContext.SaveChanges();
            PictureBox1.SetParcour(p);
            PictureBox1.Invalidate();
        }

        private void radioButtonScales_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton btn = sender as RadioButton;
            if (btn != null && btn.Checked)
            {
                switch (btn.Name)
                {
                    case "radioButtonOtherScale":
                        maskedTextBoxOtherScale.Visible = radioButtonOtherScale.Checked;
                        chkShowCalcTable.Checked = false;
                        chkShowCalcTable.Visible = false;
                        break;

                    default:
                        maskedTextBoxOtherScale.Visible = radioButtonOtherScale.Checked;
                        chkShowCalcTable.Checked = true;
                        chkShowCalcTable.Visible = !radioButtonOtherScale.Checked;
                        break;
                }
            }
        }

        private void btnChannelColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = false;
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            btnChannelColor.BackColor = cd.Color;
            ParcourSet p = activeParcour;
            //PictureBox1.ColorPROH = cd.Color;
            p.ColorChannel = cd.Color;
            PictureBox1.SetParcour(p);
            PictureBox1.Invalidate();
        }

        private void btnChannelFillColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = false;
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            btnChannelFillColor.BackColor = cd.Color;
            ParcourSet p = activeParcour;
            //PictureBox1.ColorPROH = cd.Color;
            p.ColorChannelFill = cd.Color;
            PictureBox1.SetParcour(p);
            PictureBox1.Invalidate();
        }

        private void btnIntersectColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = false;
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            btnIntersectColor.BackColor = cd.Color;
            ParcourSet p = activeParcour;
            //PictureBox1.ColorPROH = cd.Color;
            p.ColorIntersection = cd.Color;
            PictureBox1.SetParcour(p);
            PictureBox1.Invalidate();
        }

        private void radioButtonPenaltyCalcTypePROH_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonPenaltyCalcTypeChannel.Checked = !radioButtonPenaltyCalcTypePROH.Checked;

            layerBox.Visible = radioButtonPenaltyCalcTypePROH.Checked;
            groupBoxChannel.Visible = radioButtonPenaltyCalcTypeChannel.Checked;

            ParcourSet p = activeParcour;
            if (activeParcour != null)
            {
                p.PenaltyCalcType = radioButtonPenaltyCalcTypePROH.Checked ? 0 : 1;
                Client.DBContext.SaveChanges();
                PictureBox1.SetParcour(p);
                PictureBox1.Invalidate();
               // listBox1_SelectedIndexChanged(null, null);
            }
        }

        private void radioButtonPenaltyCalcTypeChannel_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonPenaltyCalcTypePROH.Checked = !radioButtonPenaltyCalcTypeChannel.Checked;

            layerBox.Visible = radioButtonPenaltyCalcTypePROH.Checked;
            groupBoxChannel.Visible = radioButtonPenaltyCalcTypeChannel.Checked;
            ParcourSet p = activeParcour;
            if (activeParcour != null)
            {
                p.PenaltyCalcType = radioButtonPenaltyCalcTypePROH.Checked ? 0 : 1;
                Client.DBContext.SaveChanges();
                PictureBox1.SetParcour(p);
                PictureBox1.Invalidate();
               // listBox1_SelectedIndexChanged(null, null);

            }
        }

        private void numericUpDownChannelPen_ValueChanged(object sender, EventArgs e)
        {
            if (activeParcour != null)
            {
                ParcourSet p = activeParcour;
                // PictureBox1.PenWidthGates = (float)numericUpDownPenGates.Value;
                p.PenWidthChannel = numericUpDownChannelPen.Value;
                Client.DBContext.SaveChanges();
                PictureBox1.SetParcour(p);
                PictureBox1.Invalidate();
            }
        }

        private void numericUpDownChannelAlpha_ValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 1)
            {
                ListItem li = listBox1.SelectedItems[0] as ListItem;
                ParcourSet p = li.getParcour();
                //p.ChannelAlpha = (int)numericUpDownChannelAlpha.Value;
                //Client.DBContext.SaveChanges();
                //PictureBox1.SetParcour(p);
                //PictureBox1.Invalidate();
            }
        }


        private void numericUpDownIntersectionCircleRadius_ValueChanged(object sender, EventArgs e)
        {
            if (activeParcour != null)
            {
                ParcourSet p = activeParcour;
                //PictureBox1. = (float)numericUpDownIntersect.Value;
                p.IntersectionCircleRadius = numericUpDownIntersectionCircleRadius.Value;
                Client.DBContext.SaveChanges();
                PictureBox1.SetParcour(p);
                PictureBox1.Invalidate();
            }
        }

        private void chkIntersectionPointsShow_CheckedChanged(object sender, EventArgs e)
        {
            ParcourSet p = activeParcour;
            if (activeParcour != null)
            {
                p.HasIntersectionCircles = chkIntersectionPointsShow.Checked;
                Client.DBContext.SaveChanges();
                PictureBox1.SetParcour(p);
                PictureBox1.Invalidate();
                // listBox1_SelectedIndexChanged(null, null);

            }
        }

        private void numericUpDownPenWidthIntersect_ValueChanged(object sender, EventArgs e)
        {
            if (activeParcour != null)
            {
                ParcourSet p = activeParcour;
                //PictureBox1. = (float)numericUpDownIntersect.Value;
                p.PenWidthIntersection = numericUpDownPenWidthIntersect.Value;
                Client.DBContext.SaveChanges();
                PictureBox1.SetParcour(p);
                PictureBox1.Invalidate();
            }
        }
    }
}
