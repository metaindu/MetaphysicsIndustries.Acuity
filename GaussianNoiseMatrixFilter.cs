
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
    public class GaussianNoiseMatrixFilter : MatrixFilter
    {
        public GaussianNoiseMatrixFilter(
            //float mean,
            float variance)
        {
            //_mean = mean;
            _variance = variance;
        }

        //private float _mean;
        private float _variance;

        public override Matrix Apply(Matrix input)
        {
            Matrix ret = new Matrix(input.RowCount, input.ColumnCount);
            int i;
            int j;

            float stdev = (float)Math.Sqrt(_variance);

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    int k;
                    int n = 20;
                    float x = 0;

                    for (k = 0; k < n; k++)
                    {
                        x += (float)_rand.NextDouble();
                    }
                    x -= n / 2.0f;
                    x *= (float)Math.Sqrt(12.0 / n);

                    x = 
                        //_mean + 
                        stdev * x;

                    ret[i,j] = input[i,j] + x;
                }
            }
            return ret;
        }
    }
}
