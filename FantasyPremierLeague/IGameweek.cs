using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public interface IGameweek
    {
        bool InsertGameweek(Gameweek gameweek);
        bool UpdateGameweek(Gameweek gameweek);
        bool DeleteGameweek(int gameweekId);
    }
}
