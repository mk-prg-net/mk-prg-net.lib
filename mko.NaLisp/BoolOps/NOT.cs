using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.BoolOps
{
    public class NOT : Core.NaLispNonTerminal
    {
        public NOT(Core.INaLisp[] Elements)            
        {
            this.Elements = Elements;
        }
        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            bool IsSubTreeValid = SubTreeValid(ElemValidationResult);

            if (Stack.ParentIs(typeof(Control.Pipe)))
            {
                if (ElemValidationResult.Count() != 0)
                    return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstVal<bool>), Name + ": innerhalb eines Pipe- Opearators muss die Anzahl der Operanden 0 sein");
            }
            else
            {
                if (ElemValidationResult.Count() != 1)
                    return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstVal<bool>), Name + ": Anzahl der Operanden muss 1 sein");
            }

            if (ElemValidationResult.All(e => e.TypeOfEvaluated == typeof(Data.ConstVal<bool>)))
                return new Core.Inspector.ProtocolEntry(this, true, IsSubTreeValid, typeof(Data.ConstVal<bool>), "");
            else
                return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstVal<bool>), Name + " darf nur Parameter haben, die zu ConstBool ausgewertet werden.");

        }

        public override Core.INaLisp Eval(Core.INaLisp[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return new Data.ConstVal<bool>(!((Data.ConstVal<bool>)EvaluatedElements[0]).Value);
        }

        protected override Core.INaLisp Create(Core.INaLisp[] Elements)
        {
            return new NOT(Elements);
        }

        //public override Core.NaLisp Clone(bool deep = true)
        //{
        //    if (deep)
        //        return new NOT(Elements.Select(e => e.Clone()).ToArray());
        //    else
        //        return new NOT(null);
        //}
    }
}
