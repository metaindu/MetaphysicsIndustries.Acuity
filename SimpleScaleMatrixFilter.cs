
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
    public class SimpleScaleMatrixFilter : MatrixFilter
    {
        public SimpleScaleMatrixFilter(int scaleFactor)
        {
            if (scaleFactor < 2) { throw new ArgumentException("scaleFactor must be greater than 1"); }

            _scaleFactor = scaleFactor;
        }

        private int _scaleFactor;

        public override Matrix Apply(Matrix input)
        {
            int i;
            int j;

            int r = input.RowCount * _scaleFactor;
            int c = input.ColumnCount * _scaleFactor;

            Matrix result = new Matrix(input.RowCount * _scaleFactor, input.ColumnCount * _scaleFactor);

            for (i = 0; i < r; i++)
            {
                for (j = 0; j < c; j++)
                {
                    result[i, j] = input[i / _scaleFactor, j / _scaleFactor];
                }
            }

            return result;
        }
    }
}
