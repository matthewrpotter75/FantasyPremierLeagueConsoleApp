//using DapperExtensions;
//using DapperExtensions.Mapper;

//namespace FantasyPremierLeague
//{
//    public static class PlayerModeMappings
//    {
//        public static void Initialize()
//        {
//            DapperExtensions.DapperExtensions.DefaultMapper = typeof(AutoClassMapper<>);

//            DapperExtensions.DapperExtensions.SetMappingAssemblies(new[]
//            {
//            typeof(PlayerModeMappings).Assembly
//        });
//        }

        //public class PlayerModelMapper : ClassMapper<Player>
        //{
        //    public PlayerModelMapper()
        //    {
        //        //use different table name
        //        Table("Players");

        //        //Use a different name property from database column
        //        Map(x => x.element_type).Column("playerPositionId");
        //        Map(x => x.team).Column("teamId");

        //        Map(x => x.id).Key(KeyType.Assigned);

        //        //Ignore this property entirely
        //        //Map(x => x.SecretDataMan).Ignore();

        //        //optional, map all other columns
        //        AutoMap();
        //    }
        //}

        //public class GameweekModelMapper : ClassMapper<Player>
        //{
        //    public GameweekModelMapper()
        //    {
        //        //use different table name
        //        Table("Gameweeks");

        //        Map(x => x.id).Key(KeyType.Assigned);

        //        //Ignore this property entirely
        //        //Map(x => x.SecretDataMan).Ignore();

        //        //optional, map all other columns
        //        AutoMap();
        //    }
        //}

//    }
//}