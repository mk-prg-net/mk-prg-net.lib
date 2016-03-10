using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.MathOps
{
    public class DIVtoInt : OpBaseToInt
    {
        public DIVtoInt(params double[] operands)
            : base(Core.NameDir.Names.DIVtoInt, operands) { }

        public DIVtoInt(params int[] operands)
            : base(Core.NameDir.Names.DIVtoInt, operands) { }


        public DIVtoInt(params Core.NaLisp[] Elements)
            : base(Core.NameDir.Names.DIVtoInt, Elements) { }


        protected override Core.NaLisp Create(Core.NaLisp[] Elements)
        {
            return new DIVtoInt(Elements);
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(EvaluatedElements, (a, b) => a / b, DebugOn);
        }

    }

}
