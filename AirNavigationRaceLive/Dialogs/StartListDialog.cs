using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AirNavigationRaceLive.Dialogs
{
    public partial class StartListDialog : Form
    {
       // private DataAccess Client;
        public int selectedIdx;
        public Flight SelectedFlight;
        public Team SelectedTeam;
        public EventHandler OnFinish;
        //private int ParcourLength;
        //private int TakeOffStartgate;
        //private int TakeOffIntervall;
        //private int ParcourIntervall;

        public StartListDialog(
            List<Team> lstTeams, 
            Flight selectedFlt, 
            int maxStartId, 
            long dateQRDate, 
            long timeTKOF0, 
            long timeStart0, 
            long timeEnd0,
            int nrOfRoutes)
        {
            InitializeComponent();
            // BindingSource bs = new BindingSource();
            // bs.DataSource = lstTeams;
            // comboBoxTKOFLines.DataSource = bs;
            if (selectedFlt==null)
            {
                SelectedFlight = new Flight();
            }
            else
            {
                SelectedFlight = selectedFlt;
            }
            SelectedTeam = SelectedFlight.Team;

            List<ComboTeam> lstCboTeam = new List<ComboTeam>();
            foreach (Team t in lstTeams)
            {
                comboBoxCrew.Items.Add(new ComboTeam(t, getTeamDsc(t)));
            }

            for (int i = 1; i <= nrOfRoutes; i++)
            {
                NetworkObjects.Route r = (NetworkObjects.Route)i;
                comboBoxRoute.Items.Add(new ComboRoute(r));
            }

            if (SelectedFlight.Id == 0)
            {
                textBoxStartId.Text = maxStartId.ToString();
                comboBoxCrew.SelectedItem = null;
                date.Value = new DateTime(dateQRDate);
                timeTakeOff.Value = new DateTime(timeTKOF0);
                timeStart.Value = new DateTime(timeStart0);
                timeEnd.Value = new DateTime(timeEnd0);
                // select first item id we have only one route
                comboBoxRoute.SelectedIndex = (nrOfRoutes == 1) ? 0: -1;

            }
            else
            {
                ComboTeam comboTeam = null;
                foreach (Object o in comboBoxCrew.Items)
                {
                    if ((o as ComboTeam).p == SelectedTeam)
                    {
                        comboTeam = o as ComboTeam;
                        comboBoxCrew.SelectedItem = comboTeam;
                        break;
                    }
                }
                ComboRoute route = null;
                foreach (Object o in comboBoxRoute.Items)
                {
                    ComboRoute r = o as ComboRoute;
                    if ((int)r.p == SelectedFlight.Route)
                    {
                        route = r;
                        comboBoxRoute.SelectedItem = route;
                        break;
                    }
                }
                comboBoxRoute.SelectedItem = route;

                textBoxStartId.Text = SelectedFlight.StartID.ToString();
                DateTime takeOff = new DateTime(SelectedFlight.TimeTakeOff);
                date.Value = takeOff;
                timeTakeOff.Value = takeOff;
                timeStart.Value = new DateTime(SelectedFlight.TimeStartLine);
                timeEnd.Value = new DateTime(SelectedFlight.TimeEndLine);
                textBoxStartId.Tag = SelectedFlight;
                textBoxStartId.Text = SelectedFlight.StartID.ToString();
            }
            UpdateEnablement();
            errorProvider1.Clear();

        }

        private void Reset()
        {
            textBoxStartId.Text = "";
            textBoxStartId.Tag = null;
            comboBoxCrew.SelectedItem = null;
            comboBoxRoute.SelectedItem = null;
        }


        public void UpdateEnablement()
        {
            btnOK.Enabled = isValidStartList();
        }

        private bool isValidStartList()
        {
            errorProvider1.Clear();
            // duplicate check on crews
            bool ret = true;

            if (comboBoxCrew.SelectedItem == null)
            {
                errorProvider1.SetError(comboBoxCrew, "Crew cannot be empty");
                ret = false;
            }

            if (comboBoxRoute.SelectedItem == null)
            {
                // only Navigator selected
                errorProvider1.SetError(comboBoxRoute, "Route cannot be empty");
                ret = false;
            }
            int isInteger;
            if (!int.TryParse(textBoxStartId.Text, out isInteger))
            {
                errorProvider1.SetError(textBoxStartId, "Start ID must Be an integer");
                ret = false;
            }

            if (ret)
            {
                errorProvider1.Clear();
            }
            return ret;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ComboTeam comboTeam = comboBoxCrew.SelectedItem as ComboTeam;
            SelectedFlight.Team = comboTeam.p;
            ComboRoute route = comboBoxRoute.SelectedItem as ComboRoute;
            SelectedFlight.TimeTakeOff = mergeDateTime(timeTakeOff.Value, date.Value).Ticks;
            SelectedFlight.TimeStartLine = mergeDateTime(timeStart.Value, date.Value).Ticks;
            SelectedFlight.TimeEndLine = mergeDateTime(timeEnd.Value, date.Value).Ticks;
            SelectedFlight.StartID = int.Parse(textBoxStartId.Text);
            SelectedFlight.Route = (int)(route.p);
            Close();
        }

        private string getTeamDsc(Team team)
        {
            Subscriber pilot = team.Pilot;
            StringBuilder sb = new StringBuilder();
            sb.Append(pilot.LastName).Append(" ").Append(pilot.FirstName);
            if (team.Navigator != null)
            {
                Subscriber navi = team.Navigator;
                sb.Append(" - ").Append(navi.LastName).Append(" ").Append(navi.FirstName);
            }
            return sb.ToString();
        }
        private DateTime mergeDateTime(DateTime time, DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0, 0);
        }

        private void comboBoxCrew_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEnablement();
        }

        private void comboBoxRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEnablement();
        }

        private void textBoxStartId_TextChanged(object sender, EventArgs e)
        {
            UpdateEnablement();
        }
    }
    class ComboTeam
    {
        public Team p;
        private String toString;
        public ComboTeam(Team p, String toString)
        {
            this.p = p;
            this.toString = toString;
        }

        public override string ToString()
        {
            return toString;
        }
    }
    class ComboRoute
    {
        public NetworkObjects.Route p;
        public ComboRoute(NetworkObjects.Route p)
        {
            this.p = p;
        }

        public override string ToString()
        {
            return p.ToString();
        }
    }
}
