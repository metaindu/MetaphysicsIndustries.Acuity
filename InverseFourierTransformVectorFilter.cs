using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class InverseFourierTransformVectorFilter : FourierTransformVectorFilter
    {
        protected override bool IsInverse
        {
            get { return true; }
        }

        protected override float ScaleForInverse(float x, int length)
        {
            return x / length;
        }
    }
}
