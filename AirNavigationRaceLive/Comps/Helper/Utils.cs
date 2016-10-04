using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirNavigationRaceLive.Comps.Helper
{
    class Utils
    {
        private static Random r = new Random();
        public static double getNextDouble()
        {
            return r.NextDouble();
        }

        private static string readDBPathFromUserSettings()
        {
            if (!Properties.Settings.Default.promptForDB && !string.IsNullOrEmpty(Properties.Settings.Default.directoryForDB))
            {
                return Properties.Settings.Default.directoryForDB;
            }
            return string.Empty;
        }
        public static string getDbPath(bool mustPrompt=false)
        {
            string dbPath = readDBPathFromUserSettings();
            if (!String.IsNullOrEmpty(dbPath) && !mustPrompt && System.IO.Directory.Exists(dbPath))
            {
                return dbPath;
            }
            else
            {
                System.Windows.Forms.SaveFileDialog dbLocationDialog = new System.Windows.Forms.SaveFileDialog();
                dbLocationDialog.RestoreDirectory = true;
                dbLocationDialog.InitialDirectory = dbPath;
                dbLocationDialog.Title = "Select a Folder where ANR will maintain its internal DataBase (anrl.mdf)";
                dbLocationDialog.FileName = "anrl.mdf";
                dbLocationDialog.OverwritePrompt = false;
                dbLocationDialog.ShowDialog();
                //dbPath = dbLocationDialog.FileName.Replace("anrl.mdf", "");
                dbPath = System.IO.Path.GetDirectoryName(dbLocationDialog.FileName);
                if (dbPath == null || dbPath == "")
                {
                    dbPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AirNavigationRace";
                }
                if (!System.IO.Directory.Exists(dbPath))
                {
                    System.IO.Directory.CreateDirectory(dbPath);
                }
                return dbPath;
            }
        }
    }
}
