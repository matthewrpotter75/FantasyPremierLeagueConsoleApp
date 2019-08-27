using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public interface IPlayerHistory
    {
        bool InsertPlayerHistory(PlayerHistory playerHistory);
        bool UpdatePlayerHistory(PlayerHistory playerHistory);
        bool DeletePlayerHistory(int playerHistoryId);
    }
}
