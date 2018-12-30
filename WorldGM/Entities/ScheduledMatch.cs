using System.Runtime.Serialization;

namespace WorldGM.Entities
{
    public class ScheduledMatch
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int SeasonDay { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        [IgnoreDataMember]
        public virtual Schedule Schedule { get; set; }
        [IgnoreDataMember]
        public virtual Team HomeTeam { get; set; }
        [IgnoreDataMember]
        public virtual Team AwayTeam { get; set; }

        public ScheduledMatch() { /* An empty constructor needs to exist for ORMs */ }

        public ScheduledMatch(Team home, Team away)
        {
            this.HomeTeam = home;
            this.AwayTeam = away;
        }
    }
}
