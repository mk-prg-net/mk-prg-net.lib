using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.MathOps
{
    public class ADDtoInt : OpBaseToInt
    {

        public ADDtoInt(params double[] operands)
            : base(Core.NameDir.Names.ADDtoInt, operands) { }

        public ADDtoInt(params int[] operands)
            : base(Core.NameDir.Names.ADDtoInt, operands) { }

        public ADDtoInt(params Core.NaLisp[] Elements)
            : base(Core.NameDir.Names.ADDtoInt, Elements) { }


        protected override Core.NaLisp Create(Core.NaLisp[] Elements)
        {
            return new ADDtoInt(Elements);
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(EvaluatedElements, (a, b) => a + b, DebugOn);
        }

    }
}
