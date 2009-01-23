using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class IntervalFitMatrixFilter : IntervalFitBaseMatrixFilter
    {
        public IntervalFitMatrixFilter()
            : base(0, 1)
        {
        }

        public override Matrix Apply(Matrix input)
        {
            Pair<float> ret;

            ret = CalcInterval(input);

            Min = ret.First;
            Max = ret.Second;

            //accumulate & fire
            return base.Apply(input);
        }

        public static Pair<float> CalcInterval(Matrix input)
        {
            int i;
            int j;

            float min = input[0, 0];
            float max = min;

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    float value = input[i, j];
                    if (!float.IsNaN(value))
                    {
                        min = Math.Min(min, value);
                        max = Math.Max(max, value);
                    }
                    else
                    {
                        value = 0;
                    }
                }
            }

            return new Pair<float>(min, max);
        }

    }
}
