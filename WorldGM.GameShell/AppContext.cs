using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldGM.Data;

namespace WorldGM.GameShell
{
    public class AppContext : WorldGmDataContext
    {

        public string DatabaseName
        {
            get
            {
                return "WorldGM_DataConsole_dev";
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=E:\Projects\C#\WorldGM\WorldGM.DataConsole\" + DatabaseName + ".db");
        }

    }
}
