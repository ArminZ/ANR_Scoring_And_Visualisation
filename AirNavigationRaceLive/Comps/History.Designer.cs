namespace AirNavigationRaceLive.Comps
{
    partial class History
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(History));
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label1.Location = new System.Drawing.Point(538, 338);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(528, 180);
            this.label1.TabIndex = 52;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.linkLabel1.Location = new System.Drawing.Point(538, 566);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(242, 20);
            this.linkLabel1.TabIndex = 59;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://github.com/helios57/anrl";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // PictureBox4
            // 
            this.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PictureBox4.Cursor = System.Windows.Forms.Cursors.Default;
            this.PictureBox4.Image = global::AirNavigationRaceLive.Properties.Resources.FAI_GAC;
            this.PictureBox4.InitialImage = ((System.Drawing.Image)(resources.GetObject("PictureBox4.InitialImage")));
            this.PictureBox4.Location = new System.Drawing.Point(23, 26);
            this.PictureBox4.Margin = new System.Windows.Forms.Padding(4);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(510, 311);
            this.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox4.TabIndex = 58;
            this.PictureBox4.TabStop = false;
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.PictureBox4);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "History";
            this.Size = new System.Drawing.Size(1111, 615);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PictureBox4;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}
