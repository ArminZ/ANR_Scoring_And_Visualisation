using System;
using System.Windows.Forms;
using System.Reflection;
using System.Deployment.Application;

namespace AirNavigationRaceLive.Comps
{
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
        }

        private Version GetRunningVersion()
        {
            try
            {
                //return ProductVersion;
                return ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        private void About_Load(object sender, EventArgs e)
        {

        }
    }
}
