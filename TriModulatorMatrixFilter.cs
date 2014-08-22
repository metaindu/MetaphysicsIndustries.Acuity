using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class TriModulatorMatrixFilter : MatrixFilter
    {
        public TriModulatorMatrixFilter(TriModulator modulator)
        {
            _modulator = modulator;
        }

        TriModulator _modulator;

        public override Matrix Apply(Matrix input)
        {
            return Apply3(new Triple<Matrix>(input, input, input));
        }

        public Matrix Apply3(Triple<Matrix> input)
        {
            int i;
            int j;

            Matrix output = input.First.CloneSize();

            for (i = 0; i < input.First.RowCount; i++)
            {
                for (j = 0; j < input.First.ColumnCount; j++)
                {
                    output[i, j] =
                        _modulator(
                            input.First[i, j],
                            input.Second[i, j],
                            input.Third[i, j]);
                }
            }

            return output;
        }
    }
}
