using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class WeightedMedianMatrixFilter : WeightedPMatrixFilter
    {
        public WeightedMedianMatrixFilter(Matrix weights)
            : base(0.5f, weights)
        {
        }
    }
}
