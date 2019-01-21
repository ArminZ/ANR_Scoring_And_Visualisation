using System;
using System.Windows.Forms;

namespace AirNavigationRaceLive.Comps
{

    public partial class UserSettings : UserControl
    {
        public UserSettings()
        {
            InitializeComponent();
            getSettings();
        }

        private void ColorButtonGeneric_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = false;
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            ((Button)(sender)).BackColor = cd.Color;
        }

        private void saveSettings()
        {
            Properties.Settings.Default.promptForDB = !chkDefaultDBDirectory.Checked;
            Properties.Settings.Default.directoryForDB = textBoxDatabasePath.Text;
            Properties.Settings.Default.parcourPDFAdditionalText = chkParcourAdditionalText.Checked;

            Properties.Settings.Default.PROHColor = btnPROHColorLayer.BackColor;
            Properties.Settings.Default.PROHTransp = numericUpDownPROHAlpha.Value;
            Properties.Settings.Default.PROHBorderPenWidth = numericUpDownPROHBorderPen.Value;
            Properties.Settings.Default.PROHBorderColor = btnPROHBorderColor.BackColor;
            Properties.Settings.Default.ShowPROHBorder = chkShowPROHBorders.Checked;
            Properties.Settings.Default.HasPROHFill = chkHasPROHFill.Checked;

            Properties.Settings.Default.SPFPColor = btnSPFPColor.BackColor;
            Properties.Settings.Default.SPFPenWidth = numericUpDownSPFPPen.Value;
            Properties.Settings.Default.SPFPCircle = chkSPFPShowCircle.Checked;

            Properties.Settings.Default.ChannelColor = btnChannelColor.BackColor;
            Properties.Settings.Default.ChannelPenWidth = numericUpDownChannelPen.Value;
            Properties.Settings.Default.ChannelFillColor = btnChannelFillColor.BackColor;
            Properties.Settings.Default.HasChannelFill = chkHasChannelFill.Checked;

            Properties.Settings.Default.IntersectionColor = btnIntersectColor.BackColor;
            Properties.Settings.Default.IntersectionPenWidth = numericUpDownIntersectPen.Value;
            Properties.Settings.Default.ShowIntersectionCircles = chkShowIntersectionCircles.Checked;
            Properties.Settings.Default.IntersectionCircleRadius = numericUpDownIntersectRadius.Value;

            Properties.Settings.Default.Save();
        }

        private void getSettings()
        {
            chkDefaultDBDirectory.Checked = !Properties.Settings.Default.promptForDB;
            textBoxDatabasePath.Enabled = chkDefaultDBDirectory.Checked;
            chkParcourAdditionalText.Checked = Properties.Settings.Default.parcourPDFAdditionalText;
            textBoxDatabasePath.Text = Properties.Settings.Default.directoryForDB;

            btnPROHColorLayer.BackColor = Properties.Settings.Default.PROHColor;
            numericUpDownPROHAlpha.Value = Properties.Settings.Default.PROHTransp;
            btnPROHBorderColor.BackColor = Properties.Settings.Default.PROHBorderColor;
            numericUpDownPROHBorderPen.Value = Properties.Settings.Default.PROHBorderPenWidth;
            chkShowPROHBorders.Checked = Properties.Settings.Default.ShowPROHBorder;
            chkHasPROHFill.Checked = Properties.Settings.Default.HasPROHFill;

            btnSPFPColor.BackColor = Properties.Settings.Default.SPFPColor;
            numericUpDownSPFPPen.Value = Properties.Settings.Default.SPFPenWidth;
            chkSPFPShowCircle.Checked = Properties.Settings.Default.SPFPCircle;

            btnChannelColor.BackColor = Properties.Settings.Default.ChannelColor;
            numericUpDownChannelPen.Value = Properties.Settings.Default.ChannelPenWidth;
            btnChannelFillColor.BackColor = Properties.Settings.Default.ChannelFillColor;
            chkHasChannelFill.Checked = Properties.Settings.Default.HasChannelFill;

            btnIntersectColor.BackColor = Properties.Settings.Default.IntersectionColor;
            numericUpDownIntersectPen.Value = Properties.Settings.Default.IntersectionPenWidth;
            numericUpDownIntersectRadius.Value = Properties.Settings.Default.IntersectionCircleRadius;
            chkShowIntersectionCircles.Checked = Properties.Settings.Default.ShowIntersectionCircles;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveSettings();     
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            getSettings();
        }

        private void btnDB_Click(object sender, EventArgs e)
        {
            textBoxDatabasePath.Text = Comps.Helper.Utils.getDbPath(true);
        }
    }
}
