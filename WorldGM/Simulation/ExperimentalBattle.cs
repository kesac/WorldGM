using Loremaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldGM.Entities;

namespace WorldGM.Simulation
{
    public class ExperimentalBattle : IBattleSimulator
    {
        private CombatParty PlayerParty;
        private CombatParty EnemyParty;
        private Random Random;
        private List<CombatEntity> AllCombatEntities;

        public ExperimentalBattle(Party playerCharacters, Party enemyCharacters)
        {
            this.PlayerParty = new CombatParty(playerCharacters);
            this.EnemyParty = new CombatParty(enemyCharacters);
            
            this.AllCombatEntities = new List<CombatEntity>();
            this.AllCombatEntities.AddRange(this.PlayerParty.CombatEntities);
            this.AllCombatEntities.AddRange(this.EnemyParty.CombatEntities);

            this.Random = new Random();
        }

        private bool BothSidesAlive()
        {
            return this.PlayerParty.CombatEntities.Any(x => x.RemainingHp > 0)
                 && this.EnemyParty.CombatEntities.Any(x => x.RemainingHp > 0);
        }

        private void ProgressActionBars()
        {
            // Find character who will reach 100% in their action bar
            // the fastest

            float ticksToNextAction = float.MaxValue;
            CombatEntity nextToAct = this.AllCombatEntities.FirstOrDefault();

            foreach(var e in this.AllCombatEntities)
            {
                var current = e.ActionBar;
                var ticksRequired = (100 - current) / e.Character.Speed;

                if(ticksRequired < ticksToNextAction || (ticksRequired == ticksToNextAction && Chance.Roll(0.50)))
                {
                    ticksToNextAction = ticksRequired;
                    nextToAct = e;
                }
            }

            foreach(var e in this.AllCombatEntities)
            {
                e.ActionBar = e.ActionBar + (e.Character.Speed * ticksToNextAction);
            }

        }

        public CombatEntity GetNextActingEntity()
        {
            var max = this.AllCombatEntities.Max(x => x.ActionBar);
            return this.AllCombatEntities.FirstOrDefault(x => x.ActionBar == max);
        }

        public BattleResult Run()
        {

            int turns = 0;
            int maxTurns = 10;

            while(this.BothSidesAlive())
            {
                this.ProgressActionBars();
                var e = this.GetNextActingEntity();

                Console.WriteLine("Next turn was {0} at {1} speed", e.Character.Name, e.Character.Speed);
                e.ActionBar = 0;

                turns++;

                if(turns >= maxTurns)
                {
                    break;
                }

            }

            var result = new BattleResult()
            {
                PlayerCharacters = this.PlayerParty,
                EnemyCharacters = this.EnemyParty,
                Victory = true
            };

            return result;
        }
    }
}
