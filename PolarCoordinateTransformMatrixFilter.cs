using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public abstract class PolarCoordinateTransformMatrixFilter : CenteredCoordinateTransformMatrixFilter
    {
        protected override Pair<float> InternalModulate(Pair<float> pair)
        {
            Pair<float> pair2 = AcuityEngine.ConvertEuclideanToPolar(pair.First, pair.Second);

            if (!CheckCoordinates(pair2))
            {
                return pair;
            }

            pair2 = InternalModulate2(pair2);

            return AcuityEngine.ConvertPolarToEuclidean(pair2.First, pair2.Second);
        }

        protected virtual bool CheckCoordinates(Pair<float> pair)
        {
            return true;
        }

        protected abstract Pair<float> InternalModulate2(Pair<float> pair);
    }
}
