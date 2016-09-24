namespace AirNavigationRaceLive.Dialogs
{
    partial class Calculator
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calculator));
            this.textEast = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textNorth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textLatitude = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textLongitude = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnToWGS = new System.Windows.Forms.Button();
            this.btnToCh = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBoxCH1903 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBoxCH1903.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textEast
            // 
            this.textEast.Location = new System.Drawing.Point(128, 45);
            this.textEast.Margin = new System.Windows.Forms.Padding(4);
            this.textEast.Name = "textEast";
            this.textEast.Size = new System.Drawing.Size(111, 22);
            this.textEast.TabIndex = 6;
            this.textEast.TextChanged += new System.EventHandler(this.textEast_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "East X, m";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(311, 51);
            this.label2.TabIndex = 7;
            this.label2.Text = "This is a legacy coordinate converter, used for \r\nconversions between the common " +
    "WGS84 CRS \r\nand the old Swiss CH1903 CRS.";
            // 
            // textNorth
            // 
            this.textNorth.Location = new System.Drawing.Point(128, 74);
            this.textNorth.Margin = new System.Windows.Forms.Padding(4);
            this.textNorth.Name = "textNorth";
            this.textNorth.Size = new System.Drawing.Size(111, 22);
            this.textNorth.TabIndex = 7;
            this.textNorth.TextChanged += new System.EventHandler(this.textNorth_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 79);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "North Y, m";
            // 
            // textLatitude
            // 
            this.textLatitude.Location = new System.Drawing.Point(128, 54);
            this.textLatitude.Margin = new System.Windows.Forms.Padding(4);
            this.textLatitude.Name = "textLatitude";
            this.textLatitude.Size = new System.Drawing.Size(111, 22);
            this.textLatitude.TabIndex = 14;
            this.textLatitude.TextChanged += new System.EventHandler(this.textLatitude_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 58);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Latitude, deg";
            // 
            // textLongitude
            // 
            this.textLongitude.Location = new System.Drawing.Point(128, 84);
            this.textLongitude.Margin = new System.Windows.Forms.Padding(4);
            this.textLongitude.Name = "textLongitude";
            this.textLongitude.Size = new System.Drawing.Size(111, 22);
            this.textLongitude.TabIndex = 15;
            this.textLongitude.TextChanged += new System.EventHandler(this.textLongitude_TextChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 88);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Longitude, deg";
            // 
            // btnToWGS
            // 
            this.btnToWGS.Location = new System.Drawing.Point(263, 45);
            this.btnToWGS.Margin = new System.Windows.Forms.Padding(4);
            this.btnToWGS.Name = "btnToWGS";
            this.btnToWGS.Size = new System.Drawing.Size(75, 57);
            this.btnToWGS.TabIndex = 8;
            this.btnToWGS.Text = "To WGS84";
            this.btnToWGS.UseVisualStyleBackColor = true;
            this.btnToWGS.Click += new System.EventHandler(this.btnToWGS_Click);
            // 
            // btnToCh
            // 
            this.btnToCh.Location = new System.Drawing.Point(263, 49);
            this.btnToCh.Margin = new System.Windows.Forms.Padding(4);
            this.btnToCh.Name = "btnToCh";
            this.btnToCh.Size = new System.Drawing.Size(75, 57);
            this.btnToCh.TabIndex = 16;
            this.btnToCh.Text = "To CH1903";
            this.btnToCh.UseVisualStyleBackColor = true;
            this.btnToCh.Click += new System.EventHandler(this.btnToCh_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(217, 405);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 37);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(298, 405);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 37);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // groupBoxCH1903
            // 
            this.groupBoxCH1903.Controls.Add(this.textEast);
            this.groupBoxCH1903.Controls.Add(this.label1);
            this.groupBoxCH1903.Controls.Add(this.label4);
            this.groupBoxCH1903.Controls.Add(this.textNorth);
            this.groupBoxCH1903.Controls.Add(this.btnToWGS);
            this.groupBoxCH1903.Location = new System.Drawing.Point(16, 117);
            this.groupBoxCH1903.Name = "groupBoxCH1903";
            this.groupBoxCH1903.Size = new System.Drawing.Size(360, 133);
            this.groupBoxCH1903.TabIndex = 19;
            this.groupBoxCH1903.TabStop = false;
            this.groupBoxCH1903.Text = "CH1903 CRS (Coordinate Reference System)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnToCh);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textLongitude);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textLatitude);
            this.groupBox1.Location = new System.Drawing.Point(16, 255);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 133);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "WGS84 CRS (Coordinate Reference System)";
            // 
            // Calculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 463);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxCH1903);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Calculator";
            this.Text = "Calculator";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBoxCH1903.ResumeLayout(false);
            this.groupBoxCH1903.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textEast;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textNorth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textLatitude;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textLongitude;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnToWGS;
        private System.Windows.Forms.Button btnToCh;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxCH1903;
    }
}