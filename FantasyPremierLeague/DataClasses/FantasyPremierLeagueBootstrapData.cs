using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;

namespace FantasyPremierLeague
{
    [DataContract]
    public class FantasyPremierLeagueBootstrapData
    {
        [DataMember]
        public List<Month> phases { get; set; }
        [DataMember]
        //[JsonProperty("Players")]
        public List<Player> elements { get; set; }
        [DataMember]
        [JsonProperty("game-settings")]
        public GameSettings game_settings { get; set; }
        [DataMember]
        [JsonProperty("total-players")]
        public int total_players { get; set; }
        [DataMember]
        public List<Team> teams { get; set; }
        [DataMember]
        public List<PlayerPosition> element_types { get; set; }
        [DataMember]
        public List<Gameweek> events { get; set; }
    }

    //Phase
    public class Month
    {
        public int id { get; set; }
        public string name { get; set; }
        public int start_event { get; set; }
        public int stop_event { get; set; }
    }

    //Element
    public class Player
    {
        [ExplicitKey]
        public int id { get; set; }
        public string photo { get; set; }
        public string web_name { get; set; }
        public int team_code { get; set; }
        public string status { get; set; }
        public int code { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public int? squad_number { get; set; }
        public string news { get; set; }
        public int now_cost { get; set; }
        public DateTime? news_added { get; set; }
        public int? chance_of_playing_this_round { get; set; }
        public int? chance_of_playing_next_round { get; set; }
        public string value_form { get; set; }
        public string value_season { get; set; }
        public int cost_change_start { get; set; }
        public int cost_change_event { get; set; }
        public int cost_change_start_fall { get; set; }
        public int cost_change_event_fall { get; set; }
        public bool in_dreamteam { get; set; }
        public int dreamteam_count { get; set; }
        public string selected_by_percent { get; set; }
        public string form { get; set; }
        public int transfers_out { get; set; }
        public int transfers_in { get; set; }
        public int transfers_out_event { get; set; }
        public int transfers_in_event { get; set; }
        public int loans_in { get; set; }
        public int loans_out { get; set; }
        public int loaned_in { get; set; }
        public int loaned_out { get; set; }
        public int total_points { get; set; }
        public int event_points { get; set; }
        public string points_per_game { get; set; }
        public string ep_this { get; set; }
        public string ep_next { get; set; }
        public bool special { get; set; }
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
        public string influence { get; set; }
        public string creativity { get; set; }
        public string threat { get; set; }
        public string ict_index { get; set; }
        public int ea_index { get; set; }
        //[JsonProperty("playerPositionId")]
        public int element_type { get; set; }
        //[JsonProperty("teamId")]
        public int team { get; set; }
    }

public class Formations
    {
        [JsonProperty("1-5-2-3")]
        public List<List<int>> formation_1_5_2_3 { get; set; }
        [JsonProperty("1-5-3-2")]
        public List<List<int>> formation_1_5_3_2 { get; set; }
        [JsonProperty("1-3-5-2")]
        public List<List<int>> formation_1_3_5_2 { get; set; }
        [JsonProperty("1-2-5-3")]
        public List<List<int>> formation_1_2_5_3 { get; set; }
        [JsonProperty("1-4-5-1")]
        public List<List<int>> formation_1_4_5_1 { get; set; }
        [JsonProperty("1-5-4-1")]
        public List<List<int>> formation_1_5_4_1 { get; set; }
        [JsonProperty("1-4-3-3")]
        public List<List<int>> formation_1_4_3_3 { get; set; }
        [JsonProperty("1-3-4-3")]
        public List<List<int>> formation_1_3_4_3 { get; set; }
        [JsonProperty("1-4-4-2")]
        public List<List<int>> formation_1_4_4_2 { get; set; }
    }

