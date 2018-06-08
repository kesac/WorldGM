using System;
using System.Collections.Generic;
using System.Text;

namespace WorldGM.Math
{
    public class WeightedElement<T>
    {
        public T Value { get; set; }
        public int Weight { get; set; }

        public WeightedElement(T value, int weight)
        {
            this.Value = value;
            this.Weight = weight;
        }
    }
}
