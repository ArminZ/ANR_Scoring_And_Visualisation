namespace AirNavigationRaceLive.Dialogs
{
    partial class SettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBoxDbSettings = new System.Windows.Forms.GroupBox();
            this.btnFindDatabase = new System.Windows.Forms.Button();
            this.textBoxDatabasePath = new System.Windows.Forms.TextBox();
            this.radioButtonPrompt = new System.Windows.Forms.RadioButton();
            this.radioButtonFixed = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBoxDbSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(734, 72);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(92, 24);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(734, 104);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 24);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // groupBoxDbSettings
            // 
            this.groupBoxDbSettings.Controls.Add(this.btnFindDatabase);
            this.groupBoxDbSettings.Controls.Add(this.textBoxDatabasePath);
            this.groupBoxDbSettings.Controls.Add(this.radioButtonPrompt);
            this.groupBoxDbSettings.Controls.Add(this.radioButtonFixed);
            this.groupBoxDbSettings.Location = new System.Drawing.Point(23, 12);
            this.groupBoxDbSettings.Name = "groupBoxDbSettings";
            this.groupBoxDbSettings.Size = new System.Drawing.Size(704, 116);
            this.groupBoxDbSettings.TabIndex = 32;
            this.groupBoxDbSettings.TabStop = false;
            this.groupBoxDbSettings.Text = "Database directory";
            // 
            // btnFindDatabase
            // 
            this.btnFindDatabase.Location = new System.Drawing.Point(644, 28);
            this.btnFindDatabase.Name = "btnFindDatabase";
            this.btnFindDatabase.Size = new System.Drawing.Size(34, 23);
            this.btnFindDatabase.TabIndex = 3;
            this.btnFindDatabase.Text = "...";
            this.btnFindDatabase.UseVisualStyleBackColor = true;
            this.btnFindDatabase.Click += new System.EventHandler(this.btnFindDatabase_Click);
            // 
            // textBoxDatabasePath
            // 
            this.textBoxDatabasePath.Location = new System.Drawing.Point(225, 28);
            this.textBoxDatabasePath.Name = "textBoxDatabasePath";
            this.textBoxDatabasePath.Size = new System.Drawing.Size(389, 22);
            this.textBoxDatabasePath.TabIndex = 2;
            this.textBoxDatabasePath.TextChanged += new System.EventHandler(this.textBoxDatabasePath_TextChanged);
            // 
            // radioButtonPrompt
            // 
            this.radioButtonPrompt.AutoSize = true;
            this.radioButtonPrompt.Location = new System.Drawing.Point(6, 73);
            this.radioButtonPrompt.Name = "radioButtonPrompt";
            this.radioButtonPrompt.Size = new System.Drawing.Size(461, 21);
            this.radioButtonPrompt.TabIndex = 1;
            this.radioButtonPrompt.TabStop = true;
            this.radioButtonPrompt.Text = "prompt me for the database location each time I open the application";
            this.radioButtonPrompt.UseVisualStyleBackColor = true;
            this.radioButtonPrompt.CheckedChanged += new System.EventHandler(this.radioButtonPrompt_CheckedChanged);
            // 
            // radioButtonFixed
            // 
            this.radioButtonFixed.AutoSize = true;
            this.radioButtonFixed.Location = new System.Drawing.Point(6, 28);
            this.radioButtonFixed.Name = "radioButtonFixed";
            this.radioButtonFixed.Size = new System.Drawing.Size(213, 21);
            this.radioButtonFixed.TabIndex = 0;
            this.radioButtonFixed.TabStop = true;
            this.radioButtonFixed.Text = "use a fixed database location";
            this.radioButtonFixed.UseVisualStyleBackColor = true;
            this.radioButtonFixed.CheckedChanged += new System.EventHandler(this.radioButtonFixed_CheckedChanged);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(843, 153);
            this.Controls.Add(this.groupBoxDbSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBoxDbSettings.ResumeLayout(false);
            this.groupBoxDbSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBoxDbSettings;
        private System.Windows.Forms.Button btnFindDatabase;
        private System.Windows.Forms.TextBox textBoxDatabasePath;
        private System.Windows.Forms.RadioButton radioButtonPrompt;
        private System.Windows.Forms.RadioButton radioButtonFixed;
    }
}