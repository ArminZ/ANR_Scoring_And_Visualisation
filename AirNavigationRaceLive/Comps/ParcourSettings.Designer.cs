﻿namespace AirNavigationRaceLive.Comps
{
    partial class ParcourSettings
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
            this.groupBoxChannel = new System.Windows.Forms.GroupBox();
            this.chkChannelShowCL = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownChannelPen = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.btnChannelColor = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSPFPColor = new System.Windows.Forms.Button();
            this.numericUpDownSPFPPen = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.chkSPFPShowCircle = new System.Windows.Forms.CheckBox();
            this.groupBoxSPFP = new System.Windows.Forms.GroupBox();
            this.btnPROHColorLayer = new System.Windows.Forms.Button();
            this.numericUpDownPROHAlpha = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDownPROHPen = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxLayer = new System.Windows.Forms.GroupBox();
            this.checkShowPROHBorders = new System.Windows.Forms.CheckBox();
            this.btnPROHColorBorder = new System.Windows.Forms.Button();
            this.groupBoxIntersect = new System.Windows.Forms.GroupBox();
            this.chkIntersectionPointsShow = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownIntersect = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.btnIntersectColor = new System.Windows.Forms.Button();
            this.groupBoxCalculationType = new System.Windows.Forms.GroupBox();
            this.radioButtonPenaltyCalcTypeChannel = new System.Windows.Forms.RadioButton();
            this.radioButtonPenaltyCalcTypePROH = new System.Windows.Forms.RadioButton();
            this.groupBoxChannel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChannelPen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSPFPPen)).BeginInit();
            this.groupBoxSPFP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPROHAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPROHPen)).BeginInit();
            this.groupBoxLayer.SuspendLayout();
            this.groupBoxIntersect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntersect)).BeginInit();
            this.groupBoxCalculationType.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxChannel
            // 
            this.groupBoxChannel.Controls.Add(this.chkChannelShowCL);
            this.groupBoxChannel.Controls.Add(this.label5);
            this.groupBoxChannel.Controls.Add(this.numericUpDownChannelPen);
            this.groupBoxChannel.Controls.Add(this.label6);
            this.groupBoxChannel.Controls.Add(this.btnChannelColor);
            this.groupBoxChannel.Location = new System.Drawing.Point(520, 179);
            this.groupBoxChannel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxChannel.Name = "groupBoxChannel";
            this.groupBoxChannel.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxChannel.Size = new System.Drawing.Size(380, 167);
            this.groupBoxChannel.TabIndex = 30;
            this.groupBoxChannel.TabStop = false;
            this.groupBoxChannel.Text = "Channel Properties";
            // 
            // chkChannelShowCL
            // 
            this.chkChannelShowCL.AutoSize = true;
            this.chkChannelShowCL.Location = new System.Drawing.Point(6, 111);
            this.chkChannelShowCL.Name = "chkChannelShowCL";
            this.chkChannelShowCL.Size = new System.Drawing.Size(151, 24);
            this.chkChannelShowCL.TabIndex = 30;
            this.chkChannelShowCL.Text = "Show Centerline";
            this.chkChannelShowCL.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(106, 31);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(4, 5, 0, 0);
            this.label5.Size = new System.Drawing.Size(86, 25);
            this.label5.TabIndex = 28;
            this.label5.Text = "Pen Width";
            // 
            // numericUpDownChannelPen
            // 
            this.numericUpDownChannelPen.DecimalPlaces = 1;
            this.numericUpDownChannelPen.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownChannelPen.Location = new System.Drawing.Point(7, 31);
            this.numericUpDownChannelPen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownChannelPen.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownChannelPen.Name = "numericUpDownChannelPen";
            this.numericUpDownChannelPen.Size = new System.Drawing.Size(64, 26);
            this.numericUpDownChannelPen.TabIndex = 26;
            this.numericUpDownChannelPen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(106, 67);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(4, 5, 0, 0);
            this.label6.Size = new System.Drawing.Size(50, 25);
            this.label6.TabIndex = 27;
            this.label6.Text = "Color";
            // 
            // btnChannelColor
            // 
            this.btnChannelColor.BackColor = System.Drawing.Color.Red;
            this.btnChannelColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChannelColor.Location = new System.Drawing.Point(7, 67);
            this.btnChannelColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnChannelColor.Name = "btnChannelColor";
            this.btnChannelColor.Size = new System.Drawing.Size(64, 28);
            this.btnChannelColor.TabIndex = 29;
            this.btnChannelColor.Text = "...";
            this.btnChannelColor.UseVisualStyleBackColor = false;
            this.btnChannelColor.Click += new System.EventHandler(this.ColorButtonGeneric_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(916, 681);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(79, 41);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(822, 681);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 41);
            this.btnCancel.TabIndex = 33;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSPFPColor
            // 
            this.btnSPFPColor.BackColor = System.Drawing.Color.Red;
            this.btnSPFPColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSPFPColor.Location = new System.Drawing.Point(376, 45);
            this.btnSPFPColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSPFPColor.Name = "btnSPFPColor";
            this.btnSPFPColor.Size = new System.Drawing.Size(43, 28);
            this.btnSPFPColor.TabIndex = 29;
            this.btnSPFPColor.Text = "...";
            this.btnSPFPColor.UseVisualStyleBackColor = false;
            this.btnSPFPColor.Click += new System.EventHandler(this.ColorButtonGeneric_Click);
            // 
            // numericUpDownSPFPPen
            // 
            this.numericUpDownSPFPPen.DecimalPlaces = 1;
            this.numericUpDownSPFPPen.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownSPFPPen.Location = new System.Drawing.Point(304, 45);
            this.numericUpDownSPFPPen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownSPFPPen.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownSPFPPen.Name = "numericUpDownSPFPPen";
            this.numericUpDownSPFPPen.Size = new System.Drawing.Size(64, 26);
            this.numericUpDownSPFPPen.TabIndex = 26;
            this.numericUpDownSPFPPen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 45);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(4, 5, 0, 0);
            this.label3.Size = new System.Drawing.Size(274, 25);
            this.label3.TabIndex = 28;
            this.label3.Text = "Pen Width and Color for SP/FP gates";
            // 
            // chkSPFPShowCircle
            // 
            this.chkSPFPShowCircle.AutoSize = true;
            this.chkSPFPShowCircle.Location = new System.Drawing.Point(17, 84);
            this.chkSPFPShowCircle.Name = "chkSPFPShowCircle";
            this.chkSPFPShowCircle.Size = new System.Drawing.Size(249, 24);
            this.chkSPFPShowCircle.TabIndex = 30;
            this.chkSPFPShowCircle.Text = "Show circle around SF and FP";
            this.chkSPFPShowCircle.UseVisualStyleBackColor = true;
            // 
            // groupBoxSPFP
            // 
            this.groupBoxSPFP.Controls.Add(this.chkSPFPShowCircle);
            this.groupBoxSPFP.Controls.Add(this.label3);
            this.groupBoxSPFP.Controls.Add(this.numericUpDownSPFPPen);
            this.groupBoxSPFP.Controls.Add(this.btnSPFPColor);
            this.groupBoxSPFP.Location = new System.Drawing.Point(30, 433);
            this.groupBoxSPFP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxSPFP.Name = "groupBoxSPFP";
            this.groupBoxSPFP.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxSPFP.Size = new System.Drawing.Size(491, 167);
            this.groupBoxSPFP.TabIndex = 29;
            this.groupBoxSPFP.TabStop = false;
            this.groupBoxSPFP.Text = "SP and FP Properties for new Parcour";
            // 
            // btnPROHColorLayer
            // 
            this.btnPROHColorLayer.BackColor = System.Drawing.Color.Red;
            this.btnPROHColorLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPROHColorLayer.Location = new System.Drawing.Point(370, 41);
            this.btnPROHColorLayer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPROHColorLayer.Name = "btnPROHColorLayer";
            this.btnPROHColorLayer.Size = new System.Drawing.Size(43, 28);
            this.btnPROHColorLayer.TabIndex = 26;
            this.btnPROHColorLayer.Text = "...";
            this.btnPROHColorLayer.UseVisualStyleBackColor = false;
            this.btnPROHColorLayer.Click += new System.EventHandler(this.ColorButtonGeneric_Click);
            // 
            // numericUpDownPROHAlpha
            // 
            this.numericUpDownPROHAlpha.Location = new System.Drawing.Point(298, 43);
            this.numericUpDownPROHAlpha.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownPROHAlpha.Name = "numericUpDownPROHAlpha";
            this.numericUpDownPROHAlpha.Size = new System.Drawing.Size(64, 26);
            this.numericUpDownPROHAlpha.TabIndex = 25;
            this.numericUpDownPROHAlpha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 41);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Padding = new System.Windows.Forms.Padding(4, 5, 0, 0);
            this.label17.Size = new System.Drawing.Size(283, 25);
            this.label17.TabIndex = 24;
            this.label17.Text = "Opacity (0=fully transparent) and Color";
            // 
            // numericUpDownPROHPen
            // 
            this.numericUpDownPROHPen.DecimalPlaces = 1;
            this.numericUpDownPROHPen.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownPROHPen.Location = new System.Drawing.Point(298, 116);
            this.numericUpDownPROHPen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownPROHPen.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownPROHPen.Name = "numericUpDownPROHPen";
            this.numericUpDownPROHPen.Size = new System.Drawing.Size(64, 26);
            this.numericUpDownPROHPen.TabIndex = 29;
            this.numericUpDownPROHPen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 116);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(4, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(233, 25);
            this.label2.TabIndex = 28;
            this.label2.Text = "Pen Width and Color for Border";
            // 
            // groupBoxLayer
            // 
            this.groupBoxLayer.Controls.Add(this.checkShowPROHBorders);
            this.groupBoxLayer.Controls.Add(this.btnPROHColorBorder);
            this.groupBoxLayer.Controls.Add(this.label2);
            this.groupBoxLayer.Controls.Add(this.numericUpDownPROHPen);
            this.groupBoxLayer.Controls.Add(this.label17);
            this.groupBoxLayer.Controls.Add(this.numericUpDownPROHAlpha);
            this.groupBoxLayer.Controls.Add(this.btnPROHColorLayer);
            this.groupBoxLayer.Location = new System.Drawing.Point(36, 179);
            this.groupBoxLayer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxLayer.Name = "groupBoxLayer";
            this.groupBoxLayer.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxLayer.Size = new System.Drawing.Size(478, 232);
            this.groupBoxLayer.TabIndex = 28;
            this.groupBoxLayer.TabStop = false;
            this.groupBoxLayer.Text = "PROH Layer Properties for new Parcour";
            // 
            // checkShowPROHBorders
            // 
            this.checkShowPROHBorders.AutoSize = true;
            this.checkShowPROHBorders.Location = new System.Drawing.Point(11, 84);
            this.checkShowPROHBorders.Name = "checkShowPROHBorders";
            this.checkShowPROHBorders.Size = new System.Drawing.Size(223, 24);
            this.checkShowPROHBorders.TabIndex = 31;
            this.checkShowPROHBorders.Text = "Draw PROH Layer borders";
            this.checkShowPROHBorders.UseVisualStyleBackColor = true;
            // 
            // btnPROHColorBorder
            // 
            this.btnPROHColorBorder.BackColor = System.Drawing.Color.Red;
            this.btnPROHColorBorder.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPROHColorBorder.Location = new System.Drawing.Point(370, 116);
            this.btnPROHColorBorder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPROHColorBorder.Name = "btnPROHColorBorder";
            this.btnPROHColorBorder.Size = new System.Drawing.Size(43, 28);
            this.btnPROHColorBorder.TabIndex = 30;
            this.btnPROHColorBorder.Text = "...";
            this.btnPROHColorBorder.UseVisualStyleBackColor = false;
            // 
            // groupBoxIntersect
            // 
            this.groupBoxIntersect.Controls.Add(this.chkIntersectionPointsShow);
            this.groupBoxIntersect.Controls.Add(this.label7);
            this.groupBoxIntersect.Controls.Add(this.numericUpDownIntersect);
            this.groupBoxIntersect.Controls.Add(this.label8);
            this.groupBoxIntersect.Controls.Add(this.btnIntersectColor);
            this.groupBoxIntersect.Location = new System.Drawing.Point(520, 354);
            this.groupBoxIntersect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxIntersect.Name = "groupBoxIntersect";
            this.groupBoxIntersect.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxIntersect.Size = new System.Drawing.Size(384, 167);
            this.groupBoxIntersect.TabIndex = 34;
            this.groupBoxIntersect.TabStop = false;
            this.groupBoxIntersect.Text = "Intersection Point Properties";
            // 
            // chkIntersectionPointsShow
            // 
            this.chkIntersectionPointsShow.AutoSize = true;
            this.chkIntersectionPointsShow.Location = new System.Drawing.Point(7, 119);
            this.chkIntersectionPointsShow.Name = "chkIntersectionPointsShow";
            this.chkIntersectionPointsShow.Size = new System.Drawing.Size(211, 24);
            this.chkIntersectionPointsShow.TabIndex = 31;
            this.chkIntersectionPointsShow.Text = "Show Intersection Points";
            this.chkIntersectionPointsShow.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(106, 31);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(4, 5, 0, 0);
            this.label7.Size = new System.Drawing.Size(86, 25);
            this.label7.TabIndex = 28;
            this.label7.Text = "Pen Width";
            // 
            // numericUpDownIntersect
            // 
            this.numericUpDownIntersect.DecimalPlaces = 1;
            this.numericUpDownIntersect.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownIntersect.Location = new System.Drawing.Point(7, 31);
            this.numericUpDownIntersect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownIntersect.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownIntersect.Name = "numericUpDownIntersect";
            this.numericUpDownIntersect.Size = new System.Drawing.Size(64, 26);
            this.numericUpDownIntersect.TabIndex = 26;
            this.numericUpDownIntersect.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(106, 67);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(4, 5, 0, 0);
            this.label8.Size = new System.Drawing.Size(50, 25);
            this.label8.TabIndex = 27;
            this.label8.Text = "Color";
            // 
            // btnIntersectColor
            // 
            this.btnIntersectColor.BackColor = System.Drawing.Color.Red;
            this.btnIntersectColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIntersectColor.Location = new System.Drawing.Point(7, 67);
            this.btnIntersectColor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnIntersectColor.Name = "btnIntersectColor";
            this.btnIntersectColor.Size = new System.Drawing.Size(64, 28);
            this.btnIntersectColor.TabIndex = 29;
            this.btnIntersectColor.Text = "...";
            this.btnIntersectColor.UseVisualStyleBackColor = false;
            this.btnIntersectColor.Click += new System.EventHandler(this.ColorButtonGeneric_Click);
            // 
            // groupBoxCalculationType
            // 
            this.groupBoxCalculationType.Controls.Add(this.radioButtonPenaltyCalcTypeChannel);
            this.groupBoxCalculationType.Controls.Add(this.radioButtonPenaltyCalcTypePROH);
            this.groupBoxCalculationType.Location = new System.Drawing.Point(514, 546);
            this.groupBoxCalculationType.Name = "groupBoxCalculationType";
            this.groupBoxCalculationType.Size = new System.Drawing.Size(386, 121);
            this.groupBoxCalculationType.TabIndex = 35;
            this.groupBoxCalculationType.TabStop = false;
            this.groupBoxCalculationType.Text = "Penalty Calculation Type";
            // 
            // radioButtonPenaltyCalcTypeChannel
            // 
            this.radioButtonPenaltyCalcTypeChannel.AutoSize = true;
            this.radioButtonPenaltyCalcTypeChannel.Location = new System.Drawing.Point(17, 76);
            this.radioButtonPenaltyCalcTypeChannel.Name = "radioButtonPenaltyCalcTypeChannel";
            this.radioButtonPenaltyCalcTypeChannel.Size = new System.Drawing.Size(309, 24);
            this.radioButtonPenaltyCalcTypeChannel.TabIndex = 1;
            this.radioButtonPenaltyCalcTypeChannel.TabStop = true;
            this.radioButtonPenaltyCalcTypeChannel.Text = "Penalty when leaving assigned channel";
            this.radioButtonPenaltyCalcTypeChannel.UseVisualStyleBackColor = true;
            // 
            // radioButtonPenaltyCalcTypePROH
            // 
            this.radioButtonPenaltyCalcTypePROH.AutoSize = true;
            this.radioButtonPenaltyCalcTypePROH.Location = new System.Drawing.Point(17, 39);
            this.radioButtonPenaltyCalcTypePROH.Name = "radioButtonPenaltyCalcTypePROH";
            this.radioButtonPenaltyCalcTypePROH.Size = new System.Drawing.Size(311, 24);
            this.radioButtonPenaltyCalcTypePROH.TabIndex = 0;
            this.radioButtonPenaltyCalcTypePROH.TabStop = true;
            this.radioButtonPenaltyCalcTypePROH.Text = "Penalty when entering prohibited zones";
            this.radioButtonPenaltyCalcTypePROH.UseVisualStyleBackColor = true;
            this.radioButtonPenaltyCalcTypePROH.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // ParcourSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxCalculationType);
            this.Controls.Add(this.groupBoxIntersect);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBoxChannel);
            this.Controls.Add(this.groupBoxSPFP);
            this.Controls.Add(this.groupBoxLayer);
            this.Name = "ParcourSettings";
            this.Size = new System.Drawing.Size(1094, 802);
            this.groupBoxChannel.ResumeLayout(false);
            this.groupBoxChannel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChannelPen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSPFPPen)).EndInit();
            this.groupBoxSPFP.ResumeLayout(false);
            this.groupBoxSPFP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPROHAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPROHPen)).EndInit();
            this.groupBoxLayer.ResumeLayout(false);
            this.groupBoxLayer.PerformLayout();
            this.groupBoxIntersect.ResumeLayout(false);
            this.groupBoxIntersect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntersect)).EndInit();
            this.groupBoxCalculationType.ResumeLayout(false);
            this.groupBoxCalculationType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxChannel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownChannelPen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnChannelColor;
        private System.Windows.Forms.CheckBox chkChannelShowCL;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSPFPColor;
        private System.Windows.Forms.NumericUpDown numericUpDownSPFPPen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkSPFPShowCircle;
        private System.Windows.Forms.GroupBox groupBoxSPFP;
        private System.Windows.Forms.Button btnPROHColorLayer;
        private System.Windows.Forms.NumericUpDown numericUpDownPROHAlpha;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown numericUpDownPROHPen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxLayer;
        private System.Windows.Forms.GroupBox groupBoxIntersect;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownIntersect;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnIntersectColor;
        private System.Windows.Forms.GroupBox groupBoxCalculationType;
        private System.Windows.Forms.RadioButton radioButtonPenaltyCalcTypeChannel;
        private System.Windows.Forms.RadioButton radioButtonPenaltyCalcTypePROH;
        private System.Windows.Forms.CheckBox chkIntersectionPointsShow;
        private System.Windows.Forms.CheckBox checkShowPROHBorders;
        private System.Windows.Forms.Button btnPROHColorBorder;
    }
}
