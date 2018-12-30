
namespace WorldGM.Entities
{
    public class Name
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public bool IsFamilyName { get; set; }
        public bool IsGivenName { get; set; }
        public bool IsMasculine { get; set; }
        public bool IsFeminine { get; set; }
        public bool IsUnisex { get; set; }

        public bool IsRegional { get; set; }
        public int? RegionId { get; set; }

        public bool IsUniqueToCity { get; set; }
        public int? CityId { get; set; }
    }
}
