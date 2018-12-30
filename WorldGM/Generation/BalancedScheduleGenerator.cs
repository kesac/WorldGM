using System;
using System.Collections.Generic;
using System.Linq;
using WorldGM.Entities;

namespace WorldGM.Generation
{
    public class BalancedScheduleGenerator : IScheduleGenerator
    {
        private struct GeneratedResult
        {
            public bool Successful { get; set; }
            public List<ScheduledMatch> Sequence { get; set; }
        }

        public int Iterations { get; set; }
        public bool DebugInfo { get; set; }

        public BalancedScheduleGenerator()
        {
            this.Iterations = 100;
        }

        public Schedule GetSchedule(List<Team> teams)
        {
            var random = new Random();

            int successfulResults = 0;
            int chosenSequence = -1;
            var bestResult = this.GenerateCandidateSchedule(random, teams);

            if (bestResult.Successful)
            {
                successfulResults++;
            }

            for (int i = 0; i < Iterations; i++)
            {
                var result = this.GenerateCandidateSchedule(random, teams);

                if (result.Successful)
                {
                    if (this.DebugInfo)
                    {
                        Console.WriteLine("Generated sequence " + i + " which uses " + result.Sequence.Max(x => x.SeasonDay) + " days");
                    }

                    successfulResults++;

                    if (result.Sequence.Max(x => x.SeasonDay) < bestResult.Sequence.Max(x => x.SeasonDay))
                    {
                        bestResult = result;
                        chosenSequence = i;
                    }
                }
                else if (this.DebugInfo)
                {
                    Console.WriteLine("Threw out sequence " + i + " which uses " + result.Sequence.Max(x => x.SeasonDay) + " days");
                }
            }

            if (this.DebugInfo)
            {
                Console.WriteLine("Chose schedule sequence " + chosenSequence + " which uses " + bestResult.Sequence.Max(x => x.SeasonDay) + " days");
                Console.WriteLine("Generated a total of " + (this.Iterations + 1) + " sequences with " + successfulResults + " meeting the desired threshold");
            }

            return new Schedule() { ScheduledMatches = bestResult.Sequence};
        }

        // This method uses randomness to generate a schedule, but it
        // is guaranteed to stop because it limits the number of attempts. If the limit
        // is reached, it uses a contingency algorithm to fill out the rest of the schedule.
        // The GeneratedResult Success flag is only true if the contingency did not have to
        // be executed.
        private GeneratedResult GenerateCandidateSchedule(Random random, List<Team> teams)
        {
            var orderedMatches = new List<ScheduledMatch>();

            // Step 1: Generate all possible matches
            var allMatches = new List<ScheduledMatch>();

            for (int i = 0; i < teams.Count - 1; i++)
            {
                for (int j = i + 1; j < teams.Count; j++)
                {
                    allMatches.Add(new ScheduledMatch(teams[i], teams[j]));
                    allMatches.Add(new ScheduledMatch(teams[j], teams[i]));
                }
            }

            // Step 2: Shuffle the matches (naively)
            allMatches = allMatches.OrderByDescending(x => random.Next(allMatches.Count)).ToList();

            // Step 3: Apply the following scheduling algorithm:
            // Choose X random matches from all possible matches and assign them to day Y (starting at 1).
            // Check the selection against constraints.
            // If the constraints are upheld, move to the next day Y+1.
            // If the constraints are violated, simply repeat the selection process.
            // If the constraints are violated too many times, reduce the number of matches to X-1 and repeat the selection process.
            // If the number of matches cannot be reduced any further, move to the next day Y+1.

            int maximumDays = teams.Count * (teams.Count - 1);
            int defaultMatchesPerDay = teams.Count / 2;
            int matchesPerDay = defaultMatchesPerDay;
            int totalAttemptsToFillDay = 0;
            bool usedContingencyPlan = false;
            int currentDay = 1;

            while (allMatches.Count > 0)
            {
                // Grab X matches randomly, where half the number of total teams or less
                var group = new List<ScheduledMatch>();
                for (int i = 0; i < matchesPerDay; i++)
                {
                    group.Add(allMatches[random.Next(allMatches.Count)]);
                }

                // Constraint: Selected teams are not allowed more than once per day.
                // Constraint: Selected teams are not allowed to play 3 consecutive days.
                // Constraint: Selected teams are not allowed to play if doing so causes an imbalance in the number of games played across all teams.
                //             In order words, at any given point in a season, the difference between the team with the highest games played and team with
                //             lowest games played must be minimal (2 games at most)
                if (group.HasDuplicateTeams()
                    || group.PlayedConsecutiveDays(orderedMatches, currentDay)
                    || group.ExceedsGamePlayedDifferentialLimit(orderedMatches, teams.Count, 3))
                {
                    totalAttemptsToFillDay++;
                }
                else
                {
                    // The selection of matches meets all constraints
                    // and can be added to the schedule.
                    foreach (var item in group)
                    {
                        allMatches.Remove(item);
                        orderedMatches.Add(item);
                        item.SeasonDay = currentDay;
                    }

                    currentDay++;
                    totalAttemptsToFillDay = 0;
                    matchesPerDay = defaultMatchesPerDay;
                }

                // Attempts to form matches for the day is random
                // If too many failed attempts are made, we relax the groupSizeLimit
                // slightly.
                if (totalAttemptsToFillDay > 25)
                {
                    matchesPerDay--;
                    totalAttemptsToFillDay = 0;

                    if (matchesPerDay == 0)
                    {
                        // If the groupSizeLimit cannot be relaxed any further
                        // just advanced to the next day.
                        currentDay++;
                        totalAttemptsToFillDay = 0;
                        matchesPerDay = defaultMatchesPerDay;
                    }
                }

                if (currentDay >= maximumDays)
                {
                    // We've likely arrived in a state where no matter how
                    // far we advance the day, the constraints cannot be fulfilled.
                    break;
                }
            }

            // The contingency plan is to allow all remaining matches
            // to take place on single days.
            if (allMatches.Count > 0)
            {
                foreach (var remainingMatch in allMatches)
                {
                    orderedMatches.Add(remainingMatch);
                    remainingMatch.SeasonDay = currentDay;
                    currentDay++;
                }
                allMatches.Clear();
                usedContingencyPlan = true;
            }

            return new GeneratedResult() { Sequence = orderedMatches, Successful = !usedContingencyPlan };
        }

    }

