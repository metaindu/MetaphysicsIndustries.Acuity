using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class SaltAndPepperNoiseMatrixFilter : MatrixFilter
    {
        public SaltAndPepperNoiseMatrixFilter(float probability)
            :  this(probability, 0, 1)
        {
        }

        public SaltAndPepperNoiseMatrixFilter(float probability, float lowValue, float highValue)
        {
            _probability = probability;
            _lowValue = lowValue;
            _highValue = highValue;
        }

        private float _probability;
        private float _lowValue;
        private float _highValue;

        public override Matrix Apply(Matrix input)
        {
            int i;
            int j;
            float randomNumber;
            float isHighOrLow;
            Matrix ret = new Matrix(input.RowCount, input.ColumnCount);

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    randomNumber = (float)_rand.NextDouble();

                    if (randomNumber < _probability)
                    {
                        isHighOrLow = (float)_rand.NextDouble();

                        ret[i, j] = isHighOrLow > 0.5 ? _highValue : _lowValue;//  _lowValue + y * (_highValue - _lowValue);
                    }
                    else
                    {
                        ret[i, j] = input[i, j];
                    }
                }
            }

            return ret;
        }
    }
}
