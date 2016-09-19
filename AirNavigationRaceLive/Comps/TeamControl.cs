using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AirNavigationRaceLive.Comps.Helper;
using System.IO;
using AirNavigationRaceLive.Dialogs;

namespace AirNavigationRaceLive.Comps
{
    public partial class TeamControl : UserControl
    {
        private Client.DataAccess Client;
        private List<string> lstTeamIdPilotNavNames;

        public TeamControl(Client.DataAccess iClient)
        {
            Client = iClient;
            InitializeComponent();
        }

        private void TeamControl_Load(object sender, EventArgs e)
        {
            LoadLists();
            UpdateEnablement();
            groupBoxTeams.Text = string.Format("{0} - Crews", Client.SelectedCompetition.Name);
            //dataGridView1.RowHeadersVisible = false;
            //           dataGridView1.RowHeadersWidth = 35;
        }

        private void LoadLists()
        {
            List<Subscriber> pilots = Client.SelectedCompetition.Subscriber.OrderBy(p => p.LastName).ToList();
            List<Team> teams = Client.SelectedCompetition.Team.ToList();
            lstTeamIdPilotNavNames = new List<string>();
            dataGridView1.Rows.Clear();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            foreach (Team team in teams)
            {
                DataGridViewRow dgvr = new DataGridViewRow();
                dgvr.Tag = team;   
                dgvr.CreateCells(dataGridView1);
                dgvr.SetValues(
                    team.CNumber, 
                    team.Nationality != null ? team.Nationality : "", team.Pilot.LastName + " " + team.Pilot.FirstName, 
                    team.Navigator != null ? team.Navigator.LastName + " " + team.Navigator.FirstName : "-", 
                    team.AC, 
                    team.Color);
                dgvr.Cells[5].Style.BackColor = getColor(team.Color);
                dataGridView1.Rows.Add(dgvr);
                lstTeamIdPilotNavNames.Add(getCrewName(team));
            }
        }

        private string getCrewName(Team team)
        {
            string pilName = team.Pilot != null ? team.Pilot.LastName + " " + team.Pilot.FirstName : " - ";
            string navName = team.Navigator != null ? "|" + team.Navigator.LastName + " " + team.Navigator.FirstName : " - ";
            return team.Id.ToString() + "|" + pilName + navName;
        }

        private void UpdateEnablement()
        {
            btnExport.Enabled = dataGridView1.RowCount > 1;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadLists();
            UpdateEnablement();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            String dirPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + @"\AirNavigationRace\";
            DirectoryInfo di = Directory.CreateDirectory(dirPath);
            if (!di.Exists)
            {
                di.Create();
            }
            PDFCreator.CreateTeamsPDF(Client.SelectedCompetition.Team.ToList(), Client, dirPath +
                @"\CrewsPrintout_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf");

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells ==null)
            {
                return;
            }
            Team team = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Tag as Team;
            Client.DBContext.TeamSet.Remove(team);
            Client.DBContext.SaveChanges();

            this.BeginInvoke(new MethodInvoker(LoadLists));
            this.BeginInvoke(new MethodInvoker(UpdateEnablement));
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Show a modal dialog and determine if DialogResult = OK.
            // NOTE: must set the button property accordingly
            int rIdx = -1;
            if (dataGridView1.SelectedCells.Count == 0)
            {
                return;
            }
            else
            {
                rIdx = dataGridView1.SelectedCells[0].RowIndex;
            }
            List<Subscriber> lstSubsc = Client.SelectedCompetition.Subscriber.ToList();
            List<Team> lstCrews = Client.SelectedCompetition.Team.ToList();
            Team ts = dataGridView1.Rows[rIdx].Tag as Team;
            if (ts == null)
            {
                ts = new Team();
                ts.Competition = Client.SelectedCompetition;
                ts.Color = "Black";
            }

            using (TeamDialog frmTeamDialog = new TeamDialog(ts, lstSubsc, lstCrews, lstTeamIdPilotNavNames))
            {
                DialogResult rRes = frmTeamDialog.ShowDialog();
                if (rRes == DialogResult.Cancel)
                {
                    //do nothing
                }

                if (rRes == DialogResult.OK)
                {
                    Team tm = frmTeamDialog.SelectedTeam;
                    if (tm.Id == 0)
                    {
                        Client.DBContext.TeamSet.Add(tm);

                        DataGridViewRow dgvr = new DataGridViewRow();
                        dgvr.CreateCells(dataGridView1);
                        dgvr.SetValues(
                            tm.CNumber,
                            tm.Nationality != null ? tm.Nationality : "",
                            tm.Pilot.LastName + " " + tm.Pilot.FirstName,
                            (tm.Navigator != null) ? tm.Navigator.LastName + " " + tm.Navigator.FirstName : "-",
                            tm.AC,
                            tm.Color);
                        dgvr.Tag = tm;
                        dataGridView1.Rows.Add(dgvr);
                    }
                    Client.DBContext.SaveChanges();
                    this.BeginInvoke(new MethodInvoker(LoadLists));
                    this.BeginInvoke(new MethodInvoker(UpdateEnablement));
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].Cells[1] != null && dataGridView1.SelectedRows[0].Cells[1].Value != null)
                {
                    Team tm = dataGridView1.SelectedRows[0].Tag as Team;
                }

                if (dataGridView1.SelectedRows[0].Index == dataGridView1.NewRowIndex)
                {

                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
       //     btnEdit_Click(sender, e);
        }


        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string str = "CNum: " + e.Row.Cells[0].Value.ToString() + "\nPilot: " + e.Row.Cells[2].Value.ToString() + "\nNavigator: " + e.Row.Cells[3].Value.ToString();
            if (MessageBox.Show(string.Format("Delete the selected Crew:\n\n {0} ?", str), "Delete Crew", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            btnDelete_Click(sender, e);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {  // user may use ENTER to add new rows or change rows
            if (e.KeyCode == Keys.Enter)
            {
                btnEdit_Click(sender, e);
            } 
        }

        public static Color getColor(string strColor)
        {
            Color c = Color.FromName(strColor);
            if (c.A == 0 && c.B == 0 && c.G == 0 && c.R == 0)
            {
                ColorConverter cc = new ColorConverter();
                return (Color)cc.ConvertFromString("#" + strColor);
            }
            else
            {
                return c;
            }
        }
    }
}
