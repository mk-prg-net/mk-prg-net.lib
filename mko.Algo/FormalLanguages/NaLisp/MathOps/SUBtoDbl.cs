using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.MathOps
{
    public class SUBtoDbl : OpBaseToDbl
    {
        public SUBtoDbl(params double[] operands)
            : base(Core.NameDir.Names.SUBtoDbl, operands) { }

        public SUBtoDbl(params int[] operands)
            : base(Core.NameDir.Names.SUBtoDbl, operands) { }

        public SUBtoDbl(params Core.NaLisp[] Elements)
            : base(Core.NameDir.Names.SUBtoDbl, Elements) { }

        protected override Core.NaLisp Create(Core.NaLisp[] Elements)
        {
            return new SUBtoDbl(Elements);
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(EvaluatedElements, (a, b) => a - b, DebugOn);
        }


    }
}
