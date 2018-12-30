using System;
using System.Collections.Generic;
using System.Linq;
using WorldGM.Entities;

namespace WorldGM.Web.v1.ViewModels
{
    public class ScheduleViewModel
    {
        public int ScheduledId { get; set; }
        public List<ScheduledMatchViewModel> ScheduledMatches { get; set; }

        public ScheduleViewModel(Schedule schedule)
        {
            this.ScheduledMatches = schedule.ScheduledMatches.Select(x => new ScheduledMatchViewModel(x)).ToList();
            this.ScheduledId = schedule.Id;
        }
    }
}
