using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;


namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public abstract class CoordinateTransformMatrixFilter : MatrixFilter
    {
        //public CoordinateTransformMatrixFilter(PairModulator mod)
        //{
        //    _mod = mod;
        //}

        //private PairModulator _mod;

        protected abstract STuple<float, float> Modulate(float x, float y);

        public override Matrix Apply(Matrix input)
        {
            int i;
            int j;
            Matrix ret = new Matrix(input.RowCount, input.ColumnCount);
            STuple<float, float> pair;

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    pair = new STuple<float, float>(i / (float)input.RowCount, j / (float)input.ColumnCount);
                    pair = Modulate(pair.Value1, pair.Value2);
                    if (pair.Value1 < 0 || pair.Value1 > 1 ||
                        pair.Value2 < 0 || pair.Value2 > 1)
                    {
                        ret[i, j] = 0;
                    }
                    else
                    {
                        ret[i, j] =
                            input[
                                (int)(Math.Round(pair.Value1 * (input.RowCount - 1))),
                                (int)(Math.Round(pair.Value2 * (input.ColumnCount - 1)))];
                    }
                }
            }

            return ret;
        }
    }
}
