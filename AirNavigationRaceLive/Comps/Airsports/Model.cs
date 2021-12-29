using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AirNavigationRaceLive.Comps.Airsports
{
    // the classes here based on Json objects on airsports.no
    // the classes are used for data exchange with airsports.no (team, sucscriber, parcour)

    #region Reading ContestTeams data

    public class Aeroplane
    {
        public int id { get; set; }
        public string registration { get; set; }
        public string colour { get; set; }
        public string type { get; set; }
        public string picture { get; set; }
    }

    public class Member
    {
        public long id { get; set; }
        public string country_flag_url { get; set; }
        public string country { get; set; }
        public string phone_country_prefix { get; set; }
        public string phone_national_number { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public DateTime creation_time { get; set; }
        public bool validated { get; set; }
        public string app_tracking_id { get; set; }
        public string simulator_tracking_id { get; set; }
        public string app_aircraft_registration { get; set; }
        public string picture { get; set; }
        public string biography { get; set; }
        public bool is_public { get; set; }
        public DateTime? last_seen { get; set; }
    }

    public class Crew
    {
        public long id { get; set; }
        public Member member1 { get; set; }
        public Member member2 { get; set; }
    }

    public class Club
    {
        public long id { get; set; }
        public string country_flag_url { get; set; }
        public string country { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
    }

    public class Team
    {
        public long id { get; set; }
        public string country_flag_url { get; set; }
        public Aeroplane aeroplane { get; set; }
        public string country { get; set; }
        public Crew crew { get; set; }
        public Club club { get; set; }
        public string logo { get; set; }
    }

    public class ContestTeam
    {
        public long id { get; set; }
        public Team team { get; set; }
        public double air_speed { get; set; }
        public string tracking_service { get; set; }
        public string tracking_device { get; set; }
        public string tracker_device_id { get; set; }
        public long contest { get; set; }
    }

    #endregion

    #region Contestants to NavigationTasks

    // Classes for /api/v1/contests/{contest_pk}/navigationtasks/{navigationtask_pk}/contestantsteamid/ 

        public class GateScoreOverride
    {
        public long id { get; set; }
        public List<string> for_gate_types { get; set; }
        public int checkpoint_grace_period_before { get; set; }
        public int checkpoint_grace_period_after { get; set; }
        public int checkpoint_penalty_per_second { get; set; }
        public int checkpoint_maximum_penalty { get; set; }
        public int checkpoint_not_found { get; set; }
        public int missing_procedure_turn_penalty { get; set; }
        public int bad_course_penalty { get; set; }
        public int bad_crossing_extended_gate_penalty { get; set; }
    }

    public class TrackScoreOverride
    {
        public long id { get; set; }
        public float? bad_course_grace_time { get; set; }
        public float bad_course_penalty { get; set; }
        public float bad_course_maximum_penalty { get; set; }
        public float prohibited_zone_penalty { get; set; }
        public float penalty_zone_grace_time { get; set; }
        public float penalty_zone_penalty_per_second { get; set; }
        public float penalty_zone_maximum { get; set; }
        public float corridor_width { get; set; }
        public float corridor_grace_time { get; set; }
        public float corridor_outside_penalty { get; set; }
        public float corridor_maximum_penalty { get; set; }
    }

    public class ContestantsTeam
    {
        public List<GateScoreOverride> gate_score_override { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TrackScoreOverride track_score_override { get; set; }
        public Dictionary<string, string> gate_times { get; set; }
        public bool adaptive_start { get; set; }
        public DateTime takeoff_time { get; set; }
        public float minutes_to_starting_point { get; set; }
        public DateTime finished_by_time { get; set; }
        public float air_speed { get; set; }
        public int contestant_number { get; set; }
        public string tracking_service { get; set; }
        public string tracking_device { get; set; }
        public string tracker_device_id { get; set; }
        public DateTime tracker_start_time { get; set; }
        public string competition_class_longform { get; set; }
        public string competition_class_shortform { get; set; }
        public bool calculator_started { get; set; }
        public float wind_speed { get; set; }
        public float wind_direction { get; set; }
        public long annotation_index { get; set; }
        public long team { get; set; }
        public long id { get; set; }

        [JsonIgnore]
        // Property isNew is not part of the original Airsports model, will not be sent to Airsports
        // It is only used to distinguish between new/ and existing Contestants on a Task
        // // (i.e. decision what HTTP method to use,  POST or PUT)
        public bool isNew { get; set; }

    }

    #endregion
}
