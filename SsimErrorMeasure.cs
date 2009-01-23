using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class SsimErrorMeasure
    {

        public SsimErrorMeasure()
        {
            int windowSize = 7;

            _windowSize = windowSize;
        }

        private int _windowSize;

        public float Measure(Matrix x, Matrix y)
        {
            if (x == null) { throw new ArgumentNullException("a"); }
            if (y == null) { throw new ArgumentNullException("b"); }
            if (x.RowCount != y.RowCount ||
                x.ColumnCount != y.ColumnCount)
            {
                throw new ArgumentException("Matrix sizes do not match");
            }

            Matrix map = GenerateMap(x, y);

            float mssim = CalculateMeasureFromMap(map);

            return mssim;
        }

        public static float CalculateMeasureFromMap(Matrix map)
        {
            float mssim = 0;
            foreach (float value in map)
            {
                mssim += value;
            }

            mssim /= map.Count;
            return mssim;
        }

        public Matrix GenerateMap(Matrix x, Matrix y)
        {
            return GenerateMap(x, y, _windowSize);
        }

        public static Matrix GenerateMap(Matrix x, Matrix y, int windowSize)
        {
            Matrix map = x.CloneSize();

            int r;
            int c;

            float L = 1;

            float k1 = 0.01f;
            float k2 = 0.03f;

            float c1 = k1 * k1 * L * L;
            float c2 = k2 * k2 * L * L;
            float c3 = c2 / 2;

            float alpha = 1;
            float beta = 1;
            float gamma = 1;




            for (r = 0; r < x.RowCount; r++)
            {
                for (c = 0; c < x.ColumnCount; c++)
                {
                    float xMean = 0;
                    float yMean = 0;
                    float xSigma = 0;
                    float ySigma = 0;
                    float sigma2 = 0;

                    int i;
                    int j;
                    int w2 = windowSize / 2;

                    int count;

                    //Pair<float> pair = new Pair<float>(0,0);

                    count = 0;
                    for (i = Math.Max(r - w2, 0); i <= r + w2; i++)
                    {
                        if (i >= x.RowCount) { break; }

                        for (j = Math.Max(c - w2, 0); j <= c + w2; j++)
                        {
                            if (j >= x.ColumnCount) { break; }

                            xMean += x[i, j];
                            yMean += y[i, j];

                            count++;
                        }
                    }

                    xMean /= count;
                    yMean /= count;

                    count = 0;
                    for (i = Math.Max(r - w2, 0); i <= r + w2; i++)
                    {
                        if (i >= x.RowCount) { break; }

                        for (j = Math.Max(c - w2, 0); j <= c + w2; j++)
                        {
                            if (j >= x.ColumnCount) { break; }

                            float xValue = x[i, j] - xMean;
                            xSigma += xValue * xValue;

                            float yValue = y[i, j] - yMean;
                            ySigma += yValue * yValue;

                            sigma2 += xValue * yValue;

                            count++;
                        }
                    }

                    xSigma = (float)Math.Sqrt(xSigma / (count - 1));
                    ySigma = (float)Math.Sqrt(ySigma / (count - 1));
                    sigma2 /= (count - 1);


                    float lum = (2 * xMean * yMean + c1) / (xMean * xMean + yMean * yMean + c1);
                    float con = (2 * xSigma * ySigma + c2) / (xSigma * xSigma + ySigma * ySigma + c2);
                    float str = (sigma2 + c3) / (xSigma * ySigma + c3);

                    float ssim = (float)(Math.Pow(lum, alpha) *
                                    Math.Pow(con, beta) *
                                    Math.Pow(str, gamma));

                    map[r, c] = ssim;
                }
            }
            return map;
        }

        public static float Measure(Matrix x, Matrix y, int windowSize)
        {
            return CalculateMeasureFromMap(GenerateMap(x, y, windowSize));
        }
    }
}
