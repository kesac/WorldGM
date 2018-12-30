using System;
using System.Collections.Generic;
using WorldGM.Entities;

namespace WorldGM.Web.v1.ViewModels
{
    /**
     * If you only want to get a list of teams, use TeamViewModel instead. This viewmodel will return
     * teams, cities, team contracts, and athletes!
     */
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public List<AthleteViewModel> Athletes { get; set; }
        
        public TeamViewModel(Team team)
        {
            this.Id = team.Id;
            this.City = team.City.Name;
            this.Name = team.Name;

            this.Athletes = new List<AthleteViewModel>();

            if(team.TeamContracts != null)
            {
                foreach (var contract in team.TeamContracts)
                {
                    this.Athletes.Add(new AthleteViewModel(contract.Athlete));
                }
            }

        }
    }
}
