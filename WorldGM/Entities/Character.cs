using System.Runtime.Serialization;

namespace WorldGM.Entities
{
    public class Character : IEntity
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
        public int Strength { get; set; }      // Atk and Def
        public int Intelligence { get; set; }  // M.Atk and M.Def
        public int Dexterity { get; set; }     // Ability to apply debuffs
        public int Resistance { get; set; }    // Resistance to debuffs
        public int Speed { get; set; }         // Turn frequency
        public int Endurance { get; set; }     // HP

    }
}
