using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM
{
    public class World
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int CurrentYear { get; set; }

        public virtual List<Region> Regions { get; set; }

    }
}
