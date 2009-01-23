using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class SignalVarianceMeasure
    {
        public float Measure(IEnumerable<float> input)
        {
            float sum = 0;
            float signalMean = (new SignalMeanMeasure()).Measure(input);
            int count = 0;

            foreach (float value in input)
            {
                float value2 = value - signalMean;

                sum += value2 * value2;
                count++;
            }

            return sum / (count - 1);
        }
    }
}
