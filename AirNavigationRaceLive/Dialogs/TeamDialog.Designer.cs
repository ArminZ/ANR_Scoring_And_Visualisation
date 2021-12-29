﻿namespace AirNavigationRaceLive.Dialogs
{
    partial class TeamDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeamDialog));
            this.btnOK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxPilot = new System.Windows.Forms.ComboBox();
            this.comboBoxNavigator = new System.Windows.Forms.ComboBox();
            this.textBoxCrewNumber = new System.Windows.Forms.TextBox();
            this.textBoxNationality = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAC = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnColorSelect = new System.Windows.Forms.Button();
            this.textBoxExternalId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(696, 131);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(104, 30);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 108);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "Pilot";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(809, 131);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(372, 108);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Navigator";
            // 
            // comboBoxPilot
            // 
            this.comboBoxPilot.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxPilot.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxPilot.FormattingEnabled = true;
            this.comboBoxPilot.Location = new System.Drawing.Point(26, 131);
            this.comboBoxPilot.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxPilot.Name = "comboBoxPilot";
            this.comboBoxPilot.Size = new System.Drawing.Size(281, 28);
            this.comboBoxPilot.TabIndex = 1;
            this.comboBoxPilot.SelectedIndexChanged += new System.EventHandler(this.comboBoxPilot_SelectedIndexChanged);
            // 
            // comboBoxNavigator
            // 
            this.comboBoxNavigator.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxNavigator.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxNavigator.FormattingEnabled = true;
            this.comboBoxNavigator.Location = new System.Drawing.Point(376, 132);
            this.comboBoxNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxNavigator.Name = "comboBoxNavigator";
            this.comboBoxNavigator.Size = new System.Drawing.Size(281, 28);
            this.comboBoxNavigator.TabIndex = 2;
            this.comboBoxNavigator.SelectedIndexChanged += new System.EventHandler(this.comboBoxNavigator_SelectedIndexChanged);
            // 
            // textBoxCrewNumber
            // 
            this.textBoxCrewNumber.Location = new System.Drawing.Point(26, 34);
            this.textBoxCrewNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxCrewNumber.Name = "textBoxCrewNumber";
            this.textBoxCrewNumber.Size = new System.Drawing.Size(112, 26);
            this.textBoxCrewNumber.TabIndex = 3;
            this.textBoxCrewNumber.TextChanged += new System.EventHandler(this.textBoxCrewNumber_TextChanged);
            // 
            // textBoxNationality
            // 
            this.textBoxNationality.Location = new System.Drawing.Point(195, 38);
            this.textBoxNationality.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxNationality.Name = "textBoxNationality";
            this.textBoxNationality.Size = new System.Drawing.Size(112, 26);
            this.textBoxNationality.TabIndex = 4;
            this.textBoxNationality.TextChanged += new System.EventHandler(this.textBoxNationality_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 20);
            this.label2.TabIndex = 31;
            this.label2.Text = "Crew Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(191, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.TabIndex = 32;
            this.label4.Text = "Nationality";
            // 
            // textBoxAC
            // 
            this.textBoxAC.Location = new System.Drawing.Point(376, 34);
            this.textBoxAC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxAC.Name = "textBoxAC";
            this.textBoxAC.Size = new System.Drawing.Size(112, 26);
            this.textBoxAC.TabIndex = 5;
            this.textBoxAC.TextChanged += new System.EventHandler(this.textBoxAC_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(372, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 20);
            this.label5.TabIndex = 34;
            this.label5.Text = "AC";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // btnColorSelect
            // 
            this.btnColorSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnColorSelect.Location = new System.Drawing.Point(558, 30);
            this.btnColorSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnColorSelect.Name = "btnColorSelect";
            this.btnColorSelect.Size = new System.Drawing.Size(99, 35);
            this.btnColorSelect.TabIndex = 6;
            this.btnColorSelect.Text = "Color Select";
            this.btnColorSelect.UseVisualStyleBackColor = true;
            this.btnColorSelect.Click += new System.EventHandler(this.btnColorSelect_Click);
            // 
            // textBoxExternalId
            // 
            this.textBoxExternalId.Location = new System.Drawing.Point(801, 34);
            this.textBoxExternalId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxExternalId.Name = "textBoxExternalId";
            this.textBoxExternalId.Size = new System.Drawing.Size(112, 26);
            this.textBoxExternalId.TabIndex = 35;
            this.textBoxExternalId.TextChanged += new System.EventHandler(this.textBoxExternalId_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(797, 11);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 20);
            this.label6.TabIndex = 36;
            this.label6.Text = "ExternalId";
            // 
            // TeamDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(944, 191);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxExternalId);
            this.Controls.Add(this.btnColorSelect);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxAC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxNationality);
            this.Controls.Add(this.textBoxCrewNumber);
            this.Controls.Add(this.comboBoxNavigator);
            this.Controls.Add(this.comboBoxPilot);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TeamDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Crew";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TeamDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxPilot;
        private System.Windows.Forms.ComboBox comboBoxNavigator;
        private System.Windows.Forms.TextBox textBoxCrewNumber;
        private System.Windows.Forms.TextBox textBoxNationality;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxAC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnColorSelect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxExternalId;
    }
}