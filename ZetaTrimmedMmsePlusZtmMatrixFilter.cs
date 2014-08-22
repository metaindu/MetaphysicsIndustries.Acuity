using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class ZetaTrimmedMmsePlusZtmMatrixFilter : ZetaTrimmedMmseMatrixFilter
    {
        public ZetaTrimmedMmsePlusZtmMatrixFilter(int windowSize, float noiseVariance, float zeta)//, float Value2Zeta)
            : base(windowSize, noiseVariance, zeta)
        {
        }

        protected override float CalculateFinalValue(Matrix input, int row, int column, float signalMean2, float signalVariance2, float noiseVariance)
        {

            // impulse rejection stage

            float ratio = noiseVariance / signalVariance2;
            float value = input[row, column];
            float z = (float)Math.Abs((value - signalMean2) / Math.Sqrt(signalVariance2));

            if (z > Zeta)
            {
                List<float> measures = new List<float>(WindowSize * WindowSize);
                int trimLeft = 2;
                int trimRight = 2;

                DoWindowPass(input, row, column, 3, AddValueToMeasures, measures);
                measures.Sort(Compare);
                if (measures.Count > trimLeft)
                {
                    measures.RemoveRange(0, trimLeft);
                }
                if (measures.Count > trimRight)
                {
                    measures.RemoveRange(measures.Count - trimRight, trimRight);
                }

                if (measures.Count > 0)
                {
                    float sum = 0;
                    foreach (float value2 in measures)
                    {
                        sum += value2;
                    }
                    value = sum / measures.Count;
                }
                else
                {
                    return signalMean2;
                }
            }

            return CalculateFinalValue(value, signalMean2, ratio);
        }
    }
}
