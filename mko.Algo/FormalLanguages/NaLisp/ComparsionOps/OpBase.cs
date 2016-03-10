using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.ComparsionOps
{
    public abstract class OpBase : Core.NaLispNonTerminal
    {
        public OpBase(Core.NameDir.Names Name, Core.NaLisp[] Elements)
            : base((int)Name)
        {           
            this.Elements = Elements;
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            if (!SubTreeValid(ElemValidationResult))
                return new Core.Inspector.ProtocolEntry(this, false, false, typeof(Data.ConstBool), GetOpName() + " Argumente sind ungültig");
            if (ElemValidationResult.Length != 2)
                return new Core.Inspector.ProtocolEntry(this, false, true, typeof(Data.ConstBool), GetOpName() + " muss genau zwei Elemente haben!");

            var left = ElemValidationResult[0];
            var right = ElemValidationResult[1];
            if (left.TypeOfEvaluated != typeof(Data.ConstInt) && left.TypeOfEvaluated != typeof(Data.ConstDbl))
                return new Core.Inspector.ProtocolEntry(this, false, true, typeof(Data.ConstBool), GetOpName() + " Element der linken Seite muss zu ConstInt oder ConstDbl auswertbar sein !");
            if (right.TypeOfEvaluated != typeof(Data.ConstInt) && right.TypeOfEvaluated != typeof(Data.ConstDbl))
                return new Core.Inspector.ProtocolEntry(this, false, true, typeof(Data.ConstBool), GetOpName() + " Element der rechten Seite muss zu ConstInt oder ConstDbl auswertbar sein !");

            // Alle Prüfungen bestanden
            return new Core.Inspector.ProtocolEntry(this, true, true, typeof(Data.ConstBool), ToString());

        }

        protected Core.Evaluator.Result EvalImpl(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn, Func<int, int, bool> opInt, Func<double, double, bool> opDouble)
        {
            var left = EvaluatedElements[0];
            var right = EvaluatedElements[1];

            if (left.Valid && right.Valid)
            {
                // Wenn beide Seiten Integer, dann Int- Vgl.
                if (left.ResultTerm is Data.ConstInt && right.ResultTerm is Data.ConstInt)
                {
                    var leftInt = (Data.ConstInt)left.ResultTerm;
                    var rightInt = (Data.ConstInt)right.ResultTerm;

                    return new Core.Evaluator.Result(new Data.ConstBool(opInt(leftInt.Value, rightInt.Value)), new Core.Inspector.ProtocolEntry(this, true, true, typeof(Data.ConstBool), "", EvaluatedElements.Select(e => e.ResultProtocolEntry)));
                }
                else
                {
                    // Sonst Double Vgl.

                    var leftDbl = left.ResultTerm is Data.ConstDbl ? (Data.ConstDbl)left.ResultTerm : new Data.ConstDbl(((Data.ConstInt)left.ResultTerm).Value);
                    var rightDbl = right.ResultTerm is Data.ConstDbl ? (Data.ConstDbl)right.ResultTerm : new Data.ConstDbl(((Data.ConstInt)right.ResultTerm).Value);

                    return new Core.Evaluator.Result(new Data.ConstBool(opDouble(leftDbl.Value, rightDbl.Value)), new Core.Inspector.ProtocolEntry(this, true, true, typeof(Data.ConstBool), "", EvaluatedElements.Select(e => e.ResultProtocolEntry)));

                }
            } return new Core.Evaluator.Result(new Data.ConstBool(false), new Core.Inspector.ProtocolEntry(this, false, false, typeof(Data.ConstBool), "Linke oder rechte Seite konnte nicht korrekt ausgewertet werden"));
        }

        /// <summary>
        /// Hilffunktion für Clone- Implementierung
        /// </summary>
        /// <param name="Elements"></param>
        /// <returns></returns>
        protected abstract Core.NaLisp Create(Core.NaLisp[] Elements);



        public override Core.NaLisp Clone(bool deep = true)
        {
            if (deep)
                return Create(Elements);
            else
                return Create(null);
        }

        protected abstract string GetOpName();

        public override string ToString()
        {
            var Ev = new Core.Evaluator();
            var bld = new StringBuilder();
            bld.Append(GetOpName() + "(");
            foreach (var e in Elements.Select(e => Ev.Eval(e)))
            {
                bld.Append(e.ToString());
                bld.Append(", ");
            }

            bld.Remove(bld.Length - 2, 2);
            bld.Append(")");
            return bld.ToString();
        }

    }
}
