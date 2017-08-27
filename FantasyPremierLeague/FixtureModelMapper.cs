﻿using DapperExtensions;
using DapperExtensions.Mapper;

namespace FantasyPremierLeague
{
    public static class FixtureModelMappings
    {
        public class FixtureModelMapper : ClassMapper<Fixture2>
        {
            public FixtureModelMapper()
            {
                //use different table name
                Table("Fixtures");

                //Use a different name property from database column
                Map(x => x.@event).Column("gameweekId");

                Map(x => x.id).Key(KeyType.Assigned);

                //Ignore this property entirely
                Map(x => x.team_h_score).Ignore();
                Map(x => x.team_a_score).Ignore();

                //optional, map all other columns
                AutoMap();
            }
        }
    }
}
