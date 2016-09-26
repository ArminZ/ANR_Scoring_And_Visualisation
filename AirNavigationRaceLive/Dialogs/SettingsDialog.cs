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
            textBoxDatabasePath.Text = getDBFilePath();
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
        }

        private void radioButtonPrompt_CheckedChanged(object sender, EventArgs e)
        {
            textBoxDatabasePath.Enabled = !radioButtonPrompt.Checked;
        }

        private string getDBFilePath()
        {
            string dbPath = string.Empty;
            SaveFileDialog dbLocationDialog = new SaveFileDialog();
            dbLocationDialog.RestoreDirectory = true;
            dbLocationDialog.Title = "Select a Folder where ANR will maintain its internal DataBase (anrl.mdf)";
            dbLocationDialog.FileName = "anrl.mdf";
            dbLocationDialog.OverwritePrompt = false;
            dbLocationDialog.ShowDialog();
            dbPath = dbLocationDialog.FileName.Replace("anrl.mdf", "");
            if (dbPath == null || dbPath == "")
            {
                dbPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AirNavigationRace";
            }
            if (System.IO.Directory.Exists(dbPath))
            {
                return dbPath;
            }
            else
            {
                return string.Empty;
            }
        }

        private void textBoxDatabasePath_TextChanged(object sender, EventArgs e)
        {
           btnOK.Enabled= isValidSettings();
        }
    }
}
