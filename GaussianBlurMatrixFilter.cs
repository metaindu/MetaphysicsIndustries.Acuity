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
