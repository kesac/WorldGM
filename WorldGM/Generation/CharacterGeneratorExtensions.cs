using System;

namespace WorldGM.Generation
{
    public static class GenerationHelpers
    {
        // Lower and upper limits are both inclusive
        public static int Between(this Random r, int lowerInclusive, int upperInclusive)
        {
            return r.Next(1 + upperInclusive - lowerInclusive) + lowerInclusive;
        }

        public static int NormalizedBetween(this Random r, int lower, int upper)
        {
            // https://stackoverflow.com/questions/218060/random-gaussian-variables
            double u1 = 1.0 - r.NextDouble();
            double u2 = 1.0 - r.NextDouble();
            double randStdNormal = System.Math.Sqrt(-2.0 * System.Math.Log(u1)) * System.Math.Sin(2.0 * System.Math.PI * u2);

            // https://stackoverflow.com/questions/1303368/how-to-generate-normally-distributed-random-from-an-integer-range
            double randNormal = 0.5 + 0.155 * randStdNormal;

            int value = Convert.ToInt32((randNormal * (upper - lower)) + lower);

            if(value < lower)
            {
                value = lower;
            }
            else if(value > upper)
            {
                value = upper;
            }

            return value;
        }

        public static bool OnChance(this Random r, double chance)
        {
            return r.NextDouble() < chance;
        }

        

    }
}
