using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class MmsePlusAtmMatrixFilter : MinimalMeanSquareErrorMatrixFilter
    {
        public MmsePlusAtmMatrixFilter(float alphaForRejection, int windowSize, float noiseVariance)
            :base(windowSize, noiseVariance)
        {
            _alphaForRejection = alphaForRejection;
        }

        private float _alphaForRejection;
        public float AlphaForRejection
        {
            get { return _alphaForRejection; }
            set { _alphaForRejection = value; }
        }

        protected override float CalculateFinalValue(Matrix input, int row, int column, float signalMean, float ratio)
        {
            float value = input[row, column];
            int rejectionWindowSize = 3;

            List<float> measures = new List<float>(rejectionWindowSize * rejectionWindowSize);
            int alphaCount = (int)Math.Ceiling(rejectionWindowSize * rejectionWindowSize * AlphaForRejection / 2);

            DoWindowPass(input, row, column, rejectionWindowSize, AddValueToMeasures, measures);
            measures.Sort(Compare);

            bool doAtm = false;

            if (value > measures[measures.Count / 2])
            {
                //white impulse?
                if (measures.GetRange(measures.Count - alphaCount, alphaCount).Contains(value))
                {
                    //yes, replace mmse calc with atm
                    doAtm = true;
                }
            }
            else
            {
                //black impulse?
                if (measures.GetRange(0, alphaCount).Contains(value))
                {
                    //yes, replace mmse calc with atm
                    doAtm = true;
                }
            }

            if (doAtm)
            {
                if (measures.Count > alphaCount)
                {
                    measures.RemoveRange(0, alphaCount);
                }
                if (measures.Count > alphaCount)
                {
                    measures.RemoveRange(measures.Count - alphaCount, alphaCount);
                }

                if (measures.Count > 0)
                {
                    //float sum = 0;
                    //foreach (float measure in measures)
                    //{
                    //    sum += measure;
                    //}
                    //value = sum / measures.Count;
                    value = AcuityEngine.CalculateMean(measures);
                }
                else
                {
                    value = 0;
                }

                return value;
            }

            return base.CalculateFinalValue(input, row, column, signalMean, ratio);
        }
    }
}
