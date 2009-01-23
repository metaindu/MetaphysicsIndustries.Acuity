using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class MovingAverageVectorFilter : ConvolutionVectorFilter
    {
        public MovingAverageVectorFilter(int width)
            : base(Vector.FromUniformSequence(1 / (float)width, width))
        {
        }
    }
}
