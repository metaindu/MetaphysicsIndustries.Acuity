
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
    public class HistogramMatrixFilter : MatrixFilter
    {
        public override Matrix Apply(Matrix input)
        {
            Matrix result = Matrix.FromUniform(256, 256, 1);

            int[] counts = new int[256];

            foreach (float value in input)
            {
                float value2 = 255 * Math.Max(0, Math.Min(1, value));

                counts[(int)Math.Round(value2)]++;
            }

            int max = counts[0];
            foreach (int value in counts)
            {
                max = (int)Math.Max(max, value);
            }

            int col;
            for (col = 0; col < 256; col++)
            {
                int height = 255 * counts[col] / max;
                int row;
                for (row = 0; row <= height; row++)
                {
                    result[255 - row, col] = 0;
                }
            }

            return result;
        }
    }
}
