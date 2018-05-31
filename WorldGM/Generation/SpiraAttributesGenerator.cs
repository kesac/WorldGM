using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spiraball.Math;

namespace WorldGM.Generation
{
    /// <summary>
    /// TODO
    /// </summary>
    public class SpiraAttributesGenerator : IAttributesGenerator
    {
        private static readonly int DefaultBaseMinimum = 30;
        private static readonly int DefaultBaseMaximum = 50;
        private static readonly int DefaultPotentialMinimum = 60;
        private static readonly int DefaultPotentialMaximum = 80;

        private static readonly Function[] GrowthFunctions =
        {
            Function.Linear,
            Function.QuadraticEaseOut,
            Function.QuadraticEaseInOut,
            Function.QuadraticEaseIn,
            Function.ExponentialEaseOut,
            Function.ExponentialEaseInOut,
            Function.ExponentialEaseIn,
            //Function.BackEaseOut,
            //Function.BackEaseInOut,
            Function.BackEaseIn,
            //Function.BounceEaseOut,
            //Function.BounceEaseInOut,
            //Function.ElasticEaseIn
        };

        private static readonly Function[] DeclineFunctions =
        {
            Function.Linear,
            Function.QuadraticEaseIn,
            Function.QuadraticEaseInOut,
            Function.ExponentialEaseInOut
        };

        private Random Random;

        public int BaseMinimum { get; set; }
        public int BaseMaximum { get; set; }
        public int PotentialMinimum { get; set; }
        public int PotentialMaximum { get; set; }

        public SpiraAttributesGenerator()
        {
            this.BaseMaximum = DefaultBaseMaximum;
            this.BaseMinimum = DefaultBaseMinimum;
            this.PotentialMaximum = DefaultPotentialMaximum;
            this.PotentialMinimum = DefaultPotentialMinimum;

            this.Random = new Random();
        }

        public List<Attribute> NextAttributes()
        {
            List<Attribute> attributes = new List<Attribute>();

            foreach(AttributeType type in Enum.GetValues(typeof(AttributeType)))
            {
                //attributes.Add(this.NextAttribute(Enum.GetName(typeof(AttributeType), type)));
                attributes.Add(this.NextAttribute(type));
            }

            return attributes;
        }

        private Attribute NextAttribute(AttributeType type)
        {
            Attribute attribute = new Attribute(type);

            attribute.Minimum = this.Random.Next(DefaultBaseMinimum, DefaultBaseMaximum + 1);
            attribute.Maximum = this.Random.Next(DefaultPotentialMinimum, DefaultPotentialMaximum + 1);
            attribute.Value = this.Random.Next(attribute.Minimum, attribute.Maximum + 1);
            attribute.Growth = GrowthFunctions[this.Random.Next(GrowthFunctions.Count())];
            attribute.Decline = DeclineFunctions[this.Random.Next(DeclineFunctions.Count())];

            return attribute;
        }
    }
}
