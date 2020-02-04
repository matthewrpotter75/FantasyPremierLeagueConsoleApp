﻿using DapperExtensions;
using DapperExtensions.Mapper;

namespace FantasyPremierLeague
{
    public static class PlayerHistoryMappings
    {
        public class PlayerHistoryMapper : ClassMapper<PlayerHistory>
        {
            public PlayerHistoryMapper()
            {
                //use different table name
                Table("PlayerHistory");

                //Use a different name property from database column
                Map(x => x.round).Column("gameweekId");
                Map(x => x.element).Column("playerId");
                Map(x => x.fixture).Column("fixtureId");
                Map(x => x.opponent_team).Column("opponent_teamId");

                Map(x => x.id).Key(KeyType.Identity);

                //Ignore this property entirely
                //Map(x => x.kickoff_time_formatted).Ignore();

                //optional, map all other columns
                AutoMap();
            }
        }
    }
}