using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FantasyPremierLeague
{
    [DataContract]
    public class FantasyPremierLeagueFixtureData
    {
        [DataMember]
        public Fixture fixture { get; set; }
    }

    public class Fixture
    {
        public int code { get; set; }
        public int @event { get; set; }
        public bool finished { get; set; }
        public bool finished_provisional { get; set; }
        public int id { get; set; }
        public DateTime kickoff_time { get; set; }
        public int minutes { get; set; }
        public bool provisional_start_time { get; set; }
        public bool started { get; set; }
        public int team_a { get; set; }
        public int? team_a_score { get; set; }
        public int team_h { get; set; }
        public int? team_h_score { get; set; }
        public List<FixtureStat> stats { get; set; }
        public int team_h_difficulty { get; set; }
        public int team_a_difficulty { get; set; }
    }

    public class FixtureStat
    {
        public int id { get; set; }
        public int fixtureid { get; set; }
        public string identifier { get; set; }
        public List<FixtureStatsValue> a { get; set; }
        public List<FixtureStatsValue> h { get; set; }
    }

    public class FixtureStatsValue
    {
        public int id { get; set; }
        public int fixtureid { get; set; }
        public int fixtureStatid { get; set; }
        public int playerStatid { get; set; }
        public bool isHome { get; set; }
        public int value { get; set; }
        public int element { get; set; }
    }
}