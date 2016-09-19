using System.Windows.Forms;

namespace AirNavigationRaceLive.Comps
{
    public partial class History : UserControl
    {
        public History()
        {
            InitializeComponent();
            PictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            linkLabel1.Links.Add(0,33, linkLabel1.Text.Trim());
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

    }
}
