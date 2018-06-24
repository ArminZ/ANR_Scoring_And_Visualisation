using System;
using System.Windows.Forms;
using AirNavigationRaceLive.Comps;
using AirNavigationRaceLive.Client;
using AirNavigationRaceLive.Dialogs;

namespace AirNavigationRaceLive
{
    public partial class AirNavigationRaceLiveMain : Form
    {
        private static AirNavigationRaceLiveMain main;
        private DataAccess Client;
        private CompetitionControl CompetitionO;
        private About About;
        private Pilot Pilot;
        private TeamControl Team;
        private QualificationRoundControl QualificationRound;
        private MapControl Map;
        private ParcourImport ParcourImport;
        private ParcourOverview ParcourOverview;
        private MapLegacy MapLegacy;
        private ParcourOverviewZoomed ParcourOverviewZoomed;
        private Results Results;
        //private ParcourGen ParcourGen;
        //private Visualisation Visualisation;
        //private ParcourEditSingle ParcourEditSingle;
        private ParcourEdit ParcourEdit;
        //private MapSelection MapSelection;
        //private MapImportFromMaps MapImportFromMaps;  // removed functionality 15.9.2016
        private ImportExport ImportExport;
        private RouteGenerator RouteGenerator;
        private History History;
        private UserSettings UserSettings;

        public static void SetStatusText(String text)
        {
            if (main != null)
            {
                SetStatusCallback d = new SetStatusCallback(main.SetStatusTextCB);
                main.Invoke(d, new object[] { text });
            }
        }

        delegate void SetStatusCallback(String statusText);
        public void SetStatusTextCB(String statusText)
        {
            StatusStripLabel.Text = statusText;
        }

        public AirNavigationRaceLiveMain()
        {
            try
            {
                Client = DataAccess.Instance;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Exception - Connection to database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }

            InitializeComponent();
            main = this;
        }

        public void UpdateEnablement()
        {
            if (Client==null)
            {
                return;
            }
                Boolean connected = Client.SelectedCompetition != null;
                competitionToolStripMenuItem.Enabled = Client != null;
                mapToolStripMenuItem.Enabled = connected;
                parcourToolStripMenuItem.Enabled = connected;
                overviewZoomedToolStripMenuItem.Enabled = connected;
                overviewToolStripMenuItem.Enabled = connected;
                importToolStripMenuItem.Enabled = connected;
                pilotsToolStripMenuItem.Enabled = connected;
                teamsToolStripMenuItem.Enabled = connected;
                qualificationRoundsToolStripMenuItem.Enabled = connected;
                resultsToolStripMenuItem.Enabled = connected;
                exportToolStripMenuItem.Enabled = connected;  
                //generateToolStripMenuItem.Enabled = connected;
                //visualisationToolStripMenuItem.Enabled = connected;
                editToolStripMenuItem.Enabled = connected;

        }

        private void AirNavigationRaceLive_Load(object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();
            if (About == null)
            {
                About = new Comps.About();
            }
            enableControl(About);
            StatusStripLabel.Text = "Ready";
            MainPanel.Controls.Clear();
            CompetitionO = new CompetitionControl(Client);
            CompetitionO.Connected += new EventHandler(CompetitionO_Connected);
            enableControl(CompetitionO);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        void CompetitionO_Connected(object sender, EventArgs e)
        {
            DataAccess c = sender as DataAccess;
            if (c != null)
            {
                Client = c;
                Pilot = null;
                Team = null;
                QualificationRound = null;
                //Visualisation = null;
                ParcourEdit = null;
                //ParcourGen = null;
                Map = null;
                ParcourImport = null;
                ParcourOverviewZoomed = null;
                MapLegacy = null;
                Results = null;
                UpdateEnablement();
            }
            enableControl(About);
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Client = null;
            Pilot = null;
            Team = null;
            QualificationRound = null;
            //Visualisation = null;
            ParcourEdit = null;
            //ParcourGen = null; 
            Map = null;
            ParcourImport = null;
            ParcourOverviewZoomed = null;
            MapLegacy = null;
            Results = null;
            CompetitionO = null;
            StatusStripLabel.Text = string.Empty;
            UpdateEnablement();
            enableControl(About);
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enableControl(About);
            versionToolStripMenuItem.Text = "Version: " + Application.ProductVersion;
        }

        private void pilotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Pilot == null)
            {
                Pilot = new Pilot(Client);
            }
            enableControl(Pilot);
        }

