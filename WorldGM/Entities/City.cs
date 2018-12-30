using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WorldGM.Entities
{
    public class City
    {
        public int Id { get; set; }
        public int RegionId { get; set; }

        [IgnoreDataMember]
        public virtual Region Region { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Population { get; set; }

        [IgnoreDataMember]
        public virtual List<Team> Teams { get; set; }

        // Future: world coordinates
        // public int WorldX { get; set; }
        // public int WorldY { get; set; }
    }
}
