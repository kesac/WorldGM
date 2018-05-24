using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGM
{
    public class Athlete
    {
        public int Id { get; set; }

        public int Age { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public AthleteGender Gender { get; set; }

        public AthletePosition Position { get; set; }

        // public int RemainingInjury { get; set; }
    }
}
