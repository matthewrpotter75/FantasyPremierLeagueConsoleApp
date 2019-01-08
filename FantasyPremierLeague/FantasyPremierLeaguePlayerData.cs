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
        public List<HistoryPast> history_past { get; set; }
        [DataMember]
        public List<FixturesSummary> fixtures_summary { get; set; }
        [DataMember]
        public List<Explain> explain { get; set; }
        [DataMember]
        public List<HistorySummary> history_summary { get; set; }
        [DataMember]
        public List<Fixture2> fixtures { get; set; }
        [DataMember]
        public List<History> history { get; set; }
    }

    public class HistoryPast
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
        public int ea_index { get; set; }
        public int season { get; set; }
        public int playerId { get; set; }
    }

    public class FixturesSummary
    {
        public int id { get; set; }
        public string kickoff_time_formatted { get; set; }
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
        public int @event { get; set; }
        public int team_a { get; set; }
        public int team_h { get; set; }
    }

    public class Minutes
    {
        public int points { get; set; }
        public string name { get; set; }
        public int value { get; set; }
    }

    public class Explain2
    {
        public Minutes minutes { get; set; }
    }

    public class H
    {
        public int value { get; set; }
        public int element { get; set; }
    }

    public class GoalsScored
    {
        public List<object> a { get; set; }
        public List<H> h { get; set; }
    }

    public class H2
    {
        public int value { get; set; }
        public int element { get; set; }
    }

    public class Assists
    {
        public List<object> a { get; set; }
        public List<H2> h { get; set; }
    }

    public class OwnGoals
    {
        public List<object> a { get; set; }
        public List<object> h { get; set; }
    }

    public class PenaltiesSaved
    {
        public List<object> a { get; set; }
        public List<object> h { get; set; }
    }

    public class PenaltiesMissed
    {
        public List<object> a { get; set; }
        public List<object> h { get; set; }
    }

    public class A
    {
        public int value { get; set; }
        public int element { get; set; }
    }

    public class H3
    {
        public int value { get; set; }
        public int element { get; set; }
    }

    public class YellowCards
    {
        public List<A> a { get; set; }
        public List<H3> h { get; set; }
    }

    public class RedCards
    {
        public List<object> a { get; set; }
        public List<object> h { get; set; }
    }

    public class A2
    {
        public int value { get; set; }
        public int element { get; set; }
    }

    public class H4
    {
        public int value { get; set; }
        public int element { get; set; }
    }

    public class Saves
    {
        public List<A2> a { get; set; }
        public List<H4> h { get; set; }
    }

    public class H5
    {
        public int value { get; set; }
        public int element { get; set; }
    }

    public class Bonus
    {
        public List<object> a { get; set; }
        public List<H5> h { get; set; }
    }

    public class A3
    {
        public int value { get; set; }
        public int element { get; set; }
    }

    public class H6
    {
        public int value { get; set; }
        public int element { get; set; }
    }

    public class Bps
    {
        public List<A3> a { get; set; }
        public List<H6> h { get; set; }
    }

    public class Stat
    {
        public GoalsScored goals_scored { get; set; }
        public Assists assists { get; set; }
        public OwnGoals own_goals { get; set; }
        public PenaltiesSaved penalties_saved { get; set; }
        public PenaltiesMissed penalties_missed { get; set; }
        public YellowCards yellow_cards { get; set; }
        public RedCards red_cards { get; set; }
        public Saves saves { get; set; }
        public Bonus bonus { get; set; }
        public Bps bps { get; set; }
    }

    public class Fixture
    {
        public int id { get; set; }
        public string kickoff_time_formatted { get; set; }
        public bool started { get; set; }
        public int event_day { get; set; }
        public DateTime deadline_time { get; set; }
        public string deadline_time_formatted { get; set; }
        public List<Stat> stats { get; set; }
        public int code { get; set; }
        public string kickoff_time { get; set; }
        public int? team_h_score { get; set; }
        public int? team_a_score { get; set; }
        public bool finished { get; set; }
        public int minutes { get; set; }
        public bool provisional_start_time { get; set; }
        public bool finished_provisional { get; set; }
        public int @event { get; set; }
        public int team_a { get; set; }
        public int team_h { get; set; }
    }

    public class Explain
    {
        public Explain2 explain { get; set; }
        public Fixture fixture { get; set; }
    }

    public class HistorySummary
    {
        public int id { get; set; }
        public DateTime kickoff_time { get; set; }
        public string kickoff_time_formatted { get; set; }
        public int? team_h_score { get; set; }
        public int? team_a_score { get; set; }
        public bool was_home { get; set; }
        public int round { get; set; }
        public int total_points { get; set; }
        public int value { get; set; }
        public int transfers_balance { get; set; }
        public int selected { get; set; }
        public int transfers_in { get; set; }
        public int transfers_out { get; set; }
        public int loaned_in { get; set; }
        public int loaned_out { get; set; }
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
        public int ea_index { get; set; }
        public int open_play_crosses { get; set; }
        public int big_chances_created { get; set; }
        public int clearances_blocks_interceptions { get; set; }
        public int recoveries { get; set; }
        public int key_passes { get; set; }
        public int tackles { get; set; }
        public int winning_goals { get; set; }
        public int attempted_passes { get; set; }
        public int completed_passes { get; set; }
        public int penalties_conceded { get; set; }
        public int big_chances_missed { get; set; }
        public int errors_leading_to_goal { get; set; }
        public int errors_leading_to_goal_attempt { get; set; }
        public int tackled { get; set; }
        public int offside { get; set; }
        public int target_missed { get; set; }
        public int fouls { get; set; }
        public int dribbles { get; set; }
        public int element { get; set; }
        public int fixture { get; set; }
        public int opponent_team { get; set; }
    }

    public class Fixture2
    {
        public int fixtureid { get; set; }
        public int id { get; set; }
        public string kickoff_time_formatted { get; set; }
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

    public class History
    {
        public int id { get; set; }
        public DateTime kickoff_time { get; set; }
        public string kickoff_time_formatted { get; set; }
        public int? team_h_score { get; set; }
        public int? team_a_score { get; set; }
        public bool was_home { get; set; }
        public int round { get; set; }
        public int total_points { get; set; }
        public int value { get; set; }
        public int transfers_balance { get; set; }
        public int selected { get; set; }
        public int transfers_in { get; set; }
        public int transfers_out { get; set; }
        public int loaned_in { get; set; }
        public int loaned_out { get; set; }
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
        public int ea_index { get; set; }
        public int open_play_crosses { get; set; }
        public int big_chances_created { get; set; }
        public int clearances_blocks_interceptions { get; set; }
        public int recoveries { get; set; }
        public int key_passes { get; set; }
        public int tackles { get; set; }
        public int winning_goals { get; set; }
        public int attempted_passes { get; set; }
        public int completed_passes { get; set; }
        public int penalties_conceded { get; set; }
        public int big_chances_missed { get; set; }
        public int errors_leading_to_goal { get; set; }
        public int errors_leading_to_goal_attempt { get; set; }
        public int tackled { get; set; }
        public int offside { get; set; }
        public int target_missed { get; set; }
        public int fouls { get; set; }
        public int dribbles { get; set; }
        public int element { get; set; }
        public int fixture { get; set; }
        public int opponent_team { get; set; }
    }

}