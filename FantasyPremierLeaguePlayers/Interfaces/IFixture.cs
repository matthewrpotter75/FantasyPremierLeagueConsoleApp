using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public interface IFixture
    {
        bool InsertFixture(Fixture2 fixture);
        bool UpdateFixture(Fixture2 fixture);
        bool DeleteFixture(int fixtureId);
    }
}