        private void teamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Team == null)
            {
                Team = new TeamControl(Client);
            }
            enableControl(Team);
        }

        private void racesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (QualificationRound == null)
            {
                QualificationRound = new QualificationRoundControl(Client);
            }
            enableControl(QualificationRound);
        }

        private void visualisationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (Visualisation == null)
            //{
            //    Visualisation = new Visualisation(Client);
            //}
            //enableControl(Visualisation);
        }

        private void MainPanel_Resize(object sender, EventArgs e)
        {
            if (MainPanel.Controls.Count == 1)
            {
                resize();
            }
        }

        private void enableControl(Control c)
        {
            foreach (Control cc in MainPanel.Controls) { cc.Visible = false; }
            MainPanel.Controls.Clear();
            MainPanel.Controls.Add(c);
            c.Visible = true;
            UpdateEnablement();
            resize();
        }
        private void resize()
        {
            MainPanel.Controls[0].SetBounds(0, 0, MainPanel.Width, MainPanel.Height);
        }

        private void mapToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if (Map == null)
            {
                Map = new MapControl(Client);
            }
            enableControl(Map);
        }

        private void parcourToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void overviewToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ParcourOverview == null)
            {
                ParcourOverview = new ParcourOverview(Client);
            }
            enableControl(ParcourOverview);
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (ParcourGen == null)
            //{
            //    ParcourGen = new ParcourGen(Client);
            //}
            //enableControl(ParcourGen);
        }
        private void generateSingleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (ParcourEditSingle == null)
            //{
            //    ParcourEditSingle = new ParcourEditSingle(Client);
            //}
            //enableControl(ParcourEditSingle);
        }
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ParcourImport == null)
            {
                ParcourImport = new ParcourImport(Client);
            }
            enableControl(ParcourImport);
        }

        private void legacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MapLegacy == null)
            {
                MapLegacy = new MapLegacy(Client);
            }
            enableControl(MapLegacy);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ParcourEdit == null)
            {
                ParcourEdit = new ParcourEdit(Client);
            }
            enableControl(ParcourEdit);
        }

        private void overviewZoomedToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ParcourOverviewZoomed == null)
            {
                ParcourOverviewZoomed = new ParcourOverviewZoomed(Client);
            }
            enableControl(ParcourOverviewZoomed);
        }

        private void adjustResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Results == null)
            {
                Results = new Results(Client);
            }
            enableControl(Results);
        }

        private void competitionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CompetitionO != null)
            {
                enableControl(CompetitionO);
            }
        }

        private void resultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Results == null)
            {
                Results = new Results(Client);
            }
            enableControl(Results);
        }


        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ImportExport == null)
            {
                ImportExport = new ImportExport(Client);
            }
            ImportExport.ImportExport_Load(null, null);
            enableControl(ImportExport);
        }

        private void routeGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RouteGenerator == null)
            {
                RouteGenerator = new RouteGenerator(Client);
            }
            enableControl(RouteGenerator);
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        { }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (History == null)
            {
                History = new History();
            }
            enableControl(History);
        }

        private void cH1903converterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Calculator ConverterSwissCH1903 = new Calculator())
            {
                ConverterSwissCH1903.StartPosition = FormStartPosition.CenterParent;
                ConverterSwissCH1903.ShowDialog();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using (SettingsDialog settingsDlg = new SettingsDialog())
            //{
            //    settingsDlg.StartPosition = FormStartPosition.CenterParent;
            //    settingsDlg.ShowDialog();
            //}

            if (UserSettings == null)
            {
                UserSettings = new UserSettings();
            }
            enableControl(UserSettings);
        }

        private void kmlCoordinateExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ParcourExportDialog routeExportDialog = new ParcourExportDialog())
            {
                routeExportDialog.StartPosition = FormStartPosition.CenterParent;
                routeExportDialog.ShowDialog();
            }
        }
    }
}
