using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.BoolOps
{
    public class OR : OpBaseBin
    {
        public OR(params Core.INaLisp[] Elements)
            : base(Elements) { }

        protected override Core.INaLisp Create(Core.INaLisp[] Elements)
        {
            return new AND(Elements);
        }

        public override Core.INaLisp Eval(Core.INaLisp[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(EvaluatedElements, (a, b) => a || b, DebugOn);
        }
    }
}
