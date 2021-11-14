using System;
using System.Collections.Generic;
using System.Text;
using WorldGM.Entities;

namespace WorldGM.Simulation
{
    public interface IBattleSimulator
    {
        BattleResult Run();
    }
}
