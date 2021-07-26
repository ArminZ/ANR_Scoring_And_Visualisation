namespace AirNavigationRaceLive.Dialogs
{
    partial class UploadLoggerData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadLoggerData));
            this.btnUploadData = new System.Windows.Forms.Button();
            this.dateLabel = new System.Windows.Forms.Label();
            this.textBoxRecords = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnImportLogger = new System.Windows.Forms.Button();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.numericUpDownTimeCorrectionHrs = new System.Windows.Forms.NumericUpDown();
            this.labelTimeCorrectionHrs = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeCorrectionHrs)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUploadData
            // 
            this.btnUploadData.Location = new System.Drawing.Point(192, 161);
            this.btnUploadData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUploadData.Name = "btnUploadData";
            this.btnUploadData.Size = new System.Drawing.Size(195, 35);
            this.btnUploadData.TabIndex = 22;
            this.btnUploadData.Text = "Import";
            this.btnUploadData.UseVisualStyleBackColor = true;
            this.btnUploadData.Click += new System.EventHandler(this.btnUploadData_Click);
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(23, 64);
            this.dateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(141, 20);
            this.dateLabel.TabIndex = 21;
            this.dateLabel.Text = "Date of first record";
            // 
            // textBoxRecords
            // 
            this.textBoxRecords.Enabled = false;
            this.textBoxRecords.Location = new System.Drawing.Point(193, 93);
            this.textBoxRecords.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxRecords.Name = "textBoxRecords";
            this.textBoxRecords.ReadOnly = true;
            this.textBoxRecords.Size = new System.Drawing.Size(194, 26);
            this.textBoxRecords.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 100);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "Records found";
            // 
            // btnImportLogger
            // 
            this.btnImportLogger.Location = new System.Drawing.Point(192, 16);
            this.btnImportLogger.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImportLogger.Name = "btnImportLogger";
            this.btnImportLogger.Size = new System.Drawing.Size(195, 35);
            this.btnImportLogger.TabIndex = 17;
            this.btnImportLogger.Text = "Select File...";
            this.btnImportLogger.UseVisualStyleBackColor = true;
            this.btnImportLogger.Click += new System.EventHandler(this.btnImportLogger_Click);
            // 
            // textBoxDate
            // 
            this.textBoxDate.Enabled = false;
            this.textBoxDate.Location = new System.Drawing.Point(193, 59);
            this.textBoxDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.ReadOnly = true;
            this.textBoxDate.Size = new System.Drawing.Size(194, 26);
            this.textBoxDate.TabIndex = 23;
            // 
            // numericUpDownTimeCorrectionHrs
            // 
            this.numericUpDownTimeCorrectionHrs.Location = new System.Drawing.Point(267, 127);
            this.numericUpDownTimeCorrectionHrs.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownTimeCorrectionHrs.Name = "numericUpDownTimeCorrectionHrs";
            this.numericUpDownTimeCorrectionHrs.Size = new System.Drawing.Size(120, 26);
            this.numericUpDownTimeCorrectionHrs.TabIndex = 24;
            this.numericUpDownTimeCorrectionHrs.Visible = false;
            // 
            // labelTimeCorrectionHrs
            // 
            this.labelTimeCorrectionHrs.AutoSize = true;
            this.labelTimeCorrectionHrs.Location = new System.Drawing.Point(23, 132);
            this.labelTimeCorrectionHrs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTimeCorrectionHrs.Name = "labelTimeCorrectionHrs";
            this.labelTimeCorrectionHrs.Size = new System.Drawing.Size(171, 20);
            this.labelTimeCorrectionHrs.TabIndex = 25;
            this.labelTimeCorrectionHrs.Text = "Time correction (hours)";
            this.labelTimeCorrectionHrs.Visible = false;
            // 
            // UploadLoggerData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 226);
            this.Controls.Add(this.labelTimeCorrectionHrs);
            this.Controls.Add(this.numericUpDownTimeCorrectionHrs);
            this.Controls.Add(this.textBoxDate);
            this.Controls.Add(this.btnUploadData);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.textBoxRecords);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnImportLogger);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UploadLoggerData";
            this.Text = "Logger Data Import";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeCorrectionHrs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUploadData;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.TextBox textBoxRecords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnImportLogger;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeCorrectionHrs;
        private System.Windows.Forms.Label labelTimeCorrectionHrs;
    }
}