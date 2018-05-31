using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorldGM.Web.v1.Model;
using Microsoft.EntityFrameworkCore;

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

    }
}