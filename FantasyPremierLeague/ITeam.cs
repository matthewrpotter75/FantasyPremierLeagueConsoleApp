using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public interface ITeam
    {
        bool InsertTeam(Team team);
        bool UpdateTeam(Team team);
        bool DeleteTeam(int code);
    }
}
