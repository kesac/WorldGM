using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGM.Generation
{
    public static class GenerationHelpers
    {
        // Lower and upper limits are both inclusive
        public static int Between(this Random r, int lowerInclusive, int upperInclusive)
        {
            return r.Next(1 + upperInclusive - lowerInclusive) + lowerInclusive;
        }

        public static int NormalizedBetween(this Random r, int lower, int upper, int mean, int deviation)
        {
            // https://stackoverflow.com/questions/218060/random-gaussian-variables
            double u1 = 1.0 - r.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - r.NextDouble();
            double randStdNormal = System.Math.Sqrt(-2.0 * System.Math.Log(u1)) * System.Math.Sin(2.0 * System.Math.PI * u2); //random normal(0,1)
            double randNormal = mean + deviation * randStdNormal; //random normal(mean,stdDev^2)

            return Convert.ToInt32((randNormal * (upper - lower)) + lower);
        }

        public static bool OnChance(this Random r, double chance)
        {
            return r.NextDouble() < chance;
        }

        

    }
}
