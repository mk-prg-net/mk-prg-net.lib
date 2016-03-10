using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Lisp
{
    public class Reverse : Core.NaLispNonTerminal
    {
        public Reverse(params Core.INaLisp[] Elements)
        {
            this.Elements = Elements;
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            return new Core.Inspector.ProtocolEntry(this, true, SubTreeValid(ElemValidationResult), typeof(Tuple), "");
        }

        public override Core.INaLisp Eval(Core.INaLisp[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return new Tuple(EvaluatedElements.Reverse().ToArray());
        }

        protected override Core.INaLisp Create(Core.INaLisp[] Elements)
        {
            return new Reverse(Elements);
        }

        //public override Core.NaLisp Clone(bool deep = true)
        //{
        //    if (deep)
        //        return new Reverse(Elements);
        //    else
        //        return new Reverse(null);
        //}
    }
}
