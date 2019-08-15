using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public interface IFixtureStatsValue
    {
        bool InsertFixtureStatsValue(FixtureStatsValue fixtureStatsValue);
        bool UpdateFixtureStatsValue(FixtureStatsValue fixtureStatsValue);
        bool DeleteFixtureStatsValue(int fixtureStatsValueId);
    }
}

