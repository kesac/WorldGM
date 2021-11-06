using System;
using Archigen;
using Syllabore;
using WorldGM.Entities;

namespace WorldGM.Examples
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var names = new NameGenerator()
                            .UsingProvider(new SyllableSet(4, 32, 4)
                                .InitializeWith(x => x
                                    .WithVowels("aei")
                                    .WithLeadingConsonants("strlmnp")))
                            .UsingSyllableCount(2, 4);

            var cities = new Generator<City>()
                            .ForProperty(x => x.Name, names);

            var regions = new Generator<Region>()
                            .ForProperty(x => x.Name, names)
                            .ForListProperty(x => x.Cities, cities).UsingSize(4);

            var continents = new Generator<Continent>()
                            .ForProperty(x => x.Name, names)
                            .ForListProperty(x => x.Regions, regions).UsingSize(4);

            var worlds = new Generator<World>()
                            .ForProperty(x => x.Name, names)
                            .ForListProperty(x => x.Continents, continents).UsingSize(4);


            for(int i = 0; i < 1; i++)
            {
                var world = worlds.Next();
                Console.WriteLine("[World of {0}]", world.Name);

                foreach(var continent in world.Continents)
                {
                    Console.WriteLine("  {0} Continent", continent.Name);

                    foreach(var region in continent.Regions)
                    {
                        Console.WriteLine("    {0} Region", region.Name);

                        foreach(var city in region.Cities)
                        {
                            Console.WriteLine("      {0} City", city.Name);
                        }

                    }

                }

            }

        }
    }
}
