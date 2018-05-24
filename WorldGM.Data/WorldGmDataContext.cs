﻿using Microsoft.EntityFrameworkCore;
using System;

namespace WorldGM.Data
{
    public class WorldGmDataContext : DbContext
    {
        public DbSet<World> Worlds { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamContract> TeamContracts { get; set; }
        public DbSet<Athlete> Athletes { get; set; }
    }

}
