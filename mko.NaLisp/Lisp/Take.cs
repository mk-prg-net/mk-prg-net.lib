using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Lisp
{
    public class Take: Core.NaLispNonTerminal
    {
        public Take(params Core.INaLisp[] Elements)
        {
            this.Elements = Elements;
        }



        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            if (ElemValidationResult.Count() < 2)
                return new Core.Inspector.ProtocolEntry(this, false, SubTreeValid(ElemValidationResult), typeof(Tuple), Name + " muss aus mindestens zwei Elementen bestehen: 1. Anzahl der zu übersrpingenden Elemente, 2. Beginn der Elementliste");
            if (ElemValidationResult.First().TypeOfEvaluated != typeof(Data.ConstValComp<int>))
                return new Core.Inspector.ProtocolEntry(this, false, SubTreeValid(ElemValidationResult), typeof(Tuple), Name + " muss als erstes Element einen Wert vom Typ ConstInt haben (Anzahl der zu überspringenden Elemente");
            else
                return new Core.Inspector.ProtocolEntry(this, true, SubTreeValid(ElemValidationResult), typeof(Tuple), ToString());

        }

        public override Core.INaLisp Eval(Core.INaLisp[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            var TakeCount = (Data.ConstVal<int>)EvaluatedElements[0];
            return new Tuple(EvaluatedElements.Skip(1).Take(TakeCount.Value).ToArray());
        }

        protected override Core.INaLisp Create(Core.INaLisp[] Elements)
        {
            return new Take(Elements);
        }

        //public override Core.NaLisp Clone(bool deep = true)
        //{
        //    if (deep)
        //        return new Take(Elements);
        //    else
        //        return new Take(null);
        //}
    }
}
