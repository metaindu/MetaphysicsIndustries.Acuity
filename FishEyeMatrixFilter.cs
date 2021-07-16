
/*
 *  MetaphysicsIndustries.Acuity
 *  Copyright (C) 2009-2021 Metaphysics Industries, Inc., Richard Sartor
 *
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 3 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 *  USA
 *
 */

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
