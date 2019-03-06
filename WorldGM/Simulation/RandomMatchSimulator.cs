using System;
using System.Collections.Generic;
using System.Text;
using WorldGM.Entities;

namespace WorldGM.Simulation
{
    public class RandomMatchSimulator : IMatchSimulator
    {
        public Team Home { get; set; }
        public Team Away { get; set; }

        public RandomMatchSimulator(Team home, Team away)
        {
            this.Home = home;
            this.Away = away;
        }

        public MatchResult Run()
        {
            var r = new Random();

            var result = new MatchResult()
            {
                Home = this.Home,
                Away = this.Away,
                HomeScore = r.Next(8),
                AwayScore = r.Next(3),
                Goals = new List<MatchResultGoal>()
            };

            for(int i = 0; i < result.HomeScore; i++)
            {
                var goalScorer = this.Home.TeamContracts[r.Next(this.Home.TeamContracts.Count)].Athlete;

                result.Goals.Add(new MatchResultGoal()
                {
                    Tick = r.Next(1000),
                    GoalScorer = goalScorer
                    // to do, assign goal scorer and assister
                });
            }

            for (int i = 0; i < result.AwayScore; i++)
            {

                var goalScorer = this.Away.TeamContracts[r.Next(this.Away.TeamContracts.Count)].Athlete;

                result.Goals.Add(new MatchResultGoal()
                {
                    Tick = r.Next(1000),
                    GoalScorer = goalScorer
                });
            }

            return result;
        }
    }
}
