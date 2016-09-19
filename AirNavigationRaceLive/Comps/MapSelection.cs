using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET;
using GMap.NET.MapProviders;

namespace AirNavigationRaceLive.Comps
{
    public partial class MapSelection : UserControl
    {
        public MapSelection()
        {
            InitializeComponent();
        }

        private void gMapControl1_MouseMove(object sender, MouseEventArgs e)
        {
            gMapControl1.Focus();
            fldCursorX.Text =e.X.ToString();
            fldCursorY.Text = e.Y.ToString();
            PointLatLng point = gMapControl1.FromLocalToLatLng(gMapControl1.CurrentPositionGPixel.X, gMapControl1.CurrentPositionGPixel.Y);
            fldLatitude.Text = point.Lat.ToString();
            fldLongitude.Text = point.Lng.ToString();
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            gMapControl1.SetCurrentPositionByKeywords("Switzerland");
            gMapControl1.MapProvider = GMapProviders.OpenStreetMap;
            gMapControl1.MinZoom = 3;
            gMapControl1.MaxZoom = 17;
            gMapControl1.Zoom = 5;
            GMapOverlay overlayOne = new GMapOverlay(gMapControl1, "OverlayOne");
            gMapControl1.Overlays.Add(overlayOne);
            gMapControl1.Manager.Mode = AccessMode.ServerAndCache;
        }

        private void gMapControl1_MouseUp(object sender, MouseEventArgs e)
        {
            numLatA.Value = Convert.ToDecimal(gMapControl1.CurrentViewArea.LocationTopLeft.Lat);
            numLongA.Value = Convert.ToDecimal(gMapControl1.CurrentViewArea.LocationTopLeft.Lng);
        }
    }
}
