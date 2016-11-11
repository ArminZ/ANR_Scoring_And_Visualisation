using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace AirNavigationRaceLive.Comps
{
    public partial class MapControl : UserControl
    {
        private Client.DataAccess Client;
        private ToolTip Tooltip;
        private Map m;

        public MapControl(Client.DataAccess iClient)
        {
            Client = iClient;
            InitializeComponent();
            lblCompetition.Text = Client.SelectedCompetition.Name + " - maps";
            Tooltip = new ToolTip();
            Tooltip.AutomaticDelay = 0;
            Tooltip.AutoPopDelay = 0;
            Tooltip.InitialDelay = 0;
            Tooltip.ReshowDelay = 0;
            Tooltip.ShowAlways = true;
            Tooltip.UseAnimation = true;
            Tooltip.UseFading = true;
            Tooltip.IsBalloon = true;
            Tooltip.SetToolTip(fldSizeX, "pixel size in the x-direction in map units/pixel; unit is degree as the position! Example: 1.669E-4");
            Tooltip.SetToolTip(fldSizeY, "pixel size in the y-direction in map units/pixel; unit is degree as the position! Example: -9.278E-5");
            Tooltip.SetToolTip(fldRotationX, "rotation about x-axis; unit is degree as the position! Example: 0");
            Tooltip.SetToolTip(fldRotationY, "rotation about y-axis; unit is degree as the position! Example: 0");
            Tooltip.SetToolTip(fldX, "x-coordinate of the center of the upper left pixel; unit is degree! Example: 8.491");
            Tooltip.SetToolTip(fldY, "y-coordinate of the center of the upper left pixel; unit is degree! Example: 50.058");
        }
        #region load
        private void loadMaps()
        {
            listBox1.Items.Clear();
            List<Map> maps = Client.SelectedCompetition.Map.ToList();
            foreach (Map m in maps)
            {
                listBox1.Items.Add(new ListItem(m));
            }
        }

        class ListItem
        {
            private Map map;
            public ListItem(Map imap)
            {
                map = imap;
            }

            public override String ToString()
            {
                return map.Name;
            }
            public Map getMap()
            {
                return map;
            }
        }


        private void Map_VisibleChanged(object sender, EventArgs e)
        {
            loadMaps();
        }
        private void Map_Load(object sender, EventArgs e)
        {
            loadMaps();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadMaps();
        }
        #endregion

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            ListItem li = listBox1.SelectedItem as ListItem;
            if (li != null)
            {
                m = li.getMap();
                fldName.Text = m.Name;
                fldSizeX.Text = m.XSize.ToString();
                fldSizeY.Text = m.YSize.ToString();
                fldRotationX.Text = m.XRot.ToString();
                fldRotationY.Text = m.YRot.ToString();
                fldX.Text = m.XTopLeft.ToString();
                fldY.Text = m.YTopLeft.ToString();

                MemoryStream ms = new MemoryStream(m.Picture.Data);
                PictureBox1.Image = System.Drawing.Image.FromStream(ms);
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
                btnSelectMap.Enabled = true;
                btnSelectWorldFile.Enabled = true;
                fldName.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
                btnSelectMap.Enabled = false;
                btnSelectWorldFile.Enabled = false;
                btnSave.Enabled = false;
                fldName.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ListItem li = listBox1.SelectedItem as ListItem;
            if (li != null)
            {
                Client.DBContext.MapSet.Remove(li.getMap());
                Client.DBContext.SaveChanges();
                loadMaps();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            m = new Map();
            fldName.Text = "";
            fldSizeX.Text = "";
            fldSizeY.Text = "";
            fldRotationX.Text = "";
            fldRotationY.Text = "";
            fldX.Text = "";
            fldY.Text = "";
            PictureBox1.Image = null;
            btnSelectMap.Enabled = true;
            btnSelectWorldFile.Enabled = true;
            fldName.Enabled = true;
        }

        private void btnSelectMap_Click(object sender, EventArgs e)
        {
            string FileFilter = "JPG Files (*.jpg, *.jpeg, *.jpe, *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|"
                                + "Bitmap Files (*.bmp)|*.bmp|"
                                + "Tiff Files (*.tif, *.tiff)|*.tif;*.tiff|"
                                + "Gif Files (*.gif)|*.gif|"
                                + "PNG Files (*.png)|*.png|"
                                + "All Picture Files|*.jpg;*.jpeg;*.jpe;*.jfif;*.bmp;*.gif;*.png;*.tif;*.tiff|"
                                + "All Files (*.*)|*.*";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Import Map picture";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.Filter = FileFilter;
            ofd.FilterIndex = 6;
            ofd.FileOk += new CancelEventHandler(ofd_FileOk);
            ofd.ShowDialog();
        }


        void ofd_FileOk(object sender, CancelEventArgs e)
        {
            OpenFileDialog ofd = sender as OpenFileDialog;
            PictureBox1.Image = Image.FromFile(ofd.FileName);
            fldName.Text = Path.GetFileNameWithoutExtension(ofd.FileName);
            btnSave.Enabled = true;

            if (!String.IsNullOrEmpty(getWorldFileByMapName(ofd.FileName)))
            {   // we found a single world file with the same name
                // set ofd.Filename, then load it directly
                ofd.FileName = getWorldFileByMapName(ofd.FileName);
                ofd_FileOkWorld(ofd, null);
            }
        }

        private string getWorldFileByMapName(string mapFileFullName)
        {
            string fnam = System.IO.Path.GetFileNameWithoutExtension(mapFileFullName);
            string path = System.IO.Path.GetDirectoryName(mapFileFullName);

            // all possbile extensions for world file
            string[] filters = new[] { ".jgw", ".pgw", ".gfw", ".tfw", ".wld" };
            for (int i = 0; i < filters.Length; i++)
            {
                filters[i] = fnam + filters[i];  // combine known file name + possible extensions
            }
            // fetch files
            string[] filePaths = filters.SelectMany(f => System.IO.Directory.GetFiles(path, f)).ToArray();

            if (filePaths.Length == 1)
            {
                // only one match, so return it
                return filePaths[0];
            }
            else
            {
                return string.Empty;
            }
        }

        private void btnSelectWorldFile_Click(object sender, EventArgs e)
        {
            string FileFilter = "World files (*.jgw, *.pgw, *.gfw, *.tfw, *.wld)|*.jgw;*.pgw;*.gfw;*.tfw;*.wld|All Files (*.*)|*.*";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "t_Picture";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.Filter = FileFilter;
            ofd.FilterIndex = 1;
            ofd.FileOk += new CancelEventHandler(ofd_FileOkWorld);
            ofd.ShowDialog();
        }

        void ofd_FileOkWorld(object sender, CancelEventArgs e)
        {
            OpenFileDialog ofd = sender as OpenFileDialog;
            // NOTE: there are numerous ways how a world file can be generated, and the number format / decimal separator may be different
            // So the world file may have a different decimal separator than the system where this application runs.
            // For a world file with the correct map projection (WGS84) we can however safely assume
            // the numbers in the world file are smaller than +/- 180 so there are no thousand separators
            // the normal Decimal separator will be either dot or comma.
            var c = System.Threading.Thread.CurrentThread.CurrentCulture;
            var s = c.NumberFormat.NumberDecimalSeparator;
            string[] world = File.ReadAllLines(ofd.FileName);
            for (int i = 0; i < world.Length; i++)
            {
                // replace dot and comma with the actual system's decimal separator
                world[i] = world[i].Replace(",", s).Replace(".", s);
            }

            // errorProvider will validate, no need to convert

            fldSizeX.Text = world[0];
            fldSizeY.Text = world[3];
            fldRotationX.Text = world[2];
            fldRotationY.Text = world[1];
            fldX.Text = world[4];
            fldY.Text = world[5];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnDelete.Enabled = false;
                btnSelectMap.Enabled = false;
                btnSelectWorldFile.Enabled = false;
                btnSave.Enabled = false;
                m.Name = fldName.Text;
                m.XSize = Double.Parse(fldSizeX.Text);
                m.YSize = Double.Parse(fldSizeY.Text);
                m.XRot = Double.Parse(fldRotationX.Text);
                m.YRot = Double.Parse(fldRotationY.Text);
                m.XTopLeft = Double.Parse(fldX.Text);
                m.YTopLeft = Double.Parse(fldY.Text);

                MemoryStream ms = new MemoryStream();
                PictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                m.Picture = new Picture();
                m.Picture.Data = ms.ToArray();
                m.Competition = Client.SelectedCompetition;
                if (m.Id == 0)
                {
                    Client.DBContext.MapSet.Add(m);
                }
                Client.DBContext.SaveChanges();
                loadMaps();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while Saving");
            }
        }

        /// <summary>
        /// Performs a validation check on the data fields read from the World file, and on the Map name field.
        /// </summary>
        private void checkMapHasValidationErrors()
        {
            bool hasErrors = false;
            errorProviderWorldFile.Clear();
            var controls = new[] { fldX, fldY, fldRotationX, fldRotationY, fldSizeX, fldSizeY };
            foreach (var control in controls)
            {
                double dbl = 0.0;
                if (!double.TryParse(control.Text, out dbl))
                {
                    errorProviderWorldFile.SetError(control, "Value must be numeric");
                    hasErrors = true;
                }
            }
            if (string.IsNullOrWhiteSpace(fldName.Text))
            {
                errorProviderWorldFile.SetError(fldName, "Fill a name");
                hasErrors = true;
            }
            btnSave.Enabled = !hasErrors;
        }

        private void fldSizeX_TextChanged(object sender, EventArgs e)
        {
            checkMapHasValidationErrors();
        }

        private void fldRotationY_TextChanged(object sender, EventArgs e)
        {
            checkMapHasValidationErrors();
        }

        private void fldRotationX_TextChanged(object sender, EventArgs e)
        {
            checkMapHasValidationErrors();
        }

        private void fldSizeY_TextChanged(object sender, EventArgs e)
        {
            checkMapHasValidationErrors();
        }

        private void fldX_TextChanged(object sender, EventArgs e)
        {
            checkMapHasValidationErrors();
        }

        private void fldY_TextChanged(object sender, EventArgs e)
        {
            checkMapHasValidationErrors();
        }

        private void fldName_TextChanged(object sender, EventArgs e)
        {
            checkMapHasValidationErrors();
        }
    }
}
