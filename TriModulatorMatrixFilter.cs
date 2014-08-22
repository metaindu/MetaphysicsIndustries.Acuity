using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;


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
            return Apply3(new STuple<Matrix, Matrix, Matrix>(input, input, input));
        }

        public Matrix Apply3(STuple<Matrix, Matrix, Matrix> input)
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
                            input.Value2[i, j],
                            input.Value3[i, j]);
                }
            }

            return output;
        }
    }
}
