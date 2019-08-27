using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    [DataContract]
    public class FantasyPremierLeaguePlayerData
    {
        [DataMember]
        public List<Fixture2> fixtures { get; set; }
        [DataMember]
        public List<PlayerHistory> history { get; set; }
        [DataMember]
        public List<PlayerHistoryPast> history_past { get; set; }
    }

    public class Fixture2
    {
        public int fixtureid { get; set; }
        public int id { get; set; }
        public string event_name { get; set; }
        public string opponent_name { get; set; }
        public string opponent_short_name { get; set; }
        public bool is_home { get; set; }
        public int difficulty { get; set; }
        public int code { get; set; }
        public string kickoff_time { get; set; }
        public object team_h_score { get; set; }
        public object team_a_score { get; set; }
        public bool finished { get; set; }
        public int minutes { get; set; }
        public bool provisional_start_time { get; set; }
        public bool finished_provisional { get; set; }
        public int? @event { get; set; }
        public int team_a { get; set; }
        public int team_h { get; set; }
        //public int is_homeINT { get; set; }
    }

    public class PlayerHistory
    {
        public int id { get; set; }
        public int element { get; set; }
        public int fixture { get; set; }
        public int opponent_team { get; set; }
        public int total_points { get; set; }
        public bool was_home { get; set; }
        public DateTime kickoff_time { get; set; }
        public int? team_h_score { get; set; }
        public int? team_a_score { get; set; }
        public int round { get; set; }
        public int minutes { get; set; }
        public int goals_scored { get; set; }
        public int assists { get; set; }
        public int clean_sheets { get; set; }
        public int goals_conceded { get; set; }
        public int own_goals { get; set; }
        public int penalties_saved { get; set; }
        public int penalties_missed { get; set; }
        public int yellow_cards { get; set; }
        public int red_cards { get; set; }
        public int saves { get; set; }
        public int bonus { get; set; }
        public int bps { get; set; }
        public decimal influence { get; set; }
        public decimal creativity { get; set; }
        public decimal threat { get; set; }
        public decimal ict_index { get; set; }
        public int value { get; set; }
        public int transfers_balance { get; set; }
        public int selected { get; set; }
        public int transfers_in { get; set; }
        public int transfers_out { get; set; }
    }

    public class PlayerHistoryPast
    {
        public int id { get; set; }
        public string season_name { get; set; }
        public int element_code { get; set; }
        public int start_cost { get; set; }
        public int end_cost { get; set; }
        public int total_points { get; set; }
        public int minutes { get; set; }
        public int goals_scored { get; set; }
        public int assists { get; set; }
        public int clean_sheets { get; set; }
        public int goals_conceded { get; set; }
        public int own_goals { get; set; }
        public int penalties_saved { get; set; }
        public int penalties_missed { get; set; }
        public int yellow_cards { get; set; }
        public int red_cards { get; set; }
        public int saves { get; set; }
        public int bonus { get; set; }
        public int bps { get; set; }
        public decimal influence { get; set; }
        public decimal creativity { get; set; }
        public decimal threat { get; set; }
        public decimal ict_index { get; set; }
        public int playerId { get; set; }
    }
}