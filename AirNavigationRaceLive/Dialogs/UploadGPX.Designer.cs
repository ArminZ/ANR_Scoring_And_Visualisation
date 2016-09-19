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
            this.textBoxPositions = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnImportGPX = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUploadData
            // 
            this.btnUploadData.Location = new System.Drawing.Point(301, 138);
            this.btnUploadData.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnUploadData.Name = "btnUploadData";
            this.btnUploadData.Size = new System.Drawing.Size(397, 55);
            this.btnUploadData.TabIndex = 22;
            this.btnUploadData.Text = "Save";
            this.btnUploadData.UseVisualStyleBackColor = true;
            this.btnUploadData.Click += new System.EventHandler(this.btnUploadData_Click);
            // 
            // textBoxPositions
            // 
            this.textBoxPositions.Enabled = false;
            this.textBoxPositions.Location = new System.Drawing.Point(301, 76);
            this.textBoxPositions.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.textBoxPositions.Name = "textBoxPositions";
            this.textBoxPositions.ReadOnly = true;
            this.textBoxPositions.Size = new System.Drawing.Size(391, 38);
            this.textBoxPositions.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 32);
            this.label3.TabIndex = 19;
            this.label3.Text = "Imported Positions";
            // 
            // btnImportGPX
            // 
            this.btnImportGPX.Location = new System.Drawing.Point(301, 7);
            this.btnImportGPX.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnImportGPX.Name = "btnImportGPX";
            this.btnImportGPX.Size = new System.Drawing.Size(397, 55);
            this.btnImportGPX.TabIndex = 17;
            this.btnImportGPX.Text = "Import GPX";
            this.btnImportGPX.UseVisualStyleBackColor = true;
            this.btnImportGPX.Click += new System.EventHandler(this.btnImportGPX_Click);
            // 
            // UploadGPX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 207);
            this.Controls.Add(this.btnUploadData);
            this.Controls.Add(this.textBoxPositions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnImportGPX);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "UploadGPX";
            this.Text = "UploadGPX";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUploadData;
        private System.Windows.Forms.TextBox textBoxPositions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnImportGPX;
    }
}