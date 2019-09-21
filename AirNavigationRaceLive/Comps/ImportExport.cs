using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AirNavigationRaceLive.Dialogs;
using System.IO;
using OfficeOpenXml;
using System.Globalization;
using AirNavigationRaceLive.Model;
using AirNavigationRaceLive.ModelExtensions;
using AirNavigationRaceLive.Comps.Helper;

namespace AirNavigationRaceLive.Comps
{
    public partial class ImportExport : UserControl
    {
        private Client.DataAccess Client;
        private AsyncCallback OnSaveAsGPXCompleted;

        const string C_TimeFormat = "HH:mm:ss";
        const double C_Timespan_StartPlanningToTKOF = 45.0;
        const double C_Timespan_EndPlanningToTKOF = 15.0;

        public ImportExport(Client.DataAccess Client)
        {
            this.Client = Client;
            InitializeComponent();
        }

        private void btnExportKLM_Click(object sender, EventArgs e)
        {
            new ExportKML(Client).Show();
        }

        public void ImportExport_Load(object sender, EventArgs e)
        {
            List<QualificationRoundSet> rounds = Client.SelectedCompetition.QualificationRoundSet.ToList();
            comboBoxQualificationRound.Items.Clear();
            foreach (QualificationRoundSet round in rounds)
            {
                comboBoxQualificationRound.Items.Add(new ComboQRExtension(round));
            }
            groupBox1.Text = string.Format("{0} - Parcour export", Client.SelectedCompetition.Name);
            groupBox2.Text = string.Format("{0} - Excel Data import/export", Client.SelectedCompetition.Name);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExportExcel.Enabled = comboBoxQualificationRound.SelectedItem != null;
            btnSyncExcel.Enabled = btnExportExcel.Enabled;
            btnExportGpx.Enabled = btnExportExcel.Enabled;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (comboBoxQualificationRound.SelectedItem != null)
            {
                ComboQRExtension item = comboBoxQualificationRound.SelectedItem as ComboQRExtension;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "AirNavigationRaceStartList.xlsx";
                sfd.Title = "Export Startlist";
                sfd.ShowDialog();
                String filename = sfd.FileName;
                ExportToExcel(item, filename);
            }
        }

        private void btnSyncExcel_Click(object sender, EventArgs e)
        {
            if (comboBoxQualificationRound.SelectedItem != null)
            {
                ComboQRExtension item = comboBoxQualificationRound.SelectedItem as ComboQRExtension;
                OpenFileDialog sfd = new OpenFileDialog();
                sfd.FileName = "AirNavigationRaceStartList.xlsx";
                sfd.Title = "Sync Startlist";
                sfd.ShowDialog();
                String filename = sfd.FileName;
                ImportFromExcel(item, filename);
                ExportToExcel(item, filename);
            }
        }

