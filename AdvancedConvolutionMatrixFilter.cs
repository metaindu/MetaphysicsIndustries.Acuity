using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class AdvancedConvolutionMatrixFilter : MatrixFilter
    {
        public AdvancedConvolutionMatrixFilter(Matrix convolutionKernal, BiModulator Value1Op, BiModulator Value2Op)
        {
            if (convolutionKernal == null) { throw new ArgumentNullException("convolutionKernal"); }

            _convolutionKernal = convolutionKernal;
            _Value1Op = Value1Op;
            _Value2Op = Value2Op;
        }

        Matrix _convolutionKernal;
        BiModulator _Value1Op;
        BiModulator _Value2Op;

        public override Matrix Apply(Matrix input)
        {
            return input.AdvancedConvolution(_convolutionKernal, _Value1Op, _Value2Op);
        }
    }
}
