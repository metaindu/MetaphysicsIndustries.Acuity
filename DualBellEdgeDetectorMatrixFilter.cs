using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class DualBellEdgeDetectorMatrixFilter : OrderStatisticMatrixFilter
    {
        public DualBellEdgeDetectorMatrixFilter(int windowSize)
            : base(windowSize)
        {
        }

        //public DualBellEdgeDetectorMatrixFilter(int windowSize, float gamma)
        //    : base(windowSize)
        //{
        //    _gamma = gamma;
        //}

        //private float _gamma;
        //public float Gamma
        //{
        //    get { return _gamma; }
        //    set { _gamma = value; }
        //}


        protected override float SelectValueFromOrderedMeasures(List<float> measures)
        {
            if (measures.Count < 1)
            {
                return 0;
            }

            int splitIndex = 0;

            float signalMean = AcuityEngine.CalculateMean(measures);
            float signalVariance = AcuityEngine.CalculateVariance(measures, signalMean);
            float signalStdev = (float)Math.Sqrt(signalVariance);

            int i;
            int start = -1;
            int count = 1;
            for (i = 0; i < measures.Count; i++)
            {
                if (measures[i] == signalMean)
                {
                    if (start < 0)
                    {
                        start = i;
                    }
                    else
                    {
                        count++;
                    }
                }
                else if (measures[i] > signalMean)
                {
                    if (start < 0)
                    {
                        start = i;
                    }

                    break;
                }
            }

            if (start < 0)
            {
                start = measures.Count - 1;
            }

            splitIndex = start + count / 2;

            float value;

            if (splitIndex < 1)
            {
                value = 0;
            }
            else
            {
                //List<float> lower = measures.GetRange(0, splitIndex);
                //List<float> higher = measures.GetRange(splitIndex, measures.Count - splitIndex);

                float lowerMean;
                //float lowerVariance;
                float higherMean;
                //float higherVariance;

                //if (lower.Count < 1 || higher.Count < 1)
                //{
                //    value = 0;
                //}
                //else 
                    if (signalVariance == 0)
                {
                    value = 0;
                }
                else
                {
                    lowerMean = AcuityEngine.CalculateMean(measures, 0, splitIndex);
                    //lowerVariance = SolusEngine.CalculateVariance(measures, lowerMean, 0, splitIndex);
                    higherMean = AcuityEngine.CalculateMean(measures, splitIndex, measures.Count - splitIndex);
                    //higherVariance = SolusEngine.CalculateVariance(measures, higherMean, splitIndex, measures.Count - splitIndex);

                    value = higherMean - lowerMean;
                    //value = Math.Abs((lowerMean - higherMean)) / Math.Sqrt(lowerVariance * higherVariance);
                    //value = 1 - higherVariance * lowerVariance / (signalVariance * signalVariance);
                    //value = (higherMean - lowerMean) / signalStdev;
                    //value = Math.Sqrt(higherMean - lowerMean);

                    //value = 1 - SolusEngine.CalculateNormalDistributionOverlap(lowerMean, lowerVariance, higherMean, higherVariance);
                    //value = 1 - Math.Sqrt(lowerVariance * higherVariance) / signalVariance;
                }

                if (float.IsNaN(value))
                {
                    value = 0;
                }
            }

            return value;// Math.Pow(value, Gamma);
        }
    }
}
