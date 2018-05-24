using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM
{
    public class Region
    {
        public int Id { get; set; }
        public int WorldId { get; set; }

        public string Name { get; set; }

        public virtual List<City> Cities { get; set; }
    }
}
