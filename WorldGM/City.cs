using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM
{
    public class City
    {
        public int Id { get; set; }
        public int RegionId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Population { get; set; }

        public virtual List<Team> Teams { get; set; }

        // Future: world coordinates
        // public int WorldX { get; set; }
        // public int WorldY { get; set; }
    }
}
