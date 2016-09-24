using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace AirNavigationRaceLive.Dialogs
{
    [ComVisibleAttribute(true)]
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public partial class VisualisationPopup : Form
    {
        private dynamic plugin = null;

        public VisualisationPopup()
        {
            InitializeComponent();
            webBrowser1.ObjectForScripting = this;
            webBrowser1.DocumentText = File.ReadAllText(@"Resources\pluginhost.html");
        }

        public void JSInitSuccessCallback_(object pluginInstance)
        {
            plugin = (dynamic)pluginInstance;
        }

        public void JSInitFailureCallback_(string error)
        {
            MessageBox.Show("Error: " + error, "Plugin Load Error", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }

        public dynamic getPlugin()
        {
            return plugin;
        }
    }
}
