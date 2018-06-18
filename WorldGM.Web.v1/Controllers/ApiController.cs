﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorldGM.Web.v1.Models;
using Microsoft.EntityFrameworkCore;
using WorldGM.Web.v1.ViewModels;

namespace WorldGM.Web.v1.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/")]
    public class ApiController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<World> World()
        {
            using(var db = new WebContext())
            {
                return db.Worlds
                    .Include(w => w.Continents)
                    .ThenInclude(x => x.Regions)
                    .ThenInclude(y => y.Cities)
                    .ThenInclude(z => z.Teams)
                    .ToList();
            }
        }

        [HttpGet("[action]")]
        public IEnumerable<AthleteViewModel> Athletes()
        {
            using (var db = new WebContext())
            {
                return db.Athletes.Select(x => new AthleteViewModel(x)).ToList();
            }
        }

        [HttpGet("[action]/{id}")]
        public IEnumerable<AthleteViewModel> Athlete(int id)
        {
            using (var db = new WebContext())
            {
                var result = db.Athletes.Where(x => x.Id == id);

                if (result.Count() > 0)
                {
                    return result.Select(x => new AthleteViewModel(x)).ToList();
                }
                else
                {
                    return new AthleteViewModel[0];
                }
            }
        }

        [HttpGet("[action]")]
        public IEnumerable<TeamViewModel> Teams()
        {
            using(var db = new WebContext())
            {
                return db.Teams
                    .Include(t => t.City)
                    .Select(x => new TeamViewModel(x))
                    .ToList();
            }
        }

    }
}