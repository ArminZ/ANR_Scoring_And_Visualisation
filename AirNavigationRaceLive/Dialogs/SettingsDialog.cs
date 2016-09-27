using AirNavigationRaceLive.Comps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AirNavigationRaceLive.Dialogs
{
    public partial class SettingsDialog : Form
    {
        public Comps.Client.DataAccess Client;
        public SettingsDialog()
        {
            InitializeComponent();
            errorProvider1.Clear();
            getSettings();
        }

        private bool isValidSettings()
        {
            errorProvider1.Clear();
            // check if path exists or
            bool ret = true;
            if (radioButtonFixed.Checked)
            {
                // check if valid file path
                if (!System.IO.Directory.Exists(textBoxDatabasePath.Text))
                {
                    ret = false;
                    errorProvider1.SetError(textBoxDatabasePath, "Invalid directory");
                }
            }
            return ret;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                saveSettings();
            }
            catch (Exception)
            {
                throw;
            }
            Close();
        }

        private void btnFindDatabase_Click(object sender, EventArgs e)
        {
            textBoxDatabasePath.Text = Comps.Helper.Utils.getDbPath(true);
        }

        private void saveSettings()
        {
            Properties.Settings.Default.promptForDB = radioButtonPrompt.Checked;
            Properties.Settings.Default.directoryForDB = textBoxDatabasePath.Text;
            Properties.Settings.Default.Save();
        }

        private void getSettings()
        {
            radioButtonPrompt.Checked = Properties.Settings.Default.promptForDB;
            radioButtonFixed.Checked = !radioButtonPrompt.Checked;
            textBoxDatabasePath.Text = Properties.Settings.Default.directoryForDB;
        }

        private void radioButtonFixed_CheckedChanged(object sender, EventArgs e)
        {
            textBoxDatabasePath.Enabled = radioButtonFixed.Checked;
            btnOK.Enabled = isValidSettings();
        }

        private void radioButtonPrompt_CheckedChanged(object sender, EventArgs e)
        {
            textBoxDatabasePath.Enabled = !radioButtonPrompt.Checked;
            btnOK.Enabled = isValidSettings();
        }

        private void textBoxDatabasePath_TextChanged(object sender, EventArgs e)
        {
           btnOK.Enabled= isValidSettings();
        }
    }
}
