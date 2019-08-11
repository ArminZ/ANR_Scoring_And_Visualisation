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
            this.btnFindDatabase = new System.Windows.Forms.Button();
            this.textBoxDatabasePath = new System.Windows.Forms.TextBox();
            this.checkBoxDefaultDBDirectory = new System.Windows.Forms.CheckBox();
            this.checkBoxParcourAdditionalText = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(826, 90);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(104, 30);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(826, 130);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 30);
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
            // btnFindDatabase
            // 
            this.btnFindDatabase.Location = new System.Drawing.Point(891, 16);
            this.btnFindDatabase.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFindDatabase.Name = "btnFindDatabase";
            this.btnFindDatabase.Size = new System.Drawing.Size(38, 29);
            this.btnFindDatabase.TabIndex = 3;
            this.btnFindDatabase.Text = "...";
            this.btnFindDatabase.UseVisualStyleBackColor = true;
            this.btnFindDatabase.Click += new System.EventHandler(this.btnFindDatabase_Click);
            // 
            // textBoxDatabasePath
            // 
            this.textBoxDatabasePath.Location = new System.Drawing.Point(400, 16);
            this.textBoxDatabasePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxDatabasePath.Name = "textBoxDatabasePath";
            this.textBoxDatabasePath.Size = new System.Drawing.Size(483, 26);
            this.textBoxDatabasePath.TabIndex = 2;
            this.textBoxDatabasePath.TextChanged += new System.EventHandler(this.textBoxDatabasePath_TextChanged);
            // 
            // checkBoxDefaultDBDirectory
            // 
            this.checkBoxDefaultDBDirectory.AutoSize = true;
            this.checkBoxDefaultDBDirectory.Location = new System.Drawing.Point(26, 16);
            this.checkBoxDefaultDBDirectory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxDefaultDBDirectory.Name = "checkBoxDefaultDBDirectory";
            this.checkBoxDefaultDBDirectory.Size = new System.Drawing.Size(349, 24);
            this.checkBoxDefaultDBDirectory.TabIndex = 8;
            this.checkBoxDefaultDBDirectory.Text = "Database File: use a default DB file directory";
            this.checkBoxDefaultDBDirectory.UseVisualStyleBackColor = true;
            this.checkBoxDefaultDBDirectory.CheckedChanged += new System.EventHandler(this.checkBoxDefaultDBDirectory_CheckedChanged);
            // 
            // checkBoxParcourAdditionalText
            // 
            this.checkBoxParcourAdditionalText.AutoSize = true;
            this.checkBoxParcourAdditionalText.Location = new System.Drawing.Point(26, 50);
            this.checkBoxParcourAdditionalText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxParcourAdditionalText.Name = "checkBoxParcourAdditionalText";
            this.checkBoxParcourAdditionalText.Size = new System.Drawing.Size(358, 24);
            this.checkBoxParcourAdditionalText.TabIndex = 9;
            this.checkBoxParcourAdditionalText.Text = "Parcour PDF export: Prompt for additional text";
            this.checkBoxParcourAdditionalText.UseVisualStyleBackColor = true;
            this.checkBoxParcourAdditionalText.CheckedChanged += new System.EventHandler(this.checkBoxParcourAdditionalText_CheckedChanged);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(948, 191);
            this.Controls.Add(this.checkBoxParcourAdditionalText);
            this.Controls.Add(this.checkBoxDefaultDBDirectory);
            this.Controls.Add(this.btnFindDatabase);
            this.Controls.Add(this.textBoxDatabasePath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SettingsDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnFindDatabase;
        private System.Windows.Forms.TextBox textBoxDatabasePath;
        private System.Windows.Forms.CheckBox checkBoxDefaultDBDirectory;
        private System.Windows.Forms.CheckBox checkBoxParcourAdditionalText;
    }
}