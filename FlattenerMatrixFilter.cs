
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
    public class FlattenerMatrixFilter : ConvolutionMatrixFilter
    {
        public FlattenerMatrixFilter(int width)
            : this(width, 1, 2, 1)
        {
        }

        public FlattenerMatrixFilter(int width, float alpha, float beta, float gamma)
            : base(GenerateMatrix(width, alpha, beta, gamma))
        {
        }

        protected static Matrix GenerateMatrix(int width, float alpha, float beta, float gamma)
        {
            Matrix mat = new Matrix(width, width);
            int i;
            int j;
            int ii = (int)Math.Ceiling(width / 2.0);

            for (i = 0; i < ii; i++)
            {
                float i2 = i - width / 2.0f+0.5f;
                i2*=i2;

                for (j = 0; j < ii; j++)
                {
                    float j2 = j - width / 2.0f+0.5f;
                    j2*=j2;

                    float s = i2 + j2; //x^2
                    float z = (float)((alpha - beta * s) * Math.Exp(-gamma * s));

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
