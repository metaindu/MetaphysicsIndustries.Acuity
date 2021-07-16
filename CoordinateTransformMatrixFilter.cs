
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
    public abstract class CoordinateTransformMatrixFilter : MatrixFilter
    {
        //public CoordinateTransformMatrixFilter(PairModulator mod)
        //{
        //    _mod = mod;
        //}

        //private PairModulator _mod;

        protected abstract STuple<float, float> Modulate(float x, float y);

        public override Matrix Apply(Matrix input)
        {
            int i;
            int j;
            Matrix ret = new Matrix(input.RowCount, input.ColumnCount);
            STuple<float, float> pair;

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    pair = new STuple<float, float>(i / (float)input.RowCount, j / (float)input.ColumnCount);
                    pair = Modulate(pair.Value1, pair.Value2);
                    if (pair.Value1 < 0 || pair.Value1 > 1 ||
                        pair.Value2 < 0 || pair.Value2 > 1)
                    {
                        ret[i, j] = 0;
                    }
                    else
                    {
                        ret[i, j] =
                            input[
                                (int)(Math.Round(pair.Value1 * (input.RowCount - 1))),
                                (int)(Math.Round(pair.Value2 * (input.ColumnCount - 1)))];
                    }
                }
            }

            return ret;
        }
    }
}
