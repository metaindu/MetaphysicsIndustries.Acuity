using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class SineWaveGeneratorVectorFilter : VectorFilter
    {
        public SineWaveGeneratorVectorFilter(float amplitude, float frequency, float phaseOffset)
        {
            _amplitude = amplitude;
            _frequency = frequency;
            _phaseOffset = phaseOffset;
        }

        float _amplitude;
        float _frequency;
        float _phaseOffset;

        public override Vector Apply(Vector input)
        {
            int i;
            Vector output = new Vector(input.Length);

            for (i = 0; i < input.Length; i++)
            {
                output[i] += (float)(_amplitude * Math.Sin(2 * Math.PI * _frequency * i + _phaseOffset));
            }

            return output;
        }
    }
}
