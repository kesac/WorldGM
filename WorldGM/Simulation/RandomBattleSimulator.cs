using System;
using System.Collections.Generic;
using System.Text;
using WorldGM.Entities;

namespace WorldGM.Simulation
{
    public class RandomBattleSimulator : IBattleSimulator
    {
        public Party PlayerParty { get; set; }
        public Party EnemyParty { get; set; }

        public RandomBattleSimulator(Party playerCharacters, Party enemyCharacters)
        {
            this.PlayerParty = playerCharacters;
            this.EnemyParty = enemyCharacters;
        }

        public BattleResult Run()
        {
            var r = new Random();

            var result = new BattleResult()
            {
                PlayerCharacters = this.PlayerParty,
                EnemyCharacters = this.EnemyParty
            };

            /*
            for(int i = 0; i < result.HomeScore; i++)
            {
                var goalScorer = this.Home.Memberships[r.Next(this.Home.Memberships.Count)].Character;

                result.Goals.Add(new MatchResultGoal()
                {
                    Tick = r.Next(1000),
                    GoalScorer = goalScorer
                    // to do, assign goal scorer and assister
                });
            }

            for (int i = 0; i < result.AwayScore; i++)
            {

                var goalScorer = this.Away.Memberships[r.Next(this.Away.Memberships.Count)].Character;

                result.Goals.Add(new MatchResultGoal()
                {
                    Tick = r.Next(1000),
                    GoalScorer = goalScorer
                });
            }
            */

            return result;
        }
    }
}
