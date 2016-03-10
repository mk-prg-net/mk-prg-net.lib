using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Lisp
{
    public class Reverse : Core.NaLispNonTerminal
    {
        public Reverse(params Core.NaLisp[] Elements)
            : base((int)Core.NameDir.Names.Reverse)
        {
            this.Elements = Elements;
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            return new Core.Inspector.ProtocolEntry(this, true, SubTreeValid(ElemValidationResult), typeof(Tuple), "");
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return new Core.Evaluator.Result(new Tuple(EvaluatedElements.Reverse().Select(e => e.ResultTerm).ToArray()), new Core.Inspector.ProtocolEntry(this, true, SubTreeValid(EvaluatedElements.Select(e => e.ResultProtocolEntry)), typeof(Tuple), ToString()));
        }

        public override Core.NaLisp Clone(bool deep = true)
        {
            if (deep)
                return new Reverse(Elements);
            else
                return new Reverse(null);
        }
    }
}
