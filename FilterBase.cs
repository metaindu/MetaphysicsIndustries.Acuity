using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public abstract class FilterBase<T> : FilterBase<T, T>
        where T : Tensor
    {
    }

    [Serializable]
    public abstract class FilterBase<TInput, TOutput>
        where TInput : Tensor
        where TOutput : Tensor
    {
        public abstract TOutput Apply(TInput input);
        //protected static SolusEngine _engine = new SolusEngine();
    }
}
