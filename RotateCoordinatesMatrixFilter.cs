
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
    public class RotateCoordinatesMatrixFilter : PolarCoordinateTransformMatrixFilter
    {
        public RotateCoordinatesMatrixFilter(float angle)
            //: base(this.Modulate)
        {
            _angle = angle;
        }

        float _angle;
        public virtual float Angle
        {
            get { return _angle; }
        }

        //protected override Pair<float> Modulate(float x, float y)
        //{
        //    x = SolusEngine.ConvertZeroOneToNegOneOne(x);
        //    y = SolusEngine.ConvertZeroOneToNegOneOne(y);
        //
        //    float r = Math.Sqrt(x * x + y * y);
        //    float theta = Math.Atan2(y, x);
        //
        //    theta += _angle;
        //
        //    x = r * Math.Cos(theta);
        //    y = r * Math.Sin(theta);
        //
        //    x = SolusEngine.ConvertNegOneOneToZeroOne(x);
        //    y = SolusEngine.ConvertNegOneOneToZeroOne(y);
        //
        //    return new Pair<float>(x, y);
        //}

        protected override STuple<float, float> InternalModulate2(STuple<float, float> pair)
        {
            pair.Value2 += Angle;
            return pair;
        }
    }
}
