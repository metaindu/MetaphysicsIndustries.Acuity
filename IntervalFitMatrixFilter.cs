
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
    public class IntervalFitMatrixFilter : IntervalFitBaseMatrixFilter
    {
        public IntervalFitMatrixFilter()
            : base(0, 1)
        {
        }

        public override Matrix Apply(Matrix input)
        {
            STuple<float, float> ret;

            ret = CalcInterval(input);

            Min = ret.Value1;
            Max = ret.Value2;

            //accumulate & fire
            return base.Apply(input);
        }

        public static STuple<float, float> CalcInterval(Matrix input)
        {
            int i;
            int j;

            float min = input[0, 0];
            float max = min;

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    float value = input[i, j];
                    if (!float.IsNaN(value))
                    {
                        min = Math.Min(min, value);
                        max = Math.Max(max, value);
                    }
                    else
                    {
                        value = 0;
                    }
                }
            }

            return new STuple<float, float>(min, max);
        }

    }
}
