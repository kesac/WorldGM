using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WorldGM.Entities
{
    public class Region
    {
        public int Id { get; set; }
        public int ContinentId { get; set; }

        [IgnoreDataMember]
        public virtual Continent Continent { get; set; }

        public string Name { get; set; }

        public virtual List<City> Cities { get; set; }
    }
}
