using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGM
{
    public class Continent
    {
        public int Id { get; set; }
        public int WorldId { get; set; }
        public string Name { get; set; }

        public virtual List<Region> Regions { get; set; }
    }
}
