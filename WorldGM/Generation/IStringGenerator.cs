
namespace WorldGM.Generation
{

    /// <summary>
    /// Exposes a method for generating strings.
    /// </summary>
    public interface IStringGenerator
    {
        /// <summary>
        /// Returns a random string.
        /// </summary>
        /// <returns>A random name as a string.</returns>
        string NextString();
    }
}
