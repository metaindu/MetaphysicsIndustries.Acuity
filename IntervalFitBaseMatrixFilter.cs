using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class IntervalFitBaseMatrixFilter : MatrixFilter
    {
        public IntervalFitBaseMatrixFilter(float min, float max)
        {
            _min = min;
            _max = max;
        }

        float _min;
        public float Min
        {
            get { return _min; }
            protected set { _min = value; }
        }
	
        float _max;
        public float Max
        {
            get { return _max; }
            protected set { _max = value; }
        }

        public override Matrix Apply(Matrix input)
        {
            return IntervalFit(input, Min, Max);
        }

        public static Matrix IntervalFit(Matrix input, float min, float max)
        {
            int i;
            int j;

            Matrix output = input.CloneSize();

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    float value = input[i, j];
                    if (float.IsNaN(value))
                    {
                        value = min;
                    }
                    output[i, j] = AcuityEngine.IntervalFit(value, min, max);
                }
            }

            return output;
        }
    }
}
