using System;
using WorldGM.Entities;
using WorldGM.Generation;
using WorldGM.Simulation;

namespace WorldGM.Experiments.Battle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var range = new NumberGenerator(15, 35);

            var g = new CharacterGenerator()
                        .ForProperty<int>(x => x.Strength, range)
                        .ForProperty<int>(x => x.Vitality, range)
                        .ForProperty<int>(x => x.Speed, range);

            var party1 = new Party();
            party1.Add(g.Next());
            party1.Add(g.Next());

            var party2 = new Party();
            party2.Add(g.Next());
            party2.Add(g.Next());

            var battle = new ExperimentalBattle(party1, party2);

            foreach (var e in battle.PlayerParty.CombatEntities)
            {
                Console.WriteLine("{0} [HP:{1} STR:{2} SPD:{3}]", e.Character.Name, e.RemainingHp, e.Character.Strength, e.Character.Speed);
            }

            Console.WriteLine("  VS  ");

            foreach (var e in battle.EnemyParty.CombatEntities)
            {
                Console.WriteLine("{0} [HP:{1} STR:{2} SPD:{3}]", e.Character.Name, e.RemainingHp, e.Character.Strength, e.Character.Speed);
            }

            Console.WriteLine("-------------");

            var result = battle.Run();

            if (result.Victory)
            {
                Console.WriteLine("Player party victory");
            }
            else
            {
                Console.WriteLine("Player party defeat");
            }

        }
    }
}
