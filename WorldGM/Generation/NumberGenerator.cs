using Archigen;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGM.Generation
{
    public class NumberGenerator : IGenerator<int>
    {
        private int Min;
        private int Max;
        private Random Random;

        public NumberGenerator(int minimumInclusive, int maximumInclusive)
        {
            this.Min = minimumInclusive;
            this.Max = maximumInclusive + 1;
            this.Random = new Random();
        }

        public int Next()
        {
            return this.Random.Next(Min, Max);
        }
    }
}
