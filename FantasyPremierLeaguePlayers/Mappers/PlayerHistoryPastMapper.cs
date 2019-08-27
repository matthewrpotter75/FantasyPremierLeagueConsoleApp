using DapperExtensions;
using DapperExtensions.Mapper;

namespace FantasyPremierLeague
{
    public static class PlayerHistoryPastMappings
    {
        public class PlayerHistoryPastMapper : ClassMapper<PlayerHistoryPast>
        {
            public PlayerHistoryPastMapper()
            {
                //use different table name
                Table("PlayerHistoryPast");

                //Use a different name property from database column
                //Map(x => x.round).Column("gameweekId");
                //Map(x => x.element).Column("playerId");
                //Map(x => x.fixture).Column("fixtureId");
                //Map(x => x.opponent_team).Column("opponent_teamId");

                Map(x => x.element_code).Key(KeyType.Assigned);

                //Ignore this property entirely
                Map(x => x.id).Ignore();

                //optional, map all other columns
                AutoMap();
            }
        }
    }
}
