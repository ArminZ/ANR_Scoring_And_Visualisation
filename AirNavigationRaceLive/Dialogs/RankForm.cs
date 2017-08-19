using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AirNavigationRaceLive.Client;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Dialogs
{
    public partial class RankForm : Form
    {
        private List<PenaltySet> rankinEntries;
        private List<FlightSet> teams;
        private DataAccess c;

        public RankForm()
        {
            InitializeComponent();
        }
        public void SetData(List<PenaltySet> rankinEntries, List<FlightSet> teams, DataAccess c)
        {
            this.c = c;
            this.rankinEntries = rankinEntries;
            this.teams = teams;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (c != null && rankinEntries != null && teams != null)
            {
                List<RankedTeam> rankedTeams = new List<RankedTeam>();
                foreach (FlightSet t in teams)
                {
                    int sum = 0;
                    foreach (PenaltySet p in t.PenaltySet)
                    {
                        sum += p.Points;
                    }
                    rankedTeams.Add(new RankedTeam(t,t.TeamSet,sum));
                }
                rankedTeams.Sort();
                for (int i = 0; i < rankedTeams.Count; i++)
                {
                    try{
                    RankedTeam rt = rankedTeams[i];
                    switch (i)
                    {
                        case 0:
                            {
                                lblPunkte1.Text = rt.points.ToString();
                                lblName1.Text = getTeamDsc(rt.team);
                                break;
                            }
                        case 1:
                            {
                                lblPunkte2.Text = rt.points.ToString();
                                lblName2.Text = getTeamDsc(rt.team);
                                break;
                            }
                        case 2:
                            {
                                lblPunkte3.Text = rt.points.ToString();
                                lblName3.Text = getTeamDsc(rt.team);
                                break;
                            }
                        case 3:
                            {
                                lblPunkte4.Text = rt.points.ToString();
                                lblName4.Text = getTeamDsc(rt.team);
                                break;
                            }
                    }
                    }catch{}
                }
            }
            base.OnPaint(e);
        }

        private string getTeamDsc(TeamSet team)
        {
            SubscriberSet pilot = team.Pilot;
            StringBuilder sb = new StringBuilder();
            sb.Append(pilot.LastName).Append(" ").Append(pilot.FirstName);
            if (team.Navigator!= null)
            {
                SubscriberSet navi = team.Navigator;
                sb.Append(" - ").Append(navi.LastName).Append(" ").Append(navi.FirstName);
            }
            return sb.ToString();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                this.TransparencyKey = Color.Bisque;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                Refresh();
            }
            else
            {
                this.TransparencyKey = Color.White;
                this.FormBorderStyle = FormBorderStyle.None;
                Refresh();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked)
            {
                this.TopMost = true;
                Refresh();
            }
            else
            {
                this.TopMost = false;
                Refresh();
            }
        }
    }
    class RankedTeam:IComparable<RankedTeam>
    {
        public FlightSet t;
        public int points;
        public TeamSet team;
        public RankedTeam(FlightSet t,TeamSet team, int points)
        {
            this.t = t;
            this.team = team;
            this.points = points;
        }

        public int CompareTo(RankedTeam obj)
        {
            return this.points.CompareTo(obj.points);
        }
    }
}
