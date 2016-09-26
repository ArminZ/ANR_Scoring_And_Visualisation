using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkObjects;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using AirNavigationRaceLive.Comps.Helper;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using AirNavigationRaceLive.Properties;
using System.Configuration;

namespace AirNavigationRaceLive.Comps.Client
{
    public class DataAccess
    {
        private DataAccess(){
            string dbPath = getDbPath();
            AppDomain.CurrentDomain.SetData("DataDirectory", dbPath);
            DB.Database.CreateIfNotExists();
        }
        private static DataAccess instance = new DataAccess();
        private AnrlModel2Container DB = new AnrlModel2Container();
        private Competition SelectedComp = null;

        public static DataAccess Instance { get { return instance; } }
        public AnrlModel2Container DBContext { get { return DB; } }
        public Competition SelectedCompetition {get { return SelectedComp; } set { SelectedComp = value; } }

        private string readDBPathFromUserSettings()
        {
            if (!Settings.Default.promptForDB && !string.IsNullOrEmpty(Settings.Default.directoryForDB))
            {
                return Settings.Default.directoryForDB;
            }
            return string.Empty;
        }
        public string getDbPath()
        {
            string dbPath = string.Empty;
            if (!String.IsNullOrEmpty(readDBPathFromUserSettings()))
            {
                dbPath = readDBPathFromUserSettings();
            }
            else
            {
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
                if (!Directory.Exists(dbPath))
                {
                    Directory.CreateDirectory(dbPath);
                }
            }
            return dbPath;
        }
    }
}
