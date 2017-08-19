namespace AirNavigationRaceLive.Comps
{
    partial class MapLegacy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapLegacy));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImportANR = new System.Windows.Forms.Button();
            this.fldName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxLegacy = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoBtnWGS84 = new System.Windows.Forms.RadioButton();
            this.doBtnCH1903 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.groupBoxLegacy.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(128, 28);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.refreshToolStripMenuItem.Text = "Refresh";
            // 
            // btnImportANR
            // 
            this.btnImportANR.Enabled = false;
            this.btnImportANR.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportANR.Location = new System.Drawing.Point(280, 226);
            this.btnImportANR.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportANR.Name = "btnImportANR";
            this.btnImportANR.Size = new System.Drawing.Size(111, 28);
            this.btnImportANR.TabIndex = 1;
            this.btnImportANR.Text = "Import MapSet File";
            this.btnImportANR.UseVisualStyleBackColor = true;
            this.btnImportANR.Click += new System.EventHandler(this.btnImportANR_Click);
            // 
            // fldName
            // 
            this.fldName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fldName.Location = new System.Drawing.Point(46, 226);
            this.fldName.Margin = new System.Windows.Forms.Padding(4);
            this.fldName.Name = "fldName";
            this.fldName.Size = new System.Drawing.Size(226, 22);
            this.fldName.TabIndex = 2;
            this.fldName.TextChanged += new System.EventHandler(this.fldName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 205);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Map name";
            // 
            // groupBoxLegacy
            // 
            this.groupBoxLegacy.Controls.Add(this.groupBox3);
            this.groupBoxLegacy.Controls.Add(this.groupBox2);
            this.groupBoxLegacy.Controls.Add(this.label3);
            this.groupBoxLegacy.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxLegacy.Location = new System.Drawing.Point(22, 25);
            this.groupBoxLegacy.Name = "groupBoxLegacy";
            this.groupBoxLegacy.Size = new System.Drawing.Size(857, 409);
            this.groupBoxLegacy.TabIndex = 7;
            this.groupBoxLegacy.TabStop = false;
            this.groupBoxLegacy.Text = "Legacy MapSet import (coordinates in file name)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoBtnWGS84);
            this.groupBox3.Controls.Add(this.doBtnCH1903);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btnImportANR);
            this.groupBox3.Controls.Add(this.fldName);
            this.groupBox3.Location = new System.Drawing.Point(29, 109);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(422, 283);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Legacy MapSet import selection";
            // 
            // rdoBtnWGS84
            // 
            this.rdoBtnWGS84.AutoSize = true;
            this.rdoBtnWGS84.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rdoBtnWGS84.Checked = true;
            this.rdoBtnWGS84.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoBtnWGS84.Location = new System.Drawing.Point(26, 45);
            this.rdoBtnWGS84.Name = "rdoBtnWGS84";
            this.rdoBtnWGS84.Size = new System.Drawing.Size(341, 55);
            this.rdoBtnWGS84.TabIndex = 1;
            this.rdoBtnWGS84.TabStop = true;
            this.rdoBtnWGS84.Text = "WGS84 coordinate reference system (CRS).\r\nFilename in format LatTL_LonTL_LatBR_Lo" +
    "nBR. \r\nExample: 47.23434_6.435_47.0124_6.4415.jpg\r\n";
            this.rdoBtnWGS84.UseVisualStyleBackColor = true;
            // 
            // doBtnCH1903
            // 
            this.doBtnCH1903.AutoSize = true;
            this.doBtnCH1903.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.doBtnCH1903.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doBtnCH1903.Location = new System.Drawing.Point(26, 126);
            this.doBtnCH1903.Name = "doBtnCH1903";
            this.doBtnCH1903.Size = new System.Drawing.Size(349, 55);
            this.doBtnCH1903.TabIndex = 0;
            this.doBtnCH1903.Text = "Swiss CH1903 coordinate reference system (CRS).\r\nFilename in format  yTL_xTL_yBR_" +
    "xBR.\r\nExample: 627346_229236_654885_210943.jpg";
            this.doBtnCH1903.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(467, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(368, 283);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Glossary";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(313, 170);
            this.label2.TabIndex = 8;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(44, 53);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(533, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "In the Legacy MapSet import, the MapSet coordinates are directly defined in the file na" +
    "me.\r\n";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // MapLegacy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxLegacy);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MapLegacy";
            this.Size = new System.Drawing.Size(1480, 714);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBoxLegacy.ResumeLayout(false);
            this.groupBoxLegacy.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.Button btnImportANR;
        private System.Windows.Forms.TextBox fldName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxLegacy;
        private System.Windows.Forms.RadioButton rdoBtnWGS84;
        private System.Windows.Forms.RadioButton doBtnCH1903;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
    }
}
