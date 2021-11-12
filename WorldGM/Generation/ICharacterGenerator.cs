using Archigen;
using WorldGM.Entities;

namespace WorldGM.Generation
{
    public interface ICharacterGenerator : IGenerator<Character>
    {
        // Character Next();
        Character Next(CharacterElement position);
    }
}
