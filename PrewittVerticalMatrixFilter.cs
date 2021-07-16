
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

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class PrewittVerticalMatrixFilter : ConvolutionMatrixFilter
    {
        public PrewittVerticalMatrixFilter()
            : base(GenerateMatrix())
        {
        }

        protected static Matrix GenerateMatrix()
        {
            Matrix y = new Matrix(3, 3);

            y[0, 0] = 1;
            y[0, 1] = 1;
            y[0, 2] = 1;
            y[2, 0] = -1;
            y[2, 1] = -1;
            y[2, 2] = -1;

            y.ApplyToAll((new AcuityEngine.MultiplyModulator(1 / 3.0f)).Modulate);

            return y;
        }
    }
}
