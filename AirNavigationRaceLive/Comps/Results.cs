using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AirNavigationRaceLive.Dialogs;
using AirNavigationRaceLive.Comps.Helper;
using System.IO;

namespace AirNavigationRaceLive.Comps
{
    public partial class Results : UserControl
    {
        private Client.DataAccess Client;
        private QualificationRound qualificRound = null;
        private Parcour parcour;
        private Penalty pDeleted;


        public Results(Client.DataAccess iClient)
        {
            Client = iClient;
            InitializeComponent();
            dataGridView2.MultiSelect = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView2.RowHeadersVisible = false;
            //dataGridView1.RowHeadersWidth = 30;

        }
        private void comboBoxQualificRound_SelectedIndexChanged(object sender, EventArgs e)
        {
            qualificRound = null;
            if (comboBoxQualificRound.SelectedItem != null)
            {
                QualificRoundComboEntry cce = comboBoxQualificRound.SelectedItem as QualificRoundComboEntry;
                if (cce != null)
                {
                    qualificRound = cce.qualRnd;
                    dataGridView2.Rows.Clear();
                    long min = long.MaxValue;
                    long max = long.MinValue;
                    List<Flight> CompetitionTeamList = qualificRound.Flight.ToList();
                    CompetitionTeamList.Sort((p, q) => p.StartID.CompareTo(q.StartID));
                    List<Point> points = new List<Point>();
                    foreach (Flight ct in qualificRound.Flight)
                    {
                        min = Math.Min(ct.TimeTakeOff, min);
                        max = Math.Max(ct.TimeEndLine, max);

                        DataGridViewRow dgvr = new DataGridViewRow();
                        dgvr.CreateCells(dataGridView2);
                        dgvr.SetValues(new string[] { ct.StartID.ToString(), "0", getTeamDsc(ct), new DateTime(ct.TimeTakeOff).ToShortTimeString(), new DateTime(ct.TimeStartLine).ToShortTimeString(), new DateTime(ct.TimeEndLine).ToShortTimeString(), getRouteText(ct.Route) });
                        dataGridView2.Rows.Add(dgvr);
                        dgvr.Tag = ct;

                    }

                    parcour = cce.qualRnd.Parcour;
                    Map map = parcour.Map;
                    MemoryStream ms = new MemoryStream(map.Picture.Data);
                    visualisationPictureBox1.Image = System.Drawing.Image.FromStream(ms);
                    visualisationPictureBox1.SetConverter(new Converter(map));
                    visualisationPictureBox1.SetParcour(parcour);
                    visualisationPictureBox1.Invalidate();
                    visualisationPictureBox1.Refresh();
                    this.BeginInvoke(new MethodInvoker(updatePoints));

                }
                else
                {
                    dataGridView2.Rows.Clear();
                }
            }
            else
            {
                dataGridView2.Rows.Clear();
            }
        }
        private string getTeamDsc(Flight flight)
        {
            Team team = flight.Team;
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

        public void updatePoints()
        {
            if (qualificRound != null && parcour != null)
            {

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    Flight flt = row.Tag as Flight;
                    int sum = 0;
                    foreach (Penalty p in flt.Penalty)
                    {
                        sum += p.Points;
                    }
                    row.Cells[1].Value = sum.ToString();
                }
            }
        }

        delegate void SetPenaltiesCallback(List<DataGridViewRow> penalties);

