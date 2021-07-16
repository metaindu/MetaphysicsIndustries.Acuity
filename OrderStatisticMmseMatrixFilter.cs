
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
    public abstract class OrderStatisticMmseMatrixFilter : MinimalMeanSquareErrorMatrixFilter
    {
        public OrderStatisticMmseMatrixFilter(int windowSize, float noiseVariance)
            : base(windowSize, noiseVariance)
        {
        }

        protected override float CalculateSignalMean(Matrix input, int row, int column)
        {
            List<float> measures = new List<float>(WindowSize * WindowSize);

            DoWindowPass(input, row, column, AddValueToMeasures, measures);

            return SelectValueFromMeasures(measures);
        }

        protected virtual float SelectValueFromMeasures(List<float> measures)
        {
            List<float> measures2 = new List<float>(measures);
            measures2.Sort(Compare);
            return SelectValueFromOrderedMeasures(measures);
        }

        protected abstract float SelectValueFromOrderedMeasures(List<float> measures);
    }
}
