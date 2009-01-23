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
