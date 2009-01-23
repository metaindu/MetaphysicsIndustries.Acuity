using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class HighPassVectorFilter : ConvolutionVectorFilter
    {
        public HighPassVectorFilter()
            : base(new Vector(2, 0.5f, -0.5f))
        {
        }
    }
}