    public class Game
    {
        public int scoring_ea_index { get; set; }
        public string league_prefix_public { get; set; }
        public int bps_tackles { get; set; }
        public string league_h2h_tiebreak { get; set; }
        public int scoring_long_play { get; set; }
        public int bps_recoveries_limit { get; set; }
        public string facebook_app_id { get; set; }
        public int bps_tackled { get; set; }
        public int bps_errors_leading_to_goal { get; set; }
        public int bps_yellow_cards { get; set; }
        public bool ui_el_hide_currency_qi { get; set; }
        public int scoring_bonus { get; set; }
        public int transfers_cost { get; set; }
        public List<List<int>> default_formation { get; set; }
        public int bps_long_play { get; set; }
        public int bps_long_play_limit { get; set; }
        public int scoring_assists { get; set; }
        public int scoring_long_play_limit { get; set; }
        public int fifa_league_id { get; set; }
        public int league_size_classic_max { get; set; }
        public int scoring_red_cards { get; set; }
        public int scoring_creativity { get; set; }
        public string game_timezone { get; set; }
        public string static_game_url { get; set; }
        public string currency_symbol { get; set; }
        public int bps_target_missed { get; set; }
        public int bps_penalties_saved { get; set; }
        public string support_email_address { get; set; }
        public int cup_start_event_id { get; set; }
        public int scoring_penalties_saved { get; set; }
        public int scoring_threat { get; set; }
        public int scoring_saves { get; set; }
        public int league_join_private_max { get; set; }
        public int scoring_short_play { get; set; }
        public bool sys_use_event_live_api { get; set; }
        public int scoring_concede_limit { get; set; }
        public int bps_key_passes { get; set; }
        public int bps_clearances_blocks_interceptions { get; set; }
        public int bps_pass_percentage_90 { get; set; }
        public int bps_big_chances_missed { get; set; }
        public int league_max_ko_rounds_h2h { get; set; }
        public int bps_open_play_crosses { get; set; }
        public int league_points_h2h_win { get; set; }
        public int bps_saves { get; set; }
        public int bps_cbi_limit { get; set; }
        public int league_size_h2h_max { get; set; }
        public bool sys_vice_captain_enabled { get; set; }
        public int squad_squadplay { get; set; }
        public int bps_fouls { get; set; }
        public int squad_squadsize { get; set; }
        public bool ui_selection_short_team_names { get; set; }
        public decimal transfers_sell_on_fee { get; set; }
        public string transfers_type { get; set; }
        public int scoring_ict_index { get; set; }
        public int bps_pass_percentage_80 { get; set; }
        public int bps_own_goals { get; set; }
        public int scoring_yellow_cards { get; set; }
        public int bps_pass_percentage_70 { get; set; }
        public bool ui_show_home_away { get; set; }
        public bool ui_el_hide_currency_sy { get; set; }
        public int bps_assists { get; set; }
        public int squad_team_limit { get; set; }
        public int league_points_h2h_draw { get; set; }
        public int transfers_limit { get; set; }
        public int bps_dribbles { get; set; }
        public int perform_league_id { get; set; }
        public int bps_offside { get; set; }
        public bool sys_cdn_cache_enabled { get; set; }
        public int currency_multiplier { get; set; }
        public int bps_red_cards { get; set; }
        public int bps_winning_goals { get; set; }
        public int league_join_public_max { get; set; }
        public Formations formations { get; set; }
        public int league_points_h2h_lose { get; set; }
        public int currency_string_places { get; set; }
        public int bps_errors_leading_to_goal_attempt { get; set; }
        public int ui_selection_price_gap { get; set; }
        public int bps_big_chances_created { get; set; }
        public int ui_selection_player_limit { get; set; }
        public int bps_attempted_passes_limit { get; set; }
        public int scoring_penalties_missed { get; set; }
        public string photo_base_url { get; set; }
        public int scoring_bps { get; set; }
        public int scoring_influence { get; set; }
        public int bps_penalties_conceded { get; set; }
        public int scoring_own_goals { get; set; }
        public int squad_total_spend { get; set; }
        public int bps_short_play { get; set; }
        public int ui_element_wrap { get; set; }
        public int bps_recoveries { get; set; }
        public int bps_penalties_missed { get; set; }
        public int scoring_saves_limit { get; set; }
    }

