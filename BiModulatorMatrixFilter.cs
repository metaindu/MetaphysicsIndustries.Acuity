using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;


namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class BiModulatorMatrixFilter : MatrixFilter
    {
        public BiModulatorMatrixFilter(BiModulator modulator)
        {
            _modulator = modulator;
        }

        BiModulator _modulator;

        public override Matrix Apply(Matrix input)
        {
            return Apply2(new STuple<Matrix, Matrix>(input, input.CloneSize()));
        }

        public Matrix Apply2(STuple<Matrix, Matrix> input)
        {
            int i;
            int j;

            Matrix output = input.Value1.CloneSize();

            for (i = 0; i < input.Value1.RowCount; i++)
            {
                for (j = 0; j < input.Value1.ColumnCount; j++)
                {
                    output[i, j] =
                        _modulator(
                            input.Value1[i, j],
                            input.Value2[i, j]);
                }
            }

            return output;
        }
    }
}
