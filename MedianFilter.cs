using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class MedianMatrixFilter : WeightedMedianMatrixFilter
    {
        public MedianMatrixFilter(int width)
            : base(Matrix.FromUniform(width, width, 1))
        {
        }
    }
}
