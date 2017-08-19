namespace AirNavigationRaceLive.Dialogs
{
    partial class UploadGPX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadGPX));
            this.btnUploadData = new System.Windows.Forms.Button();
            this.textBoxRecords = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnImportGPX = new System.Windows.Forms.Button();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUploadData
            // 
            this.btnUploadData.Enabled = false;
            this.btnUploadData.Location = new System.Drawing.Point(150, 106);
            this.btnUploadData.Margin = new System.Windows.Forms.Padding(4);
            this.btnUploadData.Name = "btnUploadData";
            this.btnUploadData.Size = new System.Drawing.Size(198, 28);
            this.btnUploadData.TabIndex = 22;
            this.btnUploadData.Text = "Import";
            this.btnUploadData.UseVisualStyleBackColor = true;
            this.btnUploadData.Click += new System.EventHandler(this.btnUploadData_Click);
            // 
            // textBoxRecords
            // 
            this.textBoxRecords.Enabled = false;
            this.textBoxRecords.Location = new System.Drawing.Point(151, 77);
            this.textBoxRecords.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxRecords.Name = "textBoxRecords";
            this.textBoxRecords.ReadOnly = true;
            this.textBoxRecords.Size = new System.Drawing.Size(198, 22);
            this.textBoxRecords.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "Date of first Record";
            // 
            // btnImportGPX
            // 
            this.btnImportGPX.Location = new System.Drawing.Point(150, 13);
            this.btnImportGPX.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportGPX.Name = "btnImportGPX";
            this.btnImportGPX.Size = new System.Drawing.Size(198, 28);
            this.btnImportGPX.TabIndex = 17;
            this.btnImportGPX.Text = "Select GPX File...";
            this.btnImportGPX.UseVisualStyleBackColor = true;
            this.btnImportGPX.Click += new System.EventHandler(this.btnImportGPX_Click);
            // 
            // textBoxDate
            // 
            this.textBoxDate.Enabled = false;
            this.textBoxDate.Location = new System.Drawing.Point(151, 48);
            this.textBoxDate.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.ReadOnly = true;
            this.textBoxDate.Size = new System.Drawing.Size(198, 22);
            this.textBoxDate.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Records found";
            // 
            // UploadGPX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 143);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDate);
            this.Controls.Add(this.btnUploadData);
            this.Controls.Add(this.textBoxRecords);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnImportGPX);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UploadGPX";
            this.Text = "GPX File Upload";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUploadData;
        private System.Windows.Forms.TextBox textBoxRecords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnImportGPX;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.Label label1;
    }
}