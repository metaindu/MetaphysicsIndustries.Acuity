
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
    public class SaltAndPepperNoiseMatrixFilter : MatrixFilter
    {
        public SaltAndPepperNoiseMatrixFilter(float probability)
            :  this(probability, 0, 1)
        {
        }

        public SaltAndPepperNoiseMatrixFilter(float probability, float lowValue, float highValue)
        {
            _probability = probability;
            _lowValue = lowValue;
            _highValue = highValue;
        }

        private float _probability;
        private float _lowValue;
        private float _highValue;

        public override Matrix Apply(Matrix input)
        {
            int i;
            int j;
            float randomNumber;
            float isHighOrLow;
            Matrix ret = new Matrix(input.RowCount, input.ColumnCount);

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    randomNumber = (float)_rand.NextDouble();

                    if (randomNumber < _probability)
                    {
                        isHighOrLow = (float)_rand.NextDouble();

                        ret[i, j] = isHighOrLow > 0.5 ? _highValue : _lowValue;//  _lowValue + y * (_highValue - _lowValue);
                    }
                    else
                    {
                        ret[i, j] = input[i, j];
                    }
                }
            }

            return ret;
        }
    }
}
