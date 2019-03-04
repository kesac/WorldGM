using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WorldGM.GameShell
{
    public class Shell
    {

        public string Prompt { get; set; }
        public AppContext Context { get; set; }

        public Shell()
        {
            Prompt = "WorldGM";
            Context = new AppContext();
        }

        public void Run()
        {
            Console.WriteLine("WorldGM Game Shell v0.1");

            var run = true;
            var input = string.Empty;

            while (run)
            {
                Console.Write(Prompt + " > ");
                input = Console.ReadLine().ToLower();

                if (input.StartsWith("teams"))
                {
                    Teams();
                }
            }


        }

        public async void Teams()
        {
            foreach(var team in await Context.Teams.ToListAsync())
            {
                Console.WriteLine(team.Name);
            }
        }
    }
}
