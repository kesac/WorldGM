using Loremaker;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WorldGM.Entities
{
    public class Party : Identifiable
    {
        public uint Id { get; set; }
        public List<uint> CharacterIds { get; set; }

        [IgnoreDataMember]
        public virtual List<Character> Characters {get;set;}

        public Party()
        {
            this.CharacterIds = new List<uint>();
            this.Characters = new List<Character>();
        }

        public void Add(Character c)
        {
            this.CharacterIds.Add(c.Id);
            this.Characters.Add(c);
        }
    }
}
