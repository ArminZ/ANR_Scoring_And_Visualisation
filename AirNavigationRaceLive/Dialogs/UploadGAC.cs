using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using AirNavigationRaceLive.Client;
using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Dialogs
{
    public partial class UploadGAC : Form
    {
        private DataAccess Client;
        private FlightSet ct;

        public EventHandler OnFinish;
        public UploadGAC(DataAccess Client, FlightSet ct)
        {
            this.Client = Client;
            this.ct = ct;
            InitializeComponent();
            dateGAC.Enabled=false;

        }

        private void btnImportGAC_Click(object sender, EventArgs e)
        {
            textBoxPositions.Text = String.Empty;
            textBoxPositions.Tag = null;
            OpenFileDialog ofd = new OpenFileDialog();
            string FileFilter = "GAC  (*.gac)|*.gac";
            ofd.Title = "GAC Import";
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
                string dt = string.Empty;
                string WarningText = String.Empty;
                DateTime? CompDate0 = new DateTime();
                DateTime CompDate = new DateTime();
                bool isValidDate = Importer.GACFileHasValidDate(ofd.FileName, out CompDate0);
                //dateTimePicker1.Value = CompDate;
                dateGAC.Text = String.IsNullOrEmpty(CompDate0.ToString()) ? String.Empty : ((DateTime)CompDate0).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                btnUploadData.Visible = isValidDate;

                if (Importer.lstWarnings.Count > 0)
                {
                    string res = string.Join("\n", Importer.lstWarnings) + "\nDefine the correct date (default: actual date):";
                    string strCompDate = DateTime.Today.ToString("ddMMyy");
                    if (InputBoxClass.InputBox("Invalid Date", res, ref strCompDate) == DialogResult.OK)
                    {
                        CompDate = DateTime.ParseExact(strCompDate, "ddMMyy", CultureInfo.InvariantCulture);
                        dateGAC.Text = CompDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        btnUploadData.Visible = true;
                    }
                }
                List<Point> list = Importer.GPSdataFromGAC(ofd.FileName, CompDate);

                textBoxPositions.Text = list.Count.ToString();
                textBoxPositions.Tag = list;
                if (Importer.lstWarnings.Count>0)
                {
                    string res = string.Join("\n", Importer.lstWarnings);
                    MessageBox.Show(res, "Import warnings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, ex.ToString(), "Error while Parsing File",MessageBoxButtons.OK,MessageBoxIcon.Error);
                dateGAC.Text = string.Empty;
                return;
            }
            UpdateEnablement();
        }
        public void UpdateEnablement()
        {
            btnUploadData.Enabled = textBoxPositions.Tag != null && !String.IsNullOrEmpty(dateGAC.Text);
        }

        private void btnUploadData_Click(object sender, EventArgs e)
        {
            if (textBoxPositions.Tag != null && textBoxPositions.Text !="0")
            {
                List<Point> list = textBoxPositions.Tag as List<Point>;
                Client.DBContext.Point.RemoveRange(ct.Point);
                foreach (Point point in list)
                {
                    ct.Point.Add(point);
                }
                Client.DBContext.SaveChanges();
                GeneratePenalty.CalculateAndPersistPenaltyPoints(Client, ct);
                OnFinish.Invoke(null, null);
                Close();
            }
        }

        //private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        //{
        //    dateGAC.Text = dateTimePicker1.Value.ToShortDateString();
        //}
    }
}
