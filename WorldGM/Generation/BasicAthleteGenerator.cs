using System;
using System.Collections.Generic;
using System.Text;
using WorldGM.Math;

namespace WorldGM.Generation
{
    public class BasicAthleteGenerator : IAthleteGenerator
    {

        private Random Random;
        private INameGenerator NameGenerator;

        public BasicAthleteGenerator(INameGenerator nameGenerator)
        {
            this.Random = new Random();
            this.NameGenerator = nameGenerator;
        }

        public Athlete NextAthlete()
        {
            var athlete = new Athlete()
            {
                FamilyName = this.NameGenerator.NextFamilyName(),
                Age = this.Random.NormalizedBetween(18, 40, 26, 5),
                Weight = this.Random.NormalizedBetween(120, 230, 180, 20), // Needs normalized distribution later
                Height = this.Random.NormalizedBetween(155, 210, 174, 10)  // Needs normalized distribution later
            };

            var p = new WeightedCollection<AthletePosition>();
            p.Add(AthletePosition.Forward, 30);
            p.Add(AthletePosition.Midfield, 30);
            p.Add(AthletePosition.Defense, 30);
            p.Add(AthletePosition.Goalkeeper, 10);
            athlete.Position = p.Next();
            
            var g = new WeightedCollection<AthleteGender>();
            g.Add(AthleteGender.Female, 45);
            g.Add(AthleteGender.Male, 45);
            g.Add(AthleteGender.Other, 10);
            athlete.Gender = g.Next();
            
            if (athlete.Gender == AthleteGender.Female && this.Random.OnChance(0.9))
            {
                athlete.FirstName = this.NameGenerator.NextFeminineName();
            }
            else if (athlete.Gender == AthleteGender.Male && this.Random.OnChance(0.9))
            {
                athlete.FirstName = this.NameGenerator.NextMasculineName();
            }
            else
            {
                if (this.Random.OnChance(0.33))
                {
                    athlete.FirstName = this.NameGenerator.NextFeminineName();
                }
                else if(this.Random.OnChance(0.33))
                {
                    athlete.FirstName = this.NameGenerator.NextMasculineName();
                }
            }

            if(string.IsNullOrEmpty(athlete.FirstName))
            {
                athlete.FirstName = this.NameGenerator.NextUnisexName();
            }

            return athlete;
        }

        public Athlete NextAthlete(AthletePosition position)
        {
            throw new NotImplementedException();
        }
    }
}
