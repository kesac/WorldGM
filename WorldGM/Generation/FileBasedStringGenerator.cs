using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM.Generation
{
    /// <summary>
    /// Provides random strings from a flat text file.
    /// </summary>
    public class FileBasedStringGenerator : IStringGenerator
    {
        private Random Random;
        private IEnumerable<string> Strings;

        /// <summary>
        /// Initalizes this generator.
        /// </summary>
        /// <param name="path">The path to a flat text file containing strings (one per line).</param>
        public FileBasedStringGenerator(string path) : this(path, new Random()){ }


        /// <summary>
        /// Initalizes this generator.
        /// </summary>
        /// <param name="path">The path to a flat text file containing strings (one per line).</param>
        /// <param name="r">The Random object to use when returning random strings. This avoids having to create a new one internally.</param>
        public FileBasedStringGenerator(string path, Random r)
        {
            if (File.Exists(path))
            {
                string data = File.ReadAllText(path);
                string[] lines = data.Split('\n');
                this.Strings = lines.Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList();
                this.Random = r;
            }
            else
            {
                throw new ArgumentException("File " + path + " does not exist.");
            }
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
