
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
    public class GaussianNoiseVectorFilter : VectorFilter
    {
        private static Random _rand = new Random();

        public GaussianNoiseVectorFilter(float mean, float variance)
        {
            _mean = mean;
            _variance = variance;
        }

        private float _mean;
        private float _variance;

        public override Vector Apply(Vector input)
        {
            Vector ret = new Vector(input.Length);
            int i;

            for (i = 0; i < input.Length; i++)
            {

                int j;
                int n = 20;
                float x = 0;

                for (j = 0; j < n; j++)
                {
                    x += (float)_rand.NextDouble();
                }
                x -= n / 2.0f;
                x *= (float)Math.Sqrt(12.0 / n);

                x = (float)(_mean + Math.Sqrt(_variance) * x);

                ret[i] = input[i]+ x;
            }
            return ret;
        }
    }
}
