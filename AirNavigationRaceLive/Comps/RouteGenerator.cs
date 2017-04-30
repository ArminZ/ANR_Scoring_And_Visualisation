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

namespace AirNavigationRaceLive.Comps
{
    public partial class RouteGenerator : UserControl
    {
        private Client.DataAccess Client;
        private string FileNameKML;

        const string STYLENAME = "PolygonAndLine";
        const double DEFAULT_CHANNEL_WIDTH = 0.4;
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

        public RouteGenerator(Client.DataAccess iClient)
        {
            Client = iClient;
            InitializeComponent(); 
            isValidated();
            txtChannelWidth.Text = DEFAULT_CHANNEL_WIDTH.ToString();
        }

        private void btnSelectKML_Click(object sender, EventArgs e)
        {
            string FileFilter = "KML Files|*.kml|All Files|*.*";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Load KML file with Route center lines";
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

            RoutePoints = new List<Vector>();
            ListOfRoutes = new List<List<Vector>>();
            ListOfRouteNames = new List<string>();
            ListOfNBL = new List<List<Vector>>();
            ListOfNBLNames = new List<string>();

            treeViewAvailableRoutes.Nodes.Clear();
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
                Kml kml = file.Root as Kml;
                TreeView trv = treeViewAvailableRoutes;
                ExtractPlaceMarkLineStrings(kml.Feature, trv);
                btnAddRoute.Visible = true;
                chkAddAllRoutes.Visible = true;
                lblSelectedRoutes.Visible = true;
                btnClearSelectedRoutes.Visible = true;
                treeViewSelectedRoutes.Visible = true;
            }
            AirNavigationRaceLiveMain.SetStatusText(string.Format("Route Generator - loaded file {0}", ofd.FileName));

        }

