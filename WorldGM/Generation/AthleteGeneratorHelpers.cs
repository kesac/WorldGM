using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGM.Generation
{
    public static class AthleteGeneratorHelpers
    {
        // Lower and upper limits are both inclusive
        public static int Between(this Random r, int lower, int upper)
        {
            return r.Next(upper) + lower + 1;
        }

    }
}
