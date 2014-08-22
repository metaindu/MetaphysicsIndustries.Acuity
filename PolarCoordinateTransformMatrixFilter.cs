using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;


namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public abstract class PolarCoordinateTransformMatrixFilter : CenteredCoordinateTransformMatrixFilter
    {
        protected override STuple<float, float> InternalModulate(STuple<float, float> pair)
        {
            STuple<float, float> pair2 = AcuityEngine.ConvertEuclideanToPolar(pair.Value1, pair.Value2);

            if (!CheckCoordinates(pair2))
            {
                return pair;
            }

            pair2 = InternalModulate2(pair2);

            return AcuityEngine.ConvertPolarToEuclidean(pair2.Value1, pair2.Value2);
        }

        protected virtual bool CheckCoordinates(STuple<float, float> pair)
        {
            return true;
        }

        protected abstract STuple<float, float> InternalModulate2(STuple<float, float> pair);
    }
}
