
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
    public class SeparableConvolutionMatrixFilter : MatrixFilter
    {
        public SeparableConvolutionMatrixFilter(Vector rowKernel, Vector columnKernel)
        {
            if (rowKernel == null) { throw new ArgumentNullException("rowKernel"); }
            if (columnKernel == null) { throw new ArgumentNullException("columnKernel"); }

            _rowKernel = rowKernel;
            _columnKernel = columnKernel;
        }

        Vector _rowKernel;
        Vector _columnKernel;

        public override Matrix Apply(Matrix input)
        {
            Matrix temp = ApplyRow(input);
            return ApplyColumn(temp);
        }

        private Matrix ApplyColumn(Matrix m)
        {
            int r;
            int c;
            int radius = _columnKernel.Length / 2;
            Matrix output = m.CloneSize();

            for (r = 0; r < m.RowCount; r++)
            {
                for (c = 0; c < m.ColumnCount; c++)
                {
                    float sum = 0;
                    for (int k = -radius; k < -radius + _columnKernel.Length; k++)
                    {
                        int d = c + k;
                        if (d >= 0 && d < m.ColumnCount)
                            sum += m[r, d] * _columnKernel[radius - k];
                    }
                    output[r, c] = sum;
                }
            }

            return output;
        }

        private Matrix ApplyRow(Matrix m)
        {
            int r;
            int c;
            int radius = _rowKernel.Length / 2;
            Matrix output = m.CloneSize();

            for (r = 0; r < m.RowCount; r++)
            {
                for (c = 0; c < m.ColumnCount; c++)
                {
                    float sum = 0;
                    for (int k = -radius; k < -radius + _columnKernel.Length; k++)
                    {
                        int d = r + k;
                        if (d >= 0 && d < m.RowCount)
                            sum += m[d, c] * _rowKernel[radius - k];
                    }
                    output[r, c] = sum;
                }
            }

            return output;
        }
    }
}
