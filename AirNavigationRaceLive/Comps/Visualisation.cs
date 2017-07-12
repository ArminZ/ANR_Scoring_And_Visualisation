using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AirNavigationRaceLive.Dialogs;
using AirNavigationRaceLive.Comps.Helper;
using System.IO;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Comps
{
    public partial class Visualisation : UserControl
    {
        private Client.DataAccess Client;
        private VisualisationPopup vp;
        private GEControl geControl = new GEControl();
        private Timer t;
        private QualificationRoundSet qRnd;
        private List<FlightSet> flights = new List<FlightSet>();
        private volatile bool updating = false;
        private ParcourSet parcour;
        private RankForm rankForm;
        private List<PenaltySet> penaltyPoints = new List<PenaltySet>();
        public Visualisation(Client.DataAccess iClient)
        {
            Client = iClient;
            InitializeComponent();
            t = new Timer();
            t.Interval = 5000;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }

        void t_Tick(object sender, EventArgs e)
        {
            if (this.Visible && qRnd != null && !updating)
            {
                updating = true;
                long mintime = long.MaxValue;
                long maxtime = long.MinValue;
                flights.Clear();
                foreach (ListViewItem lvi in listViewCompetitionTeam.Items)
                {
                    if (lvi.Checked && lvi.Tag != null)
                    {
                        FlightSet ct = lvi.Tag as FlightSet;
                        flights.Add(ct);
                        mintime = Math.Min(mintime, ct.TimeTakeOff);
                        maxtime = Math.Max(maxtime, ct.TimeEndLine);
                    }
                }
                recieveData();
            }
        }
        public void recieveData()
        {
            try
            {
                if (flights.Count>0)
                {
                    geControl.SetDaten(flights);
                    visualisationPictureBox1.SetData(flights);
                    visualisationPictureBox1.Invalidate();
                    if (qRnd != null)
                    {
                        penaltyPoints.Clear();
                        foreach (FlightSet flight in flights)
                        {
                            TeamSet t = flight.TeamSet;
                            if (flight.Point.Count>0)
                            {
                                List<PenaltySet> penalties = GeneratePenalty.CalculatePenaltyPoints(flight);
                                penaltyPoints.AddRange(penalties);
                            }
                        }
                        if (rankForm != null && !rankForm.IsDisposed)
                        {
                            rankForm.SetData(penaltyPoints, flights.ToList(), Client);
                        }
                    }
                }
            }
            catch { }
            updating = false;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                QualificRoundComboEntry cce = comboBox1.SelectedItem as QualificRoundComboEntry;
                if (cce != null)
                {
                    qRnd = cce.qualRnd;

                    parcour = qRnd.ParcourSet;
                    MapSet map = parcour.MapSet;
                    MemoryStream ms = new MemoryStream(map.PictureSet.Data);
                    visualisationPictureBox1.Image = System.Drawing.Image.FromStream(ms);
                    visualisationPictureBox1.SetConverter(new Converter(map));
                    visualisationPictureBox1.SetParcour(parcour);
                    visualisationPictureBox1.Invalidate();
                    visualisationPictureBox1.Refresh();
                    geControl.SetParcour(parcour);
                    listViewCompetitionTeam.Items.Clear();
                    List<FlightSet> CompetitionTeamList = qRnd.FlightSet.ToList();
                    CompetitionTeamList.Sort((p, q) => p.StartID.CompareTo(q.StartID));
                    foreach (FlightSet ct in CompetitionTeamList)
                    {
                        ListViewItem lvi2 = new ListViewItem(new string[] { ct.StartID.ToString(),getTeamDsc(ct.TeamSet), new DateTime(ct.TimeTakeOff).ToShortTimeString(), new DateTime(ct.TimeStartLine).ToShortTimeString(), new DateTime(ct.TimeEndLine).ToShortTimeString(), getRouteText(ct.Route) });
                        lvi2.Tag = ct;
                        listViewCompetitionTeam.Items.Add(lvi2);
                    }
                }
            }
        }
        private string getTeamDsc(TeamSet team)
        {
            SubscriberSet pilot = team.Pilot;
            StringBuilder sb = new StringBuilder();
            sb.Append(pilot.LastName).Append(" ").Append(pilot.FirstName);
            if (team.Navigator!=null)
            {
                SubscriberSet navi = team.Navigator;
                sb.Append(" - ").Append(navi.LastName).Append(" ").Append(navi.FirstName);
            }
            return sb.ToString();
        }
        private string getRouteText(int id)
        {
            return ((Route)id).ToString();
        }
        private void btnStartClient_Click(object sender, EventArgs e)
        {
            // the Google Earth plugin is deprecated since 2014
            if (vp != null && !vp.IsDisposed)
            {
                vp.Close();
            }
            vp = new VisualisationPopup();
            vp.Show();
            while (vp.getPlugin() == null) Application.DoEvents();
            geControl.SetPlugin(vp.getPlugin());
            if (parcour != null)
            {
                geControl.SetParcour(parcour);
            }
        }


        private void Visualisation_Load(object sender, EventArgs e)
        {
            List<QualificationRoundSet> qRnds = Client.SelectedCompetition.QualificationRoundSet.ToList();
            comboBox1.Items.Clear();
            foreach (QualificationRoundSet c in qRnds)
            {
                comboBox1.Items.Add(new QualificRoundComboEntry(c));
            }
        }

        private void fldVisualLineWidth_ValueChanged(object sender, EventArgs e)
        {
            geControl.SetLineWidth((int)fldVisualLineWidth.Value);
        }

        private void fldPenaltyHeight_ValueChanged(object sender, EventArgs e)
        {
            geControl.SetHeightPenalty((int)fldPenaltyHeight.Value);
            if (parcour != null)
            {
                geControl.SetParcour(parcour);
            }
        }

        private void fldTrackerHeight_ValueChanged(object sender, EventArgs e)
        {
            geControl.SetTrackerHeightAdjustment((int)fldTrackerHeight.Value);
        }

        private void btnShowRanking_Click(object sender, EventArgs e)
        {
            if (rankForm != null && !rankForm.IsDisposed)
            {
                rankForm.Close();
            }
            rankForm = new RankForm();
            rankForm.Show();
        }
    }
    //class QualificRoundComboEntry
    //{
    //    public readonly QualificationRoundSet qualRnd;
    //    public QualificRoundComboEntry(QualificationRoundSet qualRnd)
    //    {
    //        this.qualRnd = qualRnd;
    //    }
    //    public override string ToString()
    //    {
    //        return qualRnd.Name;
    //    }
    //}
}
