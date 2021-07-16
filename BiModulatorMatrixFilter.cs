
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
    public class BiModulatorMatrixFilter : MatrixFilter
    {
        public BiModulatorMatrixFilter(BiModulator modulator)
        {
            _modulator = modulator;
        }

        BiModulator _modulator;

        public override Matrix Apply(Matrix input)
        {
            return Apply2(new STuple<Matrix, Matrix>(input, input.CloneSize()));
        }

        public Matrix Apply2(STuple<Matrix, Matrix> input)
        {
            int i;
            int j;

            Matrix output = input.Value1.CloneSize();

            for (i = 0; i < input.Value1.RowCount; i++)
            {
                for (j = 0; j < input.Value1.ColumnCount; j++)
                {
                    output[i, j] =
                        _modulator(
                            input.Value1[i, j],
                            input.Value2[i, j]);
                }
            }

            return output;
        }
    }
}