        private void SetPenalties(List<DataGridViewRow> penalties)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.dataGridView1.InvokeRequired)
            {
                SetPenaltiesCallback d = new SetPenaltiesCallback(SetPenalties);
                this.Invoke(d, new object[] { penalties });
            }
            else
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Rows.AddRange(penalties.ToArray());
            }
        }

        private void Results_Load(object sender, EventArgs e)
        {
            List<QualificationRound> comps = Client.SelectedCompetition.QualificationRound.ToList();
            comboBoxQualificRound.Items.Clear();
            foreach (QualificationRound c in comps)
            {
                comboBoxQualificRound.Items.Add(new QualificRoundComboEntry(c));
            }
            updateEnablement();
            lblResults.Text = string.Format("{0} - Results", Client.SelectedCompetition.Name);
            radioButtonSingleRes.Checked = true;
        }

        private void updateEnablement()
        {
            btnLoggerImport.Enabled = dataGridView2.SelectedRows.Count > 0;
            btnExportResults.Enabled = dataGridView2.SelectedRows.Count > 0;
            dataGridView1.Visible = dataGridView2.SelectedRows.Count > 0;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            if (dataGridView1.SelectedRows[0].Tag == null)
            {
                return;
            }
            Penalty p = dataGridView1.SelectedRows[0].Tag as Penalty;
            updateEnablement();
        }

        private void btnExportSingle_Click(object sender, EventArgs e)
        {
            if (qualificRound != null && dataGridView2.SelectedRows.Count > 0)
            {
                List<ComboBoxFlights> ctl = new List<ComboBoxFlights>();

                DataGridViewRow dgvr = dataGridView2.SelectedRows[0];
                {
                    Flight flt = dgvr.Tag as Flight;
                    if (flt.Point.Count > 0)  // skip flights without imported logger data
                    {
                        ComboBoxFlights cboFlt = new ComboBoxFlights(flt, new string[] { "--" });
                        ctl.Add(cboFlt);

                        String dirPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\AirNavigationRace\";
                        DirectoryInfo di = Directory.CreateDirectory(dirPath);
                        if (!di.Exists)
                        {
                            di.Create();
                        }
                        PDFCreator.CreateResultPDF(visualisationPictureBox1, Client, qualificRound, ctl, dirPath +
                            @"\Results_" + qualificRound.Name + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf");
                    }
                    else
                    {
                        MessageBox.Show("Logger data for the selected flight is missing. The map will not be exported.","Single Map Export",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnExportAll_Click(object sender, EventArgs e)
        {
            if (qualificRound != null && dataGridView2.Rows.Count > 0)
            {
                List<ComboBoxFlights> ctl = new List<ComboBoxFlights>();
                foreach (DataGridViewRow dgvr in dataGridView2.Rows)
                {
                    Flight flt = dgvr.Tag as Flight;
                    if (flt.Point.Count == 0)  // skip flights without imported logger data
                    {
                        continue;
                    }
                    ComboBoxFlights cboFlt = new ComboBoxFlights(flt, new string[] { "--" });
                    ctl.Add(cboFlt);
                }
                String dirPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\AirNavigationRace\";
                DirectoryInfo di = Directory.CreateDirectory(dirPath);
                if (!di.Exists)
                {
                    di.Create();
                }
                PDFCreator.CreateResultPDF(visualisationPictureBox1, Client, qualificRound, ctl, dirPath +
                    @"\Results_" + qualificRound.Name + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf");
            }
        }

        private void btnExportTopList_Click(object sender, EventArgs e)
        {
            if (qualificRound != null && dataGridView2.Rows.Count > 0)
            {
                List<Flight> lstFlt = new List<Flight>();
                List<ComboBoxFlights> ctl = new List<ComboBoxFlights>();

                foreach (DataGridViewRow dgvr in dataGridView2.Rows)
                {
                    Flight flt = dgvr.Tag as Flight;
                    if (flt.Point.Count > 0)
                    {
                        ComboBoxFlights cboFlt = new ComboBoxFlights(flt, new string[] { "--" });
                        ctl.Add(cboFlt);
                    }
                }

                String dirPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\AirNavigationRace\";
                DirectoryInfo di = Directory.CreateDirectory(dirPath);
                if (!di.Exists)
                {
                    di.Create();
                }
                PDFCreator.CreateToplistResultPDF(Client, qualificRound, ctl, dirPath +
                    @"\Results_" + qualificRound.Name + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf");
            }
        }

        private void btnLoggerImport_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                var checkedButton = groupBoxLoggerImport.Controls.OfType<RadioButton>()
                          .FirstOrDefault(r => r.Checked);
                if (checkedButton == radioButtonGACimport)
                {
                    Flight selectedTeamFlight = dataGridView2.SelectedRows[0].Tag as Flight;
                    UploadGAC upload = new UploadGAC(Client, selectedTeamFlight);
                    upload.OnFinish += new EventHandler(UploadFinished);
                    upload.Show();
                }
                if (checkedButton == radioButtonGPXimport)
                {
                    Flight selectedTeamFlight = dataGridView2.SelectedRows[0].Tag as Flight;
                    UploadGPX upload = new UploadGPX(Client, selectedTeamFlight);
                    upload.OnFinish += new EventHandler(UploadFinished);
                    upload.Show();
                }
            }
        }
        delegate void OnFinishCallback(IAsyncResult result);

        public void UploadFinished(object o, EventArgs ea)
        {
            OnFinishCallback d = new OnFinishCallback(UploadFinished);
            dataGridView2.Invoke(d, new object[] { null });
        }
        public void UploadFinished(IAsyncResult ass)
        {
            updatePoints();
            dataGridView2_SelectionChanged(null, null);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            List<QualificationRound> comps = Client.SelectedCompetition.QualificationRound.ToList();
            comboBoxQualificRound.Items.Clear();
            comboBoxQualificRound.SelectedItem = null;
            comboBoxQualificRound.Text = "";
            foreach (QualificationRound c in comps)
            {
                comboBoxQualificRound.Items.Add(new QualificRoundComboEntry(c));
            }
            comboBoxQualificRound_SelectedIndexChanged(null, null);
        }

        private void AllCheckBoxes_CheckedChanged(Object sender, EventArgs e)
        { // check if radio button control is checked. Wire this to all required rb controls in a group
            if (((RadioButton)sender).Checked)
            {
                RadioButton rb = (RadioButton)sender;
            }
        }

        private void btnExportResults_Click(object sender, EventArgs e)
        { // button to handle all PDF Exports
            var checkedButton = groupBoxExportResults.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
            if (checkedButton == radioButtonSingleRes) { btnExportSingle_Click(sender, e); }
            if (checkedButton == radioButtonAllRes) { btnExportAll_Click(sender, e); }
            if (checkedButton == radioButtonTopRes) { btnExportTopList_Click(sender, e); }

        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (!e.Row.IsNewRow)
            {
                string str = e.Row.Cells[1].Value.ToString() + " " + e.Row.Cells[2].Value.ToString();
                if (MessageBox.Show(string.Format("Delete the selected Penalty:\n {0} ?", str), "Delete Penalty", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                pDeleted = e.Row.Tag as Penalty;
            }
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (pDeleted != null)
            {
                Client.DBContext.PenaltySet.Remove(pDeleted);
                pDeleted = null;
                Client.DBContext.SaveChanges();
                updatePoints();
                updateEnablement();
            }
        }

        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!dataGridView1.IsCurrentRowDirty)
            {
                return;
            }
            int penVal;
            var iDval = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue;
            var penaltyValue = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue;
            var penaltyReason = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue;
            if (string.IsNullOrWhiteSpace(penaltyValue.ToString()))
            {
                dataGridView1.Rows[e.RowIndex].ErrorText = "Penalty points cannot be empty";
                e.Cancel = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(penaltyReason.ToString()))
            {
                dataGridView1.Rows[e.RowIndex].ErrorText = "Penalty reason cannot be empty";
                e.Cancel = true;
                return;
            }
            if (!int.TryParse(penaltyValue.ToString(), out penVal))
            {
                dataGridView1.Rows[e.RowIndex].ErrorText = "Penalty points must be numeric";
                e.Cancel = true;
                return;
            }

            dataGridView1.Rows[e.RowIndex].ErrorText = string.Empty;
            Penalty p = new Penalty();
            if (string.IsNullOrEmpty(iDval.ToString()))
            {
                Flight flt = dataGridView2.SelectedRows[0].Tag as Flight;
                p.Reason = penaltyReason.ToString().Trim();
                p.Points = penVal;
                if (!flt.Penalty.Contains(p))
                {
                    flt.Penalty.Add(p);
                }
                dataGridView1.Rows[e.RowIndex].Tag = p;
            }
            else
            {
                p = dataGridView1.Rows[e.RowIndex].Tag as Penalty;
                p.Reason = penaltyReason.ToString().Trim();
                p.Points = penVal;
            }
            Client.DBContext.SaveChanges();
            updatePoints();
            updateEnablement();

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count == 0)
            {
                return;
            }
            int rowIdx = dataGridView2.SelectedCells[0].RowIndex;
            if (dataGridView2.SelectedRows.Count > 0 && dataGridView2.SelectedRows[0].Tag != null)
            {
                btnLoggerImport.Enabled = true;
                List<Flight> flights = new List<Flight>(1);
                Flight flt = dataGridView2.Rows[rowIdx].Tag as Flight;
                flights.Add(flt);
                visualisationPictureBox1.SetData(flights);
                visualisationPictureBox1.Invalidate();
                List<DataGridViewRow> ppd = new List<DataGridViewRow>();
                foreach (Penalty p in flt.Penalty)
                {
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(dataGridView1);
                    dgvr.SetValues(new String[] { p.Id.ToString(), p.Points.ToString(), p.Reason });
                    dgvr.Tag = p;
                    ppd.Add(dgvr);
                }
                SetPenalties(ppd);
            }
            else
            {
                btnLoggerImport.Enabled = false;
                dataGridView1.Rows.Clear();
            }
            updateEnablement();

        }

        private void dataGridView2_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            //Sort numerical string as number
            if (e.Column.Index == 0 || e.Column.Index == 1)
            {
                e.SortResult = int.Parse(e.CellValue1.ToString()).CompareTo(int.Parse(e.CellValue2.ToString()));
                e.Handled = true;//pass by the default sorting
            }
        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            //Sort numerical string as number
            if (e.Column.Index == 0 || e.Column.Index == 1)
            {
                e.SortResult = int.Parse(e.CellValue1.ToString()).CompareTo(int.Parse(e.CellValue2.ToString()));
                e.Handled = true;//pass by the default sorting
            }
        }
    }
    public class ComboBoxFlights : ListViewItem
    {
        public readonly Flight flight;
        public ComboBoxFlights(Flight team, string[] display)
            : base(display)
        {
            this.flight = team;
        }
    }
}
