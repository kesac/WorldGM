namespace WorldGM.Entities
{
    public class AbilityAction
    {
        public AbilityActionType Type { get; set; }
        public TargetType TargetType { get; set; }
        // public CharacterAttribute AttributeSource { get; set; }
        public float MinMultiplier { get; set; }
        public float MaxMultiplier { get; set; }
    }
}
