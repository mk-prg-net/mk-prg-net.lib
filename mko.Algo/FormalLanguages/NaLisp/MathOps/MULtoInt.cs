using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.MathOps
{
    public class MULtoInt : OpBaseToInt
    {
        public MULtoInt(params double[] operands)
            : base(Core.NameDir.Names.MULtoInt, operands) { }

        public MULtoInt(params int[] operands)
            : base(Core.NameDir.Names.MULtoInt, operands) { }

        public MULtoInt(params Core.NaLisp[] Elements)
            : base(Core.NameDir.Names.MULtoInt, Elements) { }


        protected override Core.NaLisp Create(Core.NaLisp[] Elements)
        {
            return new MULtoInt(Elements);
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(EvaluatedElements, (a, b) => a * b, DebugOn);
        }

    }
}
