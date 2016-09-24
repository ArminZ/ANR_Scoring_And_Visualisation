using System;
using System.Windows.Forms;
using AirNavigationRaceLive.Comps.Helper;

namespace AirNavigationRaceLive.Dialogs
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
            btnToCh.Enabled = false;
            btnToWGS.Enabled = false;
        }

        private void btnToWGS_Click(object sender, EventArgs e)
        {
            if (!hasValidationErrors(sender))
            {
                try
                {
                    double east = double.Parse(textEast.Text);
                    double north = double.Parse(textNorth.Text);
                    textLatitude.Text = Converter.CHtoWGSlat(east, north).ToString();
                    textLongitude.Text = Converter.CHtoWGSlng(east, north).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnToCh_Click(object sender, EventArgs e)
        {
            if (!hasValidationErrors(sender))
            {
                try
                {
                    double latitude = double.Parse(textLatitude.Text);
                    double longitude = double.Parse(textLongitude.Text);
                    textEast.Text = Converter.WGStoChEastY(longitude, latitude).ToString();
                    textNorth.Text = Converter.WGStoChNorthX(longitude, latitude).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private bool hasValidationErrors( object sender)
        {
            bool hasErrors = false;
            errorProvider1.Clear();
            double dbl = 0.0;
            if (sender == textEast || sender == textNorth)
            {
                var controls = new[] { textEast, textNorth };
                foreach (var control in controls)
                {
                    if (!double.TryParse(control.Text, out dbl))
                    {
                        errorProvider1.SetError(control, "Value must be numeric");
                        hasErrors = true;
                    }
                }
            }
            if (sender == textLatitude || sender == textLongitude)
            {
                var controls = new[] { textLatitude, textLongitude };
                foreach (var control in controls)
                {
                    if (!double.TryParse(control.Text, out dbl) || double.TryParse(control.Text, out dbl) && Math.Abs(dbl) > 180)
                    {
                        errorProvider1.SetError(control, "Value must be numeric and between -180 and +180");
                        hasErrors = true;
                    }
                }
            }
            return hasErrors;
        }

        private void textEast_TextChanged(object sender, EventArgs e)
        {
            btnToWGS.Enabled = !hasValidationErrors(sender);
        }

        private void textNorth_TextChanged(object sender, EventArgs e)
        {
            btnToWGS.Enabled = !hasValidationErrors(sender);
        }

        private void textLatitude_TextChanged(object sender, EventArgs e)
        {
            btnToCh.Enabled = !hasValidationErrors(sender);
        }

        private void textLongitude_TextChanged(object sender, EventArgs e)
        {
            btnToCh.Enabled = !hasValidationErrors(sender);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textEast.Clear();
            textNorth.Clear();
            textLatitude.Clear();
            textLongitude.Clear();
            errorProvider1.Clear();
        }
    }
}
