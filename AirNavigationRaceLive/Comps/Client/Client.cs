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
            string dbPath = Comps.Helper.Utils.getDbPath(false);
            AppDomain.CurrentDomain.SetData("DataDirectory", dbPath);
            DB.Database.CreateIfNotExists();
        }
        private static DataAccess instance = new DataAccess();
        private AnrlModel2Container DB = new AnrlModel2Container();
        private Competition SelectedComp = null;

        public static DataAccess Instance { get { return instance; } }
        public AnrlModel2Container DBContext { get { return DB; } }
        public Competition SelectedCompetition {get { return SelectedComp; } set { SelectedComp = value; } }
    }
}
