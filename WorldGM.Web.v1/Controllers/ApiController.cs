using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WorldGM.Web.v1.Models;
using Microsoft.EntityFrameworkCore;
using WorldGM.Web.v1.ViewModels;
using WorldGM.Entities;

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
            using(var db = new WebContext())
            {
                return db.Athletes.Select(x => new AthleteViewModel(x)).ToList();
            }
        }

        [HttpGet("[action]/{id}")]
        public IEnumerable<AthleteViewModel> Athlete(int id)
        {
            using(var db = new WebContext())
            {
                var result = db.Athletes
                    .Include(x => x.TeamContract)
                    .ThenInclude(y => y.Team)
                    .ThenInclude(z => z.City)
                    .Where(x => x.Id == id);

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

        [HttpGet("[action]/{id}")]
        public IEnumerable<TeamViewModel> Team(int id)
        {
            using(var db = new WebContext())
            {
                var result = db.Teams
                    .Include(t => t.City)
                    .Include(t => t.TeamContracts)
                    .ThenInclude(x => x.Athlete)
                    .Where(x => x.Id == id);

                if (result.Count() > 0)
                {
                    return result.Select(x => new TeamViewModel(x)).ToList();
                }
                else
                {
                    return new TeamViewModel[0];
                }
            }
        }

        [HttpGet("[action]/{id}")]
        public IEnumerable<City> City(int id)
        {
            using (var db = new WebContext())
            {
                return db.Cities.Where(x => x.Id == id).ToList();

            }
        }

        [HttpGet("[action]")]
        public IEnumerable<ScheduleViewModel> Schedule()
        {
            using(var db = new WebContext())
            {
                var result = db.Schedules
                    .Include(x => x.ScheduledMatches).ThenInclude(y => y.HomeTeam)
                    .Include(x => x.ScheduledMatches).ThenInclude(y => y.AwayTeam)
                    .Select(x => new ScheduleViewModel(x))
                    .ToList();

                foreach(var r in result)
                {
                    r.ScheduledMatches = r.ScheduledMatches.OrderBy(x => x.SeasonDay).ToList();
                }

                return result;

            }
        }
    }
}