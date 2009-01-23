using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class ThresholdMatrixFilter : MatrixFilter
    {
        public ThresholdMatrixFilter(float threshold)
        {
            _threshold = threshold;
        }

        private float _threshold;
        public float Threshold
        {
            get { return _threshold; }
        }

        public override Matrix Apply(Matrix input)
        {
            Matrix m = input.Clone();

            m.ApplyToAll(ApplyThreshold);

            return m;
        }

        public float ApplyThreshold(float x)
        {
            return x >= _threshold ? 1 : 0;
        }
    }
}
