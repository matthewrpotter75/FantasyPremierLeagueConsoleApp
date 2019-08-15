using DapperExtensions;
using DapperExtensions.Mapper;

namespace FantasyPremierLeague
{
    public static class FixtureStatsValueMappings
    {
        public class FixtureStatsValueMapper : ClassMapper<FixtureStatsValue>
        {
            public FixtureStatsValueMapper()
            {
                //use different table name
                Table("FixtureStatsValues");

                //Use a different name property from database column
                Map(x => x.element).Column("playerid");

                Map(x => x.id).Key(KeyType.Identity);

                //Ignore this property entirely
                //Map(x => x.id).Ignore();
                //Map(x => x.fixtureid).Ignore();
                //Map(x => x.fixtureStatid).Ignore();
                //Map(x => x.playerStatid).Ignore();
                //Map(x => x.isHome).Ignore();

                //optional, map all other columns
                AutoMap();
            }
        }
    }
}