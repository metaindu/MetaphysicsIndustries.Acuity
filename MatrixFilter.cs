using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public abstract class MatrixFilter : FilterBase<Matrix>
    {
        protected static Random _rand = new Random();
    }
}
