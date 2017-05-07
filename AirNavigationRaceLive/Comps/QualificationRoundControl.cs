using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AirNavigationRaceLive.Dialogs;
using AirNavigationRaceLive.Comps.Helper;
using System.IO;

namespace AirNavigationRaceLive.Comps
{
    public partial class QualificationRoundControl : UserControl
    {
        private Client.DataAccess Client;
        private Flight deleteFlt;
        private int qrIdx =-1; //index of selected QR

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
            List<QualificationRound> comps = Client.SelectedCompetition.QualificationRound.ToList();
            listViewQualificationRound.Items.Clear();
            foreach (QualificationRound c in comps)
            {
                ListViewItem lvi = new ListViewItem(new String[] { c.Id.ToString(), c.Name });
                lvi.Tag = c;
                listViewQualificationRound.Items.Add(lvi);
            }
            UpdateEnablement();
            lblQRound.Text = string.Format("{0} - Qualification Rounds", Client.SelectedCompetition.Name);
            groupBoxStartList.Visible = false;
        }

        private void LoadTeams()
        {
            List<Team> teams = Client.SelectedCompetition.Team.ToList();
            UpdateEnablement();
        }
        private void LoadParcours()
        {
            List<Parcour> parcour = Client.SelectedCompetition.Parcour.ToList();
            comboBoxParcour.Items.Clear();
            foreach (Parcour c in parcour)
            {
                ComboParcour cp = new ComboParcour(c);
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
            SetTimes();
            errorProviderQualification.Clear();
        }

        private void SetTimes()
        {
            DateTime time = DateTime.Now;
            timeTakeOffIntervall.Value = new DateTime(time.Year, time.Month, time.Day, time.Hour, 1, 0);
            timeParcourLength.Value = new DateTime(time.Year, time.Month, time.Day, time.Hour, 12, 0);
            timeParcourIntervall.Value = new DateTime(time.Year, time.Month, time.Day, time.Hour, 20, 0);
            timeTakeOffStartgate.Value = new DateTime(time.Year, time.Month, time.Day, time.Hour, 12, 0);
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
                QualificationRound c = listViewQualificationRound.SelectedItems[0].Tag as QualificationRound;
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
            QualificationRound qualificationRound = new QualificationRound();
            qualificationRound.Competition = Client.SelectedCompetition;
            textName.Tag = qualificationRound;
            qrIdx =-1;
            comboBoxParcour.SelectedIndex = -1;
            textName.SelectAll();
            textName.Focus();
        }

        private void btnSaveQualificationRound_Click(object sender, EventArgs e)
        {
            UpdateEnablement();

            QualificationRound c = textName.Tag as QualificationRound;
            if (c == null)
            {
                c = new QualificationRound();
                c.Competition = Client.SelectedCompetition;
            }
            c.Parcour = (comboBoxParcour.SelectedItem as ComboParcour).p;
            c.Name = textName.Text;

            Vector start = new Vector(double.Parse(takeOffLeftLongitude.Text), double.Parse(takeOffLeftLatitude.Text), 0);
            Vector end = new Vector(double.Parse(takeOffRightLongitude.Text), double.Parse(takeOffRightLatitude.Text), 0);
            Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
            Line line = new Line();
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

        private DateTime mergeDateTime(DateTime time, DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0, 0);
        }

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
                QualificationRound c = lvi.Tag as QualificationRound;
                if (c == null) return;
                //qR = c;
                qrIdx = lvi.Index;
                textName.Tag = c;
                textName.Text = c.Name;
                ComboParcour cp = null;
                foreach (Object o in comboBoxParcour.Items)
                {
                    if ((o as ComboParcour).p == c.Parcour)
                    {
                        cp = o as ComboParcour;
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
            }

        }

        private void updateList(QualificationRound c)
        {
            dataGridView1.Rows.Clear();
            List<Flight> flights = new List<Flight>(c.Flight);
            flights.Sort((p, q) => p.StartID.CompareTo(q.StartID));
            foreach (Flight fl in flights)
            {
                DataGridViewRow dgvr = new DataGridViewRow();
                dgvr.CreateCells(dataGridView1);
                dgvr.SetValues(
                    fl.StartID.ToString(),
                    fl.Team.CNumber,
                    fl.Team.AC,
                    getTeamDsc(fl.Team),
                new DateTime(fl.TimeTakeOff).ToString("HH:mm:ss"),
                        new DateTime(fl.TimeStartLine).ToString("HH:mm:ss"),
                        new DateTime(fl.TimeEndLine).ToString("HH:mm:ss"),
                        getRouteText(fl.Route),
                        new DateTime(fl.TimeTakeOff).ToShortDateString());

                fl.QualificationRound = listViewQualificationRound.SelectedItems[0].Tag as QualificationRound;
                dgvr.Tag = fl;
                dataGridView1.Rows.Add(dgvr);
            }
            //  dataGridView1.Sort(dataGridView1.Columns["Crew"],ListSortDirection.Ascending);

        }
        private string getTeamDsc(Team team)
        {
            Subscriber pilot = team.Pilot;
            StringBuilder sb = new StringBuilder();
            sb.Append(pilot.LastName).Append(" ").Append(pilot.FirstName);
            if (team.Navigator != null)
            {
                Subscriber navi = team.Navigator;
                sb.Append(" - ").Append(navi.LastName).Append(" ").Append(navi.FirstName);
            }
            return sb.ToString();
        }
        private string getRouteText(int id)
        {
            return ((NetworkObjects.Route)id).ToString();
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
            QualificationRound c = textName.Tag as QualificationRound;
            if (c != null)
            {
                String dirPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\AirNavigationRace\";
                DirectoryInfo di = Directory.CreateDirectory(dirPath);
                if (!di.Exists)
                {
                    di.Create();
                }
                PDFCreator.CreateStartListPDF(c, Client, dirPath +
                    @"\StartList_" + c.Id + "_" + c.Name + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf");
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

        class ComboParcour
        {
            public Parcour p;
            public ComboParcour(Parcour p)
            {
                this.p = p;
            }

            public override string ToString()
            {
                return p.Name;
            }
        }

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
            List<Team> lstTeams = Client.SelectedCompetition.Team.ToList();
            List<ComboTeam> lstCboTeam = new List<ComboTeam>();
            foreach (Team t in lstTeams)
            {
                lstCboTeam.Add(new ComboTeam(t, getTeamDsc(t)));
            }

            long intervTKOF = timeTakeOffIntervall.Value.Minute * 60 + timeTakeOffIntervall.Value.Second;  // timeTakeOffIntervall.Value.Ticks;
            long intervStartL = timeParcourIntervall.Value.Minute * 60 + timeParcourIntervall.Value.Second;
            long tkofToStart = timeTakeOffStartgate.Value.Minute * 60 + timeTakeOffStartgate.Value.Second;
            long parcourLength = timeParcourLength.Value.Minute * 60 + timeParcourLength.Value.Second;

            // as a default, use the actual date
            long dateQRdate0 = DateTime.Now.Ticks;
            long timeTKOF0 = dateQRdate0;
            long timeStart0 = new DateTime(timeTKOF0).AddSeconds(intervStartL).Ticks;
            long timeEnd0 = new DateTime(timeStart0).AddSeconds(parcourLength).Ticks;

            if (dataGridView1.RowCount > 1 && dataGridView1.SelectedRows[0].IsNewRow)
            {
                // read Tag from previous row and get date and previous starttime
                // add  takeOffInterval to previous starttime  
                Flight flt0 = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index - 1].Tag as Flight;

                dateQRdate0 = flt0.TimeTakeOff;
                timeTKOF0 = new DateTime(flt0.TimeTakeOff).AddSeconds(intervTKOF).Ticks;
                timeStart0 = new DateTime(timeTKOF0).AddSeconds(intervStartL).Ticks;
                timeEnd0 = new DateTime(timeStart0).AddSeconds(parcourLength).Ticks;
            }

            Flight flt = dataGridView1.SelectedRows[0].Tag as Flight;
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
                    Flight fl = frmStartListDialog.SelectedFlight;
                    if (fl.Id == 0)
                    {
                        Client.DBContext.FlightSet.Add(fl);

                        DataGridViewRow dgvr = new DataGridViewRow();
                        dgvr.CreateCells(dataGridView1);
                        dgvr.SetValues(
                            fl.StartID.ToString(),
                            fl.Team.CNumber,
                            fl.Team.AC,
                            getTeamDsc(fl.Team),
                        new DateTime(fl.TimeTakeOff).ToString("HH:mm:ss"),
                        new DateTime(fl.TimeStartLine).ToString("HH:mm:ss"),
                        new DateTime(fl.TimeEndLine).ToString("HH:mm:ss"),
                        getRouteText(fl.Route),
                        new DateTime(fl.TimeTakeOff).ToShortDateString());

                        fl.QualificationRound = listViewQualificationRound.SelectedItems[0].Tag as QualificationRound;
                        dgvr.Tag = fl;
                        dataGridView1.Rows.Add(dgvr);
                    }
                    Client.DBContext.SaveChanges();
                    updateList(fl.QualificationRound);
                    dataGridView1.Rows[0].Selected = false;
                    dataGridView1.Rows[idx].Selected = true;
                    return;
                }
            }
        }

