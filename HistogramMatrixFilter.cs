using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class HistogramMatrixFilter : MatrixFilter
    {
        public override Matrix Apply(Matrix input)
        {
            Matrix result = Matrix.FromUniform(256, 256, 1);

            int[] counts = new int[256];

            foreach (float value in input)
            {
                float value2 = 255 * Math.Max(0, Math.Min(1, value));

                counts[(int)Math.Round(value2)]++;
            }

            int max = counts[0];
            foreach (int value in counts)
            {
                max = (int)Math.Max(max, value);
            }

            int col;
            for (col = 0; col < 256; col++)
            {
                int height = 255 * counts[col] / max;
                int row;
                for (row = 0; row <= height; row++)
                {
                    result[255 - row, col] = 0;
                }
            }

            return result;
        }
    }
}
