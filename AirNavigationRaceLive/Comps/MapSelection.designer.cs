namespace AirNavigationRaceLive.Comps
{
    partial class MapSelection
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fldLongitude = new System.Windows.Forms.TextBox();
            this.fldLatitude = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.fldCursorY = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.fldCursorX = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lineBox = new System.Windows.Forms.GroupBox();
            this.fldLineTyp = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.numLongO = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.numLatO = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.numLongB = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.numLongA = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numLatA = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.lineBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLongO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLatO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLongB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLongA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLatA)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 457);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(283, 3);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(500, 500);
            this.gMapControl1.TabIndex = 1;
            this.gMapControl1.Zoom = 0D;
            this.gMapControl1.Load += new System.EventHandler(this.gMapControl1_Load);
            this.gMapControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gMapControl1_MouseMove);
            this.gMapControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gMapControl1_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fldLongitude);
            this.groupBox1.Controls.Add(this.fldLatitude);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.fldCursorY);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.fldCursorX);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(3, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 61);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cursor";
            // 
            // fldLongitude
            // 
            this.fldLongitude.Location = new System.Drawing.Point(115, 35);
            this.fldLongitude.Name = "fldLongitude";
            this.fldLongitude.ReadOnly = true;
            this.fldLongitude.Size = new System.Drawing.Size(66, 20);
            this.fldLongitude.TabIndex = 7;
            // 
            // fldLatitude
            // 
            this.fldLatitude.Location = new System.Drawing.Point(115, 9);
            this.fldLatitude.Name = "fldLatitude";
            this.fldLatitude.ReadOnly = true;
            this.fldLatitude.Size = new System.Drawing.Size(66, 20);
            this.fldLatitude.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(56, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "longitude:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(56, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "latitude:";
            // 
            // fldCursorY
            // 
            this.fldCursorY.Location = new System.Drawing.Point(19, 32);
            this.fldCursorY.Name = "fldCursorY";
            this.fldCursorY.ReadOnly = true;
            this.fldCursorY.Size = new System.Drawing.Size(31, 20);
            this.fldCursorY.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Y:";
            // 
            // fldCursorX
            // 
            this.fldCursorX.Location = new System.Drawing.Point(19, 13);
            this.fldCursorX.Name = "fldCursorX";
            this.fldCursorX.ReadOnly = true;
            this.fldCursorX.Size = new System.Drawing.Size(31, 20);
            this.fldCursorX.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "X:";
            // 
            // lineBox
            // 
            this.lineBox.Controls.Add(this.fldLineTyp);
            this.lineBox.Controls.Add(this.label16);
            this.lineBox.Controls.Add(this.label15);
            this.lineBox.Controls.Add(this.numLongO);
            this.lineBox.Controls.Add(this.label13);
            this.lineBox.Controls.Add(this.numLatO);
            this.lineBox.Controls.Add(this.label14);
            this.lineBox.Controls.Add(this.numLongB);
            this.lineBox.Controls.Add(this.label11);
            this.lineBox.Controls.Add(this.numericUpDown1);
            this.lineBox.Controls.Add(this.label12);
            this.lineBox.Controls.Add(this.numLongA);
            this.lineBox.Controls.Add(this.label10);
            this.lineBox.Controls.Add(this.numLatA);
            this.lineBox.Controls.Add(this.label9);
            this.lineBox.Enabled = false;
            this.lineBox.Location = new System.Drawing.Point(3, 235);
            this.lineBox.Name = "lineBox";
            this.lineBox.Size = new System.Drawing.Size(187, 176);
            this.lineBox.TabIndex = 20;
            this.lineBox.TabStop = false;
            this.lineBox.Text = "Line";
            // 
            // fldLineTyp
            // 
            this.fldLineTyp.Location = new System.Drawing.Point(78, 13);
            this.fldLineTyp.Name = "fldLineTyp";
            this.fldLineTyp.ReadOnly = true;
            this.fldLineTyp.Size = new System.Drawing.Size(100, 20);
            this.fldLineTyp.TabIndex = 14;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 13);
            this.label16.TabIndex = 13;
            this.label16.Text = "Line Typ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 118);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(165, 13);
            this.label15.TabIndex = 12;
            this.label15.Text = "Point O is used for the Orientation";
            // 
            // numLongO
            // 
            this.numLongO.DecimalPlaces = 12;
            this.numLongO.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numLongO.Location = new System.Drawing.Point(79, 152);
            this.numLongO.Name = "numLongO";
            this.numLongO.ReadOnly = true;
            this.numLongO.Size = new System.Drawing.Size(100, 20);
            this.numLongO.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 154);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Longitude O:";
            // 
            // numLatO
            // 
            this.numLatO.DecimalPlaces = 12;
            this.numLatO.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numLatO.Location = new System.Drawing.Point(79, 133);
            this.numLatO.Name = "numLatO";
            this.numLatO.ReadOnly = true;
            this.numLatO.Size = new System.Drawing.Size(100, 20);
            this.numLatO.TabIndex = 9;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 135);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 8;
            this.label14.Text = "Latitude O:";
            // 
            // numLongB
            // 
            this.numLongB.DecimalPlaces = 12;
            this.numLongB.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numLongB.Location = new System.Drawing.Point(78, 97);
            this.numLongB.Name = "numLongB";
            this.numLongB.ReadOnly = true;
            this.numLongB.Size = new System.Drawing.Size(100, 20);
            this.numLongB.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Longitude B:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 12;
            this.numericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericUpDown1.Location = new System.Drawing.Point(78, 78);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.ReadOnly = true;
            this.numericUpDown1.Size = new System.Drawing.Size(100, 20);
            this.numericUpDown1.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Latitude B:";
            // 
            // numLongA
            // 
            this.numLongA.DecimalPlaces = 12;
            this.numLongA.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numLongA.Location = new System.Drawing.Point(79, 55);
            this.numLongA.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numLongA.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.numLongA.Name = "numLongA";
            this.numLongA.ReadOnly = true;
            this.numLongA.Size = new System.Drawing.Size(100, 20);
            this.numLongA.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Longitude A:";
            // 
            // numLatA
            // 
            this.numLatA.DecimalPlaces = 12;
            this.numLatA.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numLatA.Location = new System.Drawing.Point(79, 36);
            this.numLatA.Maximum = new decimal(new int[] {
            190,
            0,
            0,
            0});
            this.numLatA.Minimum = new decimal(new int[] {
            190,
            0,
            0,
            -2147483648});
            this.numLatA.Name = "numLatA";
            this.numLatA.ReadOnly = true;
            this.numLatA.Size = new System.Drawing.Size(100, 20);
            this.numLatA.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Latitude A:";
            // 
            // MapSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lineBox);
            this.Controls.Add(this.gMapControl1);
            this.Controls.Add(this.button1);
            this.Name = "MapSelection";
            this.Size = new System.Drawing.Size(786, 555);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.lineBox.ResumeLayout(false);
            this.lineBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLongO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLatO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLongB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLongA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLatA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox fldLongitude;
        private System.Windows.Forms.TextBox fldLatitude;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox fldCursorY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox fldCursorX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox lineBox;
        private System.Windows.Forms.TextBox fldLineTyp;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numLongO;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numLatO;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numLongB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numLongA;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numLatA;
        private System.Windows.Forms.Label label9;
    }
}
