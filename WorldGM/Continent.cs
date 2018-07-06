using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WorldGM
{
    public class Continent
    {
        public int Id { get; set; }
        public int WorldId { get; set; }

        [IgnoreDataMember]
        public virtual World World { get; set; }

        public string Name { get; set; }

        public virtual List<Region> Regions { get; set; }
    }
}
