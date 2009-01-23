using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class AlphaTrimmedDualBellEdgeDetectorMatrixFilter : DualBellEdgeDetectorMatrixFilter
    {
        public AlphaTrimmedDualBellEdgeDetectorMatrixFilter(float alpha, int windowSize)
            : base(windowSize)
        {
            _alpha = alpha;
        }

        private float _alpha;
        public float Alpha
        {
            get { return _alpha; }
            set { _alpha = value; }
        }

        protected override float SelectValueFromOrderedMeasures(List<float> measures)
        {
            List<float> measures2 = new List<float>(measures);

            int alphaCount = (int)Math.Ceiling(WindowSize * WindowSize * Alpha / 2);

            if (measures2.Count > alphaCount)
            {
                measures2.RemoveRange(0, alphaCount);
            }
            if (measures2.Count > alphaCount)
            {
                measures2.RemoveRange(measures2.Count - alphaCount, alphaCount);
            }

            return base.SelectValueFromOrderedMeasures(measures2);
        }
    }
}
