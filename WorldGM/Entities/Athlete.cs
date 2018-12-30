using System.Runtime.Serialization;

namespace WorldGM.Entities
{
    public class Athlete
    {
        public int Id { get; set; }

        [IgnoreDataMember]
        public virtual TeamContract TeamContract { get; set; }

        public string FirstName { get; set; }
        public string FamilyName { get; set; }

        public AthleteGender Gender { get; set; }
        public AthletePosition Position { get; set; }
        
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        public int Offense { get; set; }     // Offensive intelligence: higher means more shots
        public int Playmaking { get; set; }  // Playmaking intelligence: higher means more passes
        public int Defense { get; set; }     // Defensive intelligence: higher means more blocks/steals
        public int Shot { get; set; }        // Probability that shot on goal will beat goalkeeper
        public int Pass { get; set; }        // Probability that pass overcomes blocks and steals
        public int Accuracy { get; set; }    // Probability for shot or pass is on target
        public int Block { get; set; }       // Probability for block to overcome shot
        public int Steal { get; set; }       // Probability for intercept to overcome pass
        public int Vision { get; set; }      // Probability that a block or steal is on target
        public int Goaltending { get; set; } // Probability to save shots on goal
        public int Speed { get; set; }       // Provides boost to shot, pass, block, steal, goaltending
        public int Endurance { get; set; }   // Provides max energy at start of a game
                                             // Once energy dips below 70% speed boosts are lost
                                             // Once energy dips below 50%, penality occurs on shot, pass, block, steal, goaltending

        public int Reflection { get; set; }  // Ability to self-improve (a little) without dedicated training
        public int Learning { get; set; }    // Ability to improve with dedicated training
        public int Dedication { get; set; }  // Ability to withstand prolonged dedicated training
        public int Leadership { get; set; }  // Probability player is captain

        public int Strength { get; set; }    // Ability to withstand injury
        public int Injury { get; set; }      // Days left until recovery
        public int Resilience { get; set; }  // Ability to resist high pressure situations
        public int Conditioning { get; set; }// Higher means in-game actions bring energy down less


        // public int RemainingInjury { get; set; }
    }
}
