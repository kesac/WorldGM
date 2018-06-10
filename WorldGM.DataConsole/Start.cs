using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using WorldGM.Generation;

namespace WorldGM.DataConsole
{
    public class Start
    {

        public static void Main(string[] args)
        {
            InitializeWorld();
            InitializeNames();
            InitializeAthletes();
            Console.WriteLine("Initialization complete!");
            Console.ReadKey();
        }

        private static void InitializeWorld()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(@"E:\Projects\C#\WorldGM\WorldGM.DataConsole\Data\World.xml");

            var worldNode = xml.FirstChild;

            using (var db = new AppContext())
            {
                if (worldNode.Name == "world")
                {
                    var worldName = worldNode.Attributes["name"].Value;
                    var world = db.EnsureWorldExists(worldName);

                    Console.WriteLine("Added world '" + worldName + "'");

                    foreach (XmlNode continentNode in worldNode.ChildNodes)
                    {
                        if (continentNode.Name == "continent")
                        {
                            var continentName = continentNode.Attributes["name"].Value;
                            var continent = db.EnsureContinentExists(world, continentName);
                            Console.WriteLine("Added continent '" + continentName+ "'");

                            foreach (XmlNode regionNode in continentNode.ChildNodes)
                            {
                                if (regionNode.Name == "region")
                                {
                                    var regionName = regionNode.Attributes["name"].Value;
                                    var region = db.EnsureRegionExists(continent, regionName);
                                    Console.WriteLine("Added region '" + regionName + "'");

                                    foreach (XmlNode cityNode in regionNode.ChildNodes)
                                    {
                                        if (cityNode.Name == "city")
                                        {
                                            var cityName = cityNode.Attributes["name"].Value;
                                            var city = db.EnsureCityExists(region, cityName);
                                            Console.WriteLine("Added city '" + cityName + "'");

                                            city.Population = int.Parse(cityNode.Attributes["population"].Value);
                                            db.SaveChanges();

                                            foreach (XmlNode teamNode in cityNode.ChildNodes)
                                            {
                                                if (teamNode.Name == "team")
                                                {
                                                    var teamName = teamNode.Attributes["name"].Value;
                                                    var team = db.EnsureTeamExists(city, teamName);
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }



                }
            }
        }

        private static void InitializeNames()
        {
            bool saveIndividually = false;
            using (var db = new AppContext())
            {
                string[] familyNames = File.ReadAllLines("Data/AstoriaFamilyNames.txt");

                foreach(var name in familyNames)
                {
                    db.EnsureNameExists(new Name()
                    {
                        Value = name,
                        IsFamilyName = true
                    }, saveIndividually);
                }
                
                Console.WriteLine("Added " + familyNames.Length + " family names");

                string[] feminineNames = File.ReadAllLines("Data/AstoriaFeminineNames.txt");

                foreach (var name in feminineNames)
                {
                    db.EnsureNameExists(new Name()
                    {
                        Value = name,
                        IsGivenName = true,
                        IsFeminine = true
                    }, saveIndividually);
                }
                
                Console.WriteLine("Added " + feminineNames.Length + " feminine names");

                string[] masculineNames = File.ReadAllLines("Data/AstoriaMasculineNames.txt");

                foreach (var name in masculineNames)
                {
                    
                    db.EnsureNameExists(new Name()
                    {
                        Value = name,
                        IsGivenName = true,
                        IsMasculine = true
                    }, saveIndividually);
                }

                Console.WriteLine("Added " + masculineNames.Length + " masculine names");


                db.EnsureNameExists(new Name()
                {
                    Value = "Jalin",
                    IsGivenName = true,
                    IsUnisex = true
                }, saveIndividually);
                
                Console.WriteLine("Added 1 unisex name for debugging purposes");

                db.SaveChanges();
                

            }
        }
        
        private static void InitializeAthletes()
        {
            using(var db = new AppContext())
            {
                var nameGenerator = new BasicNameGenerator(db.Names);
                var playerGenerator = new BasicAthleteGenerator(nameGenerator);

                for (int i = 0; i < 100; i++)
                {
                    db.Athletes.Add(playerGenerator.NextAthlete());
                }
                Console.WriteLine("Added 50 new athletes");

                db.SaveChanges();
                
            }
        }
    }
}
