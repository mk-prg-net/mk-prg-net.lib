using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.BoolOps
{
    public class NOT : Core.NaLispNonTerminal
    {
        public NOT(Core.NaLisp[] Elements)
            : base((int)Core.NameDir.Names.NOT) 
        {
            this.Elements = Elements;
        }
        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            bool IsSubTreeValid = SubTreeValid(ElemValidationResult);

            if (Stack.ParentIs((int)Core.NameDir.Names.Pipe))
            {
                if (ElemValidationResult.Count() != 0)
                    return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstBool), Core.NameDir.Get(this).Name + ": innerhalb eines Pipe- Opearators muss die Anzahl der Operanden 0 sein");
            }
            else
            {
                if (ElemValidationResult.Count() != 1)
                    return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstBool), Core.NameDir.Get(this).Name + ": Anzahl der Operanden muss 1 sein");
            }

            if (ElemValidationResult.All(e => e.TypeOfEvaluated == typeof(Data.ConstBool)))
                return new Core.Inspector.ProtocolEntry(this, true, IsSubTreeValid, typeof(Data.ConstBool), "");
            else
                return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstBool), Core.NameDir.Get(this).Name + " darf nur Parameter haben, die zu ConstBool ausgewertet werden.");

        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return new Core.Evaluator.Result(new Data.ConstBool(!((Data.ConstBool)EvaluatedElements[0].ResultTerm).Value), new Core.Inspector.ProtocolEntry(this, true, true, typeof(Data.ConstBool), ""));
        }

        public override Core.NaLisp Clone(bool deep = true)
        {
            if (deep)
                return new NOT(Elements.Select(e => e.Clone()).ToArray());
            else
                return new NOT(null);
        }
    }
}
