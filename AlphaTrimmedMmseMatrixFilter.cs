
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
    public class AlphaTrimmedMmseMatrixFilter : OrderStatisticMmseMatrixFilter
    {
        //public AlphaTrimmedMmseMatrixFilter(int windowSize, float noiseVariance, int trimSize)
        //    : this(windowSize, noiseVariance, trimSize, trimSize)
        //{
        //}

        public AlphaTrimmedMmseMatrixFilter(int windowSize, float noiseVariance, float alpha)
            : this(windowSize, noiseVariance, alpha / 2, alpha / 2)
        {
        }

        protected AlphaTrimmedMmseMatrixFilter(int windowSize, float noiseVariance, float alphaLeft, float alphaRight)
            : this(windowSize, noiseVariance, (int)Math.Round(alphaLeft * windowSize * windowSize), (int)Math.Round(alphaRight * windowSize * windowSize))
        {
        }

        protected AlphaTrimmedMmseMatrixFilter(int windowSize, float noiseVariance, int trimLeft, int trimRight)
            : base(windowSize, noiseVariance)
        {
            _trimLeft = trimLeft;
            _trimRight = trimRight;
        }

        int _trimLeft;
        int _trimRight;

        protected override float CalculateSignalVariance(Matrix input, int row, int column, float signalMean)
        {
            float signalVariance;
            SignalVarianceInfo signalVarianceInfo = new SignalVarianceInfo();
            signalVarianceInfo.signalMean = signalMean;
            DoWindowPass(input, row, column, InternalCalcSignalVariance, signalVarianceInfo);

            signalVariance = signalVarianceInfo.sum / (signalVarianceInfo.count - 1);
            return signalVariance;
        }

        protected override float SelectValueFromOrderedMeasures(List<float> measures)
        {
            float value;

            if (measures.Count > _trimLeft)
            {
                measures.RemoveRange(0, _trimLeft);
            }
            if (measures.Count > _trimRight)
            {
                measures.RemoveRange(measures.Count - _trimRight, _trimRight);
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

    }
}
