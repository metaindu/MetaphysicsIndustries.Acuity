
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
