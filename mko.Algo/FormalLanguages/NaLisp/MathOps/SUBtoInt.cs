using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.MathOps
{
    public class SUBtoInt : OpBaseToInt
    {
        public SUBtoInt(params double[] operands)
            : base(Core.NameDir.Names.SUBtoInt, operands) { }

        public SUBtoInt(params int[] operands)
            : base(Core.NameDir.Names.SUBtoInt, operands) { }

        public  SUBtoInt(params Core.NaLisp[] Elements)
            : base(Core.NameDir.Names.SUBtoInt, Elements) { }

        protected override Core.NaLisp Create(Core.NaLisp[] Elements)
        {
            return new SUBtoInt(Elements);
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(EvaluatedElements, (a, b) => a - b, DebugOn);
        }
    }
}
