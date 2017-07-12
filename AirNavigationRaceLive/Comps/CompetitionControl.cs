using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Comps
{
    public partial class CompetitionControl : UserControl
    {
        public event EventHandler Connected;
        private Client.DataAccess c;

        private volatile bool active = false;
        private CompetitionSet compDeleted;
        private int Idx = -1;  // index of the added or changed row

        public CompetitionControl(Client.DataAccess client)
        {
            InitializeComponent();
            c = client;
            reloadCompetitions();
            UpdateEnablement();
           // dataGridView1.RowHeadersWidth = 30;

        }
        private void UpdateEnablement()
        {
            bool loggedIn = !active;
            btnUse.Enabled = loggedIn; // && !string.IsNullOrEmpty(textBoxSelectedComp.Text);
        }

        private void reloadCompetitions()
        {
            List<CompetitionSet> list = c.DBContext.CompetitionSet.ToList();
            dataGridView1.Rows.Clear();
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            foreach (CompetitionSet cs in list)
            {
                DataGridViewRow dgvr = new DataGridViewRow();
                dgvr.Tag = cs;
                dgvr.CreateCells(dataGridView1);
                dgvr.SetValues(cs.Id, cs.Name);
                dataGridView1.Rows.Add(dgvr);
            }
            if (Idx>=0)
            {
                // set selected Row / Current cell 
                dataGridView1.Rows[Idx].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[Idx].Cells[1];
            }
        }


        private void btnUse_Click(object sender, EventArgs e)
        {
            try
            {
                active = true;
                UpdateEnablement();
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    CompetitionSet cs = dataGridView1.SelectedRows[0].Tag as CompetitionSet;
                    if (cs != null)
                    {
                        c.SelectedCompetition = dataGridView1.SelectedRows[0].Tag as CompetitionSet;
                        UpdateEnablement();
                        Status.SetStatus("Connected to Server, download finished");
                        Connected.Invoke(c, e);
                    }
                }
            }
            finally
            {
                active = false;
                UpdateEnablement();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            reloadCompetitions();
            UpdateEnablement();
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (!e.Row.IsNewRow)
            {
                string str = e.Row.Cells[0].Value.ToString() + " " + e.Row.Cells[1].Value.ToString();
                if (MessageBox.Show(string.Format("Delete the selected Competition:\n {0} ?", str), "Delete Competition", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                compDeleted = e.Row.Tag as CompetitionSet;
            }
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (compDeleted != null)
            {
                c.DBContext.CompetitionSet.Remove(compDeleted);
                compDeleted = null;
                c.DBContext.SaveChanges();
                Idx = -1;
                this.BeginInvoke(new MethodInvoker(reloadCompetitions));
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].Cells[1] != null && dataGridView1.SelectedRows[0].Cells[1].Value != null)
                {
                    textBoxSelectedComp.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                }
            }
        }

        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!dataGridView1.IsCurrentRowDirty)
            {
                return;
            }

            var idVal = dataGridView1.Rows[e.RowIndex].Cells[0].EditedFormattedValue;
            var compVal = dataGridView1.Rows[e.RowIndex].Cells[1].EditedFormattedValue;
            string newCompName = compVal.ToString().Trim();

            if (string.IsNullOrEmpty(newCompName))
            {
                dataGridView1.Rows[e.RowIndex].ErrorText = "Empty values are not allowed";
                e.Cancel = true;
                return;
            }

            if (isDuplicateCompetition(newCompName, idVal.ToString()))
            {
                dataGridView1.Rows[e.RowIndex].ErrorText = "A CompetitionSet with this name is already listed";
                e.Cancel = true;
                return;
            }

            if (string.IsNullOrEmpty(idVal.ToString()))
            {
                CompetitionSet comp = new CompetitionSet();
                comp.Name = newCompName;
                c.DBContext.CompetitionSet.Add(comp);
                c.DBContext.SaveChanges();
            }
            else
            {
                CompetitionSet comp = dataGridView1.Rows[e.RowIndex].Tag as CompetitionSet;
                comp.Name = compVal.ToString().Trim();
                c.DBContext.SaveChanges();
            }
            Idx = e.RowIndex;
            dataGridView1.Rows[e.RowIndex].ErrorText = "";
            this.BeginInvoke(new MethodInvoker(reloadCompetitions));
        }

        private bool isDuplicateCompetition(string name, string id)
        {  // check if the new or changed CompetitionSet already exists in the  list
            List<CompetitionSet> competitions = c.DBContext.CompetitionSet.Where(x => x.Name == name && x.Id.ToString() != id).ToList<CompetitionSet>();
            return competitions.Count > 0;
        }
    }

    //class RoleCombo
    //{
    //    public NetworkObjects.Access role;
    //    public RoleCombo(NetworkObjects.Access role)
    //    {
    //        this.role = role;
    //    }
    //    public override string ToString()
    //    {
    //        return System.Enum.GetName(NetworkObjects.Access.Admin.GetType(), role);
    //    }
    //}
}
