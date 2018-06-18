using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM
{
    public class Team
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }

        [IgnoreDataMember]
        public virtual City City { get; set; }
        [IgnoreDataMember]
        public virtual List<TeamContract> TeamContracts { get; set; }


        // Optionals
        public int? CaptainAthleteId { get; set; }
        public int? AssistantCaptainAthleteId { get; set; }

    }
}