        private int calculateMaxStartId()
        {
            int _maxStartId = 0;
            QualificationRound c = textName.Tag as QualificationRound;
            if (c != null && c.Flight.Count > 0)
            {
                foreach (Flight ct in c.Flight)
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
            deleteFlt = e.Row.Tag as Flight;
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (!(deleteFlt == null))
            {
                // Flight ct = dataGridView1.SelectedRows[0].Tag as Flight;
                Client.DBContext.FlightSet.Remove(deleteFlt);
                Client.DBContext.SaveChanges();
                listViewQualificationRound_SelectedIndexChanged(null, null);
                updateList(listViewQualificationRound.SelectedItems[0].Tag as QualificationRound);
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
            QualificationRound qRnd = null;
            long intervTKOF = timeTakeOffIntervall.Value.Minute * 60 + timeTakeOffIntervall.Value.Second;  // timeTakeOffIntervall.Value.Ticks;
            long intervStartL = timeParcourIntervall.Value.Minute * 60 + timeParcourIntervall.Value.Second;
            long tkofToStart = timeTakeOffStartgate.Value.Minute * 60 + timeTakeOffStartgate.Value.Second;
            long parcourLength = timeParcourLength.Value.Minute * 60 + timeParcourLength.Value.Second;
            int NrOfRoutes = (int)numericUpDownRoutes.Value;
            Flight f = null;
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

                f = dataGridView1.Rows[idx].Tag as Flight;
                tkof0 = f.TimeTakeOff;
                qRnd = f.QualificationRound;
            }

            // re-calculate from selected index upwards
            for (int i = idx; i < dataGridView1.Rows.Count; i++)
            {
                f = dataGridView1.Rows[i].Tag as Flight;
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
            QualificationRound qRnd = null;
            long intervTKOF = timeTakeOffIntervall.Value.Minute * 60 + timeTakeOffIntervall.Value.Second;  // timeTakeOffIntervall.Value.Ticks;
            long intervStartL = timeParcourIntervall.Value.Minute * 60 + timeParcourIntervall.Value.Second;
            long tkofToStart = timeTakeOffStartgate.Value.Minute * 60 + timeTakeOffStartgate.Value.Second;
            long parcourLenght = timeParcourLength.Value.Minute * 60 + timeParcourLength.Value.Second;

            int NrOfRoutes = (int)numericUpDownRoutes.Value;
            Flight f = null;
            if (dataGridView1.Rows.Count > 1)
            {
                f = dataGridView1.Rows[0].Tag as Flight;
                tkof0 = f.TimeTakeOff;
                qRnd = f.QualificationRound;
            }
            else
            {
                ListViewItem lvi = listViewQualificationRound.SelectedItems[0];
                qRnd = lvi.Tag as QualificationRound;
                tkof0 = DateTime.Now.Ticks;
            }

            List<Team> lstTeam = Client.SelectedCompetition.Team.ToList();
            for (int i = 0; i < lstTeam.Count; i++)
            {
                Flight flt = new Flight();

                flt.TimeTakeOff = new DateTime(tkof0).AddSeconds(i * intervTKOF).AddSeconds((i / NrOfRoutes) * (intervStartL - intervTKOF)).Ticks;
                flt.TimeStartLine = new DateTime(tkof0).AddSeconds(tkofToStart).AddSeconds(i * intervTKOF).AddSeconds((i / NrOfRoutes) * (intervStartL + intervTKOF)).Ticks;
                flt.TimeEndLine = new DateTime(tkof0).AddSeconds(tkofToStart + parcourLenght).AddSeconds(i * intervTKOF).AddSeconds((i / NrOfRoutes) * (intervStartL + intervTKOF)).Ticks;
                flt.Route = i % NrOfRoutes + 1;
                flt.Team = lstTeam[i];
                flt.StartID = i + 1;
                flt.QualificationRound = qRnd;
                Client.DBContext.FlightSet.Add(flt);
            }

            Client.DBContext.SaveChanges();
            updateList(qRnd);
        }
    }
}
