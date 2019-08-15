using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public interface IFixture
    {
        bool InsertFixture(FixtureData fixture);
        bool UpdateFixture(FixtureData fixture);
        bool DeleteFixture(int fixtureId);
    }
}
