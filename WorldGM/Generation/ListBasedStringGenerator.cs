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
    public class ListBasedStringGenerator : IStringGenerator
    {
        private Random Random;
        private List<string> Strings;
        
        /// <summary>
        /// Initalizes this generator.
        /// </summary>
        /// <param name="path">The path to a flat text file containing strings (one per line).</param>
        public ListBasedStringGenerator(IEnumerable<String> strings) : this(strings, new Random()){ }

        /// <summary>
        /// Initalizes this generator.
        /// </summary>
        /// <param name="path">The path to a flat text file containing strings (one per line).</param>
        /// <param name="r">The Random object to use when returning random strings. This avoids having to create a new one internally.</param>
        public ListBasedStringGenerator(IEnumerable<String> strings, Random r)
        {
            this.Strings = strings.ToList();
            this.Random = r;
        }

        /// <summary>
        /// Returns a random string.
        /// </summary>
        /// <returns>A random string.</returns>
        public string NextString()
        {
            return this.Strings.ElementAt(this.Random.Next(this.Strings.Count() - 1));
        }
    }
}
