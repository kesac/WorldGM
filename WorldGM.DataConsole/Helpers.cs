using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGM.DataConsole
{
    public static class Helpers
    {
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

    }
}
