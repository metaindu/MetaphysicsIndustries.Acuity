using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class SobelMatrixFilter : MatrixFilter
    {
        public override Matrix Apply(Matrix input)
        {
            return GenerateMagnitudeMap(input);
        }

        public static Matrix GenerateMagnitudeMap(Matrix input)
        {
            return GenerateMaps(input, true, false).First;
        }

        public static Matrix GenerateDirectionMap(Matrix input)
        {
            return GenerateMaps(input, false, true).Second;
        }

        public static Pair<Matrix> GenerateMaps(Matrix input)
        {
            return GenerateMaps(input, true, true);
        }

        protected static Pair<Matrix> GenerateMaps(Matrix input, bool calcMagnitude, bool calcDirection)
        {
            Matrix x;
            Matrix y;
            GenerateGradients(input, out x, out y);

            return GenerateMaps(input, calcMagnitude, calcDirection, x, y);
        }

        public static Pair<Matrix> GenerateMaps(Matrix input, bool calcMagnitude, bool calcDirection, Matrix x, Matrix y)
        {

            Matrix magnitudeMap = input.CloneSize();
            Matrix directionMap = input.CloneSize();
            int r;
            int c;

            for (r = 0; r < input.RowCount; r++)
            {
                for (c = 0; c < input.ColumnCount; c++)
                {
                    float xx = x[r, c];
                    float yy = y[r, c];

                    if (calcMagnitude)
                    {
                        magnitudeMap[r, c] = (float)Math.Sqrt(xx * xx + yy * yy);
                    }
                    if (calcDirection)
                    {
                        directionMap[r, c] = (float)Math.Atan2(yy, xx);
                    }
                }
            }

            return new Pair<Matrix>(magnitudeMap, directionMap);
        }

        private static ExpandEdgeMatrixFilter _expandEdgeFilter = new ExpandEdgeMatrixFilter(1);

        static Vector _a = new Vector(3,1,2,1);
        static Vector _b = new Vector(3,1,0,-1);
        static SeparableConvolutionMatrixFilter _sepfilterx = new SeparableConvolutionMatrixFilter(_a, _b);
        static SeparableConvolutionMatrixFilter _sepfiltery = new SeparableConvolutionMatrixFilter(_b, _a);

        public static void GenerateGradients(Matrix input, out Matrix x, out Matrix y)
        {
            int time = Environment.TickCount;
            x = _sepfilterx.Apply(input);
            time = Environment.TickCount - time;

            time = Environment.TickCount;
            y = _sepfiltery.Apply(input);
            time = Environment.TickCount - time;

        }
    }

}
