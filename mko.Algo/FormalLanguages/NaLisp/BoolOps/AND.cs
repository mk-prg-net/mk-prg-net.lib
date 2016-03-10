using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.BoolOps
{
    public class AND : OpBaseBin
    {
        public AND(params Core.NaLisp[] Elements)
            : base(Core.NameDir.Names.AND, Elements) { }

        protected override Core.NaLisp Create(Core.NaLisp[] Elements)
        {
            return new AND(Elements);
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(EvaluatedElements, (a, b) => a && b, DebugOn);
        }
    }
}
