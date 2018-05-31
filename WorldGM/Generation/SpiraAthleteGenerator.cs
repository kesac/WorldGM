using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM.Generation
{
    public class SpiraAthleteGenerator : IAthleteGenerator
    {
        private static int DefaultCompetitionMinimum = 16;
        private static int DefaultCompetitionMaximum = 44;
        private static int DefaultPrimeStartMinimum = 28;
        private static int DefaultPrimeStartMaximum = 32;
        private static int DefaultPrimeEndMinimum = 30;
        private static int DefaultPrimeEndMaximum = 34;

        private IProfileGenerator ProfileGenerator;
        private IAttributesGenerator AttributesGenerator;
        private AttributesUpdater AttributesUpdater;
        private Random Random;

        public int PrimeStartMinimum { get; set; }
        public int PrimeStartMaximum { get; set; }
        public int PrimeEndMinimum { get; set; }
        public int PrimeEndMaximum { get; set; }

        public SpiraAthleteGenerator(IProfileGenerator profileGenerator, IAttributesGenerator attributesGenerator)
        {
            this.ProfileGenerator = profileGenerator;
            this.AttributesGenerator = attributesGenerator;

            this.AttributesUpdater = new AttributesUpdater(DefaultCompetitionMinimum, DefaultCompetitionMaximum);
            this.Random = new Random();

            this.PrimeStartMaximum = DefaultPrimeStartMaximum;
            this.PrimeStartMinimum = DefaultPrimeStartMinimum;
            this.PrimeEndMaximum = DefaultPrimeEndMaximum;
            this.PrimeEndMinimum = DefaultPrimeEndMinimum;
        }

        private Athlete GenerateRandomAthlete()
        {
            Athlete athlete = new Athlete(this.ProfileGenerator.NextProfile(), this.AttributesGenerator.NextAttributes());

            // Define the range of this athlete's prime
            athlete.PrimeStartAge = this.Random.Next(PrimeStartMinimum, PrimeStartMaximum + 1);
            athlete.PrimeEndAge = this.Random.Next(PrimeEndMinimum, PrimeEndMaximum + 1);

            if (athlete.PrimeEndAge <= athlete.PrimeStartAge)
            {
                athlete.PrimeEndAge = athlete.PrimeStartAge + 1;
            }

            // Define the position, based on potentials and intelligences

            int oi = athlete.Max(AttributeType.OffensiveIntelligence);
            int pi = athlete.Max(AttributeType.PlaymakingIntelligence);
            int di = athlete.Max(AttributeType.DefensiveIntelligence);
            double gk = this.Random.NextDouble();


            if (gk < 0.075)
            {
                athlete.Position = Position.Goalkeeper;
            }
            else if (oi > pi && oi > di) // Offense is greatest
            {
                athlete.Position = Position.Forward;
            }
            else if (pi > oi && pi > di) // Playmaking is greatest
            {
                athlete.Position = Position.Midfield;
            }
            else if (di > oi && di > pi) // Defense is greatest
            {
                athlete.Position = Position.Defense;
            }
            else
            {
                athlete.Position = Position.Midfield;
            }

            return athlete;
        }

        private void ShapeAttributes(Athlete athlete)
        {
            if (athlete.Position == Position.Goalkeeper)
            {
                Attribute cat = athlete.Attribute(AttributeType.Catch);
                cat.Maximum = 70 + this.Random.Next(30);

                athlete.Attribute(AttributeType.Shot).Maximum = 0;
                athlete.Attribute(AttributeType.OffensiveIntelligence).Maximum = 0;
            }
            if (athlete.Position == Position.Forward)
            {
                Attribute shot = athlete.Attribute(AttributeType.Shot);
                if (shot.Maximum < 70)
                {
                    shot.Maximum = 70 + this.Random.Next(30);
                }

                Attribute pass = athlete.Attribute(AttributeType.Pass);
                if (pass.Maximum > 70)
                {
                    pass.Maximum = 70;
                }

                Attribute block = athlete.Attribute(AttributeType.Block);
                if (block.Maximum > 70)
                {
                    block.Maximum = 70 - this.Random.Next(10);
                }
            }
            if (athlete.Position == Position.Midfield)
            {

                Attribute shot = athlete.Attribute(AttributeType.Shot);
                if (shot.Maximum > 70)
                {
                    shot.Maximum = 70 - this.Random.Next(5);
                }

                Attribute pass = athlete.Attribute(AttributeType.Pass);
                if (pass.Maximum < 70)
                {
                    pass.Maximum = 70 + this.Random.Next(30);
                }

                Attribute block = athlete.Attribute(AttributeType.Block);
                if (block.Maximum > 70)
                {
                    block.Maximum = 70 - this.Random.Next(5);
                }
            }
            if (athlete.Position == Position.Defense)
            {

                Attribute shot = athlete.Attribute(AttributeType.Shot);
                if (shot.Maximum > 70)
                {
                    shot.Maximum = 70 - this.Random.Next(10);
                }

                Attribute pass = athlete.Attribute(AttributeType.Pass);
                if (pass.Maximum > 70)
                {
                    pass.Maximum = 70;
                }

                Attribute block = athlete.Attribute(AttributeType.Block);
                if (block.Maximum < 70)
                {
                    block.Maximum = 70 + this.Random.Next(30);
                }
            }
            if (athlete.Position != Position.Goalkeeper)
            {
                Attribute cat = athlete.Attribute(AttributeType.Catch);
                cat.Minimum = 0;
                cat.Maximum = 0;
            }

            this.AttributesUpdater.Update(athlete);
        }

        public Athlete NextAthlete()
        {
            Athlete athlete = this.GenerateRandomAthlete();
            this.ShapeAttributes(athlete);
            return athlete;
        }

        /// <summary>
        /// Given a set of three attributes, ensures the primary attribute has the highest maximum value.
        /// </summary>
        private void PrioritizePrimaryAttribute(Attribute primary, Attribute secondary, Attribute tertiary)
        {
            int primaryMax = primary.Maximum;

            if (secondary.Maximum > primary.Maximum && secondary.Maximum > tertiary.Maximum)
            {
                primary.Maximum = secondary.Maximum;
                secondary.Maximum = primaryMax;
            }
            else if(tertiary.Maximum > primary.Maximum)
            {
                primary.Maximum = tertiary.Maximum;
                tertiary.Maximum = primaryMax;
            }
        }

        public Athlete NextAthlete(Position position)
        {
            Athlete athlete = this.GenerateRandomAthlete();

            if(athlete.Position != position)
            {
                // If we're looking for a goalie, just set the position
                // and attribute shaping will take care of the rest
                if(position == Position.Goalkeeper)
                {
                    athlete.Position = Position.Goalkeeper;
                }
                // If it's a field position, swap the highest intelligence value to the position-focused one
                if(position == Position.Forward)
                {
                    athlete.Position = Position.Forward;
                    this.PrioritizePrimaryAttribute(athlete.Attribute(AttributeType.OffensiveIntelligence), athlete.Attribute(AttributeType.PlaymakingIntelligence), athlete.Attribute(AttributeType.DefensiveIntelligence));
                }
                if (position == Position.Midfield)
                {
                    athlete.Position = Position.Midfield;
                    this.PrioritizePrimaryAttribute(athlete.Attribute(AttributeType.PlaymakingIntelligence), athlete.Attribute(AttributeType.OffensiveIntelligence), athlete.Attribute(AttributeType.DefensiveIntelligence));
                }
                if (position == Position.Defense)
                {
                    athlete.Position = Position.Defense;
                    this.PrioritizePrimaryAttribute(athlete.Attribute(AttributeType.DefensiveIntelligence), athlete.Attribute(AttributeType.OffensiveIntelligence), athlete.Attribute(AttributeType.PlaymakingIntelligence));
                }

            }

            this.ShapeAttributes(athlete);
            return athlete;
        }
    }
}
