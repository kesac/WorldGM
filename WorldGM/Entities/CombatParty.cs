using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGM.Entities
{
    public class CombatParty
    {
        public List<CombatEntity> CombatEntities { get; set; }
        public bool IsAlive
        {
            get
            {
                return this.CombatEntities.Any(x => x.IsAlive);
            }
        }

        public CombatParty(Party basis)
        {
            this.CombatEntities = new List<CombatEntity>();

            foreach(var character in basis.Characters)
            {
                var e = new CombatEntity(character) { Party = this };
                this.CombatEntities.Add(e);
            }
        }

    }
}
