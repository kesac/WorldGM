using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM.Generation
{
    /// <summary>
    /// Exposes methods for generating names of people.
    /// </summary>
    public interface INameGenerator
    {
        /// <summary>
        /// Returns a random name that is suitable as the first name of a boy.
        /// </summary>
        string NextMaleName();

        /// <summary>
        /// Returns a random name that is suitable as the first name of a girl.
        /// </summary>
        string NextFemaleName();

        /// <summary>
        /// Returns a random name that is suitable as the family name of a person.
        /// </summary>
        string NextFamilyName();

    }
}
