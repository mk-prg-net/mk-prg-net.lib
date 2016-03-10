using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.MathOps
{
    public class DIVtoDbl : OpBaseToDbl
    {
        public DIVtoDbl(params double[] operands)
            : base(Core.NameDir.Names.DIVtoDbl, operands) { }

        public DIVtoDbl(params int[] operands)
            : base(Core.NameDir.Names.DIVtoDbl, operands) { }


        public DIVtoDbl(params Core.NaLisp[] Elements)
            : base(Core.NameDir.Names.DIVtoDbl, Elements) { }


        protected override Core.NaLisp Create(Core.NaLisp[] Elements)
        {
            return new DIVtoDbl(Elements);
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(EvaluatedElements, (a, b) => a / b, DebugOn);
        }

    }

}
