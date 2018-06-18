using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldGM.Web.v1.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Name { get; set; }

        public List<AthleteViewModel> Athletes { get; set; }
        /**
         * Expects City, TeamContracts, and TeamContracts.select(x => x.Athlete) to be included
         */
        public TeamViewModel(Team team)
        {
            this.Id = team.Id;
            this.City = team.City.Name;
            this.Name = team.Name;

            this.Athletes = new List<AthleteViewModel>();
            foreach(var contract in team.TeamContracts)
            {
                this.Athletes.Add(new AthleteViewModel(contract.Athlete));
            }
        }
    }
}
