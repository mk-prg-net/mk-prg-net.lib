using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.MathOps
{
    public class MULtoDbl : OpBaseToDbl
    {
        public MULtoDbl(params double[] operands)
            : base(Core.NameDir.Names.MULtoDbl, operands) { }

        public MULtoDbl(params int[] operands)
            : base(Core.NameDir.Names.MULtoDbl, operands) { }

        public MULtoDbl(params Core.NaLisp[] Elements)
            : base(Core.NameDir.Names.MULtoDbl, Elements) { }


        protected override Core.NaLisp Create(Core.NaLisp[] Elements)
        {
            return new MULtoDbl(Elements);
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(EvaluatedElements, (a, b) => a * b, DebugOn);
        }



    }
}
