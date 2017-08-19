using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Comps
{
    public partial class MapLegacy : UserControl
    {
        private Client.DataAccess Client;

        public MapLegacy(Client.DataAccess iClient)
        {
            Client = iClient;
            InitializeComponent();   
            groupBoxLegacy.Text = string.Format("{0} - Legacy MapSet import", Client.SelectedCompetition.Name);

        }




        private void btnImportANR_Click(object sender, EventArgs e)
        {
            btnImportANR.Enabled = false;
            OpenFileDialog ofd = new OpenFileDialog();
            string FileFilter = "JPG Files (*.jpg, *.jpeg, *.jpe, *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|"
                    + "Bitmap Files (*.bmp)|*.bmp|"
                    + "Gif Files (*.gif)|*.gif|"
                    + "Png Files (*.png)|*.png";
            string GraphicFileFilter = "All Picture Files|*.jpg;*.jpeg;*.jpe;*.jfif;*.bmp;*.gif;*.png";
            ofd.Title = "Legacy MapSet Import";
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;
            ofd.Filter = FileFilter + "|" + GraphicFileFilter;
            ofd.FilterIndex = 5;
            ofd.FileOk += new CancelEventHandler(ofd_FileOk);
            ofd.ShowDialog();
        }

        void ofd_FileOk(object sender, CancelEventArgs e)
        {
            OpenFileDialog ofd = sender as OpenFileDialog;
            if(rdoBtnWGS84.Checked)
            {
                legacyImportWGS84(ofd.FileName);
            }
            else
            {
                legacyImportCH1903(ofd.FileName);
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    button1.Enabled = false;
        //    OpenFileDialog ofd = new OpenFileDialog();
        //    string FileFilter = "JPG Dateien (*.jpg, *.jpeg, *.jpe, *.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|"
        //            + "Bitmap Dateien (*.bmp)|*.bmp|"
        //            + "Gif Dateien (*.gif)|*.gif|"
        //            + "Png Dateien (*.png)|*.png";
        //    string GraphicFileFilter = "Alle Bilddateien|*.jpg;*.jpeg;*.jpe;*.jfif;*.bmp;*.gif;*.png";
        //    ofd.Title = "t_Picture";
        //    ofd.RestoreDirectory = true;
        //    ofd.Multiselect = false;
        //    ofd.Filter = FileFilter + "|" + GraphicFileFilter;
        //    ofd.FilterIndex = 5;
        //    ofd.FileOk += new CancelEventHandler(ofd_FileOk2);    
        //    ofd.ShowDialog();
        //}

        //void ofd_FileOk2(object sender, CancelEventArgs e)
        //{
        //    OpenFileDialog ofd = sender as OpenFileDialog;
        //    PictureBox p = new PictureBox();
        //    p.Image = Image.FromFile(ofd.FileName);
        //    MapSet m = new Map();
        //    m.Name = fldName.Text;
        //    double topLeftLatitude;
        //    double topLeftLongitude;
        //    double bottomRightLatitude;
        //    double bottomRightLongitude;
        //    string[] coordinatesFromPath = ofd.FileName.Remove(ofd.FileName.LastIndexOf(".")).Substring(ofd.FileName.LastIndexOf(@"\") + 1).Split("_".ToCharArray());
        //    topLeftLatitude = Convert.ToDouble(coordinatesFromPath[0]); 
        //    topLeftLongitude = Convert.ToDouble(coordinatesFromPath[1]);
        //    bottomRightLatitude = Convert.ToDouble(coordinatesFromPath[2]);
        //    bottomRightLongitude = Convert.ToDouble(coordinatesFromPath[3]);

        //    m.XSize = (bottomRightLongitude -topLeftLongitude ) / p.Image.Width;
        //    m.YSize = (bottomRightLatitude - topLeftLatitude) / p.Image.Height;
        //    m.XRot = 0;
        //    m.YRot = 0;
        //    m.XTopLeft = topLeftLongitude;
        //    m.YTopLeft = topLeftLatitude;
        //    MemoryStream ms = new MemoryStream();
        //    p.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //    m.Picture = new Picture();
        //    m.Picture.Data = ms.ToArray();
        //    m.Competition = Client.SelectedCompetition;
        //    Client.DBContext.MapSet.Add(m);
        //    Client.DBContext.SaveChanges();
        //    btnImportANR.Enabled = true;
        //}

        void legacyImportWGS84(string fname)
        {
            PictureBox p = new PictureBox();
            p.Image = Image.FromFile(fname);
            MapSet m = new MapSet();
            m.Name = fldName.Text;
            double topLeftLatitude;
            double topLeftLongitude;
            double bottomRightLatitude;
            double bottomRightLongitude;
            string fname1 = setDecimalSeparator(Path.GetFileNameWithoutExtension(fname));
            string[] coordinatesFromPath = fname1.Split("_".ToCharArray());

            foreach (string coordinate in coordinatesFromPath)
            {
                double ret;
                if (string.IsNullOrEmpty(coordinate) || !double.TryParse(coordinate,out ret) || Math.Abs(ret)>180)
                {
                    throw (new FormatException("Coordinates in image name not in correct format!"));
                }
            }
            topLeftLatitude = Convert.ToDouble(coordinatesFromPath[0]);
            topLeftLongitude = Convert.ToDouble(coordinatesFromPath[1]);
            bottomRightLatitude = Convert.ToDouble(coordinatesFromPath[2]);
            bottomRightLongitude = Convert.ToDouble(coordinatesFromPath[3]);

            m.XSize = (bottomRightLongitude - topLeftLongitude) / p.Image.Width;
            m.YSize = (bottomRightLatitude - topLeftLatitude) / p.Image.Height;
            m.XRot = 0;
            m.YRot = 0;
            m.XTopLeft = topLeftLongitude;
            m.YTopLeft = topLeftLatitude;
            MemoryStream ms = new MemoryStream();
            p.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            m.PictureSet = new PictureSet();
            m.PictureSet.Data = ms.ToArray();
            m.CompetitionSet = Client.SelectedCompetition;
            Client.DBContext.MapSet.Add(m);
            Client.DBContext.SaveChanges();
            btnImportANR.Enabled = true;
        }

        void legacyImportCH1903(string fname)
        {
            PictureBox p = new PictureBox();
            p.Image = Image.FromFile(fname);
            MapSet m = new MapSet();
            m.Name = fldName.Text;
            string[] coordinatesFromPath = Path.GetFileNameWithoutExtension(fname).Split("_".ToCharArray());
            foreach (string coordinate in coordinatesFromPath)
            {
                if (coordinate.Length != 6 || string.IsNullOrEmpty(coordinate) || !coordinate.All(char.IsDigit))
                {
                    throw (new FormatException("Coordinates in image name not in correct format!"));
                }
            }

            double topLeftLatitude = Converter.CHtoWGSlat(Convert.ToDouble(coordinatesFromPath[0]), Convert.ToDouble(coordinatesFromPath[1]));
            double topLeftLongitude = Converter.CHtoWGSlng(Convert.ToDouble(coordinatesFromPath[0]), Convert.ToDouble(coordinatesFromPath[1]));
            double bottomRightLatitude = Converter.CHtoWGSlat(Convert.ToDouble(coordinatesFromPath[2]), Convert.ToDouble(coordinatesFromPath[3]));
            double bottomRightLongitude = Converter.CHtoWGSlng(Convert.ToDouble(coordinatesFromPath[2]), Convert.ToDouble(coordinatesFromPath[3]));

            m.XSize = (bottomRightLongitude - topLeftLongitude) / p.Image.Width;
            m.YSize = (bottomRightLatitude - topLeftLatitude) / p.Image.Height;
            m.XRot = 0;
            m.YRot = 0;
            m.XTopLeft = topLeftLongitude;
            m.YTopLeft = topLeftLatitude;
            MemoryStream ms = new MemoryStream();
            p.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            m.PictureSet = new PictureSet();
            m.PictureSet.Data = ms.ToArray();
            m.CompetitionSet = Client.SelectedCompetition;
            Client.DBContext.MapSet.Add(m);
            Client.DBContext.SaveChanges();
            btnImportANR.Enabled = true;
        }

        private string setDecimalSeparator(string inp)
        {
            string ret = inp;
            var c = System.Threading.Thread.CurrentThread.CurrentCulture;
            var s = c.NumberFormat.NumberDecimalSeparator;
            // replace dot and comma with the actual system's decimal separator
            ret = inp.Replace(",", s).Replace(".", s);
            return ret;
        }

        private void fldName_TextChanged(object sender, EventArgs e)
        {
            btnImportANR.Enabled = !string.IsNullOrWhiteSpace(fldName.Text);
            if (!btnImportANR.Enabled)
            {
                errorProvider1.SetError(fldName, "Map name cannot be empty");
            }
            else
            {
                errorProvider1.Clear();
            }

        }
    }

   
}
