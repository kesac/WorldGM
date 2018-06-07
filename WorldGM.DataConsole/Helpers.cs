using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGM.DataConsole
{
    public static class Helpers
    {
        public static Name EnsureNameExists(this AppContext db, Name newName, bool saveChanges = true)
        {
            var existing = db.Names.FirstOrDefault(x => x.Value == newName.Value 
                && x.IsGivenName == newName.IsGivenName
                && x.IsFamilyName == newName.IsFamilyName
                && x.IsMasculine == newName.IsMasculine
                && x.IsFeminine == newName.IsFeminine
                && x.IsUnisex == newName.IsUnisex
                && x.IsRegional == newName.IsRegional
                && x.RegionId == newName.RegionId
                && x.IsUniqueToCity == newName.IsUniqueToCity
                && x.CityId == newName.CityId);

            if (existing == null)
            {
                db.Names.Add(newName);

                if (saveChanges)
                {
                    db.SaveChanges();
                }

                return newName;
            }
            else
            {
                return existing;
            }

            
        }

        public static World EnsureWorldExists(this AppContext db, string worldName)
        {
            var existing = db.Worlds.FirstOrDefault(x => x.Name == worldName);

            if(existing == null)
            {
                db.Worlds.Add(new World() { Name = worldName, CurrentYear = DateTime.Now.Year - 1800 });
                db.SaveChanges();
                existing = db.Worlds.FirstOrDefault(x => x.Name == worldName);
            }

            return existing;
        }

        public static Continent EnsureContinentExists(this AppContext db, World parentWorld, string continentName)
        {
            var existing = db.Continents.FirstOrDefault(x => x.Name == continentName);

            if (existing == null)
            {
                db.Continents.Add(new Continent{ Name = continentName, WorldId = parentWorld.Id });
                db.SaveChanges();
                existing = db.Continents.FirstOrDefault(x => x.Name == continentName);
            }

            return existing;
        }

        public static Region EnsureRegionExists(this AppContext db, Continent parentContinent, string regionName)
        {
            var existing = db.Regions.FirstOrDefault(x => x.Name == regionName);

            if (existing == null)
            {
                db.Regions.Add(new Region { Name = regionName, ContinentId = parentContinent.Id });
                db.SaveChanges();
                existing = db.Regions.FirstOrDefault(x => x.Name == regionName);
            }

            return existing;
        }

        public static City EnsureCityExists(this AppContext db, Region parentRegion, string cityName)
        {
            var existing = db.Cities.FirstOrDefault(x => x.Name == cityName);

            if (existing == null)
            {
                db.Cities.Add(new City { Name = cityName, RegionId = parentRegion.Id });
                db.SaveChanges();
                existing = db.Cities.FirstOrDefault(x => x.Name == cityName);
            }

            return existing;
        }

        public static Team EnsureTeamExists(this AppContext db, City parentCity, string teamName)
        {
            var existing = db.Teams.FirstOrDefault(x => x.Name == teamName);

            if (existing == null)
            {
                db.Teams.Add(new Team { Name = teamName, CityId = parentCity.Id});
                db.SaveChanges();
                existing = db.Teams.FirstOrDefault(x => x.Name == teamName);
            }

            return existing;
        }

    }
}
