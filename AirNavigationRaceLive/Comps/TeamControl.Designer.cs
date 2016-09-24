namespace AirNavigationRaceLive.Comps
{
    partial class TeamControl
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBoxTeams = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nationality1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PilotNam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NavigatorNam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AC1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Color1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(11, 89);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(111, 48);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(12, 22);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(111, 59);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Crew List To PDF";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // groupBoxTeams
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxTeams, 3);
            this.groupBoxTeams.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxTeams.Location = new System.Drawing.Point(23, 22);
            this.groupBoxTeams.Name = "groupBoxTeams";
            this.groupBoxTeams.Size = new System.Drawing.Size(732, 34);
            this.groupBoxTeams.TabIndex = 41;
            this.groupBoxTeams.TabStop = false;
            this.groupBoxTeams.Text = "Crews";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CNum,
            this.Nationality1,
            this.PilotNam,
            this.NavigatorNam,
            this.AC1,
            this.Color1});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(23, 62);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.Size = new System.Drawing.Size(588, 617);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView1_UserDeletingRow);
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // CNum
            // 
            this.CNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CNum.HeaderText = "CNum";
            this.CNum.Name = "CNum";
            this.CNum.Width = 75;
            // 
            // Nationality1
            // 
            this.Nationality1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Nationality1.HeaderText = "Nationality";
            this.Nationality1.Name = "Nationality1";
            this.Nationality1.Width = 24;
            // 
            // PilotNam
            // 
            this.PilotNam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PilotNam.HeaderText = "Pilot";
            this.PilotNam.Name = "PilotNam";
            this.PilotNam.Width = 64;
            // 
            // NavigatorNam
            // 
            this.NavigatorNam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NavigatorNam.HeaderText = "Navigator";
            this.NavigatorNam.Name = "NavigatorNam";
            this.NavigatorNam.Width = 98;
            // 
            // AC1
            // 
            this.AC1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.AC1.HeaderText = "AC";
            this.AC1.Name = "AC1";
            this.AC1.Width = 24;
            // 
            // Color1
            // 
            this.Color1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Color1.HeaderText = "Color";
            this.Color1.Name = "Color1";
            this.Color1.Width = 24;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExport);
            this.groupBox2.Controls.Add(this.btnRefresh);
            this.groupBox2.Location = new System.Drawing.Point(617, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(138, 154);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxTeams, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 2, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(17, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(775, 683);
            this.tableLayoutPanel1.TabIndex = 44;
            // 
            // TeamControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TeamControl";
            this.Size = new System.Drawing.Size(795, 743);
            this.Load += new System.EventHandler(this.TeamControl_Load);
            this.VisibleChanged += new System.EventHandler(this.TeamControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox groupBoxTeams;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nationality1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PilotNam;
        private System.Windows.Forms.DataGridViewTextBoxColumn NavigatorNam;
        private System.Windows.Forms.DataGridViewTextBoxColumn AC1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Color1;
    }
}
