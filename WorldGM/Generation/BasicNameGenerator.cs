using System;
using System.Linq;
using WorldGM.Entities;

namespace WorldGM.Generation
{
    public class BasicNameGenerator : INameGenerator
    {

        private Random Random;
        private IQueryable<Name> Names;
        private IQueryable<Name> FamilyNames;
        private IQueryable<Name> FeminineNames;
        private IQueryable<Name> MasculineNames;
        private IQueryable<Name> UnisexNames;

        public BasicNameGenerator(IQueryable<Name> names)
        {
            this.Random = new Random();
            this.Names = names;
            this.FamilyNames = this.Names.Where(x => x.IsFamilyName);
            this.FeminineNames = this.Names.Where(x => x.IsFeminine && x.IsGivenName);
            this.MasculineNames = this.Names.Where(x => x.IsMasculine && x.IsGivenName);
            this.UnisexNames = this.Names.Where(x => x.IsUnisex && x.IsGivenName);
        }

        public string NextFamilyName()
        {
            return this.FamilyNames.Skip(this.Random.Next(this.FamilyNames.Count())).FirstOrDefault().Value;
        }

        public string NextFeminineName()
        {
            return this.FeminineNames.Skip(this.Random.Next(this.FeminineNames.Count())).FirstOrDefault().Value;
        }

        public string NextMasculineName()
        {
            return this.MasculineNames.Skip(this.Random.Next(this.MasculineNames.Count())).FirstOrDefault().Value;
        }

        public string NextUnisexName()
        {
            return this.UnisexNames.Skip(this.Random.Next(this.UnisexNames.Count())).FirstOrDefault().Value;
        }
    }
}
