namespace AirNavigationRaceLive.Comps
{
    partial class RouteGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RouteGenerator));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkAddAllRoutes = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.lblSelectedRoutes = new System.Windows.Forms.Label();
            this.btnClearSelectedRoutes = new System.Windows.Forms.Button();
            this.btnAddRoute = new System.Windows.Forms.Button();
            this.lblRoute = new System.Windows.Forms.Label();
            this.txtChannelWidth = new System.Windows.Forms.TextBox();
            this.btnSelectKML = new System.Windows.Forms.Button();
            this.lblChannelWidth = new System.Windows.Forms.Label();
            this.treeViewAvailableRoutes = new System.Windows.Forms.TreeView();
            this.treeViewSelectedRoutes = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSaveKML = new System.Windows.Forms.Button();
            this.btnTEST = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 34);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(142, 30);
            this.refreshToolStripMenuItem.Text = "Refresh";
            // 
            // chkAddAllRoutes
            // 
            this.chkAddAllRoutes.AutoSize = true;
            this.chkAddAllRoutes.Checked = true;
            this.chkAddAllRoutes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddAllRoutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.chkAddAllRoutes.Location = new System.Drawing.Point(401, 293);
            this.chkAddAllRoutes.Margin = new System.Windows.Forms.Padding(5);
            this.chkAddAllRoutes.Name = "chkAddAllRoutes";
            this.chkAddAllRoutes.Size = new System.Drawing.Size(197, 24);
            this.chkAddAllRoutes.TabIndex = 3;
            this.chkAddAllRoutes.Text = "Add all available routes";
            this.chkAddAllRoutes.UseVisualStyleBackColor = true;
            this.chkAddAllRoutes.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.label3.Location = new System.Drawing.Point(34, 51);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(903, 100);
            this.label3.TabIndex = 37;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.lblHeight.Location = new System.Drawing.Point(150, 451);
            this.lblHeight.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(233, 20);
            this.lblHeight.TabIndex = 33;
            this.lblHeight.Text = "Height AGL [m] for LiveTracking";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(38, 445);
            this.txtHeight.Margin = new System.Windows.Forms.Padding(5);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(79, 31);
            this.txtHeight.TabIndex = 31;
            this.txtHeight.Text = "300";
            this.txtHeight.TextChanged += new System.EventHandler(this.txtHeight_TextChanged);
            // 
            // lblSelectedRoutes
            // 
            this.lblSelectedRoutes.AutoSize = true;
            this.lblSelectedRoutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblSelectedRoutes.Location = new System.Drawing.Point(630, 190);
            this.lblSelectedRoutes.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSelectedRoutes.Name = "lblSelectedRoutes";
            this.lblSelectedRoutes.Size = new System.Drawing.Size(141, 20);
            this.lblSelectedRoutes.TabIndex = 36;
            this.lblSelectedRoutes.Text = "Selected routes:";
            this.lblSelectedRoutes.Visible = false;
            // 
            // btnClearSelectedRoutes
            // 
            this.btnClearSelectedRoutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnClearSelectedRoutes.Location = new System.Drawing.Point(401, 329);
            this.btnClearSelectedRoutes.Margin = new System.Windows.Forms.Padding(5);
            this.btnClearSelectedRoutes.Name = "btnClearSelectedRoutes";
            this.btnClearSelectedRoutes.Size = new System.Drawing.Size(206, 41);
            this.btnClearSelectedRoutes.TabIndex = 4;
            this.btnClearSelectedRoutes.Text = "Clear selected";
            this.btnClearSelectedRoutes.UseVisualStyleBackColor = true;
            this.btnClearSelectedRoutes.Visible = false;
            this.btnClearSelectedRoutes.Click += new System.EventHandler(this.btnClearSelected_Click);
            // 
            // btnAddRoute
            // 
            this.btnAddRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnAddRoute.Location = new System.Drawing.Point(401, 249);
            this.btnAddRoute.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddRoute.Name = "btnAddRoute";
            this.btnAddRoute.Size = new System.Drawing.Size(206, 40);
            this.btnAddRoute.TabIndex = 2;
            this.btnAddRoute.Text = "Add to selected";
            this.btnAddRoute.UseVisualStyleBackColor = true;
            this.btnAddRoute.Visible = false;
            this.btnAddRoute.Click += new System.EventHandler(this.btnAddRoute_Click);
            // 
            // lblRoute
            // 
            this.lblRoute.AutoSize = true;
            this.lblRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.lblRoute.Location = new System.Drawing.Point(34, 190);
            this.lblRoute.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblRoute.Name = "lblRoute";
            this.lblRoute.Size = new System.Drawing.Size(142, 20);
            this.lblRoute.TabIndex = 34;
            this.lblRoute.Text = "Available routes:";
            // 
            // txtChannelWidth
            // 
            this.txtChannelWidth.Location = new System.Drawing.Point(38, 415);
            this.txtChannelWidth.Margin = new System.Windows.Forms.Padding(5);
            this.txtChannelWidth.Name = "txtChannelWidth";
            this.txtChannelWidth.Size = new System.Drawing.Size(79, 31);
            this.txtChannelWidth.TabIndex = 29;
            this.txtChannelWidth.TextChanged += new System.EventHandler(this.txtChannelWidth_TextChanged);
            // 
            // btnSelectKML
            // 
            this.btnSelectKML.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnSelectKML.Location = new System.Drawing.Point(38, 491);
            this.btnSelectKML.Margin = new System.Windows.Forms.Padding(5);
            this.btnSelectKML.Name = "btnSelectKML";
            this.btnSelectKML.Size = new System.Drawing.Size(206, 40);
            this.btnSelectKML.TabIndex = 1;
            this.btnSelectKML.Text = "Open *.kml route file...";
            this.btnSelectKML.UseVisualStyleBackColor = true;
            this.btnSelectKML.Click += new System.EventHandler(this.btnSelectKML_Click);
            // 
            // lblChannelWidth
            // 
            this.lblChannelWidth.AutoSize = true;
            this.lblChannelWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.lblChannelWidth.Location = new System.Drawing.Point(150, 421);
            this.lblChannelWidth.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblChannelWidth.Name = "lblChannelWidth";
            this.lblChannelWidth.Size = new System.Drawing.Size(218, 20);
            this.lblChannelWidth.TabIndex = 30;
            this.lblChannelWidth.Text = "Channel width [Nautical miles]";
            // 
            // treeViewAvailableRoutes
            // 
            this.treeViewAvailableRoutes.Location = new System.Drawing.Point(38, 246);
            this.treeViewAvailableRoutes.Margin = new System.Windows.Forms.Padding(5);
            this.treeViewAvailableRoutes.Name = "treeViewAvailableRoutes";
            this.treeViewAvailableRoutes.Size = new System.Drawing.Size(337, 159);
            this.treeViewAvailableRoutes.TabIndex = 32;
            // 
            // treeViewSelectedRoutes
            // 
            this.treeViewSelectedRoutes.Location = new System.Drawing.Point(634, 249);
            this.treeViewSelectedRoutes.Margin = new System.Windows.Forms.Padding(5);
            this.treeViewSelectedRoutes.Name = "treeViewSelectedRoutes";
            this.treeViewSelectedRoutes.Size = new System.Drawing.Size(300, 156);
            this.treeViewSelectedRoutes.TabIndex = 39;
            this.treeViewSelectedRoutes.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.label1.Location = new System.Drawing.Point(34, 210);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(421, 20);
            this.label1.TabIndex = 40;
            this.label1.Text = "Only the characters A, B, C, D are allowed as route names.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(34, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 20);
            this.label2.TabIndex = 41;
            this.label2.Text = "Route Generator";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // btnSaveKML
            // 
            this.btnSaveKML.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.btnSaveKML.Location = new System.Drawing.Point(634, 493);
            this.btnSaveKML.Margin = new System.Windows.Forms.Padding(5);
            this.btnSaveKML.Name = "btnSaveKML";
            this.btnSaveKML.Size = new System.Drawing.Size(206, 38);
            this.btnSaveKML.TabIndex = 5;
            this.btnSaveKML.Text = "Save layer as *.kml file...";
            this.btnSaveKML.UseVisualStyleBackColor = true;
            this.btnSaveKML.Click += new System.EventHandler(this.btnSaveKML_Click);
            // 
            // btnTEST
            // 
            this.btnTEST.Location = new System.Drawing.Point(737, 125);
            this.btnTEST.Name = "btnTEST";
            this.btnTEST.Size = new System.Drawing.Size(140, 47);
            this.btnTEST.TabIndex = 42;
            this.btnTEST.Text = "TEST_inverter";
            this.btnTEST.UseVisualStyleBackColor = true;
            this.btnTEST.Visible = false;
            this.btnTEST.Click += new System.EventHandler(this.btnTEST_Click);
            // 
            // RouteGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnTEST);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeViewSelectedRoutes);
            this.Controls.Add(this.chkAddAllRoutes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.lblSelectedRoutes);
            this.Controls.Add(this.btnClearSelectedRoutes);
            this.Controls.Add(this.btnAddRoute);
            this.Controls.Add(this.lblRoute);
            this.Controls.Add(this.btnSaveKML);
            this.Controls.Add(this.txtChannelWidth);
            this.Controls.Add(this.btnSelectKML);
            this.Controls.Add(this.lblChannelWidth);
            this.Controls.Add(this.treeViewAvailableRoutes);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "RouteGenerator";
            this.Size = new System.Drawing.Size(968, 560);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkAddAllRoutes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label lblSelectedRoutes;
        private System.Windows.Forms.Button btnClearSelectedRoutes;
        private System.Windows.Forms.Button btnAddRoute;
        private System.Windows.Forms.Label lblRoute;
        private System.Windows.Forms.TextBox txtChannelWidth;
        private System.Windows.Forms.Button btnSelectKML;
        private System.Windows.Forms.Label lblChannelWidth;
        private System.Windows.Forms.TreeView treeViewAvailableRoutes;
        private System.Windows.Forms.TreeView treeViewSelectedRoutes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnSaveKML;
        private System.Windows.Forms.Button btnTEST;
    }
}
