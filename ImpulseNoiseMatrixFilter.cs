using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class ImpulseNoiseMatrixFilter : SaltAndPepperNoiseMatrixFilter
    {
        public ImpulseNoiseMatrixFilter(float probability)
            : base(probability, 0, 1)
        {
        }
    }
}
