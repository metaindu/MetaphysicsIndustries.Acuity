using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class MaxMinMatrixFilter : AdvancedConvolutionMatrixFilter
    {
        public MaxMinMatrixFilter(int windowSize)
            : this(Matrix.FromUniform(windowSize, windowSize, 1))
        {
        }

        public MaxMinMatrixFilter(Matrix convolutionKernel)
            : base(convolutionKernel, Math.Max, Math.Min)
        {
        }
    }
}
