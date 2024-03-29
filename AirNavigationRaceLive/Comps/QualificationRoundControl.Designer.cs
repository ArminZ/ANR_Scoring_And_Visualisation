﻿namespace AirNavigationRaceLive.Comps
{
    partial class QualificationRoundControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnDeleteQualificationRound = new System.Windows.Forms.Button();
            this.btnRefreshCompetitions = new System.Windows.Forms.Button();
            this.listViewQualificationRound = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorProviderQualification = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBoxQualification = new System.Windows.Forms.GroupBox();
            this.groupBoxTKOFLine = new System.Windows.Forms.GroupBox();
            this.takeOffLeftLongitude = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.takeOffLeftLatitude = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.takeOffRightLatitude = new System.Windows.Forms.TextBox();
            this.takeOffRightLongitude = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnImportTKOFline = new System.Windows.Forms.Button();
            this.btnSwitchLeftRight = new System.Windows.Forms.Button();
            this.comboBoxParcour = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxStartList = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Crew = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TakeOff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.End = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Route = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeTakeOffToStartgateDuration = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.timeTakeOffInterval = new System.Windows.Forms.DateTimePicker();
            this.timeStartBlockInterval = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.btnExportToPDF = new System.Windows.Forms.Button();
            this.btnSaveQualificationRound = new System.Windows.Forms.Button();
            this.btnNewQualificationRound = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label19 = new System.Windows.Forms.Label();
            this.timeParcourDuration = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxGeneral = new System.Windows.Forms.GroupBox();
            this.btnAutoFillStartList = new System.Windows.Forms.Button();
            this.btnRecalcStartList = new System.Windows.Forms.Button();
            this.numericUpDownRoutes = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.lblQRound = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderQualification)).BeginInit();
            this.groupBoxQualification.SuspendLayout();
            this.groupBoxTKOFLine.SuspendLayout();
            this.groupBoxStartList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBoxGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRoutes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDeleteQualificationRound
            // 
            this.btnDeleteQualificationRound.Location = new System.Drawing.Point(0, 722);
            this.btnDeleteQualificationRound.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDeleteQualificationRound.Name = "btnDeleteQualificationRound";
            this.btnDeleteQualificationRound.Size = new System.Drawing.Size(307, 44);
            this.btnDeleteQualificationRound.TabIndex = 14;
            this.btnDeleteQualificationRound.Text = "Delete";
            this.btnDeleteQualificationRound.UseVisualStyleBackColor = true;
            this.btnDeleteQualificationRound.Click += new System.EventHandler(this.btnDeleteQualificationRound_Click);
            // 
            // btnRefreshCompetitions
            // 
            this.btnRefreshCompetitions.Location = new System.Drawing.Point(0, 829);
            this.btnRefreshCompetitions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRefreshCompetitions.Name = "btnRefreshCompetitions";
            this.btnRefreshCompetitions.Size = new System.Drawing.Size(307, 45);
            this.btnRefreshCompetitions.TabIndex = 13;
            this.btnRefreshCompetitions.Text = "Refresh";
            this.btnRefreshCompetitions.UseVisualStyleBackColor = true;
            this.btnRefreshCompetitions.Click += new System.EventHandler(this.btnRefreshCompetitions_Click);
            // 
            // listViewQualificationRound
            // 
            this.listViewQualificationRound.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewQualificationRound.FullRowSelect = true;
            this.listViewQualificationRound.GridLines = true;
            this.listViewQualificationRound.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewQualificationRound.HideSelection = false;
            this.listViewQualificationRound.Location = new System.Drawing.Point(4, 54);
            this.listViewQualificationRound.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewQualificationRound.MultiSelect = false;
            this.listViewQualificationRound.Name = "listViewQualificationRound";
            this.listViewQualificationRound.Size = new System.Drawing.Size(304, 503);
            this.listViewQualificationRound.TabIndex = 3;
            this.listViewQualificationRound.UseCompatibleStateImageBehavior = false;
            this.listViewQualificationRound.View = System.Windows.Forms.View.Details;
            this.listViewQualificationRound.SelectedIndexChanged += new System.EventHandler(this.listViewQualificationRound_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 251;
            // 
            // errorProviderQualification
            // 
            this.errorProviderQualification.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProviderQualification.ContainerControl = this;
            // 
            // groupBoxQualification
            // 
            this.groupBoxQualification.Controls.Add(this.groupBoxTKOFLine);
            this.groupBoxQualification.Controls.Add(this.btnImportTKOFline);
            this.groupBoxQualification.Controls.Add(this.btnSwitchLeftRight);
            this.groupBoxQualification.Controls.Add(this.comboBoxParcour);
            this.groupBoxQualification.Controls.Add(this.label8);
            this.groupBoxQualification.Controls.Add(this.textName);
            this.groupBoxQualification.Controls.Add(this.label2);
            this.groupBoxQualification.Location = new System.Drawing.Point(327, 54);
            this.groupBoxQualification.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxQualification.Name = "groupBoxQualification";
            this.groupBoxQualification.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxQualification.Size = new System.Drawing.Size(1004, 206);
            this.groupBoxQualification.TabIndex = 51;
            this.groupBoxQualification.TabStop = false;
            this.groupBoxQualification.Text = "Selected Qualification Round";
            // 
            // groupBoxTKOFLine
            // 
            this.groupBoxTKOFLine.Controls.Add(this.takeOffLeftLongitude);
            this.groupBoxTKOFLine.Controls.Add(this.label10);
            this.groupBoxTKOFLine.Controls.Add(this.label11);
            this.groupBoxTKOFLine.Controls.Add(this.takeOffLeftLatitude);
            this.groupBoxTKOFLine.Controls.Add(this.label13);
            this.groupBoxTKOFLine.Controls.Add(this.takeOffRightLatitude);
            this.groupBoxTKOFLine.Controls.Add(this.takeOffRightLongitude);
            this.groupBoxTKOFLine.Controls.Add(this.label12);
            this.groupBoxTKOFLine.Location = new System.Drawing.Point(313, 26);
            this.groupBoxTKOFLine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxTKOFLine.Name = "groupBoxTKOFLine";
            this.groupBoxTKOFLine.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxTKOFLine.Size = new System.Drawing.Size(462, 160);
            this.groupBoxTKOFLine.TabIndex = 57;
            this.groupBoxTKOFLine.TabStop = false;
            this.groupBoxTKOFLine.Text = "Take-Off Line  (in WGS84 CRS)";
            // 
            // takeOffLeftLongitude
            // 
            this.takeOffLeftLongitude.Location = new System.Drawing.Point(291, 68);
            this.takeOffLeftLongitude.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.takeOffLeftLongitude.Name = "takeOffLeftLongitude";
            this.takeOffLeftLongitude.Size = new System.Drawing.Size(145, 26);
            this.takeOffLeftLongitude.TabIndex = 59;
            this.takeOffLeftLongitude.TextChanged += new System.EventHandler(this.takeOffLeftLongitude_TextChanged);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 68);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 20);
            this.label10.TabIndex = 59;
            this.label10.Text = "Left Point";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(119, 41);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 20);
            this.label11.TabIndex = 61;
            this.label11.Text = "Latitude";
            // 
            // takeOffLeftLatitude
            // 
            this.takeOffLeftLatitude.Location = new System.Drawing.Point(123, 68);
            this.takeOffLeftLatitude.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.takeOffLeftLatitude.Name = "takeOffLeftLatitude";
            this.takeOffLeftLatitude.Size = new System.Drawing.Size(145, 26);
            this.takeOffLeftLatitude.TabIndex = 58;
            this.takeOffLeftLatitude.TextChanged += new System.EventHandler(this.takeOffLeftLatitude_TextChanged);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(288, 41);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 20);
            this.label13.TabIndex = 63;
            this.label13.Text = "Longitude";
            // 
            // takeOffRightLatitude
            // 
            this.takeOffRightLatitude.Location = new System.Drawing.Point(123, 106);
            this.takeOffRightLatitude.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.takeOffRightLatitude.Name = "takeOffRightLatitude";
            this.takeOffRightLatitude.Size = new System.Drawing.Size(145, 26);
            this.takeOffRightLatitude.TabIndex = 60;
            this.takeOffRightLatitude.TextChanged += new System.EventHandler(this.takeOffRightLatitude_TextChanged);
            // 
            // takeOffRightLongitude
            // 
            this.takeOffRightLongitude.Location = new System.Drawing.Point(291, 109);
            this.takeOffRightLongitude.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.takeOffRightLongitude.Name = "takeOffRightLongitude";
            this.takeOffRightLongitude.Size = new System.Drawing.Size(145, 26);
            this.takeOffRightLongitude.TabIndex = 61;
            this.takeOffRightLongitude.TextChanged += new System.EventHandler(this.takeOffRightLongitude_TextChanged);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 106);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 20);
            this.label12.TabIndex = 65;
            this.label12.Text = "Right Point";
            // 
            // btnImportTKOFline
            // 
            this.btnImportTKOFline.Location = new System.Drawing.Point(783, 148);
            this.btnImportTKOFline.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImportTKOFline.Name = "btnImportTKOFline";
            this.btnImportTKOFline.Size = new System.Drawing.Size(192, 39);
            this.btnImportTKOFline.TabIndex = 63;
            this.btnImportTKOFline.Text = "TKOF Line from *.kml";
            this.btnImportTKOFline.UseVisualStyleBackColor = true;
            this.btnImportTKOFline.Click += new System.EventHandler(this.btnImportTKOFline_Click);
            // 
            // btnSwitchLeftRight
            // 
            this.btnSwitchLeftRight.Location = new System.Drawing.Point(783, 92);
            this.btnSwitchLeftRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSwitchLeftRight.Name = "btnSwitchLeftRight";
            this.btnSwitchLeftRight.Size = new System.Drawing.Size(192, 39);
            this.btnSwitchLeftRight.TabIndex = 62;
            this.btnSwitchLeftRight.Text = "Switch Left/Right Point";
            this.btnSwitchLeftRight.UseVisualStyleBackColor = true;
            this.btnSwitchLeftRight.Click += new System.EventHandler(this.btnSwitchLeftRight_Click);
            // 
            // comboBoxParcour
            // 
            this.comboBoxParcour.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxParcour.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxParcour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxParcour.FormattingEnabled = true;
            this.comboBoxParcour.Location = new System.Drawing.Point(100, 78);
            this.comboBoxParcour.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxParcour.Name = "comboBoxParcour";
            this.comboBoxParcour.Size = new System.Drawing.Size(184, 28);
            this.comboBoxParcour.TabIndex = 56;
            this.comboBoxParcour.SelectedIndexChanged += new System.EventHandler(this.comboBoxParcour_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 82);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 20);
            this.label8.TabIndex = 55;
            this.label8.Text = "Parcour";
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(100, 38);
            this.textName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(184, 26);
            this.textName.TabIndex = 54;
            this.textName.TextChanged += new System.EventHandler(this.textName_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 53;
            this.label2.Text = "Name";
            // 
            // groupBoxStartList
            // 
            this.groupBoxStartList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxStartList.Controls.Add(this.dataGridView1);
            this.groupBoxStartList.Enabled = false;
            this.groupBoxStartList.Location = new System.Drawing.Point(330, 440);
            this.groupBoxStartList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxStartList.Name = "groupBoxStartList";
            this.groupBoxStartList.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxStartList.Size = new System.Drawing.Size(1001, 624);
            this.groupBoxStartList.TabIndex = 92;
            this.groupBoxStartList.TabStop = false;
            this.groupBoxStartList.Text = "Start List Management (times in UTC)";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.CNum,
            this.AC,
            this.Crew,
            this.TakeOff,
            this.Start,
            this.End,
            this.Route,
            this.Date1});
            this.dataGridView1.Location = new System.Drawing.Point(34, 44);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(921, 448);
            this.dataGridView1.TabIndex = 80;
            this.dataGridView1.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserDeletedRow);
            this.dataGridView1.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView1_UserDeletingRow);
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "StartID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 97;
            // 
            // CNum
            // 
            this.CNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.CNum.HeaderText = "CNum";
            this.CNum.Name = "CNum";
            this.CNum.ReadOnly = true;
            this.CNum.Width = 27;
            // 
            // AC
            // 
            this.AC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.AC.HeaderText = "AC";
            this.AC.Name = "AC";
            this.AC.ReadOnly = true;
            this.AC.Width = 27;
            // 
            // Crew
            // 
            this.Crew.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Crew.HeaderText = "Crew";
            this.Crew.Name = "Crew";
            this.Crew.ReadOnly = true;
            this.Crew.Width = 81;
            // 
            // TakeOff
            // 
            this.TakeOff.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TakeOff.HeaderText = "TakeOff";
            this.TakeOff.Name = "TakeOff";
            this.TakeOff.ReadOnly = true;
            this.TakeOff.Width = 102;
            // 
            // Start
            // 
            this.Start.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Start.HeaderText = "Start";
            this.Start.Name = "Start";
            this.Start.ReadOnly = true;
            this.Start.Width = 80;
            // 
            // End
            // 
            this.End.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.End.HeaderText = "End";
            this.End.Name = "End";
            this.End.ReadOnly = true;
            this.End.Width = 74;
            // 
            // Route
            // 
            this.Route.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Route.HeaderText = "Route";
            this.Route.Name = "Route";
            this.Route.ReadOnly = true;
            this.Route.Width = 27;
            // 
            // Date1
            // 
            this.Date1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Date1.HeaderText = "Date";
            this.Date1.Name = "Date1";
            this.Date1.ReadOnly = true;
            this.Date1.Width = 27;
            // 
            // timeTakeOffToStartgateDuration
            // 
            this.timeTakeOffToStartgateDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeTakeOffToStartgateDuration.CustomFormat = "mm:ss";
            this.timeTakeOffToStartgateDuration.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeTakeOffToStartgateDuration.Location = new System.Drawing.Point(181, 41);
            this.timeTakeOffToStartgateDuration.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.timeTakeOffToStartgateDuration.Name = "timeTakeOffToStartgateDuration";
            this.timeTakeOffToStartgateDuration.ShowUpDown = true;
            this.timeTakeOffToStartgateDuration.Size = new System.Drawing.Size(85, 26);
            this.timeTakeOffToStartgateDuration.TabIndex = 68;
            this.timeTakeOffToStartgateDuration.Value = new System.DateTime(2012, 4, 5, 13, 13, 48, 0);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.86765F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.13235F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.timeTakeOffInterval, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.timeStartBlockInterval, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(313, 45);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(306, 79);
            this.tableLayoutPanel1.TabIndex = 69;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 20);
            this.label5.TabIndex = 75;
            this.label5.Text = "Interval btw Take-Offs";
            // 
            // timeTakeOffInterval
            // 
            this.timeTakeOffInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeTakeOffInterval.CustomFormat = "mm:ss";
            this.timeTakeOffInterval.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeTakeOffInterval.Location = new System.Drawing.Point(209, 5);
            this.timeTakeOffInterval.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.timeTakeOffInterval.Name = "timeTakeOffInterval";
            this.timeTakeOffInterval.ShowUpDown = true;
            this.timeTakeOffInterval.Size = new System.Drawing.Size(93, 26);
            this.timeTakeOffInterval.TabIndex = 70;
            // 
            // timeStartBlockInterval
            // 
            this.timeStartBlockInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeStartBlockInterval.CustomFormat = "mm:ss";
            this.timeStartBlockInterval.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeStartBlockInterval.Location = new System.Drawing.Point(209, 41);
            this.timeStartBlockInterval.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.timeStartBlockInterval.Name = "timeStartBlockInterval";
            this.timeStartBlockInterval.ShowUpDown = true;
            this.timeStartBlockInterval.Size = new System.Drawing.Size(93, 26);
            this.timeStartBlockInterval.TabIndex = 71;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 36);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 20);
            this.label6.TabIndex = 76;
            this.label6.Text = "Interval btw Start Blocks";
            // 
            // btnExportToPDF
            // 
            this.btnExportToPDF.Location = new System.Drawing.Point(0, 776);
            this.btnExportToPDF.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExportToPDF.Name = "btnExportToPDF";
            this.btnExportToPDF.Size = new System.Drawing.Size(307, 42);
            this.btnExportToPDF.TabIndex = 15;
            this.btnExportToPDF.Text = "Export Startlist To PDF";
            this.btnExportToPDF.UseVisualStyleBackColor = true;
            this.btnExportToPDF.Click += new System.EventHandler(this.btnExportToPDF_Click);
            // 
            // btnSaveQualificationRound
            // 
            this.btnSaveQualificationRound.Location = new System.Drawing.Point(0, 670);
            this.btnSaveQualificationRound.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSaveQualificationRound.Name = "btnSaveQualificationRound";
            this.btnSaveQualificationRound.Size = new System.Drawing.Size(307, 42);
            this.btnSaveQualificationRound.TabIndex = 12;
            this.btnSaveQualificationRound.Text = "Save Qualification Round";
            this.btnSaveQualificationRound.UseVisualStyleBackColor = true;
            this.btnSaveQualificationRound.Click += new System.EventHandler(this.btnSaveQualificationRound_Click);
            // 
            // btnNewQualificationRound
            // 
            this.btnNewQualificationRound.Location = new System.Drawing.Point(0, 612);
            this.btnNewQualificationRound.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNewQualificationRound.Name = "btnNewQualificationRound";
            this.btnNewQualificationRound.Size = new System.Drawing.Size(307, 48);
            this.btnNewQualificationRound.TabIndex = 11;
            this.btnNewQualificationRound.Text = "New Qualification Round";
            this.btnNewQualificationRound.UseVisualStyleBackColor = true;
            this.btnNewQualificationRound.Click += new System.EventHandler(this.btnNewQualificationRound_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.16666F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.83333F));
            this.tableLayoutPanel2.Controls.Add(this.timeTakeOffToStartgateDuration, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label19, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.timeParcourDuration, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(19, 45);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(270, 79);
            this.tableLayoutPanel2.TabIndex = 66;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(4, 0);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(126, 20);
            this.label19.TabIndex = 66;
            this.label19.Text = "Parcour duration";
            // 
            // timeParcourDuration
            // 
            this.timeParcourDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeParcourDuration.CustomFormat = "mm:ss";
            this.timeParcourDuration.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeParcourDuration.Location = new System.Drawing.Point(181, 5);
            this.timeParcourDuration.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.timeParcourDuration.Name = "timeParcourDuration";
            this.timeParcourDuration.ShowUpDown = true;
            this.timeParcourDuration.Size = new System.Drawing.Size(85, 26);
            this.timeParcourDuration.TabIndex = 67;
            this.timeParcourDuration.Value = new System.DateTime(2012, 4, 5, 13, 13, 48, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 36);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(162, 20);
            this.label7.TabIndex = 82;
            this.label7.Text = "Take-Off to Start Line";
            // 
            // groupBoxGeneral
            // 
            this.groupBoxGeneral.Controls.Add(this.btnAutoFillStartList);
            this.groupBoxGeneral.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxGeneral.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxGeneral.Controls.Add(this.btnRecalcStartList);
            this.groupBoxGeneral.Controls.Add(this.numericUpDownRoutes);
            this.groupBoxGeneral.Controls.Add(this.label3);
            this.groupBoxGeneral.Location = new System.Drawing.Point(327, 268);
            this.groupBoxGeneral.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxGeneral.Name = "groupBoxGeneral";
            this.groupBoxGeneral.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxGeneral.Size = new System.Drawing.Size(1004, 152);
            this.groupBoxGeneral.TabIndex = 65;
            this.groupBoxGeneral.TabStop = false;
            this.groupBoxGeneral.Text = "Time Settings (in mm:ss) used for start list calculation (values are not saved)";
            // 
            // btnAutoFillStartList
            // 
            this.btnAutoFillStartList.Location = new System.Drawing.Point(783, 78);
            this.btnAutoFillStartList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAutoFillStartList.Name = "btnAutoFillStartList";
            this.btnAutoFillStartList.Size = new System.Drawing.Size(192, 39);
            this.btnAutoFillStartList.TabIndex = 76;
            this.btnAutoFillStartList.Text = "AutoFill Start List";
            this.btnAutoFillStartList.UseVisualStyleBackColor = true;
            this.btnAutoFillStartList.Click += new System.EventHandler(this.btnAutoFillStartList_Click);
            // 
            // btnRecalcStartList
            // 
            this.btnRecalcStartList.Location = new System.Drawing.Point(782, 25);
            this.btnRecalcStartList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRecalcStartList.Name = "btnRecalcStartList";
            this.btnRecalcStartList.Size = new System.Drawing.Size(192, 39);
            this.btnRecalcStartList.TabIndex = 75;
            this.btnRecalcStartList.Text = "Recalc Start List";
            this.btnRecalcStartList.UseVisualStyleBackColor = true;
            this.btnRecalcStartList.Click += new System.EventHandler(this.btnRecalcStartList_Click);
            // 
            // numericUpDownRoutes
            // 
            this.numericUpDownRoutes.Location = new System.Drawing.Point(632, 89);
            this.numericUpDownRoutes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownRoutes.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownRoutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRoutes.Name = "numericUpDownRoutes";
            this.numericUpDownRoutes.Size = new System.Drawing.Size(90, 26);
            this.numericUpDownRoutes.TabIndex = 72;
            this.numericUpDownRoutes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(628, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 113;
            this.label3.Text = "Nr of Routes";
            // 
            // lblQRound
            // 
            this.lblQRound.AutoSize = true;
            this.lblQRound.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQRound.Location = new System.Drawing.Point(3, 18);
            this.lblQRound.Name = "lblQRound";
            this.lblQRound.Size = new System.Drawing.Size(176, 20);
            this.lblQRound.TabIndex = 129;
            this.lblQRound.Text = "Qualification Rounds";
            // 
            // QualificationRoundControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblQRound);
            this.Controls.Add(this.groupBoxGeneral);
            this.Controls.Add(this.btnNewQualificationRound);
            this.Controls.Add(this.btnExportToPDF);
            this.Controls.Add(this.btnSaveQualificationRound);
            this.Controls.Add(this.groupBoxStartList);
            this.Controls.Add(this.groupBoxQualification);
            this.Controls.Add(this.btnDeleteQualificationRound);
            this.Controls.Add(this.btnRefreshCompetitions);
            this.Controls.Add(this.listViewQualificationRound);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "QualificationRoundControl";
            this.Size = new System.Drawing.Size(1354, 1068);
            this.Load += new System.EventHandler(this.QualificationRounds_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderQualification)).EndInit();
            this.groupBoxQualification.ResumeLayout(false);
            this.groupBoxQualification.PerformLayout();
            this.groupBoxTKOFLine.ResumeLayout(false);
            this.groupBoxTKOFLine.PerformLayout();
            this.groupBoxStartList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBoxGeneral.ResumeLayout(false);
            this.groupBoxGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRoutes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDeleteQualificationRound;
        private System.Windows.Forms.Button btnRefreshCompetitions;
        private System.Windows.Forms.ListView listViewQualificationRound;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ErrorProvider errorProviderQualification;
        private System.Windows.Forms.Button btnExportToPDF;
        private System.Windows.Forms.Button btnSaveQualificationRound;
        private System.Windows.Forms.GroupBox groupBoxStartList;
        private System.Windows.Forms.DateTimePicker timeTakeOffToStartgateDuration;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker timeTakeOffInterval;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker timeStartBlockInterval;
        private System.Windows.Forms.GroupBox groupBoxQualification;
        private System.Windows.Forms.Button btnImportTKOFline;
        private System.Windows.Forms.Button btnSwitchLeftRight;
        private System.Windows.Forms.TextBox takeOffRightLatitude;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox takeOffRightLongitude;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox takeOffLeftLatitude;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox takeOffLeftLongitude;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxParcour;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNewQualificationRound;
        private System.Windows.Forms.GroupBox groupBoxGeneral;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker timeParcourDuration;
        private System.Windows.Forms.GroupBox groupBoxTKOFLine;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn AC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Crew;
        private System.Windows.Forms.DataGridViewTextBoxColumn TakeOff;
        private System.Windows.Forms.DataGridViewTextBoxColumn Start;
        private System.Windows.Forms.DataGridViewTextBoxColumn End;
        private System.Windows.Forms.DataGridViewTextBoxColumn Route;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date1;
        private System.Windows.Forms.Label lblQRound;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownRoutes;
        private System.Windows.Forms.Button btnRecalcStartList;
        private System.Windows.Forms.Button btnAutoFillStartList;
    }
}
