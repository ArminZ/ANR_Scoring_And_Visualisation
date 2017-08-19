using AirNavigationRaceLive.Comps;
using AirNavigationRaceLive.Model;
using AirNavigationRaceLive.ModelExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AirNavigationRaceLive.Dialogs
{
    public partial class TeamDialog : Form
    {
        // private DataAccess Client;
        public EventHandler OnFinish;
        private List<SubscriberSet> ListParticipants { get; set; }
        private List<string> ListTeamIdPilNavNames { get; set; }
        public List<TeamSet> ListCrews { get; set; }
        public TeamSet SelectedTeam { get; set; }
        private int TeamId { get; set; }
        private int PilotId { get; set; }
        private SubscriberSet Pilot { get; set; }
        private SubscriberSet Navigator { get; set; }
        //public string Color { get; set; }
        //public string AC { get; set; }
        private string PilotName { get; set; }
        public int NavigatorId { get; set; }
        private string NavigatorName { get; set; }
        private string CrewNumber { get; set; }
        private string Nationality { get; set; }
        public TeamDialog(TeamSet team, List<SubscriberSet> lstParticipants, List<TeamSet> lstCrews, List<string> lstCrewIdPilNavNames)
        {
            InitializeComponent();
            List<SubscriberExtension> lstSubscExt = getListOfSubsc(lstParticipants).OrderBy(l => l.LastNameFirstName).ToList();
            ListTeamIdPilNavNames = lstCrewIdPilNavNames;
            ListCrews = lstCrews;
            ListParticipants = lstParticipants;
            TeamId = 0;
            SelectedTeam = team;
            //Reload();
            if (SelectedTeam == null)
            {
                SelectedTeam = new TeamSet();
                textBoxCrewNumber.Text = calculateCrewNumber();
            }
            else
            {
                TeamId = SelectedTeam.Id;
                Pilot = SelectedTeam.Pilot;
                Navigator = SelectedTeam.Navigator;

                textBoxNationality.Text = SelectedTeam.Nationality;
                textBoxCrewNumber.Text = string.IsNullOrEmpty(SelectedTeam.CNumber) ? calculateCrewNumber() : SelectedTeam.CNumber;

                textBoxAC.Text = SelectedTeam.AC;
                btnColorSelect.BackColor = Color.Gray;
                btnColorSelect.Text = SelectedTeam.Color != null ? SelectedTeam.Color.ToString() : Color.Gray.ToString();
                btnColorSelect.BackColor = SelectedTeam.Color != null ? TeamControl.getColor(SelectedTeam.Color) : Color.Gray;
            }

            if (SelectedTeam.Pilot != null)
            {
                comboBoxPilot.SelectedValue = SelectedTeam.Pilot.Id;
            }

            if (SelectedTeam.Navigator != null)
            {
                comboBoxNavigator.SelectedValue = SelectedTeam.Navigator.Id;
            }
            errorProvider1.Clear();
            UpdateEnablement();
        }

        private List<SubscriberExtension> getListOfSubsc(List<SubscriberSet> lstBase)
        {
            List<SubscriberExtension> lstSubscExt = new List<SubscriberExtension>();
            SubscriberExtension strEx = new SubscriberExtension();
            strEx.LastNameFirstName = "";
            lstSubscExt.Add(strEx);
            foreach (var item in lstBase)
            {
                strEx = new SubscriberExtension();
                strEx.Id = item.Id;
                strEx.LastNameFirstName = item.LastName + " " + item.FirstName;
                lstSubscExt.Add(strEx);
            }
            lstSubscExt = lstSubscExt.OrderBy(p => p.LastNameFirstName).ToList<SubscriberExtension>();
            BindComboDataSource(comboBoxPilot, lstSubscExt);
            BindComboDataSource(comboBoxNavigator, lstSubscExt);
            return lstSubscExt;
        }

        private void BindComboDataSource(ComboBox cbo, List<SubscriberExtension> lst)
        {
            cbo.BindingContext = new BindingContext();
            cbo.DataSource = lst;
            cbo.DisplayMember = "LastNameFirstName";
            cbo.ValueMember = "Id";
        }

        //private void Reload()
        //{
        //    if (SelectedTeam != null)
        //    {
        //        comboBoxPilot.SelectedValue = SelectedTeam.Pilot.Id;
        //        comboBoxNavigator.SelectedValue = SelectedTeam.Navigator.Id;
        //        textBoxNationality.Text = SelectedTeam.Nationality;
        //        textBoxCrewNumber.Text = SelectedTeam.CNumber == "" ? calculateCrewNumber() : SelectedTeam.CNumber;
        //        textBoxAC.Text = SelectedTeam.AC;
        //        btnColorSelect.Name = SelectedTeam.Color;
        //        btnColorSelect.BackColor = TeamControl.getColor(SelectedTeam.Color);
        //    }
        //    errorProvider1.Clear();

        //}

        public void UpdateEnablement()
        {
            btnOK.Enabled = isValidCrew();
        }


        private bool isValidCrew()
        {
            // duplicate check on crews
            bool ret = true;

            if (string.IsNullOrEmpty(textBoxCrewNumber.Text.Trim()))
            {
                errorProvider1.SetError(textBoxCrewNumber, "Crew number cannot be empty");
                ret = false;
            }


            if (PilotId <= 0)
            {
                // only Navigator selected
                errorProvider1.SetError(comboBoxPilot, "Pilot cannot be empty");
                ret = false;
            }

            if (PilotId == NavigatorId && PilotId > 0)
            {
                //same person as pilot and as navigator
                errorProvider1.SetError(comboBoxPilot, "Pilot and Navigator cannot be the same person");
                ret = false;
            }

            if (PilotId > 0)
            {
                // compare to listdata, must have different TeamId
                if (ListTeamIdPilNavNames.Any(o => o.EndsWith(getPilNavNames(Pilot, Navigator)) && !(o.StartsWith(TeamId.ToString()))))
                {
                    // we have a duplicate team
                    errorProvider1.SetError(comboBoxPilot, "A duplicate team with the same names already exists");
                    ret = false;
                }
            }

            if (ret)
            {
                errorProvider1.Clear();
            }
            return ret;
        }
        private string getPilNavNames(SubscriberSet pilot, SubscriberSet navigator)
        {
            string pilName = pilot != null ? pilot.LastName + " " + pilot.FirstName : " - ";
            string navName = navigator != null ? "|" + navigator.LastName + " " + navigator.FirstName : " - ";
            return pilName + navName;
        }

        private void TeamDialog_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxPilot_SelectedIndexChanged(object sender, EventArgs e)
        {
            PilotId = -1;
            if (Pilot == null)
            {
                Pilot = new SubscriberSet();
            }
            if (comboBoxPilot.SelectedIndex > 0)
            {
                int _id = -1;
                PilotName = comboBoxPilot.Text;
                if (int.TryParse(comboBoxPilot.SelectedValue.ToString(), out _id))
                {
                    PilotId = _id;
                    Pilot = ListParticipants.Find(r => r.Id == _id);
                }
            }
            UpdateEnablement();
        }

        private void comboBoxNavigator_SelectedIndexChanged(object sender, EventArgs e)
        {
            NavigatorId = -1;
            if (comboBoxNavigator.SelectedIndex >= 0)
            {
                if (Navigator == null)
                {
                    Navigator = new SubscriberSet();
                }
                int _id = -1;
                NavigatorName = comboBoxNavigator.Text;
                if (int.TryParse(comboBoxNavigator.SelectedValue.ToString(), out _id))
                {
                    NavigatorId = _id;
                    Navigator = ListParticipants.FirstOrDefault(r => r.Id == _id);
                }
            }
            UpdateEnablement();
        }

        private void textBoxNationality_TextChanged(object sender, EventArgs e)
        {
            Nationality = textBoxNationality.Text.Trim();
        }

        private void textBoxCrewNumber_TextChanged(object sender, EventArgs e)
        {
            SelectedTeam.CNumber = string.IsNullOrEmpty(textBoxCrewNumber.Text.Trim()) ? calculateCrewNumber() : textBoxCrewNumber.Text.Trim();
            if (textBoxCrewNumber.InvokeRequired)
            UpdateEnablement();
        }

        private void textBoxAC_TextChanged(object sender, EventArgs e)
        {
            SelectedTeam.AC = textBoxAC.Text.Trim();
        }
        private void btnColorSelect_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.AnyColor = false;
            cd.SolidColorOnly = true;
            cd.ShowDialog();
            btnColorSelect.BackColor = cd.Color;
            btnColorSelect.Text = btnColorSelect.BackColor.Name;
            SelectedTeam.Color = btnColorSelect.BackColor.Name;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedTeam.Pilot = Pilot;
            SelectedTeam.Navigator = Navigator;
            SelectedTeam.Nationality = textBoxNationality.Text.Trim();
            SelectedTeam.CNumber = textBoxCrewNumber.Text.Trim();
            SelectedTeam.AC = textBoxAC.Text;
            SelectedTeam.Color = btnColorSelect.BackColor.Name;

            Close();
        }

        private string calculateCrewNumber()
        {
            int id = 0;
            foreach (var item in ListCrews)
            {
                int crewNr = -1;
                if (int.TryParse(item.CNumber, out crewNr))
                {
                    id = Math.Max(id, crewNr);
                }
            }
            id++;
            return id.ToString().PadLeft(2, '0');
        }
    }
}