    public static class ScheduleGeneratorHelper
    {
        public static IEnumerable<Team> GetTeams(this IEnumerable<ScheduledMatch> matches)
        {
            return matches
                .Select(x => x.HomeTeam)
                .Concat(matches.Select(x => x.AwayTeam));
        }

        public static bool HasDuplicateTeams(this IEnumerable<ScheduledMatch> matches)
        {
            return matches
                .Select(x => x.HomeTeam)
                .Concat(matches.Select(x => x.AwayTeam))
                .GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .FirstOrDefault() != null;
        }

        public static IEnumerable<Team> GetDuplicateTeams(this IEnumerable<Team> teams)
        {
            return teams
                .GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key);
        }

        public static bool PlayedConsecutiveDays(this IEnumerable<ScheduledMatch> selectedMatches, IEnumerable<ScheduledMatch> orderedMatches, int currentDay)
        {
            if (currentDay < 3) // first possible day is '1'
            {
                return false;
            }

            var today = selectedMatches.GetTeams().Distinct();
            var yesterday = orderedMatches.Where(x => x.SeasonDay == currentDay - 1).GetTeams().Distinct();
            var yesteryesterday = orderedMatches.Where(x => x.SeasonDay == currentDay - 2).GetTeams().Distinct();

            var dupes1 = today.Concat(yesterday).GetDuplicateTeams();
            var dupes2 = dupes1.Concat(yesteryesterday).GetDuplicateTeams();

            return dupes2.Count() > 0;
        }

        public static int GetMinimumGamesPlayed(this IEnumerable<ScheduledMatch> orderedMatches, int teamCount)
        {

            var result = orderedMatches.GetTeams().GroupBy(x => x);

            if (result.Count() < teamCount)
            {
                return 0;
            }
            else
            {
                return result.Min(x => x.Count());
            }
        }

        public static bool ExceedsGamePlayedDifferentialLimit(this IEnumerable<ScheduledMatch> selectedMatches, IEnumerable<ScheduledMatch> orderedMatches, int teamCount, int gamePlayedDifferentialLimit)
        {
            var min = orderedMatches.GetMinimumGamesPlayed(teamCount);
            var selectedTeams = selectedMatches.GetTeams();

            foreach (var team in selectedTeams)
            {
                var gp = orderedMatches.GetTeams().Where(x => x == team).Count();

                if (gp - min >= gamePlayedDifferentialLimit)
                {
                    return true;
                }
            }

            return false;
        }
    }

}
