using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;


namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class FishEyeMatrixFilter : PolarCoordinateTransformMatrixFilter
    {
        //public FishEyeMatrixFilter()
        //    : base(Modulate)
        //{
        //}

        //protected override Pair<float> Modulate(float x, float y)
        //{
        //    Pair<float> pair = SolusEngine.ConvertZeroOneToNegOneOne(x, y);
        //
        //    pair = SolusEngine.ConvertEuclideanToPolar(pair.Value1, pair.Value2);
        //
        //    //r /= Math.Sqrt(2);
        //
        //    if (pair.Value1 >= -1 && pair.Value1 <= 1)
        //    {
        //        pair.Value1 = (float)(2 * Math.Asin(pair.Value1) / Math.PI);
        //    }
        //
        //    //r *= Math.Sqrt(2);
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
            pair.Value1 = (float)(2 * Math.Asin(pair.Value1) / Math.PI);

            return pair;
        }
    }
}
