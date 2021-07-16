
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
    public class AlphaTrimmedMmsePlusAtmMatrixFilter : AlphaTrimmedMmseMatrixFilter
    {
        public AlphaTrimmedMmsePlusAtmMatrixFilter(int windowSize, float noiseVariance, float alpha)
            : base(windowSize, noiseVariance, alpha)
            //(int)Math.Round(alpha * windowSize * windowSize / 2.0), (int)Math.Round(alpha * windowSize * windowSize / 2.0))
        {
        }

        //protected AlphaTrimmedMmsePlusAtmMatrixFilter(int windowSize, float noiseVariance, int trimLeft, int trimRight)
        //    : base(windowSize, noiseVariance, trimLeft, trimRight)
        //{
        //}

        protected override float CalculateFinalValue(Matrix input, int row, int column, float signalMean, float ratio)
        {
            float value = input[row, column];

            List<float> measures = new List<float>(WindowSize * WindowSize);
            int trimLeft = 2;
            int trimRight = 2;

            DoWindowPass(input, row, column, 3, AddValueToMeasures, measures);
            measures.Sort(Compare);

            bool doAtm = false;

            if (value > measures[measures.Count / 2])
            {
                //white impulse?
                if (measures.GetRange(measures.Count - trimRight, trimRight).Contains(value))
                {
                    //yes, replace mmse calc with atm
                    doAtm = true;
                }
            }
            else
            {
                //black impulse?
                if (measures.GetRange(0, trimLeft).Contains(value))
                {
                    //yes, replace mmse calc with atm
                    doAtm = true;
                }
            }

            if (doAtm)
            {
                if (measures.Count > trimLeft)
                {
                    measures.RemoveRange(0, trimLeft);
                }
                if (measures.Count > trimRight)
                {
                    measures.RemoveRange(measures.Count - trimRight, trimRight);
                }

                if (measures.Count > 0)
                {
                    float sum = 0;
                    foreach (float measure in measures)
                    {
                        sum += measure;
                    }
                    value = sum / measures.Count;
                }
                else
                {
                    value = 0;
                }

                return value;
            }

            return base.CalculateFinalValue(input, row, column, signalMean, ratio);
        }
    }
}
