using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Control
{
    public class IfThen : Core.NaLispNonTerminal
    {
        public IfThen(params Core.NaLisp[] Elements)
            : base((int)Core.NameDir.Names.IfThen)
        {
            Debug.Assert(Elements.Length == 3, "IfThen muss genau 3 elemente haben");
            this.Elements = Elements;
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            if (Elements.Length != 3)
            {
                return new Core.Inspector.ProtocolEntry(this, false, true, null, "IfThen muss genau 3 Elementehaben");
            }
            if (ElemValidationResult[0].TypeOfEvaluated != typeof(Data.ConstBool))
                return new Core.Inspector.ProtocolEntry(this, false, true, null, "IfThen, 1. Element muss zu ConstBool evaluierbar sein");

            if (ElemValidationResult[1].TypeOfEvaluated != ElemValidationResult[2].TypeOfEvaluated)
                return new Core.Inspector.ProtocolEntry(this, false, true, null, "IfThen, Auswertung des 2. und 3. Element müssen denselben Rückgabetyp liefern");

            return new Core.Inspector.ProtocolEntry(this, true, true, ElemValidationResult[1].TypeOfEvaluated,
                "IfThen(" +
                ElemValidationResult[0].NaLispTreeNode.ToString() + ", " +
                ElemValidationResult[0].NaLispTreeNode.ToString() + ", " +
                ElemValidationResult[0].NaLispTreeNode.ToString() + ")");

        }

        public override Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            throw new NotImplementedException();
        }

        public Core.NaLisp Condition
        {
            get
            {
                return Elements[0];
            }
        }

        public Core.NaLisp IfTrue
        {
            get
            {
                return Elements[1];
            }
        }

        public Core.NaLisp IfFalse
        {
            get
            {
                return Elements[2];
            }
        }



        public override Core.NaLisp Clone(bool deep = true)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {       
            var Inspector = new Core.Inspector();            
            var pe = Inspector.Validate(this);
            if (pe.IsCurrentValid)
            {
                return "IfThen(" + Condition.ToString() + ", " + IfTrue.ToString() + ", " + IfFalse.ToString() + ")";
            }
            else
            {
                return "IfThen: " + pe.Description;
            }
        }
    }
}
