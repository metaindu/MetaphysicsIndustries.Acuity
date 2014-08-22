using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;


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
        //    pair = SolusEngine.ConvertEuclideanToPolar(pair.Value1, pair.Value2);
        //
        //    if (pair.Value1 >= -1 && pair.Value1 <= 1)
        //    {
        //        pair.Value2 += (1 - pair.Value1) * factor;
        //    }
        //
        //    pair = SolusEngine.ConvertPolarToEuclidean(pair.Value1, pair.Value2);
        //
        //    pair = SolusEngine.ConvertNegOneOneToZeroOne(pair.Value1, pair.Value2);
        //
        //    return pair;
        //}

        protected override bool CheckCoordinates(STuple<float, float> pair)
        {
            if (pair.Value1 >= -1 && pair.Value1 <= 1)
            {
                return true;
            }

            return false;
        }

        protected override STuple<float, float> InternalModulate2(STuple<float, float> pair)
        {
            pair.Value2 += (1 - pair.Value1) * Factor;

            return pair;
        }
    }
}
