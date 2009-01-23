using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class WeightedMedianVectorFilter : MedianVectorFilter
    {
        public WeightedMedianVectorFilter(Vector weights)
            : base(weights.Length)
        {
        }
    }
}
