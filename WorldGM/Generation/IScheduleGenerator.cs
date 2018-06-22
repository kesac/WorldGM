using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGM.Generation
{
    public interface IScheduleGenerator
    {
        Schedule GetSchedule(List<Team> teams);
    }
}
