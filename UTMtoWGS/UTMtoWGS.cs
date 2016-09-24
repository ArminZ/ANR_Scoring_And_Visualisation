using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace UTMtoWGS
{
    public partial class UTMtoWGS : Form
    {
        public UTMtoWGS()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.dxf|*.dxf";
            ofd.FileOk += new CancelEventHandler(ofd_FileOk);
            ofd.ShowDialog();
        }

        void ofd_FileOk(object sender, CancelEventArgs e)
        {
            int Zone = int.Parse(textBox1.Text);
            bool southemi = checkBox1.Checked;
            OpenFileDialog ofd = sender as OpenFileDialog;
            UTM.DXFConverter.importFromDxf(Zone,southemi, ofd.FileName);
        }
    }
}
