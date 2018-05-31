using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM.Generation
{
    public interface IAthleteGenerator
    {
        Athlete NextAthlete();

        Athlete NextAthlete(AthletePosition position);
    }
}
