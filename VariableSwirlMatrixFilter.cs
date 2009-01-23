using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Solus;

namespace MetaphysicsIndustries.Acuity
{
    [Serializable]
    public class VariableSwirlMatrixFilter : SwirlMatrixFilter
    {
        public VariableSwirlMatrixFilter(VariableTable varTable, Variable variable)
            : base(0)
        {
            if (varTable == null) { throw new ArgumentNullException("varTable"); }
            if (variable == null) { throw new ArgumentNullException("variable"); }

            _varTable = varTable;
            _variable = variable;
        }

        private VariableTable _varTable;
        private Variable _variable;

        public override float Factor
        {
            get
            {
                return (float)_varTable[_variable].Eval(_varTable).Value;
            }
        }
    }
}
