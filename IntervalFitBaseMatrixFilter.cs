
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
    public class IntervalFitBaseMatrixFilter : MatrixFilter
    {
        public IntervalFitBaseMatrixFilter(float min, float max)
        {
            _min = min;
            _max = max;
        }

        float _min;
        public float Min
        {
            get { return _min; }
            protected set { _min = value; }
        }
	
        float _max;
        public float Max
        {
            get { return _max; }
            protected set { _max = value; }
        }

        public override Matrix Apply(Matrix input)
        {
            return IntervalFit(input, Min, Max);
        }

        public static Matrix IntervalFit(Matrix input, float min, float max)
        {
            int i;
            int j;

            Matrix output = input.CloneSize();

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    float value = input[i, j];
                    if (float.IsNaN(value))
                    {
                        value = min;
                    }
                    output[i, j] = AcuityEngine.IntervalFit(value, min, max);
                }
            }

            return output;
        }
    }
}
