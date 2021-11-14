using Loremaker;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WorldGM.Entities
{
    public class Schedule : Identifiable
    {
        public uint Id { get; set; }

        [IgnoreDataMember]
        public List<ScheduledMatch> ScheduledMatches { get; set; }
    }

    public class ScheduledMatch : Identifiable
    {
        public uint Id { get; set; }
        public int ScheduleId { get; set; }
        public int SeasonDay { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        [IgnoreDataMember]
        public virtual Schedule Schedule { get; set; }
        [IgnoreDataMember]
        public virtual Guild HomeGuild { get; set; }
        [IgnoreDataMember]
        public virtual Guild AwayGuild { get; set; }

        public ScheduledMatch() { /* An empty constructor needs to exist for ORMs */ }

        public ScheduledMatch(Guild home, Guild away)
        {
            this.HomeGuild = home;
            this.AwayGuild = away;
        }
    }
}
