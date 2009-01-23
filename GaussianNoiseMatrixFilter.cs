using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class GaussianNoiseMatrixFilter : MatrixFilter
    {
        public GaussianNoiseMatrixFilter(
            //float mean,
            float variance)
        {
            //_mean = mean;
            _variance = variance;
        }

        //private float _mean;
        private float _variance;

        public override Matrix Apply(Matrix input)
        {
            Matrix ret = new Matrix(input.RowCount, input.ColumnCount);
            int i;
            int j;

            float stdev = (float)Math.Sqrt(_variance);

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    int k;
                    int n = 20;
                    float x = 0;

                    for (k = 0; k < n; k++)
                    {
                        x += (float)_rand.NextDouble();
                    }
                    x -= n / 2.0f;
                    x *= (float)Math.Sqrt(12.0 / n);

                    x = 
                        //_mean + 
                        stdev * x;

                    ret[i,j] = input[i,j] + x;
                }
            }
            return ret;
        }
    }
}
