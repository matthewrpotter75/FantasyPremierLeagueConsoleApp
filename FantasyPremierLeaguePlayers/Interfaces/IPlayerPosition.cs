using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public interface IPlayerPosition
    {
        bool InsertPlayerPosition(PlayerPosition playerPosition);
        bool UpdatePlayerPosition(PlayerPosition playerPosition);
        bool DeletePlayerPosition(int playerPositionId);
    }
}
