using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class WindowMinMatrixFilter : WeightedPMatrixFilter
    {
        public WindowMinMatrixFilter(int windowSize)
            : base(0, Matrix.FromUniform(windowSize, windowSize, 1))
        {
        }
    }
}
