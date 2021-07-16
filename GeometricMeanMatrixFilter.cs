
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
    public class GeometricMeanMatrixFilter : MatrixFilter
    {
        public GeometricMeanMatrixFilter(int width)
        {
            if (width < 1) { throw new ArgumentOutOfRangeException("width must be greater than 0"); }

            _width = width;
        }

        private int _width;

        public override Matrix Apply(Matrix input)
        {
            int n;
            int m;
            int i;
            int j;
            float term;

            Matrix y = new Matrix(input.RowCount, input.ColumnCount);

            for (n = 0; n < input.RowCount; n++)
            {
                for (m = 0; m < input.ColumnCount; n++)
                {
                    term = 1;
                    for (i = 0; i < input.RowCount - 1; i++)
                    {
                        for (j = 0; j < input.ColumnCount - 1; j++)
                        {
                            term *= input[n - i, m - j];
                        }
                    }
                    y[n, m] = (float)Math.Pow(term, 1 / ((float)(input.RowCount * input.ColumnCount)));
                }
            }

            return y;
        }
    }
}
