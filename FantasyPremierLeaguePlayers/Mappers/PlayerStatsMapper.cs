using DapperExtensions;
using DapperExtensions.Mapper;

namespace FantasyPremierLeague
{
    public static class PlayerStatsMappings
    {
        public class PlayerStatsMapper : ClassMapper<PlayerStats>
        {
            public PlayerStatsMapper()
            {
                //use different table name
                Table("PlayerStats");

                //Use a different name property from database column
                Map(x => x.label).Column("playerStatLabel");
                Map(x => x.name).Column("playerStatName").Key(KeyType.Assigned);

                //Map(x => x.name).Key(KeyType.Assigned);

                //Ignore this property entirely
                //Map(x => x.id).Ignore();

                //optional, map all other columns
                AutoMap();
            }
        }
    }
}
