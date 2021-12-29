using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AirNavigationRaceLive.Dialogs;
using AirNavigationRaceLive.Comps.Helper;
using System.IO;
using AirNavigationRaceLive.Model;
using System.ComponentModel.DataAnnotations.Schema;
using AirNavigationRaceLive.ModelExtensions;
using System.Globalization;
using AirNavigationRaceLive.Comps.Airsports;

namespace AirNavigationRaceLive.Comps
{
    public partial class QualificationRoundControl : UserControl
    {
        private Client.DataAccess Client;
        private FlightSet deleteFlt;
        private int qrIdx = -1; //index of selected QR
        const string C_TimeFormat = "HH:mm:ss";

        public QualificationRoundControl(Client.DataAccess iClient)
        {
            Client = iClient;
            InitializeComponent();
            //           lblTitel.Text = lblTitel.Text + iClient.SelectedCompetition.Name;
            numericUpDownRoutes.Value = numericUpDownRoutes.Maximum;
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnImportTKOFline, "Import Take-Off lines from a *.kml file");
        }

        private void LoadQualificationRounds()
        {
            List<QualificationRoundSet> comps = Client.SelectedCompetition.QualificationRoundSet.ToList();
            listViewQualificationRound.Items.Clear();
            foreach (QualificationRoundSet c in comps)
            {
                ListViewItem lvi = new ListViewItem(new String[] { c.Id.ToString(), c.Name });
                lvi.Tag = c;
                listViewQualificationRound.Items.Add(lvi);
            }
            //if (listViewQualificationRound.Items.Count > 0)
            //{
            //    listViewQualificationRound.Items[0].Selected = true;
            //}
            UpdateEnablement();
            lblQRound.Text = string.Format("{0} - Qualification Rounds", Client.SelectedCompetition.Name);
            groupBoxStartList.Visible = false;
        }

        private void LoadTeams()
        {
            List<TeamSet> teams = Client.SelectedCompetition.TeamSet.ToList();
            UpdateEnablement();
        }
        private void LoadParcours()
        {
            List<ParcourSet> parcour = Client.SelectedCompetition.ParcourSet.ToList();
            comboBoxParcour.Items.Clear();
            foreach (ParcourSet c in parcour)
            {
                ComboParcourExtension cp = new ComboParcourExtension(c);
                comboBoxParcour.Items.Add(cp);
            }
            if (comboBoxParcour.Items.Count > 0)
            {
                comboBoxParcour.SelectedIndex = 0;
            }
            UpdateEnablement();
        }

        private void UpdateEnablement()
        {
            // TODO: review and simplify this part
            bool isQRSelected = listViewQualificationRound.SelectedItems.Count == 1 && qrIdx >= 0;
            bool Ediable = textName.Text != "";
            bool isParcourSelected = comboBoxParcour.SelectedItem != null;
            bool hasTakeOffLine = false;

            hasTakeOffLine = !hasValidationErrors();

            groupBoxTKOFLine.Enabled = Ediable;
            btnImportTKOFline.Enabled = Ediable;
            btnSwitchLeftRight.Enabled = Ediable;

            btnSaveQualificationRound.Enabled = Ediable && isParcourSelected && hasTakeOffLine;
            groupBoxGeneral.Enabled = Ediable && isParcourSelected && hasTakeOffLine;
            groupBoxStartList.Enabled = Ediable && isParcourSelected && hasTakeOffLine && qrIdx >= 0;

            btnExportToPDF.Enabled = btnSaveQualificationRound.Enabled && dataGridView1.RowCount > 1;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            groupBoxStartList.Visible = isQRSelected;
            btnDeleteQualificationRound.Enabled = isQRSelected;
            btnAutoFillStartList.Enabled = isQRSelected;
            btnRecalcStartList.Enabled = isQRSelected;

            btnRefreshCompetitions.Visible = false;  // not in use anymore!
        }

        private void Reset()
        {
            textName.Text = "";
            comboBoxParcour.SelectedItem = null;
            comboBoxParcour.Text = "";
            dataGridView1.Rows.Clear();
            UpdateEnablement();
        }

