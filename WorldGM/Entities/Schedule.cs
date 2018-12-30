using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WorldGM.Entities
{
    public class Schedule
    {
        public int Id { get; set; }

        [IgnoreDataMember]
        public List<ScheduledMatch> ScheduledMatches { get; set; }
    }
}