        private void ExportToExcel(ComboQRExtension item, string filename)
        {
            File.WriteAllBytes(filename, Properties.Resources.Template);
            FileInfo newFile = new FileInfo(filename);
            ExcelPackage pck = new ExcelPackage(newFile);
            ExcelWorksheet Participants = pck.Workbook.Worksheets.First(p => p.Name == "Participants");
            ExcelWorksheet Teams = pck.Workbook.Worksheets.First(p => p.Name == "Crews");
            ExcelWorksheet StartList = pck.Workbook.Worksheets.First(p => p.Name == "StartList");
            int i = 2;
            foreach (SubscriberSet sub in Client.SelectedCompetition.SubscriberSet.OrderBy(p => p.LastName))
            {
                Participants.Cells[("A" + i)].Value = sub.LastName;
                Participants.Cells[("B" + i)].Value = sub.FirstName;
                i++;
            }
            i = 2;
            foreach (TeamSet t in Client.SelectedCompetition.TeamSet.OrderBy(p => int.Parse(p.CNumber)))
            {
                Teams.Cells[("A" + i)].Value = int.Parse(t.CNumber);
                Teams.Cells[("B" + i)].Value = t.Nationality;
                Teams.Cells[("C" + i)].Value = t.Pilot.LastName + " " + t.Pilot.FirstName;
                Teams.Cells[("D" + i)].Value = t.Navigator == null ? "" : t.Navigator.LastName + " " + t.Navigator.FirstName;
                Teams.Cells[("E" + i)].Value = t.AC;
                i++;
            }
            i = 2;
            foreach (FlightSet f in item.q.FlightSet.OrderBy(p => p.StartID))
            {
                //if (i == 2)
                //{
                //    StartList.Cells[("J" + i)].Value = new DateTime(f.TimeTakeOff).ToString(C_TimeFormat, DateTimeFormatInfo.InvariantInfo));
                //}
                StartList.Cells[("A" + i)].Value = f.StartID;
                StartList.Cells[("B" + i)].Value = int.Parse(f.TeamSet.CNumber);
                StartList.Cells[("C" + i)].Value = f.TeamSet.AC;
                string pilot = f.TeamSet.Pilot.LastName + " " + f.TeamSet.Pilot.FirstName;
                string navigator = "";
                if (f.TeamSet.Navigator != null)
                {
                    navigator = " - " + f.TeamSet.Navigator.LastName + " " + f.TeamSet.Navigator.FirstName;
                }
                string crew = pilot + navigator;
                DateTime dt = new DateTime(f.TimeTakeOff);
                StartList.Cells[("D" + i)].Value = crew;
                StartList.Cells[("E" + i)].Value = dt.AddMinutes(-C_Timespan_EndPlanningToTKOF).ToString(C_TimeFormat, DateTimeFormatInfo.InvariantInfo);
                StartList.Cells[("F" + i)].Value = dt.AddMinutes(-C_Timespan_EndPlanningToTKOF).ToString(C_TimeFormat, DateTimeFormatInfo.InvariantInfo);
                StartList.Cells[("G" + i)].Value = dt.ToString(C_TimeFormat, DateTimeFormatInfo.InvariantInfo);
                StartList.Cells[("H" + i)].Value = new DateTime(f.TimeStartLine).ToString(C_TimeFormat, DateTimeFormatInfo.InvariantInfo);
                StartList.Cells[("I" + i)].Value = new DateTime(f.TimeEndLine).ToString(C_TimeFormat, DateTimeFormatInfo.InvariantInfo);
                StartList.Cells[("J" + i)].Value = ((Route)f.Route).ToString();
                i++;
            }
            pck.Save();
        }

