using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM.Generation
{
    /// <summary>
    /// Primarily for EF integration
    /// </summary>
    public class ListBasedNameGenerator : INameGenerator
    {

        private IStringGenerator MaleNames;
        private IStringGenerator FemaleNames;
        private IStringGenerator FamilyNames;

        public ListBasedNameGenerator(IEnumerable<String> maleNames, IEnumerable<String> femaleNames, IEnumerable<String> familyNames) :
            this(maleNames, femaleNames, familyNames, new Random()) { }

        public ListBasedNameGenerator(IEnumerable<String> maleNames, IEnumerable<String> femaleNames, IEnumerable<String> familyNames, Random r)
        {
            this.MaleNames = new ListBasedStringGenerator(maleNames, r);
            this.FemaleNames = new ListBasedStringGenerator(femaleNames, r);
            this.FamilyNames = new ListBasedStringGenerator(familyNames, r);
        }

        /// <summary>
        /// Returns a random name that is suitable as the family name of a person.
        /// </summary>
        public string NextFamilyName()
        {
            return this.FamilyNames.NextString();
        }

        /// <summary>
        /// Returns a random name that is suitable as the first name of a girl.
        /// </summary>
        public string NextFemaleName()
        {
            return this.FemaleNames.NextString();
        }

        /// <summary>
        /// Returns a random name that is suitable as the first name of a boy.
        /// </summary>
        public string NextMaleName()
        {
            return this.MaleNames.NextString();
        }

    }
}
