using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.MathOps
{
    public class ADDtoDbl : OpBaseToDbl
    {

        public ADDtoDbl(params double[] operands)
            : base(Core.NameDir.Names.ADDtoDbl, operands) { }

        public ADDtoDbl(params int[] operands)
            : base(Core.NameDir.Names.ADDtoDbl, operands) { }

        public  ADDtoDbl(params Core.NaLisp[] Elements)
            : base(Core.NameDir.Names.ADDtoDbl, Elements) { }


        protected override Core.NaLisp Create(Core.NaLisp[] Elements)
        {
            return new ADDtoDbl(Elements);
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(EvaluatedElements, (a, b) => a + b, DebugOn);
        }

    }
}
