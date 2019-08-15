using DapperExtensions;
using DapperExtensions.Mapper;

namespace FantasyPremierLeague
{
    public static class GameweekChipStatsMappings
    {
        public class GameweekChipStatsMapper : ClassMapper<GameweekChipStats>
        {
            public GameweekChipStatsMapper()
            {
                //use different table name
                Table("GameweekChipStats");

                Map(x => x.gameweekid).Key(KeyType.Assigned);

                //Ignore this property entirely
                Map(x => x.id).Ignore();

                //optional, map all other columns
                AutoMap();
            }
        }
    }
}