        private void QualificationRounds_Load(object sender, EventArgs e)
        {
            Reset();
            LoadParcours();
            LoadQualificationRounds();
            LoadTeams();
            SetTimeParameters();
            errorProviderQualification.Clear();
        }

        private void SetTimeParameters()
        {
            // CHECK
            DateTime time = DateTime.UtcNow;  // show UTC in Qualification round start list
            // DateTime time = DateTime.Now; // incorrect, LOCAL time (v2.1.0 and probably before)
            timeParcourDuration.Value = new DateTime(time.Year, time.Month, time.Day, time.Hour, 12, 0);
            timeTakeOffToStartgateDuration.Value = new DateTime(time.Year, time.Month, time.Day, time.Hour, 12, 0);
            timeTakeOffInterval.Value = new DateTime(time.Year, time.Month, time.Day, time.Hour, 1, 0);
            timeStartBlockInterval.Value = new DateTime(time.Year, time.Month, time.Day, time.Hour, 20, 0);
        }
        private void SetTimeParameters(QualificationRoundSet c)
        {
            List<FlightSet> flights = new List<FlightSet>(c.FlightSet);
            if (flights.Count > 0)
            {
                FlightSet first = flights.OrderBy(x => x.TimeTakeOff).First();
                // use absolute difference - for inverted routes
                timeParcourDuration.Value = new DateTime(Math.Abs(first.TimeEndLine - first.TimeStartLine)).AddYears(2000);
                timeTakeOffToStartgateDuration.Value = new DateTime(Math.Abs(first.TimeStartLine - first.TimeTakeOff)).AddYears(2000);
                numericUpDownRoutes.Value = flights.Select(x => x.Route).Distinct().Count();

                if (flights.Count > 1)
                {
                    FlightSet secon = flights.OrderBy(x => x.TimeTakeOff).Skip(1).First();
                    timeTakeOffInterval.Value = new DateTime(secon.TimeTakeOff - first.TimeTakeOff).AddYears(2000);
                    // timeTakeOffBlocksIntervall.Value will not be set
                }
            }
        }
        private void btnRefreshCompetitions_Click(object sender, EventArgs e)
        {
            Reset();
            LoadQualificationRounds();
            LoadTeams();
            LoadParcours();
            errorProviderQualification.Clear();
        }
        private void btnDeleteQualificationRound_Click(object sender, EventArgs e)
        {
            if (listViewQualificationRound.SelectedItems.Count == 1)
            {
                QualificationRoundSet c = listViewQualificationRound.SelectedItems[0].Tag as QualificationRoundSet;
                if (MessageBox.Show(string.Format("Delete the selected Qualification Round:\n {0} ?", c.Name), "Delete Qualification", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Client.DBContext.QualificationRoundSet.Remove(c);
                    Client.DBContext.SaveChanges();
                    LoadQualificationRounds();
                    errorProviderQualification.Clear();

                    if (listViewQualificationRound.Items.Count > 0)
                    {
                        listViewQualificationRound.Items[0].Selected = true;
                    }
                    else
                    {
                        Reset();
                    }
                }
            }
        }
        private void btnNewQualificationRound_Click(object sender, EventArgs e)
        {
            Reset();
            LoadParcours();
            textName.Text = String.Empty;
            QualificationRoundSet qualificationRound = new QualificationRoundSet();
            qualificationRound.CompetitionSet = Client.SelectedCompetition;
            textName.Tag = qualificationRound;
            qrIdx = -1;
            comboBoxParcour.SelectedIndex = -1;
            textName.SelectAll();
            textName.Focus();
        }
        private void btnSaveQualificationRound_Click(object sender, EventArgs e)
        {
            UpdateEnablement();

            QualificationRoundSet c = textName.Tag as QualificationRoundSet;
            if (c == null)
            {
                c = new QualificationRoundSet();
                c.CompetitionSet = Client.SelectedCompetition;
            }
            c.ParcourSet = (comboBoxParcour.SelectedItem as ComboParcourExtension).p;
            c.Name = textName.Text;

            Vector start = new Vector(double.Parse(takeOffLeftLongitude.Text), double.Parse(takeOffLeftLatitude.Text), 0);
            Vector end = new Vector(double.Parse(takeOffRightLongitude.Text), double.Parse(takeOffRightLatitude.Text), 0);
            Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
            Line line = new Line();
            if (c.TakeOffLine != null)
            {
                // update the existing TKOF line
                line = c.TakeOffLine;
            }
            line.Type = (int)LineType.TKOF;
            line.ParcourLine_Line_Id = c.ParcourSet.Id;
            line.A = Factory.newGPSPoint(start.X, start.Y, start.Z);
            line.B = Factory.newGPSPoint(end.X, end.Y, end.Z);
            line.O = Factory.newGPSPoint(o.X, o.Y, o.Z);
            c.TakeOffLine = line;

            //List<Flight> toDelete = new List<Flight>();
            //toDelete.AddRange(c.Flight);
            //Client.DBContext.FlightSet.RemoveRange(toDelete);

            if (c.Id == 0)
            {
                Client.DBContext.QualificationRoundSet.Add(c);
                qrIdx = listViewQualificationRound.Items.Count;
            }
            Client.DBContext.SaveChanges();
            Reset();
            LoadQualificationRounds();
            UpdateEnablement();
            errorProviderQualification.Clear();
            listViewQualificationRound.Items[qrIdx].Selected = true;
        }

        //private DateTime mergeDateTime(DateTime time, DateTime date)
        //{
        //    return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0, 0);
        //}

        private void comboBoxParcour_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEnablement();
        }

