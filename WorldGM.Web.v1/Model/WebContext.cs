using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldGM.Data;

namespace WorldGM.Web.v1.Model
{
    public class WebContext : WorldGmDataContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=E:\\Projects\\C#\\WorldGM\\WorldGM.DataConsole\\WorldGM_DataConsole_dev.db");
        }
    }
}
