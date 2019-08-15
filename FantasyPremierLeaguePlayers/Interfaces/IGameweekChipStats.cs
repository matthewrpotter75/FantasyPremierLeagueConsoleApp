using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public interface IGameweekChipStats
    {
        bool InsertGameweekChipStats(GameweekChipStats gameweekChipStats);
        bool UpdateGameweekChipStats(GameweekChipStats gameweekChipStats);
        bool DeleteGameweekChipStats(int gameweekChipStatId);
    }
}
