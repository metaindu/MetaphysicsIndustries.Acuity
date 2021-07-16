
/*
 *  MetaphysicsIndustries.Acuity
 *  Copyright (C) 2009-2021 Metaphysics Industries, Inc., Richard Sartor
 *
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 3 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 *  USA
 *
 */

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
