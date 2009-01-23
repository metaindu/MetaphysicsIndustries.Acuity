using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class SignalMeanMeasure
    {
        public float Measure(IEnumerable<float> input)
        {
            float sum = 0;
            int count = 0;

            foreach (float value in input)
            {
                sum += value;
                count++;
            }

            return sum / count;
        }
    }
}
