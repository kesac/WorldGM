using Loremaker;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGM.Entities
{

    public class Ability : Identifiable
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public List<AbilityAction> Actions { get; set; }

        public Ability()
        {
            this.Actions = new List<AbilityAction>();
        }

        public static readonly Ability DefaultAbility = new Ability()
        {
            Id = 0,
            Name = "Single Strike",
            Actions = new List<AbilityAction>()
            {
                new AbilityAction()
                {
                    Type = AbilityActionType.PhysicalAttack,
                    TargetType = TargetType.EnemyRandom,
                    // AttributeSource = CharacterAttribute.Strength,
                    MinMultiplier = 1.00f,
                    MaxMultiplier = 1.20f
                }
            }
        };
    }
}
