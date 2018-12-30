using System;
using System.Collections.Generic;
using System.Linq;
using WorldGM.Entities;

namespace WorldGM.Generation
{
    public class BasicScheduleGenerator : IScheduleGenerator
    {
        public Schedule GetSchedule(List<Team> teams)
        {
            // Generate all possible matches if every team plays
            // each other twice, once home and once away

            var matches = new List<ScheduledMatch>();

            for (int i = 0; i < teams.Count - 1; i++)
            {
                for (int j = i + 1; j < teams.Count; j++)
                {
                    matches.Add(new ScheduledMatch(teams[i], teams[j]));
                    matches.Add(new ScheduledMatch(teams[j], teams[i]));
                }
            }

            // Naive shuffle, not guaranteed that teams will have roughly the same games played at any given day in the season
            var random = new Random();
            matches = matches.OrderByDescending(x => random.Next(matches.Count)).ToList();

            // Naive assignment of match day. Later, multiple matches should occur on same day as long as a team doesn't play twice
            for(int i = 0; i < matches.Count; i++)
            {
                matches[i].SeasonDay = i;
            }

            var schedule = new Schedule();
            schedule.ScheduledMatches = matches;
            return schedule;
        }
    }
}
