using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void btnPROHColorLayer_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = false;
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            btnPROHColorLayer.BackColor = cd.Color;
        }

        private void btnSPFPColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = false;
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            btnSPFPColor.BackColor = cd.Color;
        }

        private void btnChannelColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = false;
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            btnChannelColor.BackColor = cd.Color;
        }

        private void saveSettings()
        {
            Properties.Settings.Default.promptForDB = !chkDefaultDBDirectory.Checked;
            Properties.Settings.Default.directoryForDB = textBoxDatabasePath.Text;
            Properties.Settings.Default.parcourPDFAdditionalText = chkParcourAdditionalText.Checked;

            Properties.Settings.Default.PROHColor = btnPROHColorLayer.BackColor;
            Properties.Settings.Default.PROHTransp = numericUpDownPROHAlpha.Value;
            Properties.Settings.Default.PROHPenWidth = numericUpDownPROHPen.Value;

            Properties.Settings.Default.SPFPColor = btnSPFPColor.BackColor;
            Properties.Settings.Default.SPFPenWidth = numericUpDownSPFPPen.Value;
            Properties.Settings.Default.SPFPCircle = chkSPFPShowCircle.Checked;

            Properties.Settings.Default.ChannelColor = btnChannelColor.BackColor;
            Properties.Settings.Default.ChannelPenWidth = numericUpDownChannelPen.Value;
            chkChannelShowCL.Checked = Properties.Settings.Default.ChannelShowCL;

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
            numericUpDownPROHPen.Value = Properties.Settings.Default.PROHPenWidth;

            btnSPFPColor.BackColor = Properties.Settings.Default.SPFPColor;
            numericUpDownSPFPPen.Value = Properties.Settings.Default.SPFPenWidth;
            chkSPFPShowCircle.Checked = Properties.Settings.Default.SPFPCircle;

            btnChannelColor.BackColor = Properties.Settings.Default.ChannelColor;
            numericUpDownChannelPen.Value = Properties.Settings.Default.ChannelPenWidth;
            chkChannelShowCL.Checked = Properties.Settings.Default.ChannelShowCL;
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