        private void btnSaveKML_Click(object sender, EventArgs e)
        {
            // save ANR data

            string FileFilter = "KML Files|*.kml|All Files|*.*";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Generate ANR routes and save as KML";
            sfd.FileName = FileNameKML.Replace(".kml", "_out.kml");
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
            double channelRadius;
            double altitude;
            // retrieve points from selected tree element

            if (ListOfRouteNames.Count == 0)
            {
                return;
            }

            bool ret = double.TryParse(txtChannelWidth.Text, out channelRadius);
            if (!ret)
            {
                channelRadius = DEFAULT_CHANNEL_WIDTH / 2.0;
            }
            else
            {
                channelRadius = channelRadius / 2.0;
            }

            ret = double.TryParse(txtHeight.Text, out altitude);
            if (!ret)
            {
                altitude = 300;
            }

            SaveFileDialog sfd = sender as SaveFileDialog;
            string fname = sfd.FileName;
            ANRData anrData = new ANRData();
            anrData.generateParcour(ListOfRoutes, ListOfRouteNames, ListOfNBL, ListOfNBLNames, HAS_MARKERS, CREATE_PROH_AREA, USE_STANDARD_ORDER, channelRadius, STYLENAME, altitude);
            Document document = anrData.Document;
            Kml kml = new Kml();
            kml.Feature = document;
            KmlFile file = KmlFile.Create(kml, false);
            using (var stream = System.IO.File.Open(fname, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                file.Save(stream);
            }
            AirNavigationRaceLiveMain.SetStatusText(string.Format("Route Generator - saved file {0}", sfd.FileName));
        }

        private void btnAddRoute_Click(object sender, EventArgs e)
        {
            int idxStart = 0;
            int idxEnd = treeViewAvailableRoutes.Nodes.Count;
            if (!chkAddAllRoutes.Checked)
            {
                if (treeViewAvailableRoutes.SelectedNode == null)
                {
                    return;
                }
                else
                {
                    // add only the selected route
                    idxStart = treeViewAvailableRoutes.SelectedNode.Index;
                    idxEnd = treeViewAvailableRoutes.SelectedNode.Index + 1;
                }
            }

            for (int i = idxStart; i < idxEnd; i++)
            {
                int first = treeViewAvailableRoutes.Nodes[i].Text.IndexOf("(");
                string strName = treeViewAvailableRoutes.Nodes[i].Text.Substring(0, first).Trim();
                if (arrRouteNames.Any(s => strName.Contains(s)))
                {
                    List<Vector> lst = (List<Vector>)treeViewAvailableRoutes.Nodes[i].Tag;
                    ListOfRoutes.Add(lst);
                    ListOfRouteNames.Add(strName);
                    treeViewSelectedRoutes.Nodes.Add(strName);
                }
                else
                {
                    MessageBox.Show(String.Format("The selected name <{0}> in the KML file is not valid. Valid names for ANR routes are A, B, C or D. ", strName), String.Format("KML Path name <{0}>", strName), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            isValidated();
        }

        private void btnClearSelected_Click(object sender, EventArgs e)
        {
            treeViewSelectedRoutes.Nodes.Clear();
            ListOfRoutes.Clear();
            ListOfRouteNames.Clear();
            isValidated();
        }

        private void ExtractPlaceMarkLineStrings(Feature feature, TreeView trv)
        {
            // Google Earth Paths are PlaceMarks with a Geometry property of type LineString
            // Extract these and add to the treeview object (include also the points)

            // Is the passed in value a Placemark?
            Placemark placemark = feature as Placemark;
            if (placemark != null && placemark.Geometry is LineString)
            {
                LineString ln = (LineString)placemark.Geometry;

                //if (ln.Coordinates.Count > 2)   // must have at least 2 points
                //{
                if (arrRouteNames.Any(s => placemark.Name.ToUpper() == (s)))
                {
                    TreeNode node = trv.Nodes.Add(placemark.Name + " (Path with " + ln.Coordinates.Count.ToString() + " Points)");
                    // add coordinates as object to the Tag property
                    node.Tag = new List<Vector>(ln.Coordinates);
                }
                if (arrNBLNames.Any(s => placemark.Name.ToUpper().Contains(s)))
                {
                    // add NonBacktrack lines directly to list
                    ListOfNBL.Add(new List<Vector>(ln.Coordinates));
                    ListOfNBLNames.Add(placemark.Name.ToUpper());
                }
                // }
            }
            else
            {
                // Is it a Container, as the Container might have a child Placemark?
                SharpKml.Dom.Container container = feature as SharpKml.Dom.Container;
                if (container != null)
                {
                    // Check each Feature to see if it's a Placemark or another Container
                    foreach (var f in container.Features)
                    {
                        ExtractPlaceMarkLineStrings(f, trv);
                    }
                }
            }
        }

        private void txtChannelWidth_TextChanged(object sender, EventArgs e)
        {
            isValidated();
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            isValidated();
        }

        private void isValidated()
        {
            bool _isValidated = true;
            double _val;
            errorProvider1.Clear();

            if (!double.TryParse(txtChannelWidth.Text, out _val))
            {
                errorProvider1.SetError(txtChannelWidth, "Invalid number format");
            }

            if (_val < 0.1 || _val > 2.0)
            {
                errorProvider1.SetError(txtChannelWidth, "Value must be between 0.1 NM and 2.0 NM");
            }

            if (!double.TryParse(txtHeight.Text, out _val))
            {
                errorProvider1.SetError(txtHeight, "Invalid number format");
            }
            if (_val < 100 || _val > 600)
            {
                errorProvider1.SetError(txtHeight, "Value must be between 100m and 600m");
            }

            string strErr = errorProvider1.GetError(txtHeight) + errorProvider1.GetError(txtChannelWidth);
            _isValidated = string.IsNullOrEmpty(strErr) && treeViewSelectedRoutes.Nodes.Count > 0;
            btnSaveKML.Enabled = _isValidated;
        }
    }
}