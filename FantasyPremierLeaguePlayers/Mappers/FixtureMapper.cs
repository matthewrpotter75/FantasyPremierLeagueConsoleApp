using DapperExtensions;
using DapperExtensions.Mapper;

namespace FantasyPremierLeague
{
    public static class FixtureMappings
    {
        public class FixtureMapper : ClassMapper<Fixture>
        {
            public FixtureMapper()
            {
                //use different table name
                Table("Fixture");

                //Use a different name property from database column
                Map(x => x.@event).Column("gameweekId");

                Map(x => x.id).Key(KeyType.Assigned);

                //Ignore this property entirely
                Map(x => x.stats).Ignore();

                //optional, map all other columns
                AutoMap();
            }
        }
    }
}
