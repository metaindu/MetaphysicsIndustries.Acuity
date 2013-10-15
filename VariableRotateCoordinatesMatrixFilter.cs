using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;
using Environment = MetaphysicsIndustries.Solus.Environment;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class VariableRotateCoordinatesMatrixFilter : RotateCoordinatesMatrixFilter
    {
        public VariableRotateCoordinatesMatrixFilter(Environment env, string variable)
            : base(0)
        {
            if (env == null) { throw new ArgumentNullException("env"); }
            if (variable == null) { throw new ArgumentNullException("variable"); }

            _env = env;
            _variable = variable;
        }

        private Environment _env;
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
