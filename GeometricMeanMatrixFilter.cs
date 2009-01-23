using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class GeometricMeanMatrixFilter : MatrixFilter
    {
        public GeometricMeanMatrixFilter(int width)
        {
            if (width < 1) { throw new ArgumentOutOfRangeException("width must be greater than 0"); }

            _width = width;
        }

        private int _width;

        public override Matrix Apply(Matrix input)
        {
            int n;
            int m;
            int i;
            int j;
            float term;

            Matrix y = new Matrix(input.RowCount, input.ColumnCount);

            for (n = 0; n < input.RowCount; n++)
            {
                for (m = 0; m < input.ColumnCount; n++)
                {
                    term = 1;
                    for (i = 0; i < input.RowCount - 1; i++)
                    {
                        for (j = 0; j < input.ColumnCount - 1; j++)
                        {
                            term *= input[n - i, m - j];
                        }
                    }
                    y[n, m] = (float)Math.Pow(term, 1 / ((float)(input.RowCount * input.ColumnCount)));
                }
            }

            return y;
        }
    }
}
