﻿using DapperExtensions;
using DapperExtensions.Mapper;

namespace FantasyPremierLeague
{
    public static class HistoryPastModelMappings
    {
        public class HistoryPastModelMapper : ClassMapper<HistoryPast>
        {
            public HistoryPastModelMapper()
            {
                //use different table name
                Table("HistoryPast");

                //Use a different name property from database column
                //Map(x => x.round).Column("gameweekId");
                //Map(x => x.element).Column("playerId");
                //Map(x => x.fixture).Column("fixtureId");
                //Map(x => x.opponent_team).Column("opponent_teamId");

                Map(x => x.id).Key(KeyType.Assigned);

                //Ignore this property entirely
                //Map(x => x.playerId).Ignore();

                //optional, map all other columns
                AutoMap();
            }
        }
    }
}
