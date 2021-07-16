
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
    public class GaussianBlurMatrixFilter : ConvolutionMatrixFilter
    {
        public GaussianBlurMatrixFilter(int width)
            : base(GenerateMatrix(width))
        {
        }

        protected static Matrix GenerateMatrix(int width)
        {
            Matrix mat = new Matrix(width, width);
            float sigma = width / 6.0f;
            int ii = (int)Math.Ceiling(width / 2.0);
            int i;
            int j;

            float expdenom = 2 * sigma * sigma;
            float scale = (float)(1 / (Math.PI * expdenom));
            expdenom *= -1;

            for (i = 0; i < ii; i++)
            {
                float x = i - width / 2.0f + 0.5f;
                for (j = 0; j < ii; j++)
                {
                    float y = j - width / 2.0f + 0.5f;
                    float z = (float)(scale * Math.Exp((x * x + y * y) / expdenom));
                    mat[j, i] = z;
                    mat[width - j - 1, i] = z;
                    mat[j, width - i - 1] = z;
                    mat[width - j - 1, width - i - 1] = z;
                }
            }

            mat.Autoscale();

            return mat;
        }
    }
}
