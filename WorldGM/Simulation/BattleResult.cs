using System;
using System.Collections.Generic;
using System.Text;
using WorldGM.Entities;

namespace WorldGM.Simulation
{
    public class BattleResult
    {
        public CombatParty PlayerCharacters { get; set; }
        public CombatParty EnemyCharacters { get; set; }
        public bool Victory { get; set; }
        public int Experience { get; set; }

    }
}
