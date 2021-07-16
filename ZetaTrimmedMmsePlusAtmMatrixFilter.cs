
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
    public class ZetaTrimmedMmsePlusAtmMatrixFilter : ZetaTrimmedMmseMatrixFilter
    {
        public ZetaTrimmedMmsePlusAtmMatrixFilter(int windowSize, float noiseVariance, float zeta, float alpha)
            : base(windowSize, noiseVariance, zeta)
        {
            _alpha = alpha;
        }

        private float _alpha;
        public float Alpha
        {
            get { return _alpha; }
            set { _alpha = value; }
        }


        protected override float CalculateFinalValue(Matrix input, int row, int column, float signalMean2, float signalVariance2, float noiseVariance)
        {

            // impulse rejection stage

            float ratio = noiseVariance / signalVariance2;
            float value = input[row, column];

            List<float> measures = new List<float>(WindowSize * WindowSize);
            int trim = (int)Math.Ceiling(Alpha * WindowSize * WindowSize / 2.0);

            DoWindowPass(input, row, column, 3, AddValueToMeasures, measures);
            measures.Sort(Compare);

            int i = measures.BinarySearch(value);
            if (i < 0) { i = ~i; }
            if (i < trim && i >= measures.Count - trim)
            {
                if (measures.Count > trim << 1)
                {
                    float sum = 0;
                    for (i = trim; i < measures.Count - trim; i++)
                    {
                        sum += measures[i];
                    }
                    value = sum / measures.Count;
                }
                else
                {
                    value = 0;
                }

                return value;
            }

            return CalculateFinalValue(value, signalMean2, ratio);
        }
    }
}
