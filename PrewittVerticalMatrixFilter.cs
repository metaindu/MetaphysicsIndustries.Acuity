using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class PrewittVerticalMatrixFilter : ConvolutionMatrixFilter
    {
        public PrewittVerticalMatrixFilter()
            : base(GenerateMatrix())
        {
        }

        protected static Matrix GenerateMatrix()
        {
            Matrix y = new Matrix(3, 3);

            y[0, 0] = 1;
            y[0, 1] = 1;
            y[0, 2] = 1;
            y[2, 0] = -1;
            y[2, 1] = -1;
            y[2, 2] = -1;

            y.ApplyToAll((new AcuityEngine.MultiplyModulator(1 / 3.0f)).Modulate);

            return y;
        }
    }
}
