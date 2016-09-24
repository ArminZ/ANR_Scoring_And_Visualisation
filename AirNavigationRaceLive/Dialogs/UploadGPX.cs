using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using AirNavigationRaceLive.Comps.Client;
using AirNavigationRaceLive.Comps.Helper;

namespace AirNavigationRaceLive.Dialogs
{
    public partial class UploadGPX : Form
    {
        private DataAccess Client;
        private Flight ct;

        public EventHandler OnFinish;
        public UploadGPX(DataAccess Client, Flight ct)
        {
            this.Client = Client;
            this.ct = ct;
            InitializeComponent();
        }

        

        public void UpdateEnablement()
        {
            btnUploadData.Enabled = textBoxPositions.Tag != null;
        }

        private void btnUploadData_Click(object sender, EventArgs e)
        {
            if (textBoxPositions.Tag != null)
            {
                List<Point> list = textBoxPositions.Tag as List<Point>;
                Client.DBContext.PointSet.RemoveRange(ct.Point);
                this.ct.Point.Clear();
                foreach (Point point in list)
                {
                    this.ct.Point.Add(point);
                }
                Client.DBContext.SaveChanges();
                GeneratePenalty.CalculateAndPersistPenaltyPoints(Client, ct);
                OnFinish.Invoke(null, null);
                Close();
            }
        }
      
        private void btnImportGPX_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string FileFilter = "GPX  (*.gpx)|*.gpx";
            ofd.Title = "GPX Import";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.Filter = FileFilter;
            ofd.FileOk += new CancelEventHandler(ofd_FileOkGPX);
            ofd.ShowDialog();
        }

        void ofd_FileOkGPX(object sender, CancelEventArgs e)
        {
            OpenFileDialog ofd = sender as OpenFileDialog;
            try
            {
                List<Point> list = Importer.GPSdataFromGPX(ofd.FileName);
                textBoxPositions.Text = list.Count.ToString();
                textBoxPositions.Tag = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error while Parsing File");
            }
            UpdateEnablement();
        }

    }
}
