using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WorldGM
{
    public class Schedule
    {
        public int Id { get; set; }

        [IgnoreDataMember]
        public List<ScheduledMatch> ScheduledMatches { get; set; }
    }
}
