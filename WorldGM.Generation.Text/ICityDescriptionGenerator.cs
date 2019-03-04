using WorldGM.Entities;

namespace WorldGM.Generation.Text
{
    public interface ICityDescriptionGenerator
    {
        string NextDescription(City city);
    }
}
