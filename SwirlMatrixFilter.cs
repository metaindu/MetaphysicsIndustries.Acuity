using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class SwirlMatrixFilter : PolarCoordinateTransformMatrixFilter
    {
        public SwirlMatrixFilter(float factor)
        {
            _factor = factor;
        }

        float _factor;
        public virtual float Factor
        {
            get { return _factor; }
        }

        //protected override Pair<float> Modulate(float x, float y)
        //{
        //    return Modulate(x, y, _factor);
        //}

        //protected static Pair<float> Modulate(float x,float y,float factor)
        //{
        //    Pair<float> pair = SolusEngine.ConvertZeroOneToNegOneOne(x, y);
        //
        //    pair = SolusEngine.ConvertEuclideanToPolar(pair.First, pair.Second);
        //
        //    if (pair.First >= -1 && pair.First <= 1)
        //    {
        //        pair.Second += (1 - pair.First) * factor;
        //    }
        //
        //    pair = SolusEngine.ConvertPolarToEuclidean(pair.First, pair.Second);
        //
        //    pair = SolusEngine.ConvertNegOneOneToZeroOne(pair.First, pair.Second);
        //
        //    return pair;
        //}

        protected override bool CheckCoordinates(Pair<float> pair)
        {
            if (pair.First >= -1 && pair.First <= 1)
            {
                return true;
            }

            return false;
        }

        protected override Pair<float> InternalModulate2(Pair<float> pair)
        {
            pair.Second += (1 - pair.First) * Factor;

            return pair;
        }
    }
}
