using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using AirNavigationRaceLive.Client;
using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Dialogs
{
    public partial class UploadGPX : Form
    {
        private DataAccess Client;
        private FlightSet ct;

        public EventHandler OnFinish;
        public UploadGPX(DataAccess Client, FlightSet ct)
        {
            this.Client = Client;
            this.ct = ct;
            InitializeComponent();
        }

        

        public void UpdateEnablement()
        {
            btnUploadData.Enabled = textBoxRecords.Tag != null;
        }

        private void btnUploadData_Click(object sender, EventArgs e)
        {
            if (textBoxRecords.Tag != null)
            {
                List<Point> list = textBoxRecords.Tag as List<Point>;
                Client.DBContext.Point.RemoveRange(ct.Point);
                this.ct.Point.Clear();
                //foreach (Point point in list)
                //{
                //    this.ct.Point.Add(point);
                //}
               // this.ct.Point = list;
                Client.DBContext.Point.AddRange(list);
                this.ct.Point = list;
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
                textBoxDate.Text = new DateTime((long)(list[0].Timestamp)).ToShortDateString();
                textBoxRecords.Text = list.Count.ToString();
                textBoxRecords.Tag = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error while Parsing File");
            }
            UpdateEnablement();
        }

    }
}
