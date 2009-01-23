using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class LowPassVectorFilter : MovingAverageVectorFilter
    {
        public LowPassVectorFilter()
            : base(2)
        {
        }
    }
}
