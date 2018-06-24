using AirNavigationRaceLive.Client;
using System;
using System.Windows.Forms;

namespace AirNavigationRaceLive.Dialogs
{
    public partial class SettingsDialog : Form
    {
        public DataAccess Client;
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
            if (checkBoxDefaultDBDirectory.Checked)
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
            Properties.Settings.Default.promptForDB = !checkBoxDefaultDBDirectory.Checked;
            Properties.Settings.Default.directoryForDB = textBoxDatabasePath.Text;
            Properties.Settings.Default.parcourPDFAdditionalText = checkBoxParcourAdditionalText.Checked;
            Properties.Settings.Default.Save();
        }

        private void getSettings()
        {
            checkBoxDefaultDBDirectory.Checked = !Properties.Settings.Default.promptForDB;
            checkBoxParcourAdditionalText.Checked = Properties.Settings.Default.parcourPDFAdditionalText;
            textBoxDatabasePath.Text = Properties.Settings.Default.directoryForDB;
            textBoxDatabasePath.Enabled = checkBoxDefaultDBDirectory.Checked;
        }

    
        private void textBoxDatabasePath_TextChanged(object sender, EventArgs e)
        {
           btnOK.Enabled= isValidSettings();
        }

        private void checkBoxDefaultDBDirectory_CheckedChanged(object sender, EventArgs e)
        {
            textBoxDatabasePath.Enabled = checkBoxDefaultDBDirectory.Checked;
            btnOK.Enabled = isValidSettings();
        }

        private void checkBoxParcourAdditionalText_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
