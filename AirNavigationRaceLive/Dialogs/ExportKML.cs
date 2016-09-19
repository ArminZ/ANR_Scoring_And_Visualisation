using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AirNavigationRaceLive.Comps.Client;
using NetworkObjects;
using System.IO;
using System.Globalization;

namespace AirNavigationRaceLive.Dialogs
{
    public partial class ExportKML : Form
    {
        private DataAccess Client;
        public ExportKML(DataAccess Client)
        {
            this.Client = Client;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Item p = parcour.SelectedItem as Item;
            if (p != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "ANRL-Export.kml";
                sfd.DefaultExt = ".kml";
                sfd.OverwritePrompt = true;
                sfd.RestoreDirectory = true;
                sfd.Title = "Export ANRL-Parcour to KML";
                sfd.FileOk += new CancelEventHandler(sfd_FileOk);
                sfd.ShowDialog();
            }
        }

        void sfd_FileOk(object sender, CancelEventArgs e)
        {
            Item item = parcour.SelectedItem as Item;
            SaveFileDialog sfd = sender as SaveFileDialog;
            if (!e.Cancel && item != null && sfd != null)
            {
                string result = GetPolygonKml(item.p);
                File.WriteAllText(sfd.FileName, result);                
            }
        }

        private void ExportKML_Load(object sender, EventArgs e)
        {
            parcour.Items.Clear();
            foreach (Parcour p in Client.SelectedCompetition.Parcour)
            {
                parcour.Items.Add(new Item(p));
            }
        }

        private class Item
        {
            public Parcour p;
            public Item(Parcour p)
            {
                this.p = p;
            }

            public override string ToString()
            {
                return "ID:" + p.Id + " Name: " + p.Name;
            }
        }

        private string GetPolygonKml(Parcour parcour)
        {
            // double values in XML use always dot as decimal separator
            // we must explicitly set the CultureInfo, otherwise this will work onkly for dot as decimal separator
            CultureInfo ci = CultureInfo.InvariantCulture;
            int HeightPenalty = (int)height.Value;
            String result = "";
            result += GetKMLTemplateContent("headerPolygon");
            int i = 0;
            foreach (Line n in parcour.Line.Where(p => p.Type == (int)LineType.PENALTYZONE))
            {
                result += @"<Placemark><name>Polygon" + i++ + @"</name><styleUrl>#sn_ylw-pushpin</styleUrl><Polygon><extrude>1</extrude><altitudeMode>relativeToGround</altitudeMode><outerBoundaryIs><LinearRing><coordinates>";
                result += n.A.longitude.ToString(ci) + "," + n.A.latitude.ToString(ci) + "," + HeightPenalty + " ";
                result += n.O.longitude.ToString(ci) + "," + n.O.latitude.ToString(ci) + "," + HeightPenalty + " ";
                result += n.B.longitude.ToString(ci) + "," + n.B.latitude.ToString(ci) + "," + HeightPenalty + " ";
                result += n.A.longitude.ToString(ci) + "," + n.A.latitude.ToString(ci) + "," + HeightPenalty + " ";
                result += @"</coordinates></LinearRing></outerBoundaryIs></Polygon></Placemark>";
            }
            foreach (Line n in parcour.Line.Where(p => p.Type >= 3 && p.Type <= 10))
            {
                result += @"<Placemark><name>Polygon" + i++ + @"</name><styleUrl>#sn_ylw-pushpin</styleUrl><Polygon><extrude>1</extrude><altitudeMode>relativeToGround</altitudeMode><outerBoundaryIs><LinearRing><coordinates>";
                result += n.B.longitude.ToString(ci) + "," + n.B.latitude.ToString(ci) + "," + HeightPenalty + " ";
                result += n.A.longitude.ToString(ci) + "," + n.A.latitude.ToString(ci) + "," + HeightPenalty + " ";
                result += n.B.longitude.ToString(ci) + "," + n.B.latitude.ToString(ci) + "," + HeightPenalty + " ";
                result += @"</coordinates></LinearRing></outerBoundaryIs></Polygon></Placemark>";
            }
            result += GetKMLTemplateContent("footerPolygon");
            return result;
        }

        private string GetKMLTemplateContent(string Filename)
        {
            return File.ReadAllText(@"Resources\KMLTemplates\" + Filename + ".kml");
        }
    }
}
