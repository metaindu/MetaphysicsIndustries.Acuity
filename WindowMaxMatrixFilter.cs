using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class WindowMaxMatrixFilter : WeightedPMatrixFilter
    {
        public WindowMaxMatrixFilter(int windowSize)
            : base(1, Matrix.FromUniform(windowSize, windowSize, 1))
        {
        }
    }
}
