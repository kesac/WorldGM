using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldGM.Web.v1.ViewModels
{
    public class TeamListItemViewModel
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Name { get; set; }

        public TeamListItemViewModel(Team team)
        {
            this.Id = team.Id;
            this.City = team.City.Name;
            this.Name = team.Name;
        }

    }
}
