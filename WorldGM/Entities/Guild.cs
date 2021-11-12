using Loremaker;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WorldGM.Entities
{
    public class Guild : IEntity
    {
        public uint Id { get; set; }
        public string Name { get; set; }

        [IgnoreDataMember]
        public virtual PopulationCenter HomeCity { get; set; }
        [IgnoreDataMember]
        public virtual List<GuildMembership> Memberships { get; set; }

    }
}
