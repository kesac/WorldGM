using System.Collections.Generic;

namespace WorldGM
{
    public class World
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CurrentYear { get; set; }
        public int CurrentDay { get; set; }

        public virtual List<Continent> Continents { get; set; }

    }
}
