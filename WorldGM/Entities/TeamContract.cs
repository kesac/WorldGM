using System.Runtime.Serialization;

namespace WorldGM.Entities
{
    public class TeamContract
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int AthleteId { get; set; }

        [IgnoreDataMember]
        public virtual Team Team { get; set; }
        [IgnoreDataMember]
        public virtual Athlete Athlete { get; set; }

        // Guesses
        public int FirstYear { get; set; }
        public int LastYear { get; set; }
        public int AnnualPay { get; set; }
    }
}
