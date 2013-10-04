using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class VariableRotateCoordinatesMatrixFilter : RotateCoordinatesMatrixFilter
    {
        public VariableRotateCoordinatesMatrixFilter(Dictionary<string, Expression> varTable, string variable)
            : base(0)
        {
            if (varTable == null) { throw new ArgumentNullException("varTable"); }
            if (variable == null) { throw new ArgumentNullException("variable"); }

            _varTable = varTable;
            _variable = variable;
        }

        private Dictionary<string, Expression> _varTable;
        private string _variable;

        public override float Angle
        {
            get
            {
                return (float)_varTable[_variable].Eval(_varTable).Value;
            }
        }
    }
}
