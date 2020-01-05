using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public interface IFixture
    {
        bool InsertFixture(Fixture fixture);
        bool UpdateFixture(Fixture fixture);
        bool DeleteFixture(int fixtureId);
    }
}