    public class element_type_1
    {
        public int scoring_clean_sheets { get; set; }
        public int squad_min_play { get; set; }
        public int bps_clean_sheets { get; set; }
        public int scoring_goals_conceded { get; set; }
        public int scoring_goals_scored { get; set; }
        public int squad_max_play { get; set; }
        public int bps_goals_scored { get; set; }
        public bool ui_shirt_specific { get; set; }
        public int squad_select { get; set; }
        public List<int> sub_positions_locked { get; set; }
    }

    public class element_type_2
    {
        public int scoring_clean_sheets { get; set; }
        public int squad_min_play { get; set; }
        public int scoring_goals_conceded { get; set; }
        public int scoring_goals_scored { get; set; }
        public int squad_max_play { get; set; }
        public int bps_goals_scored { get; set; }
        public int bps_clean_sheets { get; set; }
        public int squad_select { get; set; }
    }

    public class element_type_3
    {
        public int scoring_clean_sheets { get; set; }
        public int squad_min_play { get; set; }
        public int scoring_goals_conceded { get; set; }
        public int scoring_goals_scored { get; set; }
        public int squad_max_play { get; set; }
        public int bps_goals_scored { get; set; }
        public int bps_clean_sheets { get; set; }
        public int squad_select { get; set; }
    }

    public class element_type_4
    {
        public int scoring_clean_sheets { get; set; }
        public int squad_min_play { get; set; }
        public int scoring_goals_conceded { get; set; }
        public int scoring_goals_scored { get; set; }
        public int squad_max_play { get; set; }
        public int bps_goals_scored { get; set; }
        public int bps_clean_sheets { get; set; }
        public int squad_select { get; set; }
    }

    public class ElementType
    {
        [JsonProperty("1")]
        public element_type_1 element_types_1 { get; set; }
        [JsonProperty("2")]
        public element_type_2 element_types_2 { get; set; }
        [JsonProperty("3")]
        public element_type_3 element_types_3 { get; set; }
        [JsonProperty("4")]
        public element_type_4 element_types_4 { get; set; }
    }

    public class GameSettings
    {
        public Game game { get; set; }
        public ElementType element_type { get; set; }
    }

    public class CurrentEventFixture
    {
        public bool is_home { get; set; }
        public int day { get; set; }
        public int event_day { get; set; }
        public int month { get; set; }
        public int id { get; set; }
        public int opponent { get; set; }
    }

    public class Team
    {
        [ExplicitKey]
        public int id { get; set; }
        [Write(false)]
        public List<CurrentEventFixture> current_event_fixture { get; set; }
        [Write(false)]
        public List<object> next_event_fixture { get; set; }
        public string name { get; set; }
        public int code { get; set; }
        public string short_name { get; set; }
        public bool unavailable { get; set; }
        public int strength { get; set; }
        public int position { get; set; }
        public int played { get; set; }
        public int win { get; set; }
        public int loss { get; set; }
        public int draw { get; set; }
        public int points { get; set; }
        public int? form { get; set; }
        public string link_url { get; set; }
        public int strength_overall_home { get; set; }
        public int strength_overall_away { get; set; }
        public int strength_attack_home { get; set; }
        public int strength_attack_away { get; set; }
        public int strength_defence_home { get; set; }
        public int strength_defence_away { get; set; }
        public int team_division { get; set; }
    }

    //ElementType
    public class PlayerPosition
    {
        [ExplicitKey]
        public int id { get; set; }
        public string singular_name { get; set; }
        public string singular_name_short { get; set; }
        public string plural_name { get; set; }
        public string plural_name_short { get; set; }
    }

    //Gameweeks
    public class Gameweek
    {
        [ExplicitKey]
        public int id { get; set; }
        public string name { get; set; }
        public DateTime deadline_time { get; set; }
        public int average_entry_score { get; set; }
        public bool finished { get; set; }
        public bool data_checked { get; set; }
        public int? highest_scoring_entry { get; set; }
        public int deadline_time_epoch { get; set; }
        public int deadline_time_game_offset { get; set; }
        public string deadline_time_formatted { get; set; }
        public int? highest_score { get; set; }
        public bool is_previous { get; set; }
        public bool is_current { get; set; }
        public bool is_next { get; set; }
    }
}
