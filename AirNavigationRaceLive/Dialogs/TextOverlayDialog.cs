using System;
using System.Windows.Forms;

namespace AirNavigationRaceLive.Dialogs
{
    public partial class TextOverlayDialog : Form
    {
        public string text = null;
        public TextOverlayDialog(string txt)
        {
            InitializeComponent();
            textBox.Text = txt;
        }

        private void button_Click(object sender, EventArgs e)
        {
            text = textBox.Text;
            Close();
        }
    }
}
