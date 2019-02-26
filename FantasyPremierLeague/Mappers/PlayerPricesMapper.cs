//using DapperExtensions;
//using DapperExtensions.Mapper;

//namespace FantasyPremierLeague
//{
//    public static class PlayerPricesModelMappings
//    {
//        //public static void Initialize()
//        //{
//        //    DapperExtensions.DapperExtensions.DefaultMapper = typeof(AutoClassMapper<>);

//        //    DapperExtensions.DapperExtensions.SetMappingAssemblies(new[]
//        //    {
//        //        typeof(PlayerModelMappings).Assembly
//        //    });
//        //}
//        public class PlayerPricesModelMapper : ClassMapper<Player>
//        {
//            public PlayerPricesModelMapper()
//            {
//                //use different table name
//                Table("PlayerPrices");

//                //Use a different name property from database column
//                Map(x => x.id).Column("playerid").Key(KeyType.Assigned);
//                Map(x => x.element_type).Column("playerPositionId");
//                Map(x => x.team).Column("teamId");
//                Map(x => x.now_cost).Column("cost");

//                //Ignore this property entirely
//                Map(x => x.photo).Ignore();
//                Map(x => x.team_code).Ignore();
//                Map(x => x.status).Ignore();
//                Map(x => x.code).Ignore();
//                Map(x => x.squad_number).Ignore();
//                Map(x => x.news).Ignore();
//                Map(x => x.chance_of_playing_this_round).Ignore();
//                Map(x => x.chance_of_playing_next_round).Ignore();
//                Map(x => x.value_form).Ignore();
//                Map(x => x.value_season).Ignore();
//                Map(x => x.cost_change_start).Ignore();
//                Map(x => x.cost_change_event).Ignore();
//                Map(x => x.cost_change_start_fall).Ignore();
//                Map(x => x.cost_change_event_fall).Ignore();
//                Map(x => x.in_dreamteam).Ignore();
//                Map(x => x.dreamteam_count).Ignore();
//                Map(x => x.selected_by_percent).Ignore();
//                Map(x => x.form).Ignore();
//                Map(x => x.transfers_out).Ignore();
//                Map(x => x.transfers_in).Ignore();
//                Map(x => x.transfers_out_event).Ignore();
//                Map(x => x.transfers_in_event).Ignore();
//                Map(x => x.loans_in).Ignore();
//                Map(x => x.loans_out).Ignore();
//                Map(x => x.loaned_in).Ignore();
//                Map(x => x.loaned_out).Ignore();
//                Map(x => x.total_points).Ignore();
//                Map(x => x.event_points).Ignore();
//                Map(x => x.points_per_game).Ignore();
//                Map(x => x.ep_this).Ignore();
//                Map(x => x.ep_next).Ignore();
//                Map(x => x.special).Ignore();
//                Map(x => x.minutes).Ignore();
//                Map(x => x.goals_scored).Ignore();
//                Map(x => x.assists).Ignore();
//                Map(x => x.clean_sheets).Ignore();
//                Map(x => x.goals_conceded).Ignore();
//                Map(x => x.own_goals).Ignore();
//                Map(x => x.penalties_saved).Ignore();
//                Map(x => x.penalties_missed).Ignore();
//                Map(x => x.yellow_cards).Ignore();
//                Map(x => x.red_cards).Ignore();
//                Map(x => x.saves).Ignore();
//                Map(x => x.bonus).Ignore();
//                Map(x => x.bps).Ignore();

//                //optional, map all other columns
//                AutoMap();
//            }
//        }
//    }
//}
