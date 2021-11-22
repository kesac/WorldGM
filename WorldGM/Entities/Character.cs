using Loremaker;
using System;
using System.Runtime.Serialization;

namespace WorldGM.Entities
{
    public class Character : Identifiable
    {
        public uint Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }

        [IgnoreDataMember]
        public string Name => FirstName + " " + FamilyName;
        public CharacterAppearance Appearance { get; set; }
        public CharacterElement Element { get; set; }
        [IgnoreDataMember]
        public virtual GuildMembership Membership { get; set; }

        public int Level { get; set; }
        public int Strength { get; set; }      // P.Atk and P.Def
        public int Intelligence { get; set; }  // M.Atk and M.Def
        public int Dexterity { get; set; }     // Ability to apply debuffs
        public int Resistance { get; set; }    // Resistance to debuffs
        public int Speed { get; set; }         // Turn frequency
        public int Vitality { get; set; }     // HP

        public Ability PrimaryAbility { get; set; }

        public int Get(CharacterAttribute attribute)
        {
            if(attribute == CharacterAttribute.Level)
            {
                return this.Level;
            }
            else if (attribute == CharacterAttribute.Strength)
            {
                return this.Strength;
            }
            else if (attribute == CharacterAttribute.Intelligence)
            {
                return this.Intelligence;
            }
            else if (attribute == CharacterAttribute.Dexterity)
            {
                return this.Dexterity;
            }
            else if (attribute == CharacterAttribute.Resistance)
            {
                return this.Resistance;
            }
            else if (attribute == CharacterAttribute.Speed)
            {
                return this.Speed;
            }
            else if (attribute == CharacterAttribute.Vitality)
            {
                return this.Vitality;
            }
            else
            {
                throw new InvalidOperationException("Tried to access an attribute that is not used by characters.");
            }
        }

    }
}
