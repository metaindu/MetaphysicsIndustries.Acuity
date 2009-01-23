using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public abstract class CenteredCoordinateTransformMatrixFilter : CoordinateTransformMatrixFilter
    {
        protected override Pair<float> Modulate(float x, float y)
        {
            Pair<float> pair = AcuityEngine.ConvertZeroOneToNegOneOne(x, y);

            pair = InternalModulate(pair);

            return AcuityEngine.ConvertNegOneOneToZeroOne(pair.First, pair.Second);
        }

        protected abstract Pair<float> InternalModulate(Pair<float> pair);
    }
}
