using AirNavigationRaceLive.Comps.Airsports;
using AirNavigationRaceLive.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirNavigationRaceLive.Comps.Airsports
{
    public class RESTClient
    {
        // Rest client, based on RestSharp package

        const string FormatUtc = @"yyyy-MM-ddTHH:mm:ssZ";

        // read settings (host, token)
        private static string host = Properties.Settings.Default.AirsportsHost;
        private static string token = Properties.Settings.Default.AirsportsToken;
        private string auth = string.Format(@"Token {0}", token);

        // recommended usage for the RestSharp client
        readonly RestClient _client;

        public RESTClient()
        {
            _client = new RestClient(host);
            // headers used on all requests
            _client.AddDefaultHeaders(
                    new Dictionary<string, string>(){
                    {"Accept", "application/json"},
                    {"Referer", "same-origin"},
                    {"authorization", auth} 
                    }
                );
            // .NET 4.5 and older uses Tls 1.1 by Default. Set explicitely => Tls 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        #region Implemented methods (GET, PUT, POST, DELETE)

        public string GetRequest(string url, out string errorString)
        {
            RestRequest request = new RestRequest(url);
            var response = _client.Get(request);
            // check the following
            errorString = string.Empty;
            if (response.IsSuccessful) { return response.Content; }
            if (response.ErrorException != null)
            {
                string innerExc = response.ErrorException.InnerException != null ? " " + response.ErrorException.InnerException.Message : String.Empty;
                errorString = response.ErrorException.Message + innerExc;
            }
            if (response.ErrorMessage != null) { errorString = response.ErrorMessage.ToString(); }
            return response.Content;
        }

        public string PostRequest(string url, string jsonString, out string errorString)
        {
            RestRequest request = new RestRequest(url);
            request.AddJsonBody(jsonString);
            var response = _client.Post(request);
            // check the following
            errorString = string.Empty;
            if (response.IsSuccessful) { return response.Content; }
            if (response.ErrorException != null)
            {
                string innerExc = response.ErrorException.InnerException != null ? " " + response.ErrorException.InnerException.Message : String.Empty;
                errorString = response.ErrorException.Message + innerExc;
            }
            if (response.ErrorMessage != null) { errorString = response.ErrorMessage.ToString(); }
            return response.Content;
        }

        public string PutRequest(string url, string jsonString, out string errorString)
        {
            RestRequest request = new RestRequest(url);
            request.AddJsonBody(jsonString);
            var response = _client.Put(request);
            // check the following
            errorString = string.Empty;
            if (response.IsSuccessful) { return response.Content; }
            if (response.ErrorException != null)
            {
                string innerExc = response.ErrorException.InnerException != null ? " " + response.ErrorException.InnerException.Message : String.Empty;
                errorString = response.ErrorException.Message + innerExc;
            }
            if (response.ErrorMessage != null) { errorString = response.ErrorMessage.ToString(); }
            return response.Content;
        }

        public string DeleteRequest(string url, out string errorString)
        {
            RestRequest request = new RestRequest(url);
            var response = _client.Delete(request);
            // check the following
            errorString = string.Empty;
            if (response.IsSuccessful) { return response.Content; }
            if (response.ErrorException != null)
            {
                string innerExc = response.ErrorException.InnerException != null ? " " + response.ErrorException.InnerException.Message : String.Empty;
                errorString = response.ErrorException.Message + innerExc;
            }
            if (response.ErrorMessage != null) { errorString = response.ErrorMessage.ToString(); }
            return response.Content;
        }


        public string GetTest(string url, out string errorString, out string httpCode)
        {
            httpCode = @"Http Code = {0}";

            RestRequest request = new RestRequest(url);
            var response = _client.Get(request);
            // check the following
            httpCode = string.Format(httpCode, response.StatusCode);
            errorString = string.Empty;
            if (response.IsSuccessful) { return response.Content; }
            if (response.ErrorException != null)
            {
                string innerExc = response.ErrorException.InnerException != null ? " " + response.ErrorException.InnerException.Message : String.Empty;
                errorString = response.ErrorException.Message + innerExc;
            }
            if (response.ErrorMessage != null) { errorString = response.ErrorMessage.ToString(); }
            return response.Content;
        }

        #endregion

        #region Read Airsports ContestTeams, add them as ANR scoring Teams and Subscribers
        public List<ContestTeam> GetASTeamsOnContest(string contestId, out string errorString)
        {
            string url = string.Format(@"/api/v1/contests/{0}/teams/", contestId);
            string content = GetRequest(url, out errorString);

            List<ContestTeam> contestTeamList = null;
            if (string.IsNullOrEmpty(errorString))
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                contestTeamList = JsonConvert.DeserializeObject<List<ContestTeam>>(content, settings);
            }
            return contestTeamList;
        }
        public List<ContestTeam> GetASTeamsOnContestFile()
        {
            //var response = client.Get(request);
            string response = File.ReadAllText(@"C:\Users\zugerarm1\Downloads\contestteams_sample.json");
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            List<ContestTeam> contestTeamList = null;
            contestTeamList = JsonConvert.DeserializeObject<List<ContestTeam>>(response, settings);

            return contestTeamList;
        }
         #endregion

        #region Read Airsports Contestants on Task, modify and PUT back to AS
        public List<ContestantsTeam> GetASContestantsTeamsOnNavTask(string contestId, string navTaskId, out string errorString)
        {
            string url = string.Format(@"/api/v1/contests/{0}/navigationtasks/{1}/contestantsteamid/", contestId, navTaskId);
            //RESTClient ac = new RESTClient();
            string content = GetRequest(url, out errorString);

            List<ContestantsTeam> contestantsTeamList = null;
            if (string.IsNullOrEmpty(errorString))
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                contestantsTeamList = JsonConvert.DeserializeObject<List<ContestantsTeam>>(content, settings);
            }
            return contestantsTeamList;
        }
        public List<ContestantsTeam> UpsertASContestantsTeamsOnNavTask(string contestId, string navTaskId, List<ContestantsTeam> lstTeamsOnTask, out List<string> lstErrorString)
        {
            List<ContestantsTeam> lstContestantsTeam = new List<ContestantsTeam>();
            lstErrorString = new List<string>();
            // put individual contestsTeam
            foreach (var contestantsTeam in lstTeamsOnTask)
            {
                var stringJson = JsonConvert.SerializeObject(contestantsTeam, Formatting.Indented);
                //File.WriteAllText( @"c:\temp\json.json", stringJson);

                string url = string.Empty;
                string errorString = string.Empty;
                string content = string.Empty;

                if (contestantsTeam.isNew == true)
                {
                    // POST new entry (does not yet exist in Airsports Task)
                    url = string.Format(@"/api/v1/contests/{0}/navigationtasks/{1}/contestantsteamid/", contestId, navTaskId);
                    content = PostRequest(url, stringJson, out errorString);
                    lstErrorString.Add(errorString);
                }
                else
                {
                    // PUT existing entries in Airsports Task
                    url = string.Format(@"/api/v1/contests/{0}/navigationtasks/{1}/contestantsteamid/{2}/", contestId, navTaskId, contestantsTeam.id);
                    content = PutRequest(url, stringJson, out errorString);
                    lstErrorString.Add(errorString);
                }


                if (string.IsNullOrEmpty(errorString))
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    var contestantsTeamRes = JsonConvert.DeserializeObject<ContestantsTeam>(content, settings);
                    lstContestantsTeam.Add(contestantsTeamRes);
                }
            }
            return lstContestantsTeam;
        }

        #endregion

        #region Mapping between ANR Scoring <=> Airsports objects

        public List<SubscriberSet> MapASTeamsOnContestToSubscribers(List<ContestTeam> lstTeamsOnContest, int competitionId)
        {
            List<SubscriberSet> lst = new List<SubscriberSet>();

            foreach (var tm in lstTeamsOnContest.Select(c => c.team).Distinct().Where(t => t.crew != null && (t.crew.member1 != null || t.crew.member2 != null)))
            {
                if (tm.crew.member1 != null)
                {
                    SubscriberSet s = new SubscriberSet();
                    s.LastName = tm.crew.member1.last_name;
                    s.FirstName = tm.crew.member1.first_name;
                    s.External_Id = tm.crew.member1.id.ToString();
                    s.Competition_Id = competitionId;
                    lst.Add(s);
                }

                if (tm.crew.member2 != null)
                {
                    SubscriberSet s = new SubscriberSet();
                    s.LastName = tm.crew.member2.last_name;
                    s.FirstName = tm.crew.member2.first_name;
                    s.External_Id = tm.crew.member2.id.ToString();
                    s.Competition_Id = competitionId;
                    lst.Add(s);
                }
            }

            // remove duplicates (if members participate multiple times with varying crew)
            lst = lst.Distinct().ToList();
            return lst;
        }
        public List<TeamSet> MapASTeamsOnContestToTeams(List<ContestTeam> lstTeamsOnContest, List<SubscriberSet> lstSubsc, int competitionId)
        {
            // load SubscriberSet data (already loaded in first step into Db) for given competition

            List<TeamSet> lst = new List<TeamSet>();
            foreach (var tm in lstTeamsOnContest.Select(c => c.team).Distinct().Where(t => t.crew != null))
            {
                TeamSet t = new TeamSet();
                t.AC = tm.aeroplane.registration;
                t.External_Id = tm.id.ToString();
                t.Nationality = tm.country;
                t.Competition_Id = competitionId;

                if (tm.crew.member1 != null)
                {
                    t.Pilot_Id = lstSubsc.FirstOrDefault(c => c.Competition_Id == competitionId && tm != null && tm.crew != null && tm.crew.member1 != null && c.External_Id == tm.crew.member1.id.ToString()).Id;
                }
                if (tm.crew.member2 != null)
                {
                    t.Navigator_Id = lstSubsc.FirstOrDefault(c => c.Competition_Id == competitionId && tm != null && tm.crew != null && tm.crew.member2 != null && c.External_Id == tm.crew.member2.id.ToString()).Id;
                }
                lst.Add(t);
            }

            lst = lst.Distinct().ToList();
            return lst;
        }
        public List<ContestantsTeam> MapUpsertFlightSetsToASContestantsTeamsOnNavTask(List<FlightSet> startingList, List<ContestTeam> lstTeamsOnContest, List<ContestantsTeam> lstTeamsOnTask, int qualifRoundId, string contestId)
        {
            // FlightSets = StartingList
            // lstTeamsOnContest = all AS Teams on AS competition level (not yet registered for specific task)
            // lstTeamsOnTask = the AS teams that should be registered for the specific task (isNew=true: add,POST, isNew=false: update,PUT)

            List<ContestantsTeam> lst = new List<ContestantsTeam>();
            int iCounter = 0;
            foreach (var tm in startingList.Where(x => x.QualificationRound_Id == qualifRoundId && x.TeamSet != null).OrderBy(x => x.TimeTakeOff))
            {
                // process only startlist entries that have a non-empty External Id
                if (!string.IsNullOrEmpty(tm.TeamSet.External_Id))
                {
                    var ctm = lstTeamsOnContest.Where(x => x.team.id == long.Parse(tm.TeamSet.External_Id)).FirstOrDefault();
                    if (ctm == null)
                    {
                        // no team on contest with given externalId, we cannot add it
                        MessageBox.Show(string.Format("In Airsports.no, there is no Team with Id={0} in this Contest (ContestId={1})", tm.TeamSet.External_Id, contestId), "Participants and Teams", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }
                    var ct = lstTeamsOnTask.Where(x => x.team == long.Parse(tm.TeamSet.External_Id)).FirstOrDefault();
            
                    if (ct == null)
                    {
                        // ContestantTeam not existing yet on task, will have to be created
                        // must add some default values
                        ct = new ContestantsTeam();
                        ct.isNew = true;
                        ct.team = ctm.team.id;
                        ct.air_speed = (float)ctm.air_speed;
                        ct.tracking_service = "traccar";
                        ct.tracking_device = "pilot_app_or_copilot_a[[";
                        ct.gate_score_override = new List<GateScoreOverride>();
                    }
                    else
                    {
                        ct.isNew = false;
                    }
                    // gate times is a dictionary object
                    Dictionary<string, string> gate_times = new Dictionary<string, string>();
                    gate_times.Add("SP", new DateTime(tm.TimeStartLine).ToString(FormatUtc, DateTimeFormatInfo.InvariantInfo));
                    gate_times.Add("FP", new DateTime(tm.TimeEndLine).ToString(FormatUtc, DateTimeFormatInfo.InvariantInfo));
                    ct.gate_times = gate_times;
                    ct.track_score_override = null;

                    ct.takeoff_time = new DateTime(tm.TimeTakeOff, DateTimeKind.Utc);
                    ct.contestant_number = iCounter + 1; // same number on AS, as on ANR Scoring
                    ct.adaptive_start = true;
                    ct.tracker_start_time = new DateTime(tm.TimeTakeOff, DateTimeKind.Utc).AddMinutes(-5.0);

                    // use time difference between TO and SP to calculate 
                    //TimeSpan ts = new DateTime(tm.TimeStartLine, DateTimeKind.Utc) - ct.takeoff_time;
                    //ct.minutes_to_starting_point = (Int32)ts.TotalMinutes;

                    // use time difference between TO and FP, and add it to FP time 
                    TimeSpan ts = new DateTime(tm.TimeEndLine, DateTimeKind.Utc) - ct.takeoff_time;
                    DateTime dt = new DateTime(tm.TimeEndLine, DateTimeKind.Utc);
                    ct.finished_by_time = dt.Add(ts);

                    lst.Add(ct);
                }

                ++iCounter; // counter for all startlist entries
            }

            // now PUT each list entry

            return lst;
        }

        #endregion

        public bool CheckConnection(out string errorString)
        {
            // getting an array of all contests
            // purely check that the result is an array
            string url = @"/api/v1/contests/";
            string httpCode = string.Empty;
            string content = GetTest(url, out errorString, out httpCode);
            try
            {
                JObject jsonres = JObject.Parse(content);
                if (jsonres.Type == JTokenType.Array)
                {
                    return true;
                }
                else
                {
                    errorString = httpCode + " " + errorString + " " + content;
                    return false;
                }
            }
            catch (Exception)
            {
                errorString = httpCode + " " + errorString + " " + content;
                return false;
            }

        }

    }
}
