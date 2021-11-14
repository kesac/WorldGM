
namespace WorldGM.Entities
{

    public enum GuildMembershipType
    {
        Unknown = 0,
        Temporary = 1,
        Permanent = 2
    }

    public enum CharacterAppearance
    {
        Unknown = 0,
        Neutral = 1,
        Feminine = 2,
        Masculine = 3
    }

    public enum CharacterElement
    {
        Unknown = 0,
        Spirit = 1,   // Strong vs elementals
        Water = 2,    // Strong vs fire
        Fire = 3,     // Strong vs earth
        Earth = 4,    // Strong vs storm
        Storm = 5,    // ie. Lightning/Sky; strong vs water
        Light = 6,    // Strong vs spirit and darkness
        Darkness = 7  // Strong vs spirit and light
    }

}
