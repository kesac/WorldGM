using WorldGM.Entities;

namespace WorldGM.Generation
{
    interface ICityDescriptionGenerator
    {
        string NextDescription(City city);
    }
}
