using System;
using System.Collections.Generic;
using System.Text;

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
            var result = new Athlete()
            {
                FamilyName = this.NameGenerator.NextFamilyName(),
                Age = this.Random.Between(18, 40)
            };

            var positionRoll = this.Random.NextDouble();

            if(positionRoll < 0.30)
            {
                result.Position = AthletePosition.Forward;
            }
            else if(positionRoll < 0.6)
            {
                result.Position = AthletePosition.Midfield;
            }
            else if (positionRoll < 0.9)
            {
                result.Position = AthletePosition.Defense;
            }
            else
            {
                result.Position = AthletePosition.Goalkeeper;
            }

            var genderRoll = this.Random.NextDouble();

            if (genderRoll < 0.45)
            {
                result.Gender = AthleteGender.Female;

                if(this.Random.NextDouble() < 0.9)
                {
                    result.FirstName = this.NameGenerator.NextFeminineName();
                }
                else
                {
                    result.FirstName = this.NameGenerator.NextUnisexName();
                }
                
            }
            else if (genderRoll < 0.90)
            {
                result.Gender = AthleteGender.Male;

                if (this.Random.NextDouble() < 0.9)
                {
                    result.FirstName = this.NameGenerator.NextMasculineName();
                }
                else
                {
                    result.FirstName = this.NameGenerator.NextUnisexName();
                }
                
            }
            else
            {
                result.Gender = AthleteGender.Other;
                var nameRoll = this.Random.NextDouble();
                if (nameRoll < 0.33)
                {
                    result.FirstName = this.NameGenerator.NextFeminineName();
                }
                else if(nameRoll < 0.66)
                {
                    result.FirstName = this.NameGenerator.NextMasculineName();
                }
                else
                {
                    result.FirstName = this.NameGenerator.NextUnisexName();
                }
            }

            return result;
        }

        public Athlete NextAthlete(AthletePosition position)
        {
            throw new NotImplementedException();
        }
    }
}
