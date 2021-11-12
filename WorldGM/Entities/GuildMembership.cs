using System.Runtime.Serialization;

namespace WorldGM.Entities
{
    public class GuildMembership
    {
        public uint Id { get; set; }
        public uint GuildId { get; set; }
        public uint CharacterId { get; set; }

    }
}
