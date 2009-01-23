using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class AtmImpulseRejectionStageMatrixFilter : OrderStatisticMatrixFilter
    {
        public AtmImpulseRejectionStageMatrixFilter(float alpha)
            : base(3)
        {
        }


        protected override float SelectValueFromOrderedMeasures(List<float> measures)
        {
            throw new NotImplementedException();
        }
    }
}
