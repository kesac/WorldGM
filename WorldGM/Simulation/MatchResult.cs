using System;
using System.Collections.Generic;
using System.Text;
using WorldGM.Entities;

namespace WorldGM.Simulation
{
    public class MatchResult
    {
        public Team Home { get; set; }
        public Team Away { get; set; }

        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

        public List<MatchResultGoal> Goals { get; set; }
    }
}
