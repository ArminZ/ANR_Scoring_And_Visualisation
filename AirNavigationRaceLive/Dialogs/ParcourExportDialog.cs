using System;
using System.Windows.Forms;
using AirNavigationRaceLive.Comps.Helper;
using System.Xml;
using AirNavigationRaceLive.Comps.ANRRouteGenerator;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;

namespace AirNavigationRaceLive.Dialogs
{
    public partial class ParcourExportDialog : Form
    {
        public ParcourExportDialog()
        {
            InitializeComponent();
            btnExportParcourCoord.Enabled = false;
        }

        private bool hasValidationErrors( object sender)
        {
            bool hasErrors = false;
            errorProvider1.Clear();
            if (String.IsNullOrEmpty(txtOutputFile.Text) || !System.IO.Directory.Exists(Path.GetDirectoryName(txtOutputFile.Text)))
            {
                errorProvider1.SetError(txtOutputFile, "Invalid directory");
                hasErrors = true;
            }
            if (String.IsNullOrEmpty(txtInputFile.Text) || !System.IO.File.Exists(txtInputFile.Text))
            {
                errorProvider1.SetError(txtInputFile, "Invalid file name");
                hasErrors = true;
            }
            btnExportParcourCoord.Enabled = !hasErrors;
            return hasErrors;
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            txtInputFile.Clear();
            txtOutputFile.Clear();
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string FileFilter = "GoogleEarth KML (*.kml)|*.kml|All Files (*.*)|*.*";
            ofd.Title = "Route input / Parcour Layer KML file";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.Filter = FileFilter;
            ofd.FilterIndex = 1;
            ofd.ShowDialog();
            txtInputFile.Text = ofd.FileName;
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            string FileFilter = "Text file (*.txt)|*.txt|All Files (*.*)|*.*";
            sfd.Title = "Coordinates of Route input / Parcour Layer";
            sfd.RestoreDirectory = true;
            sfd.CheckPathExists = true;
            sfd.Filter = FileFilter;
            sfd.FilterIndex = 1;
            sfd.ShowDialog();
            txtOutputFile.Text = sfd.FileName;
        }

        private void btnExportParcourCoord_Click(object sender, EventArgs e)
        {
            string fnameIn = txtInputFile.Text;
            string fnameOut = txtOutputFile.Text;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(fnameIn);
                ANRData anr = new ANRData();
                List<string> lst = anr.exportParcourCoordinates(xmlDoc);
                File.WriteAllLines(fnameOut, anr.exportParcourCoordinates(xmlDoc).ToArray());
            }
            catch (ApplicationException ex1)
            {
                MessageBox.Show(ex1.Message,"Parcour import from *.kml",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error while Parsing kml File");
            }
        }

        private void txtOutputFile_TextChanged(object sender, EventArgs e)
        {
            hasValidationErrors(sender);
        }

        private void txtInputFile_TextChanged(object sender, EventArgs e)
        {
            hasValidationErrors(sender);
        }
    }
}
