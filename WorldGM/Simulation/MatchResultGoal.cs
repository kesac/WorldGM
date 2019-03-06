using System;
using System.Collections.Generic;
using System.Text;
using WorldGM.Entities;

namespace WorldGM.Simulation
{
    public class MatchResultGoal
    {
        public int Tick { get; set; }
        public Athlete GoalScorer { get; set; }
        public Athlete Assister1 { get; set; }
        public Athlete Assister2 { get; set; }
    }
}
