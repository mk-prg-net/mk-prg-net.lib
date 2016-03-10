
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.MathOps
{
    public abstract class OpBaseToDbl : Core.NaLispNonTerminal
    {
        protected readonly bool IntOperands = false;
        protected readonly bool DblOperands = false;        

        protected OpBaseToDbl(Core.NameDir.Names Name, Core.NaLisp[] Elements)
            : base((int)Name)
        {
            this.Elements = Elements;
        }

        protected OpBaseToDbl(Core.NameDir.Names Name, params double[] operands)
            : base((int)Name)
        {
            DblOperands = true;
            Elements = operands.Select(e => new Data.ConstDbl(e)).ToArray();
        }

        protected OpBaseToDbl(Core.NameDir.Names Name, params int[] operands)
            : base((int)Name)
        {
            IntOperands = true;
            Elements = operands.Select(e => new Data.ConstInt(e)).ToArray();
        }


        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            bool IsSubTreeValid = SubTreeValid(ElemValidationResult);

            if (Stack.ParentIs((int)Core.NameDir.Names.Pipe))
            {
                if (ElemValidationResult.Count() < 1)
                    return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstDbl), Core.NameDir.Get(this).Name + ": innerhalb eines Pipe- Opearators muss die Anzahl der Operanden muss mindestens 1 sein");
            }
            else
            {
                if (ElemValidationResult.Count() < 2)
                    return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstDbl), Core.NameDir.Get(this).Name + ": Anzahl der Operanden muss mindestens 2 sein");
            }

            if (IntOperands || DblOperands)
                return new Core.Inspector.ProtocolEntry(this, true, IsSubTreeValid, typeof(Data.ConstDbl), "");

            if (ElemValidationResult.All(e => e.TypeOfEvaluated == typeof(Data.ConstInt) || e.TypeOfEvaluated == typeof(Data.ConstDbl) || e.TypeOfEvaluated == typeof(Lisp.Tuple)))
                return new Core.Inspector.ProtocolEntry(this, true, IsSubTreeValid, typeof(Data.ConstDbl), "");
            else
                return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstDbl), Core.NameDir.Get(this).Name + " darf nur Parameter haben, die zu ConstInt, ConstDbl oder List ausgewertet werden.");

        }

        /// <summary>
        /// Hilffunktion für Clone- Implementierung
        /// </summary>
        /// <param name="Elements"></param>
        /// <returns></returns>
        protected abstract Core.NaLisp Create(Core.NaLisp[] Elements);

        public override Core.NaLisp Clone(bool deep)
        {
            if (deep)
                return Create(Elements.Select(e => e.Clone()).ToArray());
            else
                return Create(null);
        }

        protected Core.Evaluator.Result EvalImpl(Core.Evaluator.Result[] EvaluatedElements, Func<double, double, double> opDbl, bool DebugOn)
        {
            Data.ConstDbl result = new Data.ConstDbl(0);
            Core.Inspector.ProtocolEntry pentry = null;           

            try
            {
                double akku = 0.0;

                if (IntOperands)
                {
                    akku = ((Data.ConstInt)EvaluatedElements.First().ResultTerm).Value;
                    foreach (Data.ConstInt cint in EvaluatedElements.Skip(1).Select(e => e.ResultTerm))
                    {
                        akku = opDbl(akku, (double)cint.Value);
                    }
                }
                else if (DblOperands)
                {
                    akku = ((Data.ConstDbl)EvaluatedElements.First().ResultTerm).Value;
                    foreach (Data.ConstDbl cdbl in EvaluatedElements.Skip(1).Select(e => e.ResultTerm))
                    {
                        akku = opDbl(akku, cdbl.Value);
                    }
                }
                else
                {
                    if (EvaluatedElements.First().ResultTerm is Data.ConstInt)
                        akku = ((Data.ConstInt)EvaluatedElements.First().ResultTerm).Value;
                    else
                        akku = ((Data.ConstDbl)EvaluatedElements.First().ResultTerm).Value;

                    foreach (var constVal in EvaluatedElements.Skip(1).Select(e => e.ResultTerm))
                    {
                        if (constVal is Data.ConstInt)
                            akku = opDbl(akku, (double)((Data.ConstInt)constVal).Value);
                        else
                            akku = opDbl(akku, ((Data.ConstDbl)constVal).Value);
                    }
                }

                pentry = new Core.Inspector.ProtocolEntry(this, true, true, typeof(Data.ConstDbl), ToString());
                result = new Data.ConstDbl(akku);
            }
            catch (Exception ex)
            {                
                pentry = new Core.Inspector.ProtocolEntry(this, false, true, typeof(Data.ConstDbl), mko.TraceHlp.FormatErrMsg(this, "Eval", mko.ExceptionHelper.FlattenExceptionMessages(ex)));
            }

            pentry.ChildProtocolEntries = EvaluatedElements.Select(e => e.ResultProtocolEntry).ToArray();
            return new Core.Evaluator.Result(result, pentry);
        }
    }
}
