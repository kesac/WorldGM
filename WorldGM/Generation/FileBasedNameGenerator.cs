using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM.Generation
{
    public class FileBasedNameGenerator : INameGenerator
    {
        private IStringGenerator MaleNames;
        private IStringGenerator FemaleNames;
        private IStringGenerator FamilyNames;

        public FileBasedNameGenerator(string maleNameFilePath, string femaleNameFilePath, string familyNameNamePath) :
            this(maleNameFilePath, femaleNameFilePath, familyNameNamePath, new Random()) { }

        public FileBasedNameGenerator(string maleNameFilePath, string femaleNameFilePath, string familyNameNamePath, Random r)
        {
            this.MaleNames = new FileBasedStringGenerator(maleNameFilePath, r);
            this.FemaleNames = new FileBasedStringGenerator(femaleNameFilePath, r);
            this.FamilyNames = new FileBasedStringGenerator(familyNameNamePath, r);
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
