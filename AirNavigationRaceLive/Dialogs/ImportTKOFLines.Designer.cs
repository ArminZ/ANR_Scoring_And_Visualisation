namespace AirNavigationRaceLive.Dialogs
{
    partial class ImportTKOFLines
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportTKOFLines));
            this.btnImportTKOFLine = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxTKOFLines = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnImportTKOFLine
            // 
            this.btnImportTKOFLine.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnImportTKOFLine.Location = new System.Drawing.Point(358, 40);
            this.btnImportTKOFLine.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportTKOFLine.Name = "btnImportTKOFLine";
            this.btnImportTKOFLine.Size = new System.Drawing.Size(92, 24);
            this.btnImportTKOFLine.TabIndex = 22;
            this.btnImportTKOFLine.Text = "OK";
            this.btnImportTKOFLine.UseVisualStyleBackColor = true;
            this.btnImportTKOFLine.Click += new System.EventHandler(this.btnUploadData_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "Select a Take-Off Line";
            // 
            // comboBoxTKOFLines
            // 
            this.comboBoxTKOFLines.FormattingEnabled = true;
            this.comboBoxTKOFLines.Location = new System.Drawing.Point(29, 40);
            this.comboBoxTKOFLines.Name = "comboBoxTKOFLines";
            this.comboBoxTKOFLines.Size = new System.Drawing.Size(322, 24);
            this.comboBoxTKOFLines.TabIndex = 23;
            // 
            // ImportTKOFLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 129);
            this.Controls.Add(this.comboBoxTKOFLines);
            this.Controls.Add(this.btnImportTKOFLine);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ImportTKOFLines";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Take-Off Lines for import";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImportTKOFLine;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxTKOFLines;
    }
}