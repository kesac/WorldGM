using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGM.Generation
{
    interface ICityDescriptionGenerator
    {
        string NextDescription(City city);
    }
}
