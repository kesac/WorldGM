using System;
using Spectre.Console;

namespace WorldGM.Console
{
    public class Start
    {
        public static void Main(string[] args)
        {
            var g = new GameShell();
            g.Start();
        }
    }
}
