using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public interface IPlayerHistoryPast
    {
        bool InsertPlayerHistoryPast(PlayerHistoryPast playerHistoryPast);
        bool UpdatePlayerHistoryPast(PlayerHistoryPast playerHistoryPast);
        bool DeletePlayerHistoryPast(int playerHistoryPastId);
    }
}
