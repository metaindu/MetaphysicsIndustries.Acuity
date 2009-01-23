using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public abstract class Tensor : IEnumerable<float>
    {
        public abstract IEnumerator<float> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract void ApplyToAll(Modulator mod);
    }
}
