﻿using DapperExtensions;
using DapperExtensions.Mapper;

namespace FantasyPremierLeague
{
    public static class GameweekModelMappings
    {
        public class GameweekModelMapper : ClassMapper<Gameweek>
        {
            public GameweekModelMapper()
            {
                //use different table name
                Table("Gameweeks");

                Map(x => x.id).Key(KeyType.Assigned);

                //Ignore this property entirely
                Map(x => x.chip_plays).Ignore();

                //optional, map all other columns
                AutoMap();
            }
        }
    }
}