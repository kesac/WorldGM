using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WorldGM.DataConsole
{
    public class Start
    {

        public static void Main(string[] args)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(@"E:\Projects\C#\WorldGM\WorldGM.DataConsole\World.xml");

            var worldNode = xml.FirstChild;

            using(var db = new AppContext())
            {
                if(worldNode.Name == "world")
                {
                    var worldName = worldNode.Attributes["name"].Value;
                    var world = db.EnsureWorldExists(worldName);

                    foreach(XmlNode continentNode in worldNode.ChildNodes)
                    {
                        if(continentNode.Name == "continent")
                        {
                            var continentName = continentNode.Attributes["name"].Value;
                            var continent = db.EnsureContinentExists(world, continentName);

                            foreach (XmlNode regionNode in continentNode.ChildNodes)
                            {
                                if (regionNode.Name == "region")
                                {
                                    var regionName = regionNode.Attributes["name"].Value;
                                    var region = db.EnsureRegionExists(continent, regionName);

                                    foreach (XmlNode cityNode in regionNode.ChildNodes)
                                    {
                                        if (cityNode.Name == "city")
                                        {
                                            var cityName = cityNode.Attributes["name"].Value;
                                            var city = db.EnsureCityExists(region, cityName);

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
    }
}
