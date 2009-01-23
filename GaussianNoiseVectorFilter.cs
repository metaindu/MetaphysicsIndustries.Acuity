using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class GaussianNoiseVectorFilter : VectorFilter
    {
        private static Random _rand = new Random();

        public GaussianNoiseVectorFilter(float mean, float variance)
        {
            _mean = mean;
            _variance = variance;
        }

        private float _mean;
        private float _variance;

        public override Vector Apply(Vector input)
        {
            Vector ret = new Vector(input.Length);
            int i;

            for (i = 0; i < input.Length; i++)
            {

                int j;
                int n = 20;
                float x = 0;

                for (j = 0; j < n; j++)
                {
                    x += (float)_rand.NextDouble();
                }
                x -= n / 2.0f;
                x *= (float)Math.Sqrt(12.0 / n);

                x = (float)(_mean + Math.Sqrt(_variance) * x);

                ret[i] = input[i]+ x;
            }
            return ret;
        }
    }
}
