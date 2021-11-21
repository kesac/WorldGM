using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGM.Entities
{
    public class CombatEntity
    {
        public Character Character { get; private set; }
        public float ActionBar { get; set; }
        public int RemainingHp { get; set; }

        public CombatEntity(Character basis)
        {
            this.Character = basis;
            this.ActionBar = 0;
            this.RemainingHp = basis.Vitality;
        }
    }
}
