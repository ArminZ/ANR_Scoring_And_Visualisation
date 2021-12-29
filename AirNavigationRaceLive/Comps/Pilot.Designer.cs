namespace AirNavigationRaceLive.Comps
{
    partial class Pilot
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.textBoxLastname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAddPicture = new System.Windows.Forms.Button();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.btnPilotsImport = new System.Windows.Forms.Button();
            this.groupBoxParticipants = new System.Windows.Forms.GroupBox();
            this.btnImportAirsports = new System.Windows.Forms.Button();
            this.btnResetPicture = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Firstname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.External_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtContestId = new System.Windows.Forms.TextBox();
            this.lblAirsportsContestId = new System.Windows.Forms.Label();
            this.textBoxExternalId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.groupBoxParticipants.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(539, 97);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(172, 56);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // textBoxLastname
            // 
            this.textBoxLastname.Enabled = false;
            this.textBoxLastname.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLastname.Location = new System.Drawing.Point(224, 69);
            this.textBoxLastname.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxLastname.Name = "textBoxLastname";
            this.textBoxLastname.Size = new System.Drawing.Size(219, 25);
            this.textBoxLastname.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(220, 42);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Lastname";
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Enabled = false;
            this.textBoxFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFirstName.Location = new System.Drawing.Point(224, 130);
            this.textBoxFirstName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(219, 25);
            this.textBoxFirstName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(220, 104);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Firstname";
            // 
            // btnAddPicture
            // 
            this.btnAddPicture.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPicture.Location = new System.Drawing.Point(32, 234);
            this.btnAddPicture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddPicture.Name = "btnAddPicture";
            this.btnAddPicture.Size = new System.Drawing.Size(105, 30);
            this.btnAddPicture.TabIndex = 3;
            this.btnAddPicture.Text = "Add Picture";
            this.btnAddPicture.UseVisualStyleBackColor = true;
            this.btnAddPicture.Click += new System.EventHandler(this.btnAddPicture_Click);
            // 
            // PictureBox
            // 
            this.PictureBox.Image = global::AirNavigationRaceLive.Properties.Resources._default;
            this.PictureBox.Location = new System.Drawing.Point(32, 42);
            this.PictureBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(180, 188);
            this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox.TabIndex = 13;
            this.PictureBox.TabStop = false;
            // 
            // btnPilotsImport
            // 
            this.btnPilotsImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPilotsImport.Location = new System.Drawing.Point(539, 24);
            this.btnPilotsImport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPilotsImport.Name = "btnPilotsImport";
            this.btnPilotsImport.Size = new System.Drawing.Size(172, 56);
            this.btnPilotsImport.TabIndex = 18;
            this.btnPilotsImport.Text = "Import list from CSV";
            this.btnPilotsImport.UseVisualStyleBackColor = true;
            this.btnPilotsImport.Click += new System.EventHandler(this.btnPilotsImport_Click);
            // 
            // groupBoxParticipants
            // 
            this.groupBoxParticipants.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.SetColumnSpan(this.groupBoxParticipants, 2);
            this.groupBoxParticipants.Controls.Add(this.textBoxExternalId);
            this.groupBoxParticipants.Controls.Add(this.label1);
            this.groupBoxParticipants.Controls.Add(this.txtContestId);
            this.groupBoxParticipants.Controls.Add(this.lblAirsportsContestId);
            this.groupBoxParticipants.Controls.Add(this.btnImportAirsports);
            this.groupBoxParticipants.Controls.Add(this.btnPilotsImport);
            this.groupBoxParticipants.Controls.Add(this.btnRefresh);
            this.groupBoxParticipants.Controls.Add(this.btnResetPicture);
            this.groupBoxParticipants.Controls.Add(this.btnAddPicture);
            this.groupBoxParticipants.Controls.Add(this.PictureBox);
            this.groupBoxParticipants.Controls.Add(this.label3);
            this.groupBoxParticipants.Controls.Add(this.textBoxLastname);
            this.groupBoxParticipants.Controls.Add(this.textBoxFirstName);
            this.groupBoxParticipants.Controls.Add(this.label4);
            this.groupBoxParticipants.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxParticipants.Location = new System.Drawing.Point(25, 29);
            this.groupBoxParticipants.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxParticipants.Name = "groupBoxParticipants";
            this.groupBoxParticipants.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxParticipants.Size = new System.Drawing.Size(733, 280);
            this.groupBoxParticipants.TabIndex = 20;
            this.groupBoxParticipants.TabStop = false;
            this.groupBoxParticipants.Text = "Participants";
            // 
            // btnImportAirsports
            // 
            this.btnImportAirsports.Enabled = false;
            this.btnImportAirsports.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportAirsports.Location = new System.Drawing.Point(539, 170);
            this.btnImportAirsports.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImportAirsports.Name = "btnImportAirsports";
            this.btnImportAirsports.Size = new System.Drawing.Size(172, 56);
            this.btnImportAirsports.TabIndex = 19;
            this.btnImportAirsports.Text = "Import from Airsports.no";
            this.btnImportAirsports.UseVisualStyleBackColor = true;
            this.btnImportAirsports.Click += new System.EventHandler(this.btnImportAirsports_Click);
            // 
            // btnResetPicture
            // 
            this.btnResetPicture.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetPicture.Location = new System.Drawing.Point(137, 234);
            this.btnResetPicture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnResetPicture.Name = "btnResetPicture";
            this.btnResetPicture.Size = new System.Drawing.Size(78, 30);
            this.btnResetPicture.TabIndex = 4;
            this.btnResetPicture.Text = "Reset Picture";
            this.btnResetPicture.UseVisualStyleBackColor = true;
            this.btnResetPicture.Click += new System.EventHandler(this.btnResetPicture_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 198F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxParticipants, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 288F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(789, 712);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.LastName,
            this.Firstname,
            this.External_Id});
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 2);
            this.dataGridView1.Location = new System.Drawing.Point(25, 317);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(733, 750);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_RowValidating);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserDeletedRow);
            this.dataGridView1.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView1_UserDeletingRow);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 8;
            this.ID.Name = "ID";
            this.ID.Visible = false;
            this.ID.Width = 150;
            // 
            // LastName
            // 
            this.LastName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LastName.HeaderText = "Lastname";
            this.LastName.MinimumWidth = 8;
            this.LastName.Name = "LastName";
            // 
            // Firstname
            // 
            this.Firstname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Firstname.HeaderText = "Firstname";
            this.Firstname.MinimumWidth = 8;
            this.Firstname.Name = "Firstname";
            // 
            // External_Id
            // 
            this.External_Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.External_Id.FillWeight = 30F;
            this.External_Id.HeaderText = "ExternalId";
            this.External_Id.MinimumWidth = 8;
            this.External_Id.Name = "External_Id";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtContestId
            // 
            this.txtContestId.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContestId.Location = new System.Drawing.Point(610, 234);
            this.txtContestId.Name = "txtContestId";
            this.txtContestId.Size = new System.Drawing.Size(100, 25);
            this.txtContestId.TabIndex = 134;
            this.txtContestId.TextChanged += new System.EventHandler(this.txtContestId_TextChanged);
            // 
            // lblAirsportsContestId
            // 
            this.lblAirsportsContestId.AutoSize = true;
            this.lblAirsportsContestId.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAirsportsContestId.Location = new System.Drawing.Point(458, 234);
            this.lblAirsportsContestId.Name = "lblAirsportsContestId";
            this.lblAirsportsContestId.Size = new System.Drawing.Size(146, 20);
            this.lblAirsportsContestId.TabIndex = 135;
            this.lblAirsportsContestId.Text = "Airsports ContestId";
            // 
            // textBoxExternalId
            // 
            this.textBoxExternalId.Enabled = false;
            this.textBoxExternalId.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxExternalId.Location = new System.Drawing.Point(224, 201);
            this.textBoxExternalId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxExternalId.Name = "textBoxExternalId";
            this.textBoxExternalId.Size = new System.Drawing.Size(219, 25);
            this.textBoxExternalId.TabIndex = 136;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(220, 175);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 137;
            this.label1.Text = "ExternalId";
            // 
            // Pilot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Pilot";
            this.Size = new System.Drawing.Size(1242, 712);
            this.Load += new System.EventHandler(this.Pilot_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.groupBoxParticipants.ResumeLayout(false);
            this.groupBoxParticipants.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox textBoxLastname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Button btnAddPicture;
        private System.Windows.Forms.Button btnPilotsImport;
        private System.Windows.Forms.GroupBox groupBoxParticipants;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnResetPicture;
        private System.Windows.Forms.Button btnImportAirsports;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Firstname;
        private System.Windows.Forms.DataGridViewTextBoxColumn External_Id;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtContestId;
        private System.Windows.Forms.Label lblAirsportsContestId;
        private System.Windows.Forms.TextBox textBoxExternalId;
        private System.Windows.Forms.Label label1;
    }
}
