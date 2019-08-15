using DapperExtensions;
using DapperExtensions.Mapper;

namespace FantasyPremierLeague
{
    public static class FixtureStatsMappings
    {
        public class FixtureStatsMapper : ClassMapper<FixtureStat>
        {
            public FixtureStatsMapper()
            {
                //use different table name
                Table("FixtureStats");

                //Use a different name property from database column
                //Map(x => x.@event).Column("gameweekId");

                Map(x => x.id).Key(KeyType.Identity);

                //Ignore this property entirely
                //Map(x => x.id).Ignore();
                //Map(x => x.fixtureid).Ignore();
                Map(x => x.a).Ignore();
                Map(x => x.h).Ignore();

                //optional, map all other columns
                AutoMap();
            }
        }
    }
}
