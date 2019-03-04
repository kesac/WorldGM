using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorldGM.Data;

namespace WorldGM.DataConsole
{
    public class AppContext : WorldGmDataContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=E:\Projects\C#\WorldGM\WorldGM.DataConsole\WorldGM_DataConsole_dev.db");
        }
    }
}
