
namespace WorldGM.Entities
{
    // TODO: Ensure serialization of these enums does
    // not take into account the order they are defined
    public static class Constants
    {
        public static readonly int VitalityToHpMultiplier = 10;
    }

    public enum GuildMembershipType
    {
        None,
        Temporary,
        Permanent
    }

    public enum CharacterAttribute
    {
        None,
        Level,
        Strength,
        Intelligence,
        Dexterity,
        Resistance,
        Speed,
        Vitality
    }

    public enum CharacterAppearance
    {
        None,
        Neutral,
        Feminine,
        Masculine
    }

    public enum CharacterElement
    {
        None,
        Spirit,   // Strong vs elementals
        Water,    // Strong vs fire
        Fire,     // Strong vs earth
        Earth,    // Strong vs storm
        Storm,    // ie. Lightning/Sky; strong vs water
        Light,    // Strong vs spirit and darkness
        Darkness  // Strong vs spirit and light
    }

    public enum AbilityActionType
    {
        None,
        PhysicalAttack,
        MagicAttack,
        Heal,
        Buff,
        Debuff
    }

    public enum TargetType
    {
        None,
        Self,
        AllyAll,
        AllyRandom,
        AllyLowestHealth,
        EnemyAll,
        EnemyRandom,
        EnemyLowestHealth
    }

}
