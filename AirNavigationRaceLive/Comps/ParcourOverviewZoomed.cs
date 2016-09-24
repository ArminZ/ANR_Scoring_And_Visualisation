using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using AirNavigationRaceLive.Comps.Helper;

namespace AirNavigationRaceLive.Comps
{
    public partial class ParcourOverviewZoomed : UserControl
    {
        private Client.DataAccess Client;
        Converter c = null;
        private Parcour activeParcour = new Parcour();

        private enum ActivePoint
        {
            A, B, O, NONE
        }

        public ParcourOverviewZoomed(Client.DataAccess iClient)
        {
            Client = iClient;
            InitializeComponent();
            lblCompetition.Text = Client.SelectedCompetition.Name + " - parcours";
            PictureBox1.Cursor = new Cursor(@"Resources\GPSCursor.cur");
        }
        #region load

        class ListItem
        {
            private Parcour parcour;
            public ListItem(Parcour iParcour)
            {
                parcour = iParcour;
            }

            public override String ToString()
            {
                return parcour.Name;
            }
            public Parcour getParcour()
            {
                return parcour;
            }
        }

        private void loadParcours()
        {
            deleteToolStripMenuItem.Enabled = false;
            PictureBox1.SetConverter(c);
            PictureBox1.Image = null;
            activeParcour = new Parcour();
            PictureBox1.SetParcour(activeParcour);
            PictureBox1.Invalidate();

            listBox1.Items.Clear();
            List<Parcour> parcours = Client.SelectedCompetition.Parcour.ToList();
            foreach (Parcour p in parcours)
            {
                listBox1.Items.Add(new ListItem(p));
            }
        }
        #endregion
        private void ParcourGen_VisibleChanged(object sender, EventArgs e)
        {
            loadParcours();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadParcours();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListItem li = listBox1.SelectedItem as ListItem;
            if (li != null)
            {
                Parcour p = li.getParcour();
                if (p.Id!=0)
                {
                    Client.DBContext.ParcourSet.Remove(p);
                }
                Client.DBContext.SaveChanges();
                loadParcours();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem li = listBox1.SelectedItem as ListItem;
            if (li != null)
            {
                deleteToolStripMenuItem.Enabled = true;
                Map map = li.getParcour().Map;

                MemoryStream ms = new MemoryStream(map.Picture.Data);
                PictureBox1.Image = System.Drawing.Image.FromStream(ms);
                c = new Converter(map);
                PictureBox1.SetConverter(c);

                PictureBox1.SetParcour(li.getParcour());
                activeParcour = li.getParcour();
                PictureBox1.Invalidate();
            }
        }

        private void PictureBox1_Resize(object sender, EventArgs e)
        {
            PictureBox1.Invalidate();
        }
    }
}
