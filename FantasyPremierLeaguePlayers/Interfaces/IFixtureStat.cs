using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public interface IFixtureStat
    {
        bool InsertFixtureStat(FixtureStat fixtureStat);
        bool UpdateFixtureStat(FixtureStat fixtureStat);
        bool DeleteFixtureStat(int fixtureStatId);
    }
}
