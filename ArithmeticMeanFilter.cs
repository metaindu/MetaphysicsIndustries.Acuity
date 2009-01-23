using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class ArithmeticMeanFilter : ConvolutionMatrixFilter
    {
        public ArithmeticMeanFilter(int width)
            : base(Matrix.FromUniform(width, width, 1 / (float)(width * width)))
        {
            _width = width;
        }

        int _width;

        public override Matrix Apply(Matrix input)
        {
            return RowAccumulateAverage(input, _width);
        }

        public static Matrix RowAccumulateAverage(Matrix input, int windowSize)
        {
            // improved averaging
            // two-stage "separable" averaging
            // O(n^2) vs O(n^4)

            Matrix output = input.CloneSize();

            Matrix temp = input.CloneSize();

            int delta = windowSize / 2;
            int delta2 = -delta + windowSize - 1;

            int r;
            int c;

            for (r = 0; r < input.RowCount; r++)
            {
                float sum = 0;
                for (c = 0; c < delta2; c++)
                {
                    sum += input[r, c];
                }

                for (c = 0; c < input.ColumnCount; c++)
                {
                    if (c + delta2 < input.ColumnCount) sum += input[r, c + delta2];
                    if (c - delta - 1 >= 0) sum -= input[r, c - delta - 1];
                    temp[r, c] = sum;
                }
            }

            int nr;
            int nc;

            for (c = 0; c < input.ColumnCount; c++)
            {
                float sum = 0;
                for (r = 0; r < delta2; r++)
                {
                    sum += temp[r, c];
                }

                if (c < delta)
                    nc = windowSize - delta + c;
                else if (c >= input.ColumnCount - delta2)
                    nc = windowSize - delta2 + input.ColumnCount - c - 1;
                else
                    nc = windowSize;

                for (r = 0; r < input.RowCount; r++)
                {
                    if (r < delta)
                        nr = windowSize - delta + r;
                    else if (r >= input.RowCount - delta2)
                        nr = windowSize - delta2 + input.RowCount - r - 1;
                    else
                        nr = windowSize;

                    int n = nc * nr;

                    if (r + delta2 < input.RowCount) sum += temp[r + delta2, c];
                    if (r - delta - 1 >= 0) sum -= temp[r - delta - 1, c];

                    output[r, c] = sum / n;
                }
            }

            return output;
        }






    }
}