        private void takeOffLeftLongitude_TextChanged(object sender, EventArgs e)
        {
            UpdateEnablement();
        }

        private void takeOffLeftLatitude_TextChanged(object sender, EventArgs e)
        {
            UpdateEnablement();
        }

        private void takeOffRightLongitude_TextChanged(object sender, EventArgs e)
        {
            UpdateEnablement();
        }

        private void takeOffRightLatitude_TextChanged(object sender, EventArgs e)
        {
            UpdateEnablement();
        }

        private void listViewQualificationRound_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewQualificationRound.SelectedItems.Count == 1)
            {
                ListViewItem lvi = listViewQualificationRound.SelectedItems[0];
                QualificationRoundSet c = lvi.Tag as QualificationRoundSet;
                if (c == null) return;
                //qR = c;
                qrIdx = lvi.Index;
                textName.Tag = c;
                textName.Text = c.Name;
                ComboParcourExtension cp = null;
                foreach (Object o in comboBoxParcour.Items)
                {
                    if ((o as ComboParcourExtension).p == c.ParcourSet)
                    {
                        cp = o as ComboParcourExtension;
                        break;
                    }
                }
                comboBoxParcour.SelectedItem = cp;
                takeOffLeftLongitude.Text = c.TakeOffLine.A.longitude.ToString();
                takeOffLeftLatitude.Text = c.TakeOffLine.A.latitude.ToString();
                takeOffRightLatitude.Text = c.TakeOffLine.B.latitude.ToString();
                takeOffRightLongitude.Text = c.TakeOffLine.B.longitude.ToString();
                updateList(c);
                UpdateEnablement();
                SetTimeParameters(c);
            }

        }

        private void updateList(QualificationRoundSet c)
        {
            dataGridView1.Rows.Clear();
            List<FlightSet> flights = new List<FlightSet>(c.FlightSet);
            //foreach (Flight fl in flights.OrderBy(x => x.TimeTakeOff))
            foreach (FlightSet fl in flights.OrderBy(x => x.StartID))
            {
                DataGridViewRow dgvr = new DataGridViewRow();
                dgvr.CreateCells(dataGridView1);
                dgvr.SetValues(
                    fl.StartID.ToString(),
                    fl.TeamSet.CNumber,
                    fl.TeamSet.AC,
                    getTeamDsc(fl.TeamSet),
                    new DateTime(fl.TimeTakeOff).ToString(C_TimeFormat, DateTimeFormatInfo.InvariantInfo),
                    new DateTime(fl.TimeStartLine).ToString(C_TimeFormat, DateTimeFormatInfo.InvariantInfo),
                    new DateTime(fl.TimeEndLine).ToString(C_TimeFormat, DateTimeFormatInfo.InvariantInfo),
                    getRouteText(fl.Route),
                    new DateTime(fl.TimeTakeOff).ToShortDateString());

                fl.QualificationRoundSet = listViewQualificationRound.SelectedItems[0].Tag as QualificationRoundSet;
                dgvr.Tag = fl;
                dataGridView1.Rows.Add(dgvr);
            }
            //  dataGridView1.Sort(dataGridView1.Columns["Crew"],ListSortDirection.Ascending);
        }

        private string getTeamDsc(TeamSet team)
        {
            SubscriberSet pilot = team.Pilot;
            StringBuilder sb = new StringBuilder();
            sb.Append(pilot.LastName).Append(" ").Append(pilot.FirstName);
            if (team.Navigator != null)
            {
                SubscriberSet navi = team.Navigator;
                sb.Append(" - ").Append(navi.LastName).Append(" ").Append(navi.FirstName);
            }
            return sb.ToString();
        }
        private string getRouteText(int id)
        {
            return ((Route)id).ToString();
        }

        private void comboBoxCrew_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEnablement();
        }

        private void textName_TextChanged(object sender, EventArgs e)
        {
            UpdateEnablement();
        }

        private void comboBoxRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEnablement();
        }

        private void btnExportToPDF_Click(object sender, EventArgs e)
        {
            QualificationRoundSet c = textName.Tag as QualificationRoundSet;
            if (c != null)
            {
                String dirPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\AirNavigationRace\";
                DirectoryInfo di = Directory.CreateDirectory(dirPath);
                if (!di.Exists)
                {
                    di.Create();
                }
                PDFCreator.CreateStartListPDF(c, Client, dirPath +
                    @"\StartList_" + c.Id + "_" + c.Name + "_" + DateTime.UtcNow.ToString("yyyyMMddhhmmss") + ".pdf");
            }
        }

        ///<summary>
        ///Performs validation on several controls and uses an ErrorProvider control to diplay an error message.
        ///Returns true, if there are any error messages.
        ///</summary>
        private bool hasValidationErrors()
        {
            bool hasErrors = false;
            errorProviderQualification.Clear();
            var controls = new[] { takeOffLeftLongitude, takeOffLeftLatitude, takeOffRightLongitude, takeOffRightLatitude };
            foreach (var control in controls)
            {
                double dbl = 0.0;
                if (!double.TryParse(control.Text, out dbl) || double.TryParse(control.Text, out dbl) && Math.Abs(dbl) > 180)
                {
                    errorProviderQualification.SetError(control, "Value must be numeric and between -180 and +180");
                    hasErrors = true;
                }
                if (comboBoxParcour.SelectedIndex < 0)
                {
                    errorProviderQualification.SetError(comboBoxParcour, "Select a parcours");
                }
                if (string.IsNullOrWhiteSpace(textName.Text))
                {
                    errorProviderQualification.SetError(textName, "Fill a name");
                }
            }
            return hasErrors;
        }

        //[NotMapped]
        //class ComboParcour
        //{
        //    public ParcourSet p;
        //    public ComboParcour(ParcourSet p)
        //    {
        //        this.p = p;
        //    }

        //    public override string ToString()
        //    {
        //        return p.Name;
        //    }
        //}

        private void btnSwitchLeftRight_Click(object sender, EventArgs e)
        {
            var values = new[] { takeOffLeftLatitude.Text, takeOffLeftLongitude.Text, takeOffRightLatitude.Text, takeOffRightLongitude.Text };
            takeOffLeftLatitude.Text = values[2];
            takeOffLeftLongitude.Text = values[3];
            takeOffRightLatitude.Text = values[0];
            takeOffRightLongitude.Text = values[1];
        }

        private void btnImportTKOFline_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string FileFilter = "GoogleEarth KML (*.kml)|*.kml|All Files (*.*)|*.*";
            ofd.Title = "TKOF line Import from KML file";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.Filter = FileFilter;
            ofd.FileOk += new CancelEventHandler(ofd_FileOkImportTKOFline);
            ofd.ShowDialog();
            // handle multiple lines: show modal form with selection 
            // do this now after the OpenfileDialog is closed (avoid two modal windows at the same time)
            if (!(ofd.Tag == null) && !String.IsNullOrEmpty(ofd.Tag.ToString()))
            {
                // use the filename already existing
                dialogImportTKOFline(ofd.FileName);
                // reset the Tag
                ofd.Tag = null;
            }
        }

        void ofd_FileOkImportTKOFline(object sender, CancelEventArgs e)
        {
            OpenFileDialog ofd = sender as OpenFileDialog;
            try
            {
                List<string> lstTKOFLineNames;
                List<Line> lst = Importer.importTKOFLineFromKML(ofd.FileName, out lstTKOFLineNames);

                if (lstTKOFLineNames.Count >= 1)
                {
                    int idx;
                    if (lstTKOFLineNames.Count == 1)
                    {
                        idx = 0;

                        takeOffLeftLongitude.Text = lst[idx].A.longitude.ToString();
                        takeOffLeftLatitude.Text = lst[idx].A.latitude.ToString();
                        takeOffRightLongitude.Text = lst[idx].B.longitude.ToString();
                        takeOffRightLatitude.Text = lst[idx].B.latitude.ToString();
                    }
                    else
                    {
                        // flagging that we have several Take-off lines, call separate function
                        ofd.Tag = ofd.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error while Parsing File");
            }
        }

        /// <summary>
        /// Selection of a Take-Off line for import. Shows a dialog window where the user selects a Take-off line by name
        /// </summary>
        /// <param name="fname"></param>
        void dialogImportTKOFline(string fname)
        {
            List<string> lstTKOFLineNames;
            List<Line> lst = Importer.importTKOFLineFromKML(fname, out lstTKOFLineNames);

            if (lstTKOFLineNames.Count > 1)
            {
                int idx;
                ImportTKOFLines frmImportTKOF = new ImportTKOFLines(lstTKOFLineNames);
                // Show testDialog as a modal dialog and determine if DialogResult = OK.
                // NOTE: must set the button property accordingly
                if (frmImportTKOF.ShowDialog(this) == DialogResult.OK)
                {
                    idx = frmImportTKOF.selectedIdx;
                    takeOffLeftLongitude.Text = lst[idx].A.longitude.ToString();
                    takeOffLeftLatitude.Text = lst[idx].A.latitude.ToString();
                    takeOffRightLongitude.Text = lst[idx].B.longitude.ToString();
                    takeOffRightLatitude.Text = lst[idx].B.latitude.ToString();
                }
                frmImportTKOF.Dispose();
            }
        }

        private void startListDialog()
        {
            List<string> lst = new List<string>();
            List<TeamSet> lstTeams = Client.SelectedCompetition.TeamSet.ToList();
            List<ComboTeamExtension> lstCboTeam = new List<ComboTeamExtension>();
            foreach (TeamSet t in lstTeams)
            {
                lstCboTeam.Add(new ComboTeamExtension(t, getTeamDsc(t)));
            }

            long intervTKOF = timeTakeOffInterval.Value.Minute * 60 + timeTakeOffInterval.Value.Second;  // timeTakeOffIntervall.Value.Ticks;
            long intervStartL = timeStartBlockInterval.Value.Minute * 60 + timeStartBlockInterval.Value.Second;
            long tkofToStart = timeTakeOffToStartgateDuration.Value.Minute * 60 + timeTakeOffToStartgateDuration.Value.Second;
            long parcourLength = timeParcourDuration.Value.Minute * 60 + timeParcourDuration.Value.Second;

            // as a default, use the actual date (in UTC)
            long dateQRdate0 = DateTime.UtcNow.Ticks;
            long timeTKOF0 = dateQRdate0;
            long timeStart0 = new DateTime(timeTKOF0).AddSeconds(intervStartL).Ticks;
            long timeEnd0 = new DateTime(timeStart0).AddSeconds(parcourLength).Ticks;

            if (dataGridView1.RowCount > 1 && dataGridView1.SelectedRows[0].IsNewRow)
            {
                // read Tag from previous row and get date and previous starttime
                // add  takeOffInterval to previous starttime  
                FlightSet flt0 = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index - 1].Tag as FlightSet;

                dateQRdate0 = flt0.TimeTakeOff;
                timeTKOF0 = new DateTime(flt0.TimeTakeOff).AddSeconds(intervTKOF).Ticks;
                timeStart0 = new DateTime(timeTKOF0).AddSeconds(intervStartL).Ticks;
                timeEnd0 = new DateTime(timeStart0).AddSeconds(parcourLength).Ticks;
            }

            FlightSet flt = dataGridView1.SelectedRows[0].Tag as FlightSet;
            int idx = dataGridView1.SelectedRows[0].Index;
            using (StartListDialog frmStartListDialog = new StartListDialog(lstTeams, flt, calculateMaxStartId(), dateQRdate0, timeTKOF0, timeStart0, timeEnd0, (int)numericUpDownRoutes.Value))
            {
                DialogResult rRes = frmStartListDialog.ShowDialog();
                if (rRes == DialogResult.Cancel)
                {
                    //do nothing
                }

                if (rRes == DialogResult.OK)
                {
                    // fill data
                    FlightSet fl = frmStartListDialog.SelectedFlight;
                    if (fl.Id == 0)
                    {
                        Client.DBContext.FlightSet.Add(fl);

                        DataGridViewRow dgvr = new DataGridViewRow();
                        dgvr.CreateCells(dataGridView1);
                        dgvr.SetValues(
                            fl.StartID.ToString(),
                            fl.TeamSet.CNumber,
                            fl.TeamSet.AC,
                            getTeamDsc(fl.TeamSet),
                        new DateTime(fl.TimeTakeOff).ToString(C_TimeFormat, DateTimeFormatInfo.InvariantInfo),
                        new DateTime(fl.TimeStartLine).ToString(C_TimeFormat, DateTimeFormatInfo.InvariantInfo),
                        new DateTime(fl.TimeEndLine).ToString(C_TimeFormat, DateTimeFormatInfo.InvariantInfo),
                        getRouteText(fl.Route),
                        new DateTime(fl.TimeTakeOff).ToShortDateString());

                        fl.QualificationRoundSet = listViewQualificationRound.SelectedItems[0].Tag as QualificationRoundSet;
                        dgvr.Tag = fl;
                        dataGridView1.Rows.Add(dgvr);
                    }
                    Client.DBContext.SaveChanges();
                    updateList(fl.QualificationRoundSet);
                    dataGridView1.Rows[0].Selected = false;
                    dataGridView1.Rows[idx].Selected = true;
                    return;
                }
            }
        }

        private int calculateMaxStartId()
        {
            int _maxStartId = 0;
            QualificationRoundSet c = textName.Tag as QualificationRoundSet;
            if (c != null && c.FlightSet.Count > 0)
            {
                foreach (FlightSet ct in c.FlightSet)
                {
                    _maxStartId = Math.Max(_maxStartId, ct.StartID);
                }
            }
            _maxStartId++;
            return _maxStartId;
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string str = "StartId " + e.Row.Cells[0].Value.ToString() + ", Crew " + e.Row.Cells[1].Value.ToString() + ": " + e.Row.Cells[3].Value.ToString();
            if (MessageBox.Show(string.Format("Delete the selected Startlist Entry:\n\n {0} ?", str), "Delete Startlist Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            deleteFlt = e.Row.Tag as FlightSet;
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (!(deleteFlt == null))
            {
                // Flight ct = dataGridView1.SelectedRows[0].Tag as Flight;
                Client.DBContext.FlightSet.Remove(deleteFlt);
                Client.DBContext.SaveChanges();
                listViewQualificationRound_SelectedIndexChanged(null, null);
                updateList(listViewQualificationRound.SelectedItems[0].Tag as QualificationRoundSet);
                deleteFlt = null;
            }
            UpdateEnablement();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            startListDialog();
        }

        private void btnRecalcStartList_Click(object sender, EventArgs e)
        {
            recalculateStartList();
        }
        private void recalculateStartList()
        {
            // recalculate the startList
            // use the first entry as base
            //Flight f = new Flight();
            long tkof0 = 0;
            QualificationRoundSet qRnd = null;
            long intervTKOF = timeTakeOffInterval.Value.Minute * 60 + timeTakeOffInterval.Value.Second;  // timeTakeOffIntervall.Value.Ticks;
            long intervStartL = timeStartBlockInterval.Value.Minute * 60 + timeStartBlockInterval.Value.Second;
            long tkofToStart = timeTakeOffToStartgateDuration.Value.Minute * 60 + timeTakeOffToStartgateDuration.Value.Second;
            long parcourLength = timeParcourDuration.Value.Minute * 60 + timeParcourDuration.Value.Second;
            int NrOfRoutes = (int)numericUpDownRoutes.Value;
            FlightSet f = null;
            int idx = 0;
            if (dataGridView1.Rows.Count <= 1)
            {
                // we only have a new line row but no data
                return;
            }
            if (dataGridView1.Rows.Count > 1)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // index of selected row
                    idx = dataGridView1.SelectedRows[0].Index;
                }

                f = dataGridView1.Rows[idx].Tag as FlightSet;
                tkof0 = f.TimeTakeOff;
                qRnd = f.QualificationRoundSet;
            }

            // re-calculate from selected index upwards
            for (int i = idx; i < dataGridView1.Rows.Count; i++)
            {
                f = dataGridView1.Rows[i].Tag as FlightSet;
                if (f == null)
                {
                    continue;
                }
                if (i == idx) // the reference
                {
                    tkof0 = f.TimeTakeOff;
                }
                if (i > idx)  // re-calculate
                {
                    f.TimeTakeOff = new DateTime(tkof0).AddSeconds(((i - idx) % NrOfRoutes) * intervTKOF).AddSeconds(((i - idx) / NrOfRoutes) * intervStartL).Ticks;
                }

                // calculate times based on 0-value
                f.TimeStartLine = new DateTime(tkof0).AddSeconds(tkofToStart).AddSeconds(((i - idx) / NrOfRoutes) * intervStartL).Ticks;
                f.TimeEndLine = new DateTime(tkof0).AddSeconds(tkofToStart + parcourLength).AddSeconds(((i - idx) / NrOfRoutes) * intervStartL).Ticks;
                f.Route = (i - idx) % NrOfRoutes + 1;
            }

            Client.DBContext.SaveChanges();
            updateList(qRnd);

            dataGridView1.Rows[0].Selected = false;
            dataGridView1.Rows[idx].Selected = true;
        }

        private void btnAutoFillStartList_Click(object sender, EventArgs e)
        {
            autoFillStartList();
        }

        private void autoFillStartList()
        {
            // recalculate the startList
            // use the first entry as base
            //Flight f = new Flight();
            long tkof0 = 0;
            QualificationRoundSet qRnd = null;
            long intervTKOF = timeTakeOffInterval.Value.Minute * 60 + timeTakeOffInterval.Value.Second;  // timeTakeOffIntervall.Value.Ticks;
            long intervStartL = timeStartBlockInterval.Value.Minute * 60 + timeStartBlockInterval.Value.Second;
            long tkofToStart = timeTakeOffToStartgateDuration.Value.Minute * 60 + timeTakeOffToStartgateDuration.Value.Second;
            long parcourLenght = timeParcourDuration.Value.Minute * 60 + timeParcourDuration.Value.Second;

            int NrOfRoutes = (int)numericUpDownRoutes.Value;
            FlightSet f = null;
            if (dataGridView1.Rows.Count > 1)
            {
                f = dataGridView1.Rows[0].Tag as FlightSet;
                tkof0 = f.TimeTakeOff;
                qRnd = f.QualificationRoundSet;
            }
            else
            {
                ListViewItem lvi = listViewQualificationRound.SelectedItems[0];
                qRnd = lvi.Tag as QualificationRoundSet;
                tkof0 = DateTime.UtcNow.Ticks; // actual datetime (in UTC)
            }

            List<TeamSet> lstTeam = Client.SelectedCompetition.TeamSet.ToList();
            for (int i = 0; i < lstTeam.Count; i++)
            {
                FlightSet flt = new FlightSet();

                flt.TimeTakeOff = new DateTime(tkof0).AddSeconds(i * intervTKOF).AddSeconds((i / NrOfRoutes) * (intervStartL - intervTKOF)).Ticks;
                flt.TimeStartLine = new DateTime(tkof0).AddSeconds(tkofToStart).AddSeconds(i * intervTKOF).AddSeconds((i / NrOfRoutes) * (intervStartL + intervTKOF)).Ticks;
                flt.TimeEndLine = new DateTime(tkof0).AddSeconds(tkofToStart + parcourLenght).AddSeconds(i * intervTKOF).AddSeconds((i / NrOfRoutes) * (intervStartL + intervTKOF)).Ticks;
                flt.Route = i % NrOfRoutes + 1;
                flt.TeamSet = lstTeam[i];
                flt.StartID = i + 1;
                flt.QualificationRoundSet = qRnd;
                Client.DBContext.FlightSet.Add(flt);
            }

            Client.DBContext.SaveChanges();
            updateList(qRnd);
        }

        private void txtContestId_TextChanged(object sender, EventArgs e)
        {
            long dummyLong = 0;
            btnToAirsports.Enabled =
                !(string.IsNullOrEmpty(txtContestId.Text) || string.IsNullOrEmpty(txtTaskId.Text)
                && long.TryParse(txtContestId.Text, out dummyLong)
                && long.TryParse(txtTaskId.Text, out dummyLong)
                );
        }

        private void txtTaskId_TextChanged(object sender, EventArgs e)
        {
            long dummyLong = 0;
            btnToAirsports.Enabled =
                !(string.IsNullOrEmpty(txtContestId.Text) || string.IsNullOrEmpty(txtTaskId.Text)
                && long.TryParse(txtContestId.Text, out dummyLong)
                && long.TryParse(txtTaskId.Text, out dummyLong)
                );
        }

        private void btnToAirsports_Click(object sender, EventArgs e)
        {
            QualificationRoundSet qr = listViewQualificationRound.SelectedItems[0].Tag as QualificationRoundSet;
            int qualRndId = qr.Id;  // ANR scoring software Start list;
            string contestId = txtContestId.Text; // Airsports contest id
            string navTaskId = txtTaskId.Text; //  Airsports Nav Task Id
            String errorString = string.Empty;
            List<string> lstErrorString = new List<string>();

            RESTClient _client = new RESTClient();

            // Airsports data:
            // all teams on the contest
            List<ContestTeam> existingTeamsOnContest = _client.GetASTeamsOnContest(contestId, out errorString);
            // contestants that are already scheduled on the navigation task
            List<ContestantsTeam> existingContestantsTeams = _client.GetASContestantsTeamsOnNavTask(contestId, navTaskId, out errorString);

            // ANR scoring, starting list
            var fs = Client.DBContext.FlightSet.Where(x => x.QualificationRound_Id == qualRndId).ToList();
            List<ContestantsTeam> lsMapped = _client.MapUpsertFlightSetsToASContestantsTeamsOnNavTask(fs, existingTeamsOnContest, existingContestantsTeams, qualRndId, contestId);
            if (lsMapped.Count > 0)
            {
                List<ContestantsTeam> lsOut = _client.UpsertASContestantsTeamsOnNavTask(contestId, navTaskId, lsMapped, out lstErrorString);
                MessageBox.Show(string.Format("Upserted {2} Contestant(s) on Airsports.no (ContestId={0}, NavigationTaskId={1})", contestId, navTaskId, lsMapped.Count), "Contestants", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // no data to upload
                // maybe because the externalIds are missing?
                // try to import first from Airsports
                MessageBox.Show(string.Format("No matching data (External ids) found. Cannot upload contestants to Airsports.no (ContestId={0}, NavigationTaskId={1})", contestId, navTaskId), "Contestants", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
