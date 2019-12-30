namespace AirNavigationRaceLive.Dialogs
{
    partial class UploadGAC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadGAC));
            this.btnUploadData = new System.Windows.Forms.Button();
            this.dateGACLabel = new System.Windows.Forms.Label();
            this.textBoxPositions = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnImportGAC = new System.Windows.Forms.Button();
            this.dateGAC = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // btnUploadData
            // 
            this.btnUploadData.Location = new System.Drawing.Point(169, 164);
            this.btnUploadData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUploadData.Name = "btnUploadData";
            this.btnUploadData.Size = new System.Drawing.Size(223, 35);
            this.btnUploadData.TabIndex = 22;
            this.btnUploadData.Text = "Import";
            this.btnUploadData.UseVisualStyleBackColor = true;
            this.btnUploadData.Click += new System.EventHandler(this.btnUploadData_Click);
            // 
            // dateGACLabel
            // 
            this.dateGACLabel.AutoSize = true;
            this.dateGACLabel.Location = new System.Drawing.Point(39, 64);
            this.dateGACLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dateGACLabel.Name = "dateGACLabel";
            this.dateGACLabel.Size = new System.Drawing.Size(121, 20);
            this.dateGACLabel.TabIndex = 21;
            this.dateGACLabel.Text = "Recording Date";
            // 
            // textBoxPositions
            // 
            this.textBoxPositions.Enabled = false;
            this.textBoxPositions.Location = new System.Drawing.Point(169, 96);
            this.textBoxPositions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxPositions.Name = "textBoxPositions";
            this.textBoxPositions.ReadOnly = true;
            this.textBoxPositions.Size = new System.Drawing.Size(222, 26);
            this.textBoxPositions.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 100);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "Records found";
            // 
            // btnImportGAC
            // 
            this.btnImportGAC.Location = new System.Drawing.Point(169, 16);
            this.btnImportGAC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImportGAC.Name = "btnImportGAC";
            this.btnImportGAC.Size = new System.Drawing.Size(223, 35);
            this.btnImportGAC.TabIndex = 17;
            this.btnImportGAC.Text = "Select GAC File...";
            this.btnImportGAC.UseVisualStyleBackColor = true;
            this.btnImportGAC.Click += new System.EventHandler(this.btnImportGAC_Click);
            // 
            // dateGAC
            // 
            this.dateGAC.Enabled = false;
            this.dateGAC.Location = new System.Drawing.Point(169, 60);
            this.dateGAC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dateGAC.Name = "dateGAC";
            this.dateGAC.ReadOnly = true;
            this.dateGAC.Size = new System.Drawing.Size(222, 26);
            this.dateGAC.TabIndex = 23;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(171, 130);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(221, 26);
            this.dateTimePicker1.TabIndex = 24;
            this.dateTimePicker1.Visible = false;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // UploadGAC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 363);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dateGAC);
            this.Controls.Add(this.btnUploadData);
            this.Controls.Add(this.dateGACLabel);
            this.Controls.Add(this.textBoxPositions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnImportGAC);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UploadGAC";
            this.Text = "GAC File Upload";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUploadData;
        private System.Windows.Forms.Label dateGACLabel;
        private System.Windows.Forms.TextBox textBoxPositions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnImportGAC;
        private System.Windows.Forms.TextBox dateGAC;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}