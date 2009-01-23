using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class IdentityFilter : MatrixFilter
    {
        public override Matrix Apply(Matrix input)
        {
            return input.Clone();
        }
    }
}
