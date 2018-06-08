using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGM.Math
{

    public class WeightedCollection<T>
    {
        private Random Random { get; set; }
        public List<WeightedElement<T>> Elements { get; set; }

        public WeightedCollection()
        {
            this.Random = new Random();
            this.Elements = new List<WeightedElement<T>>();
        }

        public void Add(T value, int weight)
        {
            this.Elements.Add(new WeightedElement<T>(value, weight));
        }

        public T Next()
        {
            int roll = this.Random.Next(this.Elements.Sum(x => x.Weight));
            int chance = 0;

            for(int i = 0; i < this.Elements.Count(); i++)
            {
                chance += this.Elements[i].Weight;
                if (roll < chance)
                {
                    return this.Elements[i].Value;
                }
            }

            throw new InvalidOperationException("No selection could be made because the collection is empty or because an element was removed while the collection was selecting an element");
        }
            
    }
}
