using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Lisp
{
    public class Skip : Core.NaLispNonTerminal
    {
        public Skip(params Core.NaLisp[] Elements)
            : base((int)Core.NameDir.Names.Skip)
        {
            this.Elements = Elements;
        }



        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            if (ElemValidationResult.Count() < 2)
                return new Core.Inspector.ProtocolEntry(this, false, SubTreeValid(ElemValidationResult), typeof(Tuple), Core.NameDir.Get(this).Name + " muss aus mindestens zwei Elementen bestehen: 1. Anzahl der zu übersrpingenden Elemente, 2. Beginn der Elementliste");
            if (ElemValidationResult.First().TypeOfEvaluated != typeof(Data.ConstInt))
                return new Core.Inspector.ProtocolEntry(this, false, SubTreeValid(ElemValidationResult), typeof(Tuple), Core.NameDir.Get(this).Name + " muss als erstes Element einen Wert vom Typ ConstInt haben (Anzahl der zu überspringenden Elemente");
            else
                return new Core.Inspector.ProtocolEntry(this, true, SubTreeValid(ElemValidationResult), typeof(Tuple), ToString());

        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            var SkipCount = (Data.ConstInt)EvaluatedElements[0].ResultTerm;
            return new Core.Evaluator.Result(new Tuple(EvaluatedElements.Skip(1+SkipCount.Value).Select(e => e.ResultTerm).ToArray()), new Core.Inspector.ProtocolEntry(this, true, SubTreeValid(EvaluatedElements.Select(e => e.ResultProtocolEntry)), typeof(Tuple), ToString()));
        }

        public override Core.NaLisp Clone(bool deep = true)
        {
            if (deep)
                return new Skip(Elements);
            else
                return new Skip(null);
        }
    }
}
