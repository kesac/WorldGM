using Loremaker;
using System.Runtime.Serialization;

namespace WorldGM.Entities
{
    public class GuildMembership : Identifiable
    {
        public uint Id { get; set; }
        public uint GuildId { get; set; }
        public uint CharacterId { get; set; }

        // Back references that should not be part of
        // serialization and must be manually restored
        // during deserialization

        [IgnoreDataMember]
        public virtual Guild Guild { get; set; }

        [IgnoreDataMember]
        public virtual Character Character { get; set; }

    }
}
