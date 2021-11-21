using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGM.Entities
{
    public class CombatParty
    {
        public List<CombatEntity> CombatEntities { get; set; }

        public CombatParty(Party basis)
        {
            this.CombatEntities = new List<CombatEntity>();

            foreach(var character in basis.Characters)
            {
                this.CombatEntities.Add(new CombatEntity(character));
            }
        }

    }
}
