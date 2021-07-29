using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using AirNavigationRaceLive.Client;
using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Dialogs
{
    public partial class UploadLoggerData : Form
    {
        private DataAccess Client;
        private FlightSet ct;

        //private string[] C_FILE_TYPES = { "gac", "igc", "gpx" };
        private string extension = string.Empty;

        public EventHandler OnFinish;
        public UploadLoggerData(DataAccess Client, FlightSet ct)
        {
            this.Client = Client;
            this.ct = ct;
            InitializeComponent();
            textBoxDate.Enabled = false;

        }

        private void btnImportLogger_Click(object sender, EventArgs e)
        {
            textBoxRecords.Text = String.Empty;
            textBoxRecords.Tag = null;
            Importer.lstWarnings.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            string FileFilter = "GAC and IGC files (*.gac, *.igc)|*.gac;*.igc|GPX files (*.gpx)|*.gpx|GAC, IGC and GPX files (*.gac, *.igc, *.gpx)|*.gac;*.igc;*.gpx";
            ofd.Filter = FileFilter;
            ofd.FilterIndex = 3;
            ofd.Title = "Logger data import";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;

            ofd.FileOk += new CancelEventHandler(ofd_FileOk);
            ofd.ShowDialog();
        }

        void ofd_FileOk(object sender, CancelEventArgs e)
        {
            OpenFileDialog ofd = sender as OpenFileDialog;

            // set the value for extension
            extension = Path.GetExtension(ofd.FileName).ToLowerInvariant();
            // handle igc the same way as gac
            extension = extension == ".igc" ? ".gac" : extension;

            // set visibility
            labelTimeCorrectionHrs.Visible = extension == ".gac";
            numericUpDownTimeCorrectionHrs.Visible = extension == ".gac";

            try
            {
               
                switch (extension)
                { 

                    case ".gpx":
                        List<Point> listGPX = Importer.GPSdataFromGPX(ofd.FileName);
                        // show the first time element value of the *.gpx file. This is already in UTC
                        textBoxDate.Text = new DateTime((long)(listGPX[0].Timestamp)).ToString("yyyy-MM-ddTHH:mm:ssZ", DateTimeFormatInfo.InvariantInfo);
                        textBoxRecords.Text = listGPX.Count.ToString();
                        textBoxRecords.Tag = listGPX;
                        break;

                    case ".gac":
                        // used for igc and gac
                        string dt = string.Empty;
                        string WarningText = String.Empty;
                        DateTime? CompDate0 = new DateTime();
                        DateTime? CompFirstTime0 = new DateTime();
                        DateTime CompDate = new DateTime();

                        // read threshold date for GAC files (if date is older that the threshold date, the user will have to confirm or change the date)
                        DateTime dtThreshold = new DateTime(Properties.Settings.Default.GACFileWarningThresholdDate, DateTimeKind.Utc);

                        bool isValidDate = Importer.GACFileHasValidDate(ofd.FileName, out CompDate0, out CompFirstTime0);
                       // dateGAC.Text = String.IsNullOrEmpty(CompDate0.ToString()) ? String.Empty : ((DateTime)CompDate0).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        btnUploadData.Visible = isValidDate;

                        // the normal case
                        if (isValidDate && CompDate0 != null && CompFirstTime0 != null && ((DateTime)CompDate0) >= dtThreshold)
                        {
                            // combine date + time
                            CompDate = ((DateTime)CompDate0).Add(((DateTime)CompFirstTime0).TimeOfDay);
                            textBoxDate.Text = CompDate.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                            btnUploadData.Visible = true;
                        }

                        // date in GAC file line 2 is formally valid, but older than the threshold date
                        // this date threshold is selected based on experience  - in the ANR competition in Portugal (date was March 2004) 
                        if (isValidDate && CompDate0 != null && CompFirstTime0 != null && ((DateTime)CompDate0) < dtThreshold)
                        {
                            string res = "The date {0} (given as '{1}') is formally valid, but may be outdated/incorrect.";
                            string strCompDate = ((DateTime)CompDate0).ToString("ddMMyy");
                            res = string.Format(res,
                                        ((DateTime)CompDate0).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                                         strCompDate
                                        );
                            res = string.Join("\n", res) + "\nIf required, correct the date(default: original date, format: ddMMyy):";
                            if (InputBoxClass.InputBox("Check Date", res, ref strCompDate) == DialogResult.OK)
                            {
                                CompDate0 = DateTime.ParseExact(strCompDate, "ddMMyy", CultureInfo.InvariantCulture);
                                CompDate = ((DateTime)CompDate0).Add(((DateTime)CompFirstTime0).TimeOfDay);
                                textBoxDate.Text = CompDate.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                                btnUploadData.Visible = true;
                            }
                        }

                        // invalid date in GAC file line 2
                        if (!(isValidDate) && CompFirstTime0 != null)
                        {
                            // read file creation date which might be close to the correct date (if not, its probably close to the actual date)
                            FileInfo fi = new FileInfo(ofd.FileName);
                            DateTime dtFi = fi.CreationTime.Date;

                            string res = string.Join("\n", Importer.lstWarnings) + "\nDefine the correct date (default: file creation date):";
                            string strCompDate = dtFi.ToString("ddMMyy");
                           // string res = string.Join("\n", Importer.lstWarnings) + "\nDefine the correct date (default: actual date):";
                           // string strCompDate = DateTime.Today.ToString("ddMMyy");
                            if (InputBoxClass.InputBox("Invalid Date", res, ref strCompDate) == DialogResult.OK)
                            {
                                CompDate0 = DateTime.ParseExact(strCompDate, "ddMMyy", CultureInfo.InvariantCulture);
                                CompDate = ((DateTime)CompDate0).Add(((DateTime)CompFirstTime0).TimeOfDay);
                                textBoxDate.Text = CompDate.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                                btnUploadData.Visible = true;
                            }
                        }

                        List<Point> listGACIGC = Importer.GPSdataFromGAC(ofd.FileName, CompDate);

                        textBoxRecords.Text = listGACIGC.Count.ToString();
                        textBoxRecords.Tag = listGACIGC;
                        if (Importer.lstWarnings.Count > 0)
                        {
                            string res = string.Join("\n", Importer.lstWarnings);
                            MessageBox.Show(res, "Import warnings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, ex.ToString(), "Error while Parsing File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxDate.Text = string.Empty;
                return;
            }
            UpdateEnablement();
        }
        public void UpdateEnablement()
        {
            btnUploadData.Enabled = textBoxRecords.Tag != null && !String.IsNullOrEmpty(textBoxDate.Text);
        }

        private void btnUploadData_Click(object sender, EventArgs e)
        {
            double C_CORR_HRS = (double)numericUpDownTimeCorrectionHrs.Value;

            if (textBoxRecords.Tag != null && textBoxRecords.Text != "0")
            {
                List<Point> list = textBoxRecords.Tag as List<Point>;
                Client.DBContext.Point.RemoveRange(ct.Point);
                foreach (Point point in list)
                {
                    if (C_CORR_HRS > 0.0 && extension == ".gac")
                    {
                        // we add here a potential hour shift correction 
                        // NOTE: point.Timestamp is in Ticks
                        // use a Timespan and add x hours
                        // convert back to ticks (using the ticks property of the timespan)
                        TimeSpan ts = new TimeSpan(point.Timestamp);
                        ts += TimeSpan.FromHours(C_CORR_HRS);
                        point.Timestamp = ts.Ticks;
                    }
                    ct.Point.Add(point);
                }
                Client.DBContext.SaveChanges();
                GeneratePenalty.CalculateAndPersistPenaltyPoints(Client, ct);
                OnFinish.Invoke(null, null);
                Close();
            }
        }

    }
}
