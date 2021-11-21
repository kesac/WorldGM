using Archigen;
using Loremaker;
using Loremaker.Names;
using System;
using System.Collections.Generic;
using System.Text;
using WorldGM.Entities;

namespace WorldGM.Generation
{
    public class CharacterGenerator : Generator<Character>
    {
        public CharacterGenerator() : this(new DefaultNameGenerator()) { }

        public CharacterGenerator(IGenerator<string> nameGenerator)
        {
            this.ForProperty<string>(x => x.FirstName, nameGenerator)
                .ForProperty<string>(x => x.FamilyName, nameGenerator)
                .ForProperty<int>(x => x.Level, 1)
                .ForProperty<int>(x => x.Strength, 1)
                .ForProperty<int>(x => x.Intelligence, 1)
                .ForProperty<int>(x => x.Dexterity, 1)
                .ForProperty<int>(x => x.Resistance, 1)
                .ForProperty<int>(x => x.Speed, 1)
                .ForProperty<int>(x => x.Vitality, 1)
                .ForProperty<CharacterAppearance>(x => x.Appearance, CharacterAppearance.Neutral)
                .ForProperty<CharacterElement>(x => x.Element, CharacterElement.Fire);
        }

    }
}
