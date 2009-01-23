using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class ExponentMatrixFilter : ModulatorMatrixFilter
    {
        public ExponentMatrixFilter()
            : this(1)
        {
        }

        public ExponentMatrixFilter(float gamma)
        {
            _gamma = gamma;
        }

        private float _gamma;
        public float Gamma
        {
            get { return _gamma; }
            set { _gamma = value; }
        }

        public override float Modulate(float x)
        {
            return (float)Math.Pow(x, _gamma);
        }
    }
}
