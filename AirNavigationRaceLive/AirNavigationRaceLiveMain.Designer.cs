namespace AirNavigationRaceLive
{
    partial class AirNavigationRaceLiveMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AirNavigationRaceLiveMain));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.competitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromWorldfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.legacyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parcourToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overviewZoomedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pilotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qualificationRoundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.routeGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.routeEportCoordinatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cH1903converterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualisationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.statusStrip.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusStripLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 724);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(1611, 30);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip";
            // 
            // StatusStripLabel
            // 
            this.StatusStripLabel.Name = "StatusStripLabel";
            this.StatusStripLabel.Size = new System.Drawing.Size(137, 25);
            this.StatusStripLabel.Text = "StatusStripLabel";
            // 
            // MainMenu
            // 
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.competitionToolStripMenuItem,
            this.mapToolStripMenuItem,
            this.parcourToolStripMenuItem,
            this.pilotsToolStripMenuItem,
            this.teamsToolStripMenuItem,
            this.qualificationRoundsToolStripMenuItem,
            this.resultsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.visualisationToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.MainMenu.Size = new System.Drawing.Size(1611, 33);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "Menu";
            // 
            // competitionToolStripMenuItem
            // 
            this.competitionToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.competitionToolStripMenuItem.Name = "competitionToolStripMenuItem";
            this.competitionToolStripMenuItem.Size = new System.Drawing.Size(138, 29);
            this.competitionToolStripMenuItem.Text = "Competition";
            this.competitionToolStripMenuItem.Click += new System.EventHandler(this.competitionToolStripMenuItem1_Click);
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFromWorldfileToolStripMenuItem,
            this.legacyToolStripMenuItem});
            this.mapToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(77, 29);
            this.mapToolStripMenuItem.Text = "Maps";
            // 
            // importFromWorldfileToolStripMenuItem
            // 
            this.importFromWorldfileToolStripMenuItem.Name = "importFromWorldfileToolStripMenuItem";
            this.importFromWorldfileToolStripMenuItem.Size = new System.Drawing.Size(311, 30);
            this.importFromWorldfileToolStripMenuItem.Text = "Import maps";
            this.importFromWorldfileToolStripMenuItem.Click += new System.EventHandler(this.mapToolStripMenuItem_Click);
            // 
            // legacyToolStripMenuItem
            // 
            this.legacyToolStripMenuItem.Name = "legacyToolStripMenuItem";
            this.legacyToolStripMenuItem.Size = new System.Drawing.Size(311, 30);
            this.legacyToolStripMenuItem.Text = "Legacy MapSet import";
            this.legacyToolStripMenuItem.Click += new System.EventHandler(this.legacyToolStripMenuItem_Click);
            // 
            // parcourToolStripMenuItem
            // 
            this.parcourToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.overviewToolStripMenuItem,
            this.overviewZoomedToolStripMenuItem,
            this.toolStripSeparator3,
            this.importToolStripMenuItem,
            this.toolStripSeparator1,
            this.editToolStripMenuItem});
            this.parcourToolStripMenuItem.Enabled = false;
            this.parcourToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.parcourToolStripMenuItem.Name = "parcourToolStripMenuItem";
            this.parcourToolStripMenuItem.Size = new System.Drawing.Size(110, 29);
            this.parcourToolStripMenuItem.Text = "Parcours";
            this.parcourToolStripMenuItem.Click += new System.EventHandler(this.parcourToolStripMenuItem_Click);
            // 
            // overviewToolStripMenuItem
            // 
            this.overviewToolStripMenuItem.Name = "overviewToolStripMenuItem";
            this.overviewToolStripMenuItem.Size = new System.Drawing.Size(269, 30);
            this.overviewToolStripMenuItem.Text = "Overview";
            this.overviewToolStripMenuItem.Click += new System.EventHandler(this.overviewToolStripMenuItem_Click);
            // 
            // overviewZoomedToolStripMenuItem
            // 
            this.overviewZoomedToolStripMenuItem.Name = "overviewZoomedToolStripMenuItem";
            this.overviewZoomedToolStripMenuItem.Size = new System.Drawing.Size(269, 30);
            this.overviewZoomedToolStripMenuItem.Text = "Overview Zoomed";
            this.overviewZoomedToolStripMenuItem.Visible = false;
            this.overviewZoomedToolStripMenuItem.Click += new System.EventHandler(this.overviewZoomedToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(266, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(269, 30);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(266, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(269, 30);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Visible = false;
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // pilotsToolStripMenuItem
            // 
            this.pilotsToolStripMenuItem.Enabled = false;
            this.pilotsToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.pilotsToolStripMenuItem.Name = "pilotsToolStripMenuItem";
            this.pilotsToolStripMenuItem.Size = new System.Drawing.Size(137, 29);
            this.pilotsToolStripMenuItem.Text = "Participants";
            this.pilotsToolStripMenuItem.Click += new System.EventHandler(this.pilotsToolStripMenuItem_Click);
            // 
            // teamsToolStripMenuItem
            // 
            this.teamsToolStripMenuItem.Enabled = false;
            this.teamsToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.teamsToolStripMenuItem.Name = "teamsToolStripMenuItem";
            this.teamsToolStripMenuItem.Size = new System.Drawing.Size(84, 29);
            this.teamsToolStripMenuItem.Text = "Crews";
            this.teamsToolStripMenuItem.Click += new System.EventHandler(this.teamsToolStripMenuItem_Click);
            // 
            // qualificationRoundsToolStripMenuItem
            // 
            this.qualificationRoundsToolStripMenuItem.Enabled = false;
            this.qualificationRoundsToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.qualificationRoundsToolStripMenuItem.Name = "qualificationRoundsToolStripMenuItem";
            this.qualificationRoundsToolStripMenuItem.Size = new System.Drawing.Size(223, 29);
            this.qualificationRoundsToolStripMenuItem.Text = "Qualification Rounds";
            this.qualificationRoundsToolStripMenuItem.Click += new System.EventHandler(this.racesToolStripMenuItem_Click);
            // 
            // resultsToolStripMenuItem
            // 
            this.resultsToolStripMenuItem.Enabled = false;
            this.resultsToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.resultsToolStripMenuItem.Name = "resultsToolStripMenuItem";
            this.resultsToolStripMenuItem.Size = new System.Drawing.Size(96, 29);
            this.resultsToolStripMenuItem.Text = "Results";
            this.resultsToolStripMenuItem.Click += new System.EventHandler(this.resultsToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.routeGeneratorToolStripMenuItem,
            this.routeEportCoordinatesToolStripMenuItem,
            this.cH1903converterToolStripMenuItem});
            this.toolsToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(95, 29);
            this.toolsToolStripMenuItem.Text = "Tools...";
            // 
            // routeGeneratorToolStripMenuItem
            // 
            this.routeGeneratorToolStripMenuItem.Name = "routeGeneratorToolStripMenuItem";
            this.routeGeneratorToolStripMenuItem.Size = new System.Drawing.Size(377, 30);
            this.routeGeneratorToolStripMenuItem.Text = "Route Generator";
            this.routeGeneratorToolStripMenuItem.Click += new System.EventHandler(this.routeGeneratorToolStripMenuItem_Click);
            // 
            // routeEportCoordinatesToolStripMenuItem
            // 
            this.routeEportCoordinatesToolStripMenuItem.Name = "routeEportCoordinatesToolStripMenuItem";
            this.routeEportCoordinatesToolStripMenuItem.Size = new System.Drawing.Size(377, 30);
            this.routeEportCoordinatesToolStripMenuItem.Text = "Route Coordinates Exporter";
            this.routeEportCoordinatesToolStripMenuItem.Click += new System.EventHandler(this.kmlCoordinateExportToolStripMenuItem_Click);
            // 
            // cH1903converterToolStripMenuItem
            // 
            this.cH1903converterToolStripMenuItem.Name = "cH1903converterToolStripMenuItem";
            this.cH1903converterToolStripMenuItem.Size = new System.Drawing.Size(377, 30);
            this.cH1903converterToolStripMenuItem.Text = "Legacy Coordinate Converter";
            this.cH1903converterToolStripMenuItem.Click += new System.EventHandler(this.cH1903converterToolStripMenuItem_Click);
            // 
            // visualisationToolStripMenuItem
            // 
            this.visualisationToolStripMenuItem.Enabled = false;
            this.visualisationToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.visualisationToolStripMenuItem.Name = "visualisationToolStripMenuItem";
            this.visualisationToolStripMenuItem.Size = new System.Drawing.Size(146, 29);
            this.visualisationToolStripMenuItem.Text = "Visualisation";
            this.visualisationToolStripMenuItem.Visible = false;
            this.visualisationToolStripMenuItem.Click += new System.EventHandler(this.visualisationToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(151, 29);
            this.exportToolStripMenuItem.Text = "Import/Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(102, 29);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionToolStripMenuItem,
            this.historyToolStripMenuItem});
            this.aboutToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(80, 29);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.creditsToolStripMenuItem_Click);
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(169, 30);
            this.versionToolStripMenuItem.Text = "Version";
            this.versionToolStripMenuItem.Click += new System.EventHandler(this.versionToolStripMenuItem_Click);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(169, 30);
            this.historyToolStripMenuItem.Text = "History";
            this.historyToolStripMenuItem.Click += new System.EventHandler(this.historyToolStripMenuItem_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 33);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(4);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1611, 691);
            this.MainPanel.TabIndex = 2;
            this.MainPanel.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            this.MainPanel.Resize += new System.EventHandler(this.MainPanel_Resize);
            // 
            // AirNavigationRaceLiveMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1611, 754);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.MainMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AirNavigationRaceLiveMain";
            this.Text = "Air Navigation Race Scoring & Visualisation";
            this.Load += new System.EventHandler(this.AirNavigationRaceLive_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusStripLabel;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.ToolStripMenuItem pilotsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qualificationRoundsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visualisationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem teamsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parcourToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem legacyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overviewZoomedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem competitionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFromWorldfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem routeGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cH1903converterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem routeEportCoordinatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    }
}

