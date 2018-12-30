using WorldGM.Entities;

namespace WorldGM.Web.v1.ViewModels
{
    public class AthleteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public int OverallRating { get; set; }

        public bool HasContract { get; set; }
        public int? TeamId { get; set; }
        public string TeamName { get; set; }
        public int? ContractFirstYear { get; set; }
        public int? ContractLastYear { get; set; }
        public int? AnnualPay { get; set; }
        

        public AthleteViewModel(Athlete athlete)
        {
            this.Id = athlete.Id;
            this.Name = athlete.FirstName + " " + athlete.FamilyName;
            this.Age = athlete.Age;
            this.OverallRating = 50;

            if(athlete.TeamContract != null)
            {
                this.HasContract = true;
                this.ContractFirstYear = athlete.TeamContract.FirstYear;
                this.ContractLastYear = athlete.TeamContract.LastYear;
                this.AnnualPay = athlete.TeamContract.AnnualPay;
                this.TeamId = athlete.TeamContract.Team.Id;
                this.TeamName = athlete.TeamContract.Team.City.Name + " " + athlete.TeamContract.Team.Name;
            }
            else
            {
                this.TeamName = "Free Agent";
            }

            if(athlete.Position == AthletePosition.Forward)
            {
                this.Position = "ATK";
            }
            else if (athlete.Position == AthletePosition.Midfield)
            {
                this.Position = "MID";
            }
            else if (athlete.Position == AthletePosition.Defense)
            {
                this.Position = "DEF";
            }
            else if (athlete.Position == AthletePosition.Goalkeeper)
            {
                this.Position = "GK";
            }
        }
    }
}
