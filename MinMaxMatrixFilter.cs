using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class MinMaxMatrixFilter : AdvancedConvolutionMatrixFilter
    {
        public MinMaxMatrixFilter(int windowSize)
            : this(Matrix.FromUniform(windowSize, windowSize, 1))
        {
        }

        public MinMaxMatrixFilter(Matrix convolutionKernel)
            : base(convolutionKernel, Math.Min, Math.Max)
        {
        }
    }
}
