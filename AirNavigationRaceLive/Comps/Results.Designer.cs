namespace AirNavigationRaceLive.Comps
{
    partial class Results
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
            this.comboBoxQualificRound = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnLoggerImport = new System.Windows.Forms.Button();
            this.groupBoxLoggerImport = new System.Windows.Forms.GroupBox();
            this.radioButtonGPXimport = new System.Windows.Forms.RadioButton();
            this.radioButtonGACimport = new System.Windows.Forms.RadioButton();
            this.groupBoxExportResults = new System.Windows.Forms.GroupBox();
            this.radioButtonRankingXLS = new System.Windows.Forms.RadioButton();
            this.btnExportResults = new System.Windows.Forms.Button();
            this.radioButtonRankingPDF = new System.Windows.Forms.RadioButton();
            this.radioButtonAllRes = new System.Windows.Forms.RadioButton();
            this.radioButtonSingleRes = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.StartId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lblResults = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.visualisationPictureBox1 = new AirNavigationRaceLive.Comps.PictureBoxExt();
            this.groupBoxTogglePenaltyPanel = new System.Windows.Forms.GroupBox();
            this.btnTogglePenaltyPanel = new System.Windows.Forms.Button();
            this.groupBoxLoggerImport.SuspendLayout();
            this.groupBoxExportResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visualisationPictureBox1)).BeginInit();
            this.groupBoxTogglePenaltyPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxQualificRound
            // 
            this.comboBoxQualificRound.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxQualificRound.FormattingEnabled = true;
            this.comboBoxQualificRound.Location = new System.Drawing.Point(25, 92);
            this.comboBoxQualificRound.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxQualificRound.Name = "comboBoxQualificRound";
            this.comboBoxQualificRound.Size = new System.Drawing.Size(245, 28);
            this.comboBoxQualificRound.TabIndex = 95;
            this.comboBoxQualificRound.SelectedIndexChanged += new System.EventHandler(this.comboBoxQualificRound_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(305, 92);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(89, 35);
            this.btnRefresh.TabIndex = 116;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnLoggerImport
            // 
            this.btnLoggerImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoggerImport.Enabled = false;
            this.btnLoggerImport.Location = new System.Drawing.Point(250, 33);
            this.btnLoggerImport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLoggerImport.Name = "btnLoggerImport";
            this.btnLoggerImport.Size = new System.Drawing.Size(110, 35);
            this.btnLoggerImport.TabIndex = 118;
            this.btnLoggerImport.Text = "Import";
            this.btnLoggerImport.UseVisualStyleBackColor = true;
            this.btnLoggerImport.Click += new System.EventHandler(this.btnLoggerImport_Click);
            // 
            // groupBoxLoggerImport
            // 
            this.groupBoxLoggerImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxLoggerImport.Controls.Add(this.radioButtonGPXimport);
            this.groupBoxLoggerImport.Controls.Add(this.radioButtonGACimport);
            this.groupBoxLoggerImport.Controls.Add(this.btnLoggerImport);
            this.groupBoxLoggerImport.Location = new System.Drawing.Point(9, 980);
            this.groupBoxLoggerImport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxLoggerImport.Name = "groupBoxLoggerImport";
            this.groupBoxLoggerImport.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxLoggerImport.Size = new System.Drawing.Size(385, 119);
            this.groupBoxLoggerImport.TabIndex = 121;
            this.groupBoxLoggerImport.TabStop = false;
            this.groupBoxLoggerImport.Text = "Logger Data import";
            // 
            // radioButtonGPXimport
            // 
            this.radioButtonGPXimport.AutoSize = true;
            this.radioButtonGPXimport.Location = new System.Drawing.Point(20, 80);
            this.radioButtonGPXimport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonGPXimport.Name = "radioButtonGPXimport";
            this.radioButtonGPXimport.Size = new System.Drawing.Size(119, 24);
            this.radioButtonGPXimport.TabIndex = 122;
            this.radioButtonGPXimport.Text = "Import *.gpx";
            this.radioButtonGPXimport.UseVisualStyleBackColor = true;
            // 
            // radioButtonGACimport
            // 
            this.radioButtonGACimport.AutoSize = true;
            this.radioButtonGACimport.Checked = true;
            this.radioButtonGACimport.Location = new System.Drawing.Point(20, 41);
            this.radioButtonGACimport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonGACimport.Name = "radioButtonGACimport";
            this.radioButtonGACimport.Size = new System.Drawing.Size(120, 24);
            this.radioButtonGACimport.TabIndex = 121;
            this.radioButtonGACimport.TabStop = true;
            this.radioButtonGACimport.Text = "Import *.gac";
            this.radioButtonGACimport.UseVisualStyleBackColor = true;
            // 
            // groupBoxExportResults
            // 
            this.groupBoxExportResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxExportResults.Controls.Add(this.radioButtonRankingXLS);
            this.groupBoxExportResults.Controls.Add(this.btnExportResults);
            this.groupBoxExportResults.Controls.Add(this.radioButtonRankingPDF);
            this.groupBoxExportResults.Controls.Add(this.radioButtonAllRes);
            this.groupBoxExportResults.Controls.Add(this.radioButtonSingleRes);
            this.groupBoxExportResults.Location = new System.Drawing.Point(9, 1194);
            this.groupBoxExportResults.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxExportResults.Name = "groupBoxExportResults";
            this.groupBoxExportResults.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxExportResults.Size = new System.Drawing.Size(385, 185);
            this.groupBoxExportResults.TabIndex = 122;
            this.groupBoxExportResults.TabStop = false;
            this.groupBoxExportResults.Text = "Export Results";
            // 
            // radioButtonRankingXLS
            // 
            this.radioButtonRankingXLS.AutoSize = true;
            this.radioButtonRankingXLS.Location = new System.Drawing.Point(21, 150);
            this.radioButtonRankingXLS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonRankingXLS.Name = "radioButtonRankingXLS";
            this.radioButtonRankingXLS.Size = new System.Drawing.Size(157, 24);
            this.radioButtonRankingXLS.TabIndex = 124;
            this.radioButtonRankingXLS.Text = "Ranking List XLS";
            this.radioButtonRankingXLS.UseVisualStyleBackColor = true;
            // 
            // btnExportResults
            // 
            this.btnExportResults.Location = new System.Drawing.Point(252, 41);
            this.btnExportResults.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportResults.Name = "btnExportResults";
            this.btnExportResults.Size = new System.Drawing.Size(110, 35);
            this.btnExportResults.TabIndex = 123;
            this.btnExportResults.Text = "Export";
            this.btnExportResults.UseVisualStyleBackColor = true;
            this.btnExportResults.Click += new System.EventHandler(this.btnExportResults_Click);
            // 
            // radioButtonRankingPDF
            // 
            this.radioButtonRankingPDF.AutoSize = true;
            this.radioButtonRankingPDF.Location = new System.Drawing.Point(21, 115);
            this.radioButtonRankingPDF.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonRankingPDF.Name = "radioButtonRankingPDF";
            this.radioButtonRankingPDF.Size = new System.Drawing.Size(158, 24);
            this.radioButtonRankingPDF.TabIndex = 122;
            this.radioButtonRankingPDF.Text = "Ranking List PDF";
            this.radioButtonRankingPDF.UseVisualStyleBackColor = true;
            this.radioButtonRankingPDF.CheckedChanged += new System.EventHandler(this.AllCheckBoxes_CheckedChanged);
            // 
            // radioButtonAllRes
            // 
            this.radioButtonAllRes.AutoSize = true;
            this.radioButtonAllRes.Location = new System.Drawing.Point(21, 80);
            this.radioButtonAllRes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonAllRes.Name = "radioButtonAllRes";
            this.radioButtonAllRes.Size = new System.Drawing.Size(144, 24);
            this.radioButtonAllRes.TabIndex = 121;
            this.radioButtonAllRes.Text = "All Result Maps";
            this.radioButtonAllRes.UseVisualStyleBackColor = true;
            this.radioButtonAllRes.CheckedChanged += new System.EventHandler(this.AllCheckBoxes_CheckedChanged);
            // 
            // radioButtonSingleRes
            // 
            this.radioButtonSingleRes.AutoSize = true;
            this.radioButtonSingleRes.Checked = true;
            this.radioButtonSingleRes.Location = new System.Drawing.Point(21, 45);
            this.radioButtonSingleRes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonSingleRes.Name = "radioButtonSingleRes";
            this.radioButtonSingleRes.Size = new System.Drawing.Size(182, 24);
            this.radioButtonSingleRes.TabIndex = 120;
            this.radioButtonSingleRes.TabStop = true;
            this.radioButtonSingleRes.Text = "Selected Result Map";
            this.radioButtonSingleRes.UseVisualStyleBackColor = true;
            this.radioButtonSingleRes.CheckedChanged += new System.EventHandler(this.AllCheckBoxes_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(433, 1120);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(922, 259);
            this.dataGridView1.TabIndex = 123;
            this.dataGridView1.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_RowValidating);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView1_SortCompare);
            this.dataGridView1.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserDeletedRow);
            this.dataGridView1.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView1_UserDeletingRow);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Penalty";
            this.Column2.Name = "Column2";
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Reason";
            this.Column3.Name = "Column3";
            this.Column3.Width = 500;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StartId,
            this.Column6,
            this.Column5,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            this.dataGridView2.Location = new System.Drawing.Point(25, 168);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(369, 804);
            this.dataGridView2.TabIndex = 125;
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            this.dataGridView2.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView2_SortCompare);
            // 
            // StartId
            // 
            this.StartId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.StartId.HeaderText = "StartId";
            this.StartId.Name = "StartId";
            this.StartId.ReadOnly = true;
            this.StartId.Width = 94;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column6.HeaderText = "Penalty";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 97;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column5.HeaderText = "Team";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 85;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "TKOF";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 5;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column8.HeaderText = "SP";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 66;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column9.HeaderText = "FP";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 65;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column10.HeaderText = "Route";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 89;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 127;
            this.label1.Text = "Start List";
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResults.Location = new System.Drawing.Point(21, 9);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(70, 20);
            this.lblResults.TabIndex = 128;
            this.lblResults.Text = "Results";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 20);
            this.label4.TabIndex = 129;
            this.label4.Text = "Qualification Rounds";
            // 
            // visualisationPictureBox1
            // 
            this.visualisationPictureBox1.Location = new System.Drawing.Point(405, 9);
            this.visualisationPictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.visualisationPictureBox1.Name = "visualisationPictureBox1";
            this.visualisationPictureBox1.Size = new System.Drawing.Size(1246, 1099);
            this.visualisationPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.visualisationPictureBox1.TabIndex = 97;
            this.visualisationPictureBox1.TabStop = false;
            // 
            // groupBoxTogglePenaltyPanel
            // 
            this.groupBoxTogglePenaltyPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxTogglePenaltyPanel.Controls.Add(this.btnTogglePenaltyPanel);
            this.groupBoxTogglePenaltyPanel.Location = new System.Drawing.Point(9, 1107);
            this.groupBoxTogglePenaltyPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxTogglePenaltyPanel.Name = "groupBoxTogglePenaltyPanel";
            this.groupBoxTogglePenaltyPanel.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxTogglePenaltyPanel.Size = new System.Drawing.Size(385, 79);
            this.groupBoxTogglePenaltyPanel.TabIndex = 130;
            this.groupBoxTogglePenaltyPanel.TabStop = false;
            this.groupBoxTogglePenaltyPanel.Text = "Toggle Show/Hide Penalty Panel";
            // 
            // btnTogglePenaltyPanel
            // 
            this.btnTogglePenaltyPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTogglePenaltyPanel.Enabled = false;
            this.btnTogglePenaltyPanel.Location = new System.Drawing.Point(248, 28);
            this.btnTogglePenaltyPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTogglePenaltyPanel.Name = "btnTogglePenaltyPanel";
            this.btnTogglePenaltyPanel.Size = new System.Drawing.Size(110, 35);
            this.btnTogglePenaltyPanel.TabIndex = 118;
            this.btnTogglePenaltyPanel.Text = "Hide";
            this.btnTogglePenaltyPanel.UseVisualStyleBackColor = true;
            this.btnTogglePenaltyPanel.Click += new System.EventHandler(this.btnTogglePenaltyPanel_Click);
            // 
            // Results
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBoxTogglePenaltyPanel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.comboBoxQualificRound);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBoxExportResults);
            this.Controls.Add(this.groupBoxLoggerImport);
            this.Controls.Add(this.visualisationPictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Results";
            this.Size = new System.Drawing.Size(1665, 1382);
            this.Load += new System.EventHandler(this.Results_Load);
            this.VisibleChanged += new System.EventHandler(this.Results_Load);
            this.groupBoxLoggerImport.ResumeLayout(false);
            this.groupBoxLoggerImport.PerformLayout();
            this.groupBoxExportResults.ResumeLayout(false);
            this.groupBoxExportResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visualisationPictureBox1)).EndInit();
            this.groupBoxTogglePenaltyPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxQualificRound;
        private PictureBoxExt visualisationPictureBox1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnLoggerImport;
        private System.Windows.Forms.GroupBox groupBoxLoggerImport;
        private System.Windows.Forms.GroupBox groupBoxExportResults;
        private System.Windows.Forms.RadioButton radioButtonRankingPDF;
        private System.Windows.Forms.RadioButton radioButtonAllRes;
        private System.Windows.Forms.RadioButton radioButtonSingleRes;
        private System.Windows.Forms.Button btnExportResults;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButtonGPXimport;
        private System.Windows.Forms.RadioButton radioButtonGACimport;
        private System.Windows.Forms.RadioButton radioButtonRankingXLS;
        private System.Windows.Forms.GroupBox groupBoxTogglePenaltyPanel;
        private System.Windows.Forms.Button btnTogglePenaltyPanel;
    }
}
