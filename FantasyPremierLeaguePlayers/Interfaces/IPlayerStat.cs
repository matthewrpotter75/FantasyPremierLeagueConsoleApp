using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public interface IPlayerStat
    {
        bool InsertPlayerStat(PlayerStats playerStats);
        bool UpdatePlayerStat(PlayerStats playerStats);
        bool DeletePlayerStat(int playerStatId);
    }
}