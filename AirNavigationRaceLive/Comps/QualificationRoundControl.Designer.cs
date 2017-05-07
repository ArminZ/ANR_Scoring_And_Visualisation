namespace AirNavigationRaceLive.Comps
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
            this.timeTakeOffStartgate = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.timeTakeOffIntervall = new System.Windows.Forms.DateTimePicker();
            this.timeParcourIntervall = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.btnExportToPDF = new System.Windows.Forms.Button();
            this.btnSaveQualificationRound = new System.Windows.Forms.Button();
            this.btnNewQualificationRound = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label19 = new System.Windows.Forms.Label();
            this.timeParcourLength = new System.Windows.Forms.DateTimePicker();
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
            this.btnDeleteQualificationRound.Location = new System.Drawing.Point(0, 578);
            this.btnDeleteQualificationRound.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteQualificationRound.Name = "btnDeleteQualificationRound";
            this.btnDeleteQualificationRound.Size = new System.Drawing.Size(273, 35);
            this.btnDeleteQualificationRound.TabIndex = 14;
            this.btnDeleteQualificationRound.Text = "Delete";
            this.btnDeleteQualificationRound.UseVisualStyleBackColor = true;
            this.btnDeleteQualificationRound.Click += new System.EventHandler(this.btnDeleteQualificationRound_Click);
            // 
            // btnRefreshCompetitions
            // 
            this.btnRefreshCompetitions.Location = new System.Drawing.Point(0, 663);
            this.btnRefreshCompetitions.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefreshCompetitions.Name = "btnRefreshCompetitions";
            this.btnRefreshCompetitions.Size = new System.Drawing.Size(273, 36);
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
            this.listViewQualificationRound.Location = new System.Drawing.Point(4, 43);
            this.listViewQualificationRound.Margin = new System.Windows.Forms.Padding(4);
            this.listViewQualificationRound.MultiSelect = false;
            this.listViewQualificationRound.Name = "listViewQualificationRound";
            this.listViewQualificationRound.Size = new System.Drawing.Size(271, 403);
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
            this.groupBoxQualification.Location = new System.Drawing.Point(291, 43);
            this.groupBoxQualification.Name = "groupBoxQualification";
            this.groupBoxQualification.Size = new System.Drawing.Size(892, 165);
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
            this.groupBoxTKOFLine.Location = new System.Drawing.Point(278, 21);
            this.groupBoxTKOFLine.Name = "groupBoxTKOFLine";
            this.groupBoxTKOFLine.Size = new System.Drawing.Size(411, 128);
            this.groupBoxTKOFLine.TabIndex = 57;
            this.groupBoxTKOFLine.TabStop = false;
            this.groupBoxTKOFLine.Text = "Take-Off Line  (in WGS84 CRS)";
            // 
            // takeOffLeftLongitude
            // 
            this.takeOffLeftLongitude.Location = new System.Drawing.Point(259, 54);
            this.takeOffLeftLongitude.Margin = new System.Windows.Forms.Padding(4);
            this.takeOffLeftLongitude.Name = "takeOffLeftLongitude";
            this.takeOffLeftLongitude.Size = new System.Drawing.Size(129, 22);
            this.takeOffLeftLongitude.TabIndex = 59;
            this.takeOffLeftLongitude.TextChanged += new System.EventHandler(this.takeOffLeftLongitude_TextChanged);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 54);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 17);
            this.label10.TabIndex = 59;
            this.label10.Text = "Left Point";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(106, 33);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 17);
            this.label11.TabIndex = 61;
            this.label11.Text = "Latitude";
            // 
            // takeOffLeftLatitude
            // 
            this.takeOffLeftLatitude.Location = new System.Drawing.Point(109, 54);
            this.takeOffLeftLatitude.Margin = new System.Windows.Forms.Padding(4);
            this.takeOffLeftLatitude.Name = "takeOffLeftLatitude";
            this.takeOffLeftLatitude.Size = new System.Drawing.Size(129, 22);
            this.takeOffLeftLatitude.TabIndex = 58;
            this.takeOffLeftLatitude.TextChanged += new System.EventHandler(this.takeOffLeftLatitude_TextChanged);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(256, 33);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 17);
            this.label13.TabIndex = 63;
            this.label13.Text = "Longitude";
            // 
            // takeOffRightLatitude
            // 
            this.takeOffRightLatitude.Location = new System.Drawing.Point(109, 85);
            this.takeOffRightLatitude.Margin = new System.Windows.Forms.Padding(4);
            this.takeOffRightLatitude.Name = "takeOffRightLatitude";
            this.takeOffRightLatitude.Size = new System.Drawing.Size(129, 22);
            this.takeOffRightLatitude.TabIndex = 60;
            this.takeOffRightLatitude.TextChanged += new System.EventHandler(this.takeOffRightLatitude_TextChanged);
            // 
            // takeOffRightLongitude
            // 
            this.takeOffRightLongitude.Location = new System.Drawing.Point(259, 87);
            this.takeOffRightLongitude.Margin = new System.Windows.Forms.Padding(4);
            this.takeOffRightLongitude.Name = "takeOffRightLongitude";
            this.takeOffRightLongitude.Size = new System.Drawing.Size(129, 22);
            this.takeOffRightLongitude.TabIndex = 61;
            this.takeOffRightLongitude.TextChanged += new System.EventHandler(this.takeOffRightLongitude_TextChanged);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 85);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 17);
            this.label12.TabIndex = 65;
            this.label12.Text = "Right Point";
            // 
            // btnImportTKOFline
            // 
            this.btnImportTKOFline.Location = new System.Drawing.Point(696, 118);
            this.btnImportTKOFline.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportTKOFline.Name = "btnImportTKOFline";
            this.btnImportTKOFline.Size = new System.Drawing.Size(171, 31);
            this.btnImportTKOFline.TabIndex = 63;
            this.btnImportTKOFline.Text = "TKOF Line from *.kml";
            this.btnImportTKOFline.UseVisualStyleBackColor = true;
            this.btnImportTKOFline.Click += new System.EventHandler(this.btnImportTKOFline_Click);
            // 
            // btnSwitchLeftRight
            // 
            this.btnSwitchLeftRight.Location = new System.Drawing.Point(696, 74);
            this.btnSwitchLeftRight.Margin = new System.Windows.Forms.Padding(4);
            this.btnSwitchLeftRight.Name = "btnSwitchLeftRight";
            this.btnSwitchLeftRight.Size = new System.Drawing.Size(171, 31);
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
            this.comboBoxParcour.Location = new System.Drawing.Point(89, 62);
            this.comboBoxParcour.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxParcour.Name = "comboBoxParcour";
            this.comboBoxParcour.Size = new System.Drawing.Size(164, 24);
            this.comboBoxParcour.TabIndex = 56;
            this.comboBoxParcour.SelectedIndexChanged += new System.EventHandler(this.comboBoxParcour_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 66);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 17);
            this.label8.TabIndex = 55;
            this.label8.Text = "Parcour";
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(89, 30);
            this.textName.Margin = new System.Windows.Forms.Padding(4);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(164, 22);
            this.textName.TabIndex = 54;
            this.textName.TextChanged += new System.EventHandler(this.textName_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 53;
            this.label2.Text = "Name";
            // 
            // groupBoxStartList
            // 
            this.groupBoxStartList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxStartList.Controls.Add(this.dataGridView1);
            this.groupBoxStartList.Enabled = false;
            this.groupBoxStartList.Location = new System.Drawing.Point(293, 352);
            this.groupBoxStartList.Name = "groupBoxStartList";
            this.groupBoxStartList.Size = new System.Drawing.Size(890, 499);
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
            this.dataGridView1.Location = new System.Drawing.Point(30, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(819, 358);
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
            this.Column1.Width = 80;
            // 
            // CNum
            // 
            this.CNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.CNum.HeaderText = "CNum";
            this.CNum.Name = "CNum";
            this.CNum.ReadOnly = true;
            this.CNum.Width = 24;
            // 
            // AC
            // 
            this.AC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.AC.HeaderText = "AC";
            this.AC.Name = "AC";
            this.AC.ReadOnly = true;
            this.AC.Width = 24;
            // 
            // Crew
            // 
            this.Crew.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Crew.HeaderText = "Crew";
            this.Crew.Name = "Crew";
            this.Crew.ReadOnly = true;
            this.Crew.Width = 68;
            // 
            // TakeOff
            // 
            this.TakeOff.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TakeOff.HeaderText = "TakeOff";
            this.TakeOff.Name = "TakeOff";
            this.TakeOff.ReadOnly = true;
            this.TakeOff.Width = 88;
            // 
            // Start
            // 
            this.Start.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Start.HeaderText = "Start";
            this.Start.Name = "Start";
            this.Start.ReadOnly = true;
            this.Start.Width = 67;
            // 
            // End
            // 
            this.End.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.End.HeaderText = "End";
            this.End.Name = "End";
            this.End.ReadOnly = true;
            this.End.Width = 62;
            // 
            // Route
            // 
            this.Route.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Route.HeaderText = "Route";
            this.Route.Name = "Route";
            this.Route.ReadOnly = true;
            this.Route.Width = 24;
            // 
            // Date1
            // 
            this.Date1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Date1.HeaderText = "Date";
            this.Date1.Name = "Date1";
            this.Date1.ReadOnly = true;
            this.Date1.Width = 24;
            // 
            // timeTakeOffStartgate
            // 
            this.timeTakeOffStartgate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeTakeOffStartgate.CustomFormat = "mm:ss";
            this.timeTakeOffStartgate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeTakeOffStartgate.Location = new System.Drawing.Point(160, 34);
            this.timeTakeOffStartgate.Margin = new System.Windows.Forms.Padding(4);
            this.timeTakeOffStartgate.Name = "timeTakeOffStartgate";
            this.timeTakeOffStartgate.ShowUpDown = true;
            this.timeTakeOffStartgate.Size = new System.Drawing.Size(76, 22);
            this.timeTakeOffStartgate.TabIndex = 68;
            this.timeTakeOffStartgate.Value = new System.DateTime(2012, 4, 5, 13, 13, 48, 0);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.86765F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.13235F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.timeTakeOffIntervall, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.timeParcourIntervall, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(278, 36);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(272, 63);
            this.tableLayoutPanel1.TabIndex = 69;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 17);
            this.label5.TabIndex = 75;
            this.label5.Text = "Intervall btw Take-Offs";
            // 
            // timeTakeOffIntervall
            // 
            this.timeTakeOffIntervall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeTakeOffIntervall.CustomFormat = "mm:ss";
            this.timeTakeOffIntervall.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeTakeOffIntervall.Location = new System.Drawing.Point(185, 4);
            this.timeTakeOffIntervall.Margin = new System.Windows.Forms.Padding(4);
            this.timeTakeOffIntervall.Name = "timeTakeOffIntervall";
            this.timeTakeOffIntervall.ShowUpDown = true;
            this.timeTakeOffIntervall.Size = new System.Drawing.Size(83, 22);
            this.timeTakeOffIntervall.TabIndex = 70;
            // 
            // timeParcourIntervall
            // 
            this.timeParcourIntervall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeParcourIntervall.CustomFormat = "mm:ss";
            this.timeParcourIntervall.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeParcourIntervall.Location = new System.Drawing.Point(185, 34);
            this.timeParcourIntervall.Margin = new System.Windows.Forms.Padding(4);
            this.timeParcourIntervall.Name = "timeParcourIntervall";
            this.timeParcourIntervall.ShowUpDown = true;
            this.timeParcourIntervall.Size = new System.Drawing.Size(83, 22);
            this.timeParcourIntervall.TabIndex = 71;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 30);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 17);
            this.label6.TabIndex = 76;
            this.label6.Text = "Intervall btw Start Blocks";
            // 
            // btnExportToPDF
            // 
            this.btnExportToPDF.Location = new System.Drawing.Point(0, 621);
            this.btnExportToPDF.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportToPDF.Name = "btnExportToPDF";
            this.btnExportToPDF.Size = new System.Drawing.Size(273, 34);
            this.btnExportToPDF.TabIndex = 15;
            this.btnExportToPDF.Text = "Export Startlist To PDF";
            this.btnExportToPDF.UseVisualStyleBackColor = true;
            this.btnExportToPDF.Click += new System.EventHandler(this.btnExportToPDF_Click);
            // 
            // btnSaveQualificationRound
            // 
            this.btnSaveQualificationRound.Location = new System.Drawing.Point(0, 536);
            this.btnSaveQualificationRound.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveQualificationRound.Name = "btnSaveQualificationRound";
            this.btnSaveQualificationRound.Size = new System.Drawing.Size(273, 34);
            this.btnSaveQualificationRound.TabIndex = 12;
            this.btnSaveQualificationRound.Text = "Save Qualification Round";
            this.btnSaveQualificationRound.UseVisualStyleBackColor = true;
            this.btnSaveQualificationRound.Click += new System.EventHandler(this.btnSaveQualificationRound_Click);
            // 
            // btnNewQualificationRound
            // 
            this.btnNewQualificationRound.Location = new System.Drawing.Point(0, 490);
            this.btnNewQualificationRound.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewQualificationRound.Name = "btnNewQualificationRound";
            this.btnNewQualificationRound.Size = new System.Drawing.Size(273, 38);
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
            this.tableLayoutPanel2.Controls.Add(this.timeTakeOffStartgate, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label19, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.timeParcourLength, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(17, 36);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(240, 63);
            this.tableLayoutPanel2.TabIndex = 66;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(4, 0);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(114, 17);
            this.label19.TabIndex = 66;
            this.label19.Text = "Parcour duration";
            // 
            // timeParcourLength
            // 
            this.timeParcourLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeParcourLength.CustomFormat = "mm:ss";
            this.timeParcourLength.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeParcourLength.Location = new System.Drawing.Point(160, 4);
            this.timeParcourLength.Margin = new System.Windows.Forms.Padding(4);
            this.timeParcourLength.Name = "timeParcourLength";
            this.timeParcourLength.ShowUpDown = true;
            this.timeParcourLength.Size = new System.Drawing.Size(76, 22);
            this.timeParcourLength.TabIndex = 67;
            this.timeParcourLength.Value = new System.DateTime(2012, 4, 5, 13, 13, 48, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 30);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 17);
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
            this.groupBoxGeneral.Location = new System.Drawing.Point(291, 214);
            this.groupBoxGeneral.Name = "groupBoxGeneral";
            this.groupBoxGeneral.Size = new System.Drawing.Size(892, 122);
            this.groupBoxGeneral.TabIndex = 65;
            this.groupBoxGeneral.TabStop = false;
            this.groupBoxGeneral.Text = "Time Settings (in mm:ss)";
            // 
            // btnAutoFillStartList
            // 
            this.btnAutoFillStartList.Location = new System.Drawing.Point(696, 62);
            this.btnAutoFillStartList.Name = "btnAutoFillStartList";
            this.btnAutoFillStartList.Size = new System.Drawing.Size(171, 31);
            this.btnAutoFillStartList.TabIndex = 76;
            this.btnAutoFillStartList.Text = "AutoFill Start List";
            this.btnAutoFillStartList.UseVisualStyleBackColor = true;
            this.btnAutoFillStartList.Click += new System.EventHandler(this.btnAutoFillStartList_Click);
            // 
            // btnRecalcStartList
            // 
            this.btnRecalcStartList.Location = new System.Drawing.Point(695, 20);
            this.btnRecalcStartList.Name = "btnRecalcStartList";
            this.btnRecalcStartList.Size = new System.Drawing.Size(171, 31);
            this.btnRecalcStartList.TabIndex = 75;
            this.btnRecalcStartList.Text = "Recalc Start List";
            this.btnRecalcStartList.UseVisualStyleBackColor = true;
            this.btnRecalcStartList.Click += new System.EventHandler(this.btnRecalcStartList_Click);
            // 
            // numericUpDownRoutes
            // 
            this.numericUpDownRoutes.Location = new System.Drawing.Point(562, 71);
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
            this.numericUpDownRoutes.Size = new System.Drawing.Size(80, 22);
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
            this.label3.Location = new System.Drawing.Point(558, 51);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 113;
            this.label3.Text = "Nr of Routes";
            // 
            // lblQRound
            // 
            this.lblQRound.AutoSize = true;
            this.lblQRound.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQRound.Location = new System.Drawing.Point(3, 14);
            this.lblQRound.Name = "lblQRound";
            this.lblQRound.Size = new System.Drawing.Size(159, 17);
            this.lblQRound.TabIndex = 129;
            this.lblQRound.Text = "Qualification Rounds";
            // 
            // QualificationRoundControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "QualificationRoundControl";
            this.Size = new System.Drawing.Size(1204, 854);
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
        private System.Windows.Forms.DateTimePicker timeTakeOffStartgate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker timeTakeOffIntervall;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker timeParcourIntervall;
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
        private System.Windows.Forms.DateTimePicker timeParcourLength;
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
