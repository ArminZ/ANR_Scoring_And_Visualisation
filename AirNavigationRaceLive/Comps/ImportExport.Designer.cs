namespace AirNavigationRaceLive.Comps
{
    partial class ImportExport
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
            this.btnExportKLM = new System.Windows.Forms.Button();
            this.comboBoxQualificationRound = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnSyncExcel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExportGpx = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExportKLM
            // 
            this.btnExportKLM.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportKLM.Location = new System.Drawing.Point(477, 32);
            this.btnExportKLM.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportKLM.Name = "btnExportKLM";
            this.btnExportKLM.Size = new System.Drawing.Size(129, 28);
            this.btnExportKLM.TabIndex = 0;
            this.btnExportKLM.Text = "Export KML";
            this.btnExportKLM.UseVisualStyleBackColor = true;
            this.btnExportKLM.Click += new System.EventHandler(this.btnExportKLM_Click);
            // 
            // comboBoxQualificationRound
            // 
            this.comboBoxQualificationRound.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxQualificationRound.FormattingEnabled = true;
            this.comboBoxQualificationRound.Location = new System.Drawing.Point(62, 51);
            this.comboBoxQualificationRound.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxQualificationRound.Name = "comboBoxQualificationRound";
            this.comboBoxQualificationRound.Size = new System.Drawing.Size(246, 24);
            this.comboBoxQualificationRound.TabIndex = 1;
            this.comboBoxQualificationRound.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(59, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Qualification Round";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Enabled = false;
            this.btnExportExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.Location = new System.Drawing.Point(477, 140);
            this.btnExportExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(129, 28);
            this.btnExportExcel.TabIndex = 3;
            this.btnExportExcel.Text = "Export Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnSyncExcel
            // 
            this.btnSyncExcel.Enabled = false;
            this.btnSyncExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSyncExcel.Location = new System.Drawing.Point(477, 173);
            this.btnSyncExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnSyncExcel.Name = "btnSyncExcel";
            this.btnSyncExcel.Size = new System.Drawing.Size(129, 28);
            this.btnSyncExcel.TabIndex = 4;
            this.btnSyncExcel.Text = "Syncronize Excel";
            this.btnSyncExcel.UseVisualStyleBackColor = true;
            this.btnSyncExcel.Click += new System.EventHandler(this.btnSyncExcel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnExportKLM);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(34, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(658, 82);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export parcour to *.kml file";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(59, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(364, 34);
            this.label4.TabIndex = 6;
            this.label4.Text = "Save the parcour (with prohibited zones, start lines etc.) \r\nas a *.kml file";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnExportGpx);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.comboBoxQualificationRound);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnSyncExcel);
            this.groupBox2.Controls.Add(this.btnExportExcel);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(34, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(658, 296);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Import/Export";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(59, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(190, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Export flight data as *.gpx file";
            // 
            // btnExportGpx
            // 
            this.btnExportGpx.Enabled = false;
            this.btnExportGpx.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportGpx.Location = new System.Drawing.Point(477, 235);
            this.btnExportGpx.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportGpx.Name = "btnExportGpx";
            this.btnExportGpx.Size = new System.Drawing.Size(129, 28);
            this.btnExportGpx.TabIndex = 7;
            this.btnExportGpx.Text = "Export *.gpx";
            this.btnExportGpx.UseVisualStyleBackColor = true;
            this.btnExportGpx.Click += new System.EventHandler(this.btnExportGpx_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(59, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(342, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Export: save Participants, Crews and Startlist to Excel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(59, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(407, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Synchronize: import data from Excel and update the Application";
            // 
            // ImportExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ImportExport";
            this.Size = new System.Drawing.Size(928, 537);
            this.Load += new System.EventHandler(this.ImportExport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExportKLM;
        private System.Windows.Forms.ComboBox comboBoxQualificationRound;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnSyncExcel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnExportGpx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
