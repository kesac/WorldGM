using WorldGM.Entities;

namespace WorldGM.Web.v1.ViewModels
{
    public class ScheduledMatchViewModel
    {
        public int Id { get; set; }
        public int SeasonDay { get; set; }
        public int HomeTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public int AwayTeamId { get; set; }
        public string AwayTeamName { get; set; }

        public ScheduledMatchViewModel(ScheduledMatch match)
        {
            this.Id = match.Id;
            this.SeasonDay = match.SeasonDay;
            this.HomeTeamId = match.HomeTeamId;
            this.AwayTeamId = match.AwayTeamId;

            if(match.HomeTeam != null)
            {
                this.HomeTeamName = match.HomeTeam.Name;
            }

            if (match.AwayTeam!= null)
            {
                this.AwayTeamName = match.AwayTeam.Name;
            }
        }
    }
}
