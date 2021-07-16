
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
    public class ExpandEdgeMatrixFilter : MatrixFilter
    {
        public ExpandEdgeMatrixFilter(int borderWidth)
        {
            if (borderWidth < 1) { throw new ArgumentException("borderWidth"); }

            _borderWidth = borderWidth;
        }

        private int _borderWidth;
        public int BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; }
        }

        public override Matrix Apply(Matrix input)
        {
            int i = 2*BorderWidth+1;

            Matrix result = new Matrix(input.RowCount + 2 * BorderWidth, input.ColumnCount + 2 * BorderWidth);

            int j;
            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    result[i + BorderWidth, j + BorderWidth] = input[i, j];
                }
            }
            for (i = 0; i < BorderWidth; i++)
            {
                for (j = 0; j < BorderWidth; j++)
                {
                    result[i, j] = input[0, 0];
                    result[result.RowCount - i - 1, j] = input[input.RowCount - 1, 0];
                    result[i, result.ColumnCount - j - 1] = input[0, input.ColumnCount - 1];
                    result[result.RowCount - i - 1, result.ColumnCount - j - 1] = input[input.RowCount - 1, input.ColumnCount - 1];
                }
                for (j = 0; j < input.ColumnCount; j++)
                {
                    result[i, j + BorderWidth] = input[0, j];
                    result[result.RowCount - i - 1, BorderWidth + j] = input[input.RowCount - 1, j];
                }
            }
            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < BorderWidth; j++)
                {
                    result[BorderWidth + i, j] = input[i, 0];
                    result[BorderWidth + i, result.ColumnCount - j - 1] = input[i, input.ColumnCount - 1];
                }
            }

            return result;
        }
    }
}
