using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Lisp
{
    public class First : Core.NaLispNonTerminal
    {
        public First(params Core.NaLisp[] Elements)
            : base((int)Core.NameDir.Names.First)
        {
            this.Elements = Elements;
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            if (!ElemValidationResult.Any())
                return new Core.Inspector.ProtocolEntry(this, false, false, null, "Die Eingabe von First muss mindestens ein Element enthalten");
            else
                return new Core.Inspector.ProtocolEntry(this, true, SubTreeValid(ElemValidationResult), ElemValidationResult.First().TypeOfEvaluated, "");
        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return new Core.Evaluator.Result(EvaluatedElements.First().ResultTerm, new Core.Inspector.ProtocolEntry(this, true, true, EvaluatedElements.First().ResultTerm.GetType(), ToString()));
        }

        public override Core.NaLisp Clone(bool deep = true)
        {
            if (deep)
                return new First(Elements);
            else
                return new First(null);
        }
    }
}
