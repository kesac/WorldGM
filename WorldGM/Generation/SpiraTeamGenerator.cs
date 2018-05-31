using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM.Generation
{
    public class SpiraTeamGenerator : ITeamGenerator
    {
        private IAthleteGenerator AthleteGenerator;
        private IStringGenerator TeamNameGenerator;
        private string TeamName;
        private AttributesUpdater AttributesUpdater;

        public SpiraTeamGenerator(IAthleteGenerator athleteGenerator, string teamName)
        {
            this.AthleteGenerator = athleteGenerator;
            this.TeamName = teamName;
            this.AttributesUpdater = new AttributesUpdater(16, 44);
        }

        public SpiraTeamGenerator(IAthleteGenerator athleteGenerator, IStringGenerator teamNameGenerator)
        {
            this.AthleteGenerator = athleteGenerator;
            this.TeamNameGenerator = teamNameGenerator;
            this.AttributesUpdater = new AttributesUpdater(16,44);
        }

        public Team NextTeam()
        {
            Team t = new Team();
            if (this.TeamNameGenerator != null)
            {
                t.Name = this.TeamNameGenerator.NextString();
            }
            else
            {
                t.Name = this.TeamName;
            }
            
            t.Athletes.Add(AthleteGenerator.NextAthlete(Position.Forward));
            t.Athletes.Add(AthleteGenerator.NextAthlete(Position.Forward));
            t.Athletes.Add(AthleteGenerator.NextAthlete(Position.Forward));
            t.Athletes.Add(AthleteGenerator.NextAthlete(Position.Midfield));
            t.Athletes.Add(AthleteGenerator.NextAthlete(Position.Midfield));
            t.Athletes.Add(AthleteGenerator.NextAthlete(Position.Midfield));
            t.Athletes.Add(AthleteGenerator.NextAthlete(Position.Defense));
            t.Athletes.Add(AthleteGenerator.NextAthlete(Position.Defense));
            t.Athletes.Add(AthleteGenerator.NextAthlete(Position.Defense));
            t.Athletes.Add(AthleteGenerator.NextAthlete(Position.Goalkeeper));

            foreach(Athlete a in t.Athletes)
            {
                a.Team = t;
            }

            return t;
        }
    }
}
