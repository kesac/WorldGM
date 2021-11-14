using System;
using System.Collections.Generic;
using System.Text;
using WorldGM.Entities;

namespace WorldGM.Simulation
{
    public class BattleResult
    {
        public Party PlayerCharacters { get; set; }
        public Party EnemyCharacters { get; set; }
        public bool Victory { get; set; }
        public int Experience { get; set; }

    }
}
