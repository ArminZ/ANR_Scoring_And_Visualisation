using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AirNavigationRaceLive.Dialogs;
using AirNavigationRaceLive.Comps.Helper;
using System.IO;
using AirNavigationRaceLive.Model;
using AirNavigationRaceLive.ModelExtensions;
using OfficeOpenXml;

namespace AirNavigationRaceLive.Comps
{
    public partial class Results : UserControl
    {
        private Client.DataAccess Client;
        private QualificationRoundSet qualificRound = null;
        private ParcourSet parcour;
        private PenaltySet pDeleted;


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
                ComboQRExtension cce = comboBoxQualificRound.SelectedItem as ComboQRExtension;
                if (cce != null)
                {
                    qualificRound = cce.q;
                    dataGridView2.Rows.Clear();
                    long min = long.MaxValue;
                    long max = long.MinValue;
                    List<FlightSet> CompetitionTeamList = qualificRound.FlightSet.ToList();
                    CompetitionTeamList.Sort((p, q) => p.StartID.CompareTo(q.StartID));
                    List<Point> points = new List<Point>();
                    foreach (FlightSet ct in qualificRound.FlightSet)
                    {
                        min = Math.Min(ct.TimeTakeOff, min);
                        max = Math.Max(ct.TimeEndLine, max);

                        DataGridViewRow dgvr = new DataGridViewRow();
                        dgvr.CreateCells(dataGridView2);
                        dgvr.SetValues(new string[] { ct.StartID.ToString(), "0", getTeamDsc(ct), new DateTime(ct.TimeTakeOff).ToShortTimeString(), new DateTime(ct.TimeStartLine).ToShortTimeString(), new DateTime(ct.TimeEndLine).ToShortTimeString(), getRouteText(ct.Route) });
                        dataGridView2.Rows.Add(dgvr);
                        dgvr.Tag = ct;

                    }

                    parcour = cce.q.ParcourSet;
                    MapSet map = parcour.MapSet;
                    MemoryStream ms = new MemoryStream(map.PictureSet.Data);
                    visualisationPictureBox1.Image = System.Drawing.Image.FromStream(ms);
                    visualisationPictureBox1.SetConverter(new Converter(map));
                    visualisationPictureBox1.SetParcour(parcour);
                    visualisationPictureBox1.Invalidate();
                    visualisationPictureBox1.Refresh();
                    this.BeginInvoke(new MethodInvoker(updatePoints));
                    dataGridView2_SelectionChanged(sender, e);
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
        private string getTeamDsc(FlightSet flight)
        {
            TeamSet team = flight.TeamSet;
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

        public void updatePoints()
        {
            if (qualificRound != null && parcour != null)
            {

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    FlightSet flt = row.Tag as FlightSet;
                    int sum = 0;
                    foreach (PenaltySet p in flt.PenaltySet)
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
            List<QualificationRoundSet> comps = Client.SelectedCompetition.QualificationRoundSet.ToList();
            comboBoxQualificRound.Items.Clear();
            foreach (QualificationRoundSet c in comps)
            {
                comboBoxQualificRound.Items.Add(new ComboQRExtension(c));
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
            PenaltySet p = dataGridView1.SelectedRows[0].Tag as PenaltySet;
            updateEnablement();
        }

        private void btnExportSingle_Click(object sender, EventArgs e)
        {
            if (qualificRound != null && dataGridView2.SelectedRows.Count > 0)
            {
                List<ComboBoxFlights> ctl = new List<ComboBoxFlights>();

                DataGridViewRow dgvr = dataGridView2.SelectedRows[0];
                {
                    FlightSet flt = dgvr.Tag as FlightSet;
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
                            @"\Results_" + cleanedString(qualificRound.Name) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf");
                    }
                    else
                    {
                        MessageBox.Show("Logger data for the selected flight is missing. The MapSet will not be exported.", "Single MapSet Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    FlightSet flt = dgvr.Tag as FlightSet;
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
                    @"\Results_" + cleanedString(qualificRound.Name) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf");
            }
        }

        private void ExportResultRanking(bool exportAsPdf = true)
        {
            if (qualificRound != null && dataGridView2.Rows.Count > 0)
            {
                List<FlightSet> lstFlt = new List<FlightSet>();
                List<ComboBoxFlights> ctl = new List<ComboBoxFlights>();

                foreach (DataGridViewRow dgvr in dataGridView2.Rows)
                {
                    FlightSet flt = dgvr.Tag as FlightSet;
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

                if (exportAsPdf)
                {
                PDFCreator.CreateRankingListPDF(Client, qualificRound, ctl, dirPath +
                    @"\Results_" + cleanedString(qualificRound.Name) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf");
                }
                else
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        string _fileFilter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx| Csv files (*.csv)|*.csv| All files (*.*)|*.*";

                        sfd.FileName = @"\Results_" + cleanedString(qualificRound.Name) + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";
                        sfd.Filter = _fileFilter;
                        sfd.FilterIndex = 0;
                        sfd.OverwritePrompt = true;
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            CreateRankingListExcel(Client.SelectedCompetition.Name, qualificRound.Name, ctl, sfd.FileName);
                            MessageBox.Show("Ranking list saved", "Ranking list saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private static string cleanedString(string inputString)
        {
            var invalids = System.IO.Path.GetInvalidFileNameChars();
            var cleanedName = String.Join("_", inputString.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
            return cleanedName;
        }


        private void btnLoggerImport_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                var checkedButton = groupBoxLoggerImport.Controls.OfType<RadioButton>()
                          .FirstOrDefault(r => r.Checked);
                if (checkedButton == radioButtonGACimport)
                {
                    FlightSet selectedTeamFlight = dataGridView2.SelectedRows[0].Tag as FlightSet;
                    UploadGAC upload = new UploadGAC(Client, selectedTeamFlight);
                    upload.OnFinish += new EventHandler(UploadFinished);
                    upload.Show();
                }
                if (checkedButton == radioButtonGPXimport)
                {
                    FlightSet selectedTeamFlight = dataGridView2.SelectedRows[0].Tag as FlightSet;
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
            List<QualificationRoundSet> comps = Client.SelectedCompetition.QualificationRoundSet.ToList();
            comboBoxQualificRound.Items.Clear();
            comboBoxQualificRound.SelectedItem = null;
            comboBoxQualificRound.Text = "";
            foreach (QualificationRoundSet c in comps)
            {
                comboBoxQualificRound.Items.Add(new ComboQRExtension(c));
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
            if (checkedButton == radioButtonRankingPDF) { ExportResultRanking(true); }
            if (checkedButton == radioButtonRankingXLS) { ExportResultRanking(false); }

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
                pDeleted = e.Row.Tag as PenaltySet;
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
            PenaltySet p = new PenaltySet();
            if (string.IsNullOrEmpty(iDval.ToString()))
            {
                FlightSet flt = dataGridView2.SelectedRows[0].Tag as FlightSet;
                p.Reason = penaltyReason.ToString().Trim();
                p.Points = penVal;
                if (!flt.PenaltySet.Contains(p))
                {
                    flt.PenaltySet.Add(p);
                }
                dataGridView1.Rows[e.RowIndex].Tag = p;
            }
            else
            {
                p = dataGridView1.Rows[e.RowIndex].Tag as PenaltySet;
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
                List<FlightSet> flights = new List<FlightSet>(1);
                FlightSet flt = dataGridView2.Rows[rowIdx].Tag as FlightSet;
                flights.Add(flt);
                visualisationPictureBox1.SetData(flights);
                visualisationPictureBox1.Invalidate();
                List<DataGridViewRow> ppd = new List<DataGridViewRow>();
                foreach (PenaltySet p in flt.PenaltySet)
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



        public static void CreateRankingListExcel(string CompName, string QRName, List<ComboBoxFlights> qRndFlights, String filename)
        {

            List<Toplist> toplist = new List<Toplist>();
            foreach (ComboBoxFlights cbct in qRndFlights)
            {
                int sum = 0;
                foreach (PenaltySet penalty in cbct.flight.PenaltySet)
                {
                    sum += penalty.Points;
                }
                toplist.Add(new Toplist(cbct.flight, sum));
            }
            toplist.Sort();

            var newFile = new FileInfo(filename);
            if (newFile.Exists)
            {
                newFile.Delete();
            }
            using (var pck = new ExcelPackage(newFile))
            {
                ExcelWorksheet ResultList = pck.Workbook.Worksheets.Add("ResultList");
                ResultList.Cells[1, 1].Value = String.Format("Competition: {0}", CompName);
                ResultList.Cells[2, 1].Value = String.Format("Qualification Round: {0}", QRName);

                string[] colNamesValues = { "Rank", "Points", "Nationality", "Pilot Lastname", "Pilot Firstname", "Navigator Lastname", "Navigator Firstname" };

                for (int jCol = 0; jCol < colNamesValues.Length; jCol++)
                {
                    ResultList.Cells[3, jCol + 1].Value = colNamesValues[jCol];
                }

                int oldsum = -1;
                int prevRank = 0;
                int rank = 0;
                int i = 0;
                int iBase = 3;

                foreach (Toplist top in toplist)
                {
                    rank++;
                    i++;
                    TeamSet t = top.ct.TeamSet;
                    if (i > 0 && oldsum == top.sum)  // we have a shared rank
                    {
                        ResultList.Cells[i + iBase, 1].Value = prevRank;
                    }
                    else  // the normal case
                    {
                        prevRank = rank;
                        ResultList.Cells[i + iBase, 1].Value = rank;
                    }
                    ResultList.Cells[i + iBase, 2].Value = top.sum.ToString();
                    ResultList.Cells[i + iBase, 3].Value = t.Nationality;
                    SubscriberSet pilot = t.Pilot;
                    ResultList.Cells[i + iBase, 4].Value = pilot.LastName;
                    ResultList.Cells[i + iBase, 5].Value = pilot.FirstName;
                    if (t.Navigator != null)
                    {
                        SubscriberSet navigator = t.Navigator;
                        ResultList.Cells[i + iBase, 6].Value = navigator.LastName;
                        ResultList.Cells[i + iBase, 7].Value = navigator.FirstName;
                    }
                    oldsum = top.sum;
                }
                pck.Save();
            }
        }
    }

    class Toplist : IComparable
    {
        public Toplist(FlightSet ct,
        int sum)
        {

            this.ct = ct;
            this.sum = sum;
        }
        public FlightSet ct = null;
        public int sum = 0;

        public int CompareTo(object obj)
        {
            return sum.CompareTo((obj as Toplist).sum);
        }
    }
    public class ComboBoxFlights : ListViewItem
    {
        public readonly FlightSet flight;
        public ComboBoxFlights(FlightSet team, string[] display)
            : base(display)
        {
            this.flight = team;
        }
    }

}
