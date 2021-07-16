
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
    public class AlphaTrimmedMeanMatrixFilter : OrderStatisticMatrixFilter
    {
        public AlphaTrimmedMeanMatrixFilter(int windowSize, float alpha)
            : base(windowSize)
        {
            if (alpha < 0 || alpha >= 0.5) { throw new ArgumentException("alpha must be between 0 and 0.5"); }
            _alpha = Math.Max(0, Math.Min(0.5f, alpha));
        }

        private float _alpha;

        protected override float SelectValueFromOrderedMeasures(List<float> measures)
        {
            int i;
            int n;
            float sum = 0;
            int count = 0;

            n = measures.Count; // also, n should be equal to windowSize*windowSize, if no weights are used and we're not on the edge of the image

            for (i = (int)(Math.Round(_alpha * n + 1)); i < n - _alpha * n; i++)
            {
                sum += measures[i];
                count++;
            }

            return sum / count;
        }
    }
}
