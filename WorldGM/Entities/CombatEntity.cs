using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGM.Entities
{
    public class CombatEntity
    {
        public CombatParty Party { get; set; }
        public Character Character { get; private set; }
        public float ActionBar { get; set; }
        public int RemainingHp { get; set; }

        public bool IsAlive
        {
            get
            {
                return this.RemainingHp > 0;
            }
        }

        public CombatEntity(Character basis)
        {
            this.Character = basis;
            this.ActionBar = 0;
            this.RemainingHp = basis.Vitality * Constants.VitalityToHpMultiplier;
        }
    }
}
