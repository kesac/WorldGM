using System.Collections.Generic;
using WorldGM.Entities;

namespace WorldGM.Generation
{
    public interface IScheduleGenerator
    {
        Schedule GetSchedule(List<Team> teams);
    }
}
