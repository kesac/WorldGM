using WorldGM.Entities;

namespace WorldGM.Generation
{
    public interface IAthleteGenerator
    {
        Athlete NextAthlete();

        Athlete NextAthlete(AthletePosition position);
    }
}
