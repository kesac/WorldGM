using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM.Generation
{
    public class SpiraProfileGenerator : IProfileGenerator
    {

        private static readonly int DefaultMinimumAge = 16;
        private static readonly int DefaultMaximumAge = 44;

        private Random Random;
        private INameGenerator NameGenerator;
        private IStringGenerator RegionGenerator;
        private IBiographyGenerator BiographyGenerator;

        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }

        public SpiraProfileGenerator(INameGenerator nameGenerator, IStringGenerator regionGenerator, IBiographyGenerator biographyGenerator)
        {
            this.NameGenerator = nameGenerator;
            this.RegionGenerator = regionGenerator;
            this.BiographyGenerator = biographyGenerator;

            this.Random = new Random();
            this.MinimumAge = DefaultMinimumAge;
            this.MaximumAge = DefaultMaximumAge;
        }

        public Profile NextProfile()
        {
            Profile p = new Profile();

            p.Age = this.MinimumAge + this.Random.Next(this.MaximumAge - this.MinimumAge + 1);
            p.Gender = this.Random.NextDouble() < 0.5 ? Gender.Female : Gender.Male;

            p.FamilyName = this.NameGenerator.NextFamilyName();

            if (p.Gender == Gender.Female)
            {
                p.Name = this.NameGenerator.NextFemaleName();
            }
            else if (p.Gender == Gender.Male)
            {
                p.Name = this.NameGenerator.NextMaleName();
            }

            p.Home = this.RegionGenerator.NextString();

            p.Biography = this.BiographyGenerator.NextBiography(p.FamilyName, p.Gender, p.Age);

            return p;
        }
    }
}
