using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM.Generation
{
    public interface ITeamGenerator
    {
        Team NextTeam();
    }
}
