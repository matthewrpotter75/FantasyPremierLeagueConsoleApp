using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public interface IHistory
    {
        bool InsertHistory(History history);
        bool UpdateHistory(History history);
        bool DeleteHistory(int historyId);
    }
}
