using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Globalization;
using SharpKml.Engine;
using SharpKml.Base;
using SharpKml.Dom;
using AirNavigationRaceLive.Comps.ANRRouteGenerator;
using System.Linq;
using System.Xml.Linq;

namespace AirNavigationRaceLive.Comps
{
    public partial class RouteInverter : UserControl
    {
        private Client.DataAccess Client;
        private string FileNameKML;

        const string STYLENAME = "PolygonAndLine";
        const bool HAS_MARKERS = true;
        const bool CREATE_PROH_AREA = true;
        const bool USE_STANDARD_ORDER = true;

        private string[] arrRouteNames = { "A", "B", "C", "D" };
        private string[] arrNBLNames = { "NBLINE-A", "NBLINE-B", "NBLINE-C", "NBLINE-D" };


        public String RouteName;
        public List<Vector> RoutePoints = new List<Vector>();
        public List<List<Vector>> ListOfRoutes = new List<List<Vector>>();
        public List<string> ListOfRouteNames = new List<string>();
        public List<List<Vector>> ListOfNBL = new List<List<Vector>>();
        public List<string> ListOfNBLNames = new List<string>();

        public RouteInverter(Client.DataAccess iClient)
        {
            Client = iClient;
            InitializeComponent();
        }

        private void btnSelectKML_Click(object sender, EventArgs e)
        {
            string FileFilter = "KML Files|*.kml|All Files|*.*";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Load existing KML layer file with routes";
            //ofd.RestoreDirectory = true;
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.Filter = FileFilter;
            //ofd.FilterIndex = 5;
            ofd.FileOk += new CancelEventHandler(ofd_FileOkSelectKML);
            ofd.ShowDialog();
        }

        void ofd_FileOkSelectKML(object sender, CancelEventArgs e)
        {
            AirNavigationRaceLiveMain.SetStatusText("");

            OpenFileDialog ofd = sender as OpenFileDialog;
            string fName = ofd.FileName;
            FileNameKML = ofd.FileName;

            if (fName == string.Empty)
            {
                return;
            }

            KmlFile file;
            try
            {
                using (FileStream stream = File.Open(fName, FileMode.Open))
                {
                    file = KmlFile.Load(stream);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            if ((file != null) && (file.Root != null))
            {
                AirNavigationRaceLiveMain.SetStatusText(string.Format("Route Inverter - loaded file {0}", ofd.FileName));
            }

            string FileFilter = "KML Files|*.kml|All Files|*.*";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Invert ANR routes and save as KML";
            sfd.FileName = FileNameKML.Replace(".kml", "_inverted.kml");
            sfd.CheckFileExists = false;
            sfd.RestoreDirectory = true;
            sfd.Filter = FileFilter;
            //ofd.FilterIndex = 5;
            sfd.FileOk += new CancelEventHandler(ofd_FileOkSaveKML);
            sfd.ShowDialog();

        }

        void ofd_FileOkSaveKML(object sender, CancelEventArgs e)
        {
            AirNavigationRaceLiveMain.SetStatusText("");

            XDocument xDocInverted;
            if (GetInvertedRoutes(FileNameKML, out xDocInverted))
            {
                SaveFileDialog sfd = sender as SaveFileDialog;
                string fname = sfd.FileName;
                xDocInverted.Save(sfd.FileName);

                AirNavigationRaceLiveMain.SetStatusText(string.Format("Route Inverter - saved file {0}", sfd.FileName));
            }
        }

        public bool GetInvertedRoutes(string filepath, out XDocument gpxDocInverted)
        {
            // on an existing KML parcour, rename all SP and FP lines
            // this will allow to run a parcours in the opposite direction
            // must switch the names value for STARTPOINT- and ENDPOINT-
            // must manipulate the 3 coordinate points (point 1 --> to point 0 and 2, point 0 --> point 1)
            //

            const string SP_NAME = "STARTPOINT-";
            const string FP_NAME = "ENDPOINT-";


            XNamespace nsKml = XNamespace.Get("http://www.opengis.net/kml/2.2");
            XDocument gpxDoc = XDocument.Load(filepath);
            var folders = from flder in gpxDoc.Descendants(nsKml + "Folder")
                          where flder.Element(nsKml + "name").Value.ToString().Trim() == "LiveTracking"
                          select flder;

            if (folders.Count() == 0)
            {
                throw new ApplicationException("Cannot import kml data.\r\nData is expected to be in a kml folder named 'LiveTracking', but this folder is missing from the imported file.", null);
            }
            XElement el = new XElement(nsKml + "description", "WARNING: this route has been inverted. The Start- and Final Gates are switched");
            folders.FirstOrDefault().Add(el);

            foreach (var placemark in folders.Elements(nsKml + "Placemark"))
            {
                string pmName = placemark.Element(nsKml + "name").Value.Trim();

                if (pmName.StartsWith(SP_NAME))
                {
                    placemark.Element(nsKml + "name").Value = pmName.Replace(SP_NAME, FP_NAME);

                    foreach (var coord in placemark.Descendants(nsKml + "coordinates"))
                    {
                        string[] splittedPoints = Helper.Importer.ReversedKMLCoordinateString(coord.Value).Split(' ');
                        // re-order points 0 and 1. 
                        // 2 is technically identical with 0, must also be replaced
                        splittedPoints[2] = splittedPoints[1];
                        splittedPoints[1] = splittedPoints[0];
                        splittedPoints[0] = splittedPoints[2];
                        coord.Value = string.Join(" ", splittedPoints);
                    }
                }
                else if (pmName.StartsWith(FP_NAME))
                {
                    placemark.Element(nsKml + "name").Value = pmName.Replace(FP_NAME, SP_NAME);

                    foreach (var coord in placemark.Descendants(nsKml + "coordinates"))
                    {
                        string[] splittedPoints = Helper.Importer.ReversedKMLCoordinateString(coord.Value).Split(' ');
                        // re-order points 0 and 1. 
                        // 2 is technically identical with 0, must also be replaced
                        splittedPoints[2] = splittedPoints[1];
                        splittedPoints[1] = splittedPoints[0];
                        splittedPoints[0] = splittedPoints[2];
                        coord.Value = string.Join(" ", splittedPoints);
                    }
                }

            }

            gpxDocInverted = gpxDoc;
            return true;
            //gpxDoc.Save(filepath.Replace(".kml", "_OUT_Inverted.kml"));
        }

    }
}