        private void ImportFromExcel(ComboQRExtension item, string filename)
        {
            FileInfo newFile = new FileInfo(filename);
            ExcelPackage pck = new ExcelPackage(newFile);
            ExcelWorksheet Participants = pck.Workbook.Worksheets.First(p => p.Name == "Participants");
            ExcelWorksheet Teams = pck.Workbook.Worksheets.First(p => p.Name == "Crews");
            ExcelWorksheet StartList = pck.Workbook.Worksheets.First(p => p.Name == "StartList");
            int i = 2;
            while (i < 200)
            {
                string LastName = Participants.Cells[("A" + i)].Value as string;
                string FirstName = Participants.Cells[("B" + i)].Value as string;
                if (LastName != null && FirstName != null && LastName != "" && FirstName != "")
                {
                    if (!Client.SelectedCompetition.SubscriberSet.Any(p => p.LastName == LastName && p.FirstName == FirstName))
                    {
                        SubscriberSet sub = new SubscriberSet();
                        sub.CompetitionSet = Client.SelectedCompetition;
                        sub.LastName = LastName;
                        sub.FirstName = FirstName;
                        Client.DBContext.SubscriberSet.Add(sub);
                    }
                }
                else
                {
                    break;
                }
                i++;
            }
            Client.DBContext.SaveChanges();
            i = 2;
            while (i < 200)
            {
                double? cNumber = Teams.Cells[("A" + i)].Value as double?;
                string nationality = Teams.Cells[("B" + i)].Value as string;
                string pilot = Teams.Cells[("C" + i)].Value as string;
                string navigator = Teams.Cells[("D" + i)].Value as string;
                string ac = Teams.Cells[("E" + i)].Value as string;
                if (cNumber.HasValue && pilot != null && pilot != "")
                {
                    SubscriberSet pilotS = Client.SelectedCompetition.SubscriberSet.First(p => pilot.Contains(p.FirstName) && pilot.Contains(p.LastName));
                    SubscriberSet navigatorS = null;
                    if (navigator != null && navigator != "")
                    {
                        navigatorS = Client.SelectedCompetition.SubscriberSet.First(p => navigator.Contains(p.FirstName) && navigator.Contains(p.LastName));
                    }
                    TeamSet t = null;
                    if (Client.SelectedCompetition.TeamSet.Any(p => p.CNumber == ((int)cNumber.Value).ToString()))
                    {
                        t = Client.SelectedCompetition.TeamSet.First(p => p.CNumber == ((int)cNumber.Value).ToString());
                    }
                    else
                    {
                        t = new TeamSet();
                        t.CompetitionSet = Client.SelectedCompetition;
                        Client.DBContext.TeamSet.Add(t);
                    }
                    t.Pilot = pilotS;
                    t.Navigator = navigatorS;
                    t.CNumber = ((int)cNumber.Value).ToString();
                    t.Nationality = nationality;
                    t.AC = ac;
                }
                else
                {
                    break;
                }
                i++;
            }
            Client.DBContext.SaveChanges();
            i = 2;
            DateTime? date = null;
            while (i < 200)
            {
                if (i == 2)
                {
                    date = StartList.Cells[("J" + i)].Value as DateTime?;
                }
                double? startId = StartList.Cells[("A" + i)].Value as double?;
                double? cNumber = StartList.Cells[("B" + i)].Value as double?;
                double? takeOff = StartList.Cells[("E" + i)].Value as double?;
                double? start = StartList.Cells[("F" + i)].Value as double?;
                double? end = StartList.Cells[("G" + i)].Value as double?;
                string route = StartList.Cells[("H" + i)].Value as string;
                if (date != null && date.HasValue && takeOff != null && start != null && end != null && startId.HasValue && cNumber.HasValue && takeOff.HasValue && start.HasValue && end.HasValue && route != null)
                {
                    FlightSet f = null;
                    if (item.q.FlightSet.Any(p => p.StartID == startId.Value))
                    {
                        f = item.q.FlightSet.First(p => p.StartID == startId.Value);
                    }
                    else
                    {
                        f = new FlightSet();
                        f.QualificationRoundSet = item.q;
                        f.StartID = ((int)startId.Value);
                        Client.DBContext.FlightSet.Add(f);
                    }
                    f.TeamSet = Client.SelectedCompetition.TeamSet.First(p => p.CNumber == ((int)cNumber.Value).ToString());
                    f.Route = (int)Enum.Parse(typeof(Route), route, true);
                    DateTime d = date.Value;
                    DateTime to = DateTime.FromOADate(takeOff.Value);
                    DateTime st = DateTime.FromOADate(start.Value);
                    DateTime en = DateTime.FromOADate(end.Value);
                    f.TimeTakeOff = new DateTime(d.Year, d.Month, d.Day, to.Hour, to.Minute, to.Second).Ticks;
                    f.TimeStartLine = new DateTime(d.Year, d.Month, d.Day, st.Hour, st.Minute, st.Second).Ticks;
                    f.TimeEndLine = new DateTime(d.Year, d.Month, d.Day, en.Hour, en.Minute, en.Second).Ticks;
                }
                else
                {
                    break;
                }
                i++;
            }
            Client.DBContext.SaveChanges();
        }

        //private class QualiComboBoxItem
        //{
        //    public QualificationRoundSet q;
        //    public QualiComboBoxItem(QualificationRoundSet q)
        //    {
        //        this.q = q;
        //    }

