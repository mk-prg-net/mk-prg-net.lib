using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Lisp
{
    public class Last : Core.NaLispNonTerminal
    {
        public Last(params Core.INaLisp[] Elements)
        {
            this.Elements = Elements;
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            if (!ElemValidationResult.Any())
                return new Core.Inspector.ProtocolEntry(this, false, false, null, "Die Eingabe von Last muss mindestens ein Element enthalten");
            else
                return new Core.Inspector.ProtocolEntry(this, true, SubTreeValid(ElemValidationResult), ElemValidationResult.Last().TypeOfEvaluated, "");
        }

        public override Core.INaLisp Eval(Core.INaLisp[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvaluatedElements.Last();
        }

        protected override Core.INaLisp Create(Core.INaLisp[] Elements)
        {
            return new Last(Elements);
        }

        //public override Core.NaLisp Clone(bool deep = true)
        //{
        //    if (deep)
        //        return new Last(Elements);
        //    else
        //        return new Last(null);
        //}

    }
}
