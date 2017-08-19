using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Comps
{
    public partial class Pilot : UserControl
    {
        private Client.DataAccess Client;
        private SubscriberSet selectedSubscriber = null;
        private SubscriberSet subscrDeleted;

        public Pilot(Client.DataAccess iClient)
        {
            Client = iClient;
            InitializeComponent();
        }

        private void Pilot_Load(object sender, EventArgs e)
        {
            LoadData();
            UpdateEnablement();
        }

        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.RowHeadersWidth = 30;

            List<SubscriberSet> pilots = Client.SelectedCompetition.SubscriberSet.ToList();
            foreach (SubscriberSet p in pilots)
            {
                DataGridViewRow dgvr = new DataGridViewRow();
                dgvr.CreateCells(dataGridView1);
                string[] arr = new string[] { p.Id.ToString(), p.LastName, p.FirstName };
                dgvr.Tag = p;
                dgvr.SetValues(arr);
                dataGridView1.Rows.Add(dgvr);
            }

            groupBoxParticipants.Text = string.Format("{0} - Participants", Client.SelectedCompetition.Name);
        }

        private void UpdateEnablement()
        {
            btnAddPicture.Enabled = selectedSubscriber != null;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetFields();
            LoadData();
            UpdateEnablement();
        }

        private void ResetFields()
        {
            textBoxLastname.Text = "";
            textBoxFirstName.Text = "";
            selectedSubscriber = null;
            PictureBox.Image = global::AirNavigationRaceLive.Properties.Resources._default;
            UpdateEnablement();
        }

        private void btnAddPicture_Click(object sender, EventArgs e)
        {
            string FileFilter = "JPG Files (*.jpg, *.jpeg, *.jpe, *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|"
            + "Bitmap Files (*.bmp)|*.bmp|"
            + "Gif Files (*.gif)|*.gif|"
            + "Png Files (*.png)|*.png";
            string GraphicFileFilter = "All Picture Files|*.jpg;*.jpeg;*.jpe;*.jfif;*.bmp;*.gif;*.png";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "t_Picture";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.Filter = FileFilter + "|" + GraphicFileFilter;
            ofd.FilterIndex = 5;
            ofd.FileOk += new CancelEventHandler(ofd_FileOk);
            DialogResult dr = new DialogResult();
            dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                savePicture(false);
            }
        }

        void savePicture(Boolean isResetMode)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            SubscriberSet pilot = dataGridView1.SelectedRows[0].Tag as SubscriberSet;
            if (pilot == null)
            {
                return;
            }

            if (PictureBox.Tag == null)
            {
                MemoryStream ms = new MemoryStream();
                PictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                PictureSet pic = new PictureSet();
                pic.Data = ms.ToArray();
                pilot.PictureSet = pic;
            }
            if (pilot.Id == 0)
            {
                Client.DBContext.SubscriberSet.Add(pilot);
            }
            if (isResetMode)
            {
                pilot.PictureSet = null;
                PictureBox.Image = global::AirNavigationRaceLive.Properties.Resources._default;
            }
            Client.DBContext.SaveChanges();
            this.BeginInvoke(new MethodInvoker(UpdateEnablement));


        }

        void ofd_FileOk(object sender, CancelEventArgs e)
        {
            OpenFileDialog ofd = sender as OpenFileDialog;
            PictureBox.Image = Image.FromFile(ofd.FileName);
            PictureBox.Tag = null;
            UpdateEnablement();
        }

        private void btnResetPicture_Click(object sender, EventArgs e)
        {
            savePicture(true);
        }

        private void btnPilotsImport_Click(object sender, EventArgs e)
        {
            string CSVFileFilter = "CSV and Text Files (*.csv, *.txt)|*.csv;*.txt| All files (*.*)| *.*";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Pilot List CSV Import";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.Filter = CSVFileFilter;
            //ofd.FilterIndex = 5;
            ofd.FileOk += new CancelEventHandler(ofd_PilotsListCSVOk);
            ofd.ShowDialog();
        }

        void ofd_PilotsListCSVOk(object sender, CancelEventArgs e)
        {
            if (Client.DBContext.SubscriberSet.Where(x => x.CompetitionSet.Id == Client.SelectedCompetition.Id).Count() > 0)
            {
                if (MessageBox.Show("Remove all existing competitors of this competition?", "Competitor List", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    // remove existing competitors of this competition
                    Client.DBContext.SubscriberSet.RemoveRange(
                        Client.DBContext.SubscriberSet.Where(x => x.CompetitionSet.Id == Client.SelectedCompetition.Id)
                        );
                }
            }

            OpenFileDialog ofd = sender as OpenFileDialog;
            List<SubscriberSet> lst = Importer.getPilotsListCSV(ofd.FileName);
            foreach (var item in lst)
            {
                item.CompetitionSet = Client.SelectedCompetition;
                Client.DBContext.SubscriberSet.Add(item);
            }
            Client.DBContext.SaveChanges();
            ResetFields();
            LoadData();
            UpdateEnablement();
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
            SubscriberSet pilot = dataGridView1.SelectedRows[0].Tag as SubscriberSet;
            textBoxLastname.Text = pilot.LastName;
            textBoxFirstName.Text = pilot.FirstName;
            selectedSubscriber = pilot;
            if (pilot.PictureSet != null)
            {
                MemoryStream ms = new MemoryStream(pilot.PictureSet.Data);
                PictureBox.Image = System.Drawing.Image.FromStream(ms);
            }
            else
            {
                PictureBox.Image = global::AirNavigationRaceLive.Properties.Resources._default;
            }
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
                if (!e.Row.IsNewRow)
                {
                    string str = 
                       "(" + e.Row.Cells[0].Value.ToString() + ") "+ e.Row.Cells[1].Value.ToString() + " " + e.Row.Cells[2].Value.ToString();
                    if (MessageBox.Show(string.Format("Delete the selected Participant:\n {0} ?", str), "Delete Participant", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }
                SubscriberSet pilot = e.Row.Tag as SubscriberSet;
                    subscrDeleted = pilot;
                }
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (subscrDeleted != null)
            {
                    //cscDel = e.Row.Tag as Subscriber;
                    Client.DBContext.SubscriberSet.Remove(subscrDeleted);
                    subscrDeleted = null;
                    Client.DBContext.SaveChanges();
                    textBoxLastname.Clear();
                    textBoxFirstName.Clear();
                    this.BeginInvoke(new MethodInvoker(LoadData));
            }
        }

        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!dataGridView1.IsCurrentRowDirty)
            {
                return;
            }
            var lastNameVal = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue;
            var firstNameVal = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue;
            var iDval = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue;
            SubscriberSet subsc = new SubscriberSet();
            if (string.IsNullOrEmpty(lastNameVal.ToString().Trim()) || string.IsNullOrEmpty(firstNameVal.ToString().Trim()))
            {
                dataGridView1.Rows[e.RowIndex].ErrorText = "Empty values are not allowed";
                e.Cancel = true;
                return;
            }

            if (isDuplicateParticipant(firstNameVal.ToString().Trim(), lastNameVal.ToString().Trim(), iDval.ToString()))
            {
                dataGridView1.Rows[e.RowIndex].ErrorText = "This name exists already in the list of participants";
                e.Cancel = true;
                return;
            }
            dataGridView1.Rows[e.RowIndex].ErrorText = string.Empty;

            if (string.IsNullOrEmpty(iDval.ToString()))
            {
                subsc.LastName = lastNameVal.ToString().Trim();
                subsc.FirstName = firstNameVal.ToString().Trim();
                subsc.CompetitionSet = Client.SelectedCompetition;
                dataGridView1.Rows[e.RowIndex].Tag = subsc;
                Client.DBContext.SubscriberSet.Add(subsc);
                Client.DBContext.SaveChanges();
            }
            else
            {
                subsc = dataGridView1.Rows[e.RowIndex].Tag as SubscriberSet;
                subsc.LastName = lastNameVal.ToString().Trim();
                subsc.FirstName = firstNameVal.ToString().Trim();
                Client.DBContext.SaveChanges();
            }

            this.BeginInvoke(new MethodInvoker(LoadData));
        }

        private bool isDuplicateParticipant(string firstName, string lastName, string id)
        {  // check if the new or changed participant already exists in the participants list

            List<SubscriberSet> pilots = Client.SelectedCompetition.SubscriberSet.Where(p => p.LastName == lastName && p.FirstName == firstName && p.Id.ToString() != id).ToList<SubscriberSet>();
            return pilots.Count > 0;
        }
    }
}
