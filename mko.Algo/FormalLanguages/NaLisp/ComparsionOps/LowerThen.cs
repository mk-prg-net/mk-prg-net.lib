using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.ComparsionOps
{
    public class LowerThen : OpBase
    {
        public LowerThen(params Core.NaLisp[] Elements)
            : base(Core.NameDir.Names.LT, Elements) { }

        protected override Core.NaLisp Create(Core.NaLisp[] Elements)
        {
            return new Equal(Elements);            
        }

        protected override string GetOpName()
        {
            return "LT";
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(EvaluatedElements, StackInstance, DebugOn, (left, right) => left < right, (left, right) => left < right);
        }

    }
}
