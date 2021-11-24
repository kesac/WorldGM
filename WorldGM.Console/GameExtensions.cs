using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM.Console
{
    public static class GameExtensions
    {
        private static Random Random = new Random();
        public static T GetRandom<T>(this List<T> list)
        {
            return list[Random.Next(list.Count)];
        }
    }
}
