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
        public CombatParty PlayerParty { get; set; }
        public CombatParty EnemyParty { get; set; }
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

        private bool IsBothSidesAlive()
        {
            return this.PlayerParty.IsAlive && this.EnemyParty.IsAlive;
        }

        private void FillActionBarsToNextTurn()
        {
            // Find character who will reach 100% in their action bar
            // the fastest

            var ticksToNextAction = float.MaxValue;
            var aliveEntities = this.AllCombatEntities.Where(x => x.IsAlive);

            var nextToAct = aliveEntities.FirstOrDefault();

            foreach(var e in aliveEntities)
            {
                var current = e.ActionBar;
                var ticksRequired = (100 - current) / e.Character.Speed;

                if(ticksRequired < ticksToNextAction || (ticksRequired == ticksToNextAction && Chance.Roll(0.50)))
                {
                    ticksToNextAction = ticksRequired;
                    nextToAct = e;
                }
            }

            // Progress everyone's action bars until the fastest
            // character's bar is full

            foreach(var e in aliveEntities)
            {
                e.ActionBar = e.ActionBar + (e.Character.Speed * ticksToNextAction);
            }

        }

        private CombatEntity GetNextActingEntity()
        {
            var alive = this.AllCombatEntities.Where(x => x.IsAlive);
            var max = alive.Max(x => x.ActionBar);
            return alive.FirstOrDefault(x => x.ActionBar == max);
        }

        private void ExecuteAction(CombatEntity e)
        {
            var ability = e.Character.PrimaryAbility;

            foreach(var action in ability.Actions)
            {
                var target = this.GetTarget(e, action.TargetType);

                if(action.Type == AbilityActionType.PhysicalAttack)
                {
                    var physAttack = e.Character.Strength;  // TODO: + equipment
                    var physDefense = e.Character.Strength; // TODO: + equipment

                    // TODO: Affinity multiplier

                    var defensiveMultiplier = ((float)(physAttack + target.Character.Level * 3) / (physDefense + (target.Character.Level*2)));
                    var minDamage = (int)(physAttack * action.MinMultiplier * defensiveMultiplier);
                    var maxDamage = (int)(physAttack * action.MaxMultiplier * defensiveMultiplier);
                    var damage = this.Random.Next(minDamage, maxDamage + 1);

                    target.RemainingHp -= System.Math.Max(damage, 1);
                    Console.WriteLine("{0} [{1}] attacked {2} [{3}] with {4} for {5} damage!", e.Character.Name, e.RemainingHp, target.Character.Name, target.RemainingHp, ability.Name, damage);
                }
                else
                {
                    throw new NotImplementedException();
                }
                
            }

            e.ActionBar = 0;
        }

        private CombatEntity GetTarget(CombatEntity source, TargetType targetType)
        {
            if(targetType == TargetType.EnemyRandom)
            {
                if(source.Party == this.PlayerParty)
                {
                    return this.EnemyParty.CombatEntities.Where(x => x.IsAlive).ToList().GetRandom<CombatEntity>();
                }
                else
                {
                    return this.PlayerParty.CombatEntities.Where(x => x.IsAlive).ToList().GetRandom<CombatEntity>();
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public BattleResult Run()
        {

            while (this.IsBothSidesAlive())
            {
                this.FillActionBarsToNextTurn();
                this.ExecuteAction(this.GetNextActingEntity());
            }

            var result = new BattleResult()
            {
                PlayerCharacters = this.PlayerParty,
                EnemyCharacters = this.EnemyParty,
                Victory = this.PlayerParty.IsAlive
            };

            return result;
        }
    }
}
