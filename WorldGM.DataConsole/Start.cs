﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using WorldGM.Entities;
using WorldGM.Generation;
using WorldGM.Generation.Text;

namespace WorldGM.DataConsole
{
    public class Start
    {
        private static bool TeamCountChanged { get; set; }
        private static bool ForceScheduleRecreation { get; set; }
        private static string RootDataFolder = @"E:\Projects\C#\WorldGM\WorldGM.DataConsole\Data";

        public static void Main(string[] args)
        {
            InitializeWorld();
            InitializeNames();
            InitializeAthletes();
            InitializeTeams();
            InitializeSchedule();

            Console.WriteLine("Initialization complete!");
            Console.WriteLine("(Press enter to exit)");
            Console.ReadKey();
        }

        private static void InitializeWorld()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(RootDataFolder + @"\World.xml");

            var worldNode = xml.FirstChild;
            var cityDescriptor = new ExperimentalCityDescriptionGenerator();

            using (var db = new AppContext())
            {
                int currentTeamCount = db.Teams.Count();
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
                                            city.Description = cityDescriptor.NextDescription(city);
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
                int newTeamCount = db.Teams.Count();

                if(currentTeamCount != newTeamCount)
                {
                    TeamCountChanged = true;
                }
            }
        }

        private static void InitializeNames()
        {
            bool saveIndividually = false;
            using (var db = new AppContext())
            {
                string[] familyNames = File.ReadAllLines(RootDataFolder + @"\FamilyNames.txt");

                foreach(var name in familyNames)
                {
                    db.EnsureNameExists(new Name()
                    {
                        Value = name,
                        IsFamilyName = true
                    }, saveIndividually);
                }
                
                Console.WriteLine("Added " + familyNames.Length + " family names");

                string[] feminineNames = File.ReadAllLines(RootDataFolder + @"\FeminineNames.txt");

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

                string[] masculineNames = File.ReadAllLines(RootDataFolder + @"\MasculineNames.txt");

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
                int limit = 500;
                if (db.Athletes.Count() < limit)
                {
                    var nameGenerator = new BasicNameGenerator(db.Names);
                    var playerGenerator = new BasicAthleteGenerator(nameGenerator);

                    for (int i = 0; i < 500; i++)
                    {
                        db.Athletes.Add(playerGenerator.NextAthlete());
                    }
                    Console.WriteLine("Added 500 new athletes");

                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("There are already " + limit + " or more existing athletes");
                }
                
            }
        }

        private static void InitializeTeams()
        {
            using (var db = new AppContext())
            {
                bool saveChangesIncrementally = false; // if false, changes are saved in bulk
                Console.WriteLine("Ensuring all teams have contracted players...");
                foreach (var team in db.Teams)
                {
                    var contracts = db.TeamContracts.Where(x => x.TeamId == team.Id);

                    if(contracts.Count() == 0)
                    {
                        // 2 goalies
                        // 4 forwards
                        // 3 midfielders
                        // 3 defense
                        var goalies = db.Athletes.Where(x => x.Position == AthletePosition.Goalkeeper && !x.PlayerHasContract(db));
                        var forwards = db.Athletes.Where(x => x.Position == AthletePosition.Forward && !x.PlayerHasContract(db));
                        var midfielders = db.Athletes.Where(x => x.Position == AthletePosition.Midfield && !x.PlayerHasContract(db));
                        var defense = db.Athletes.Where(x => x.Position == AthletePosition.Defense && !x.PlayerHasContract(db));

                        for(int i = 0; i < 2; i++)
                        {
                            db.Contract(team, goalies.GetRandomPlayer(), saveChangesIncrementally);
                        }

                        for (int i = 0; i < 4; i++)
                        {
                            db.Contract(team, forwards.GetRandomPlayer(), saveChangesIncrementally);
                        }

                        for (int i = 0; i < 3; i++)
                        {
                            db.Contract(team, midfielders.GetRandomPlayer(), saveChangesIncrementally);
                        }

                        for (int i = 0; i < 3; i++)
                        {
                            db.Contract(team, defense.GetRandomPlayer(), saveChangesIncrementally);
                        }

                        db.SaveChanges();
                    }
                }
                Console.WriteLine("Complete!");
            }
        }

        private static void InitializeSchedule()
        {
            using(var db = new AppContext())
            {
                var scheduler = new BalancedScheduleGenerator() { DebugInfo = false };

                if (db.Schedules.Count() == 0 || TeamCountChanged || ForceScheduleRecreation)
                {

                    Console.WriteLine("Clearing existing games");
                    db.ScheduledMatches.RemoveRange(db.ScheduledMatches.ToList());
                    db.Schedules.RemoveRange(db.Schedules.ToList());

                    Console.WriteLine("Scheduling games... (this might take a few seconds)");
                    var schedule = scheduler.GetSchedule(db.Teams.ToList());

                    var totalGames = schedule.ScheduledMatches.Count;
                    var totalDays = schedule.ScheduledMatches.Max(x => x.SeasonDay);

                    db.Schedules.Add(schedule);
                    db.SaveChanges();
                    Console.WriteLine("Scheduled " + totalGames + " games over " + totalDays + " days!");
                }
                else
                {
                    Console.WriteLine("Keeping same schedule");
                }
            }
        }
    }
}
