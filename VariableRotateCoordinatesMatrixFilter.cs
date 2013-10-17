using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class VariableRotateCoordinatesMatrixFilter : RotateCoordinatesMatrixFilter
    {
        public VariableRotateCoordinatesMatrixFilter(SolusEnvironment env, string variable)
            : base(0)
        {
            if (env == null) { throw new ArgumentNullException("env"); }
            if (variable == null) { throw new ArgumentNullException("variable"); }

            _env = env;
            _variable = variable;
        }

        private SolusEnvironment _env;
        private string _variable;

        public override float Angle
        {
            get
            {
                return (float)_env.Variables[_variable].Eval(_env).Value;
            }
        }
    }
}
