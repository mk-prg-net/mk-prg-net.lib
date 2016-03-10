using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Lisp
{
    public class First : Core.NaLispNonTerminal
    {
        public First(params Core.INaLisp[] Elements)
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

        public override Core.INaLisp Eval(Core.INaLisp[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return EvaluatedElements.First();
        }

        protected override Core.INaLisp Create(Core.INaLisp[] Elements)
        {
            return new First(Elements);
        }

        //public override Core.NaLisp Clone(bool deep = true)
        //{
        //    if (deep)
        //        return new First(Elements);
        //    else
        //        return new First(null);
        //}        
    }
}
