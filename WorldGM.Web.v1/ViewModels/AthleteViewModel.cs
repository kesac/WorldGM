using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldGM.Web.v1.ViewModels
{
    public class AthleteViewModel
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public int OverallRating { get; set; }

        public AthleteViewModel(Athlete athlete)
        {
            this.Name = athlete.FirstName + " " + athlete.FamilyName;
            this.Age = athlete.Age;
            this.OverallRating = 50;

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
