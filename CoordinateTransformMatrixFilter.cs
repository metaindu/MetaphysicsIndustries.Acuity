using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Collections;

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

        protected abstract Pair<float> Modulate(float x, float y);

        public override Matrix Apply(Matrix input)
        {
            int i;
            int j;
            Matrix ret = new Matrix(input.RowCount, input.ColumnCount);
            Pair<float> pair;

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    pair = new Pair<float>(i / (float)input.RowCount, j / (float)input.ColumnCount);
                    pair = Modulate(pair.First, pair.Second);
                    if (pair.First < 0 || pair.First > 1 ||
                        pair.Second < 0 || pair.Second > 1)
                    {
                        ret[i, j] = 0;
                    }
                    else
                    {
                        ret[i, j] =
                            input[
                                (int)(Math.Round(pair.First * (input.RowCount - 1))),
                                (int)(Math.Round(pair.Second * (input.ColumnCount - 1)))];
                    }
                }
            }

            return ret;
        }
    }
}
