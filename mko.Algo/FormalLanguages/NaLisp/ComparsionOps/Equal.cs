using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.ComparsionOps
{
    public class Equal : OpBase
    {
        public Equal(params Core.NaLisp[] Elements)
            : base(Core.NameDir.Names.EQU, Elements) { }

        protected override Core.NaLisp Create(Core.NaLisp[] Elements)
        {
            return new Equal(Elements);            
        }

        protected override string GetOpName()
        {
            return "EQU";
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvalImpl(EvaluatedElements, StackInstance, DebugOn, (left, right) => left == right, (left, right) => left == right);
        }
    }
}
