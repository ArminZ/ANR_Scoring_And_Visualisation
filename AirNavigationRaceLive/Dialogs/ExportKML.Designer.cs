namespace AirNavigationRaceLive.Dialogs
{
    partial class ExportKML
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
            this.parcour = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.export = new System.Windows.Forms.Button();
            this.height = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.height)).BeginInit();
            this.SuspendLayout();
            // 
            // parcour
            // 
            this.parcour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.parcour.FormattingEnabled = true;
            this.parcour.Location = new System.Drawing.Point(68, 6);
            this.parcour.Name = "parcour";
            this.parcour.Size = new System.Drawing.Size(306, 21);
            this.parcour.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Parcour";
            // 
            // export
            // 
            this.export.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.export.Location = new System.Drawing.Point(68, 59);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(306, 23);
            this.export.TabIndex = 2;
            this.export.Text = "Export KML";
            this.export.UseVisualStyleBackColor = true;
            this.export.Click += new System.EventHandler(this.button1_Click);
            // 
            // height
            // 
            this.height.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.height.Location = new System.Drawing.Point(68, 33);
            this.height.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(306, 20);
            this.height.TabIndex = 3;
            this.height.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Height";
            // 
            // ExportKML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 116);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.height);
            this.Controls.Add(this.export);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.parcour);
            this.Name = "ExportKML";
            this.Text = "ExportKML";
            this.Load += new System.EventHandler(this.ExportKML_Load);
            ((System.ComponentModel.ISupportInitialize)(this.height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox parcour;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button export;
        private System.Windows.Forms.NumericUpDown height;
        private System.Windows.Forms.Label label2;
    }
}