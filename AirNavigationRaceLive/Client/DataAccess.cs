using System;
using AirNavigationRaceLive.Model;
using System.Data.Entity;
using AirNavigationRaceLive;

namespace AirNavigationRaceLive.Client
{
    public class DataAccess
    {
        private DataAccess()
        {
            string dbPath = Comps.Helper.Utils.getDbPath(false);
            AppDomain.CurrentDomain.SetData("DataDirectory", dbPath);

            CustomDbInitializer<AnrlModel> dbi = new CustomDbInitializer<AnrlModel>();
            dbi.InitializeDatabase(DBContext);
        }
        private static DataAccess instance = new DataAccess();
        private AnrlModel dbcontext = new AnrlModel();
        private CompetitionSet selectedCompetition = null;

        public static DataAccess Instance { get { return instance; } }
        public AnrlModel DBContext { get { return dbcontext; } }
        public CompetitionSet SelectedCompetition { get { return selectedCompetition; } set { selectedCompetition = value; } }
    }
}
