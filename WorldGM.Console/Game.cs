using Loremaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGM.Entities;

namespace WorldGM.Console
{
    public class Game
    {
        public World World { get; set; }
        public PopulationCenter HomeCity { get; set; }
        public List<Character> GuildMembers { get; set; }

        public Game()
        {
            this.GuildMembers = new List<Character>();
        }

    }
}
