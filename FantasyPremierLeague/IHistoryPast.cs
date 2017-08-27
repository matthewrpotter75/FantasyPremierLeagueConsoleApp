using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public interface IHistoryPast
    {
        bool InsertHistoryPast(HistoryPast historyPast);
        bool UpdateHistoryPast(HistoryPast historyPast);
        bool DeleteHistoryPast(int historyPastId);
    }
}