        //    public override string ToString()
        //    {
        //        return q.Name;
        //    }
        //}

        private void btnExportGpx_Click(object sender, EventArgs e)
        {
            {
                SaveFileDialog fbd = new SaveFileDialog();
                fbd.Filter = "all files (*.*)| *.*";
                fbd.InitialDirectory = Path.GetDirectoryName(Environment.SpecialFolder.Recent.ToString());
                fbd.FileName = "File.gpx";
                fbd.DefaultExt = ".gpx";
                fbd.OverwritePrompt = true;
                fbd.RestoreDirectory = true;
                fbd.Title = "Export Flights";

                ComboQRExtension item = comboBoxQualificationRound.SelectedItem as ComboQRExtension;
                QualificationRoundSet q = item.q;
                if (fbd.ShowDialog() == DialogResult.OK )
                {
                    // Invoke the SaveAsGPX method on a new thread.
                    Action<string, QualificationRoundSet> invoker = new Action<string, QualificationRoundSet>(SaveAsGPX);
                    invoker.BeginInvoke(Path.GetDirectoryName(fbd.FileName),q, OnSaveAsGPXCompleted, invoker);
                }
            }
        }

        protected void OnSaveSaveAsGPXCompleted(IAsyncResult result)
        {
            Action<string, QualificationRoundSet> invoker = (Action<string, QualificationRoundSet>)result.AsyncState;
            invoker.EndInvoke(result);
            // perform other actions after the file has been saved (also occurs on non-UI thread)
        }

        private void SaveAsGPX( string fileDir, QualificationRoundSet q)
        {
            // for a selected Qualification Round, save flight data as *.gpx
            CultureInfo ci = CultureInfo.InvariantCulture;
            string _cName = Client.SelectedCompetition.Name;
            foreach (TeamSet t in Client.SelectedCompetition.TeamSet.OrderBy(p => int.Parse(p.CNumber)))
            {
                foreach (FlightSet flt in t.FlightSet.Where(x=>x.QualificationRoundSet == q))
                {
                   string  _nameQr = _cName + "_" + flt.QualificationRoundSet.Name;
                    string _nameNav = flt.TeamSet.Navigator != null ? flt.TeamSet.Navigator.LastName : "";
                    string _name2 = _nameQr + " " + flt.Id + " " + flt.TeamSet.Pilot.LastName + "_" + _nameNav;
                    string _nameShort = "Crew " + flt.TeamSet.CNumber;
                    var pts = flt.Point;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    sb.Append("<gpx xmlns='http://www.topografix.com/GPX/1/1' version='1.1'>");
                    //sb.Append("<metadata><link href='" + "http://www.fai.org" + "'><text>"+ _name2 +" </text></link></metadata>");
                    sb.Append("<trk>");
                    sb.Append("<name>"+ _nameShort + "</name>");
                    sb.Append("<trkseg>");
                    foreach (var data in pts)
                    {
                        sb.Append("<trkpt lat=\"" + data.latitude.ToString(ci) + "\" lon =\"" + data.longitude.ToString(ci) + "\">");
                        sb.Append("<ele>" + data.altitude.ToString(ci) + "</ele>");
                        sb.Append("<time>" + new DateTime(data.Timestamp).ToString("yyyy-MM-ddTHH:mm:ssZ") + "</time>");
                        sb.Append("<desc>" + String.Format("<![CDATA[lat.={0}, lon.={1}, Alt.={2}m. Speed={3}m/h.]]>", data.latitude.ToString(ci), data.longitude.ToString(ci), data.altitude.ToString(ci), "0.0") + "</desc>");
                        sb.Append("</trkpt>");
                    }
                    sb.Append("</trkseg></trk>");
                    sb.Append("</gpx>");
                    System.IO.File.WriteAllText(System.IO.Path.Combine(fileDir, _name2 + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss")+".gpx"), sb.ToString());
                }
            }
            //MessageBox.Show("Download of *.gpx files completed", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }

}
