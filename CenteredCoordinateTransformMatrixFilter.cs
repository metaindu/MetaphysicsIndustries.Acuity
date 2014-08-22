using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;


namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public abstract class CenteredCoordinateTransformMatrixFilter : CoordinateTransformMatrixFilter
    {
        protected override STuple<float, float> Modulate(float x, float y)
        {
            STuple<float, float> pair = AcuityEngine.ConvertZeroOneToNegOneOne(x, y);

            pair = InternalModulate(pair);

            return AcuityEngine.ConvertNegOneOneToZeroOne(pair.Value1, pair.Value2);
        }

        protected abstract STuple<float, float> InternalModulate(STuple<float, float> pair);
    }
}
