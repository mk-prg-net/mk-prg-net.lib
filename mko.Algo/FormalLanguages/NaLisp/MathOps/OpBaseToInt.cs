using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.MathOps
{
    public abstract class OpBaseToInt : Core.NaLispNonTerminal
    {
        protected readonly bool IntOperands = false;
        protected readonly bool DblOperands = false;

        protected OpBaseToInt(Core.NameDir.Names Name, Core.NaLisp[] Elements)
            : base((int)Name)
        {
            this.Elements = Elements;
        }

        protected OpBaseToInt(Core.NameDir.Names Name, params double[] operands)
            : base((int)Name)
        {
            DblOperands = true;
            Elements = operands.Select(e => new Data.ConstDbl(e)).ToArray();
        }

        protected OpBaseToInt(Core.NameDir.Names Name, params int[] operands)
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
                    return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstInt), Core.NameDir.Get(this).Name + ": innerhalb eines Pipe- Opearators muss die Anzahl der Operanden muss mindestens 1 sein");
            }
            else
            {
                if (ElemValidationResult.Count() < 2)
                    return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstInt), Core.NameDir.Get(this).Name + ": Anzahl der Operanden muss mindestens 2 sein");
            }

            if (IntOperands || DblOperands)
                return new Core.Inspector.ProtocolEntry(this, true, IsSubTreeValid, typeof(Data.ConstInt), "");

            if (ElemValidationResult.All(e => e.TypeOfEvaluated == typeof(Data.ConstInt) || e.TypeOfEvaluated == typeof(Data.ConstDbl) || e.TypeOfEvaluated == typeof(Lisp.Tuple)))
                return new Core.Inspector.ProtocolEntry(this, true, IsSubTreeValid, typeof(Data.ConstInt), "");
            else
                return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstInt), Core.NameDir.Get(this).Name + " darf nur Parameter haben, die zu ConstInt, ConstDbl oder List ausgewertet werden.");

        }

        /// <summary>
        /// Hilfsfunktion für Clone- Implementierung
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

        protected Core.Evaluator.Result EvalImpl(Core.Evaluator.Result[] EvaluatedElements, Func<int, int, int> opInt, bool DebugOn)
        {
            Data.ConstInt result = new Data.ConstInt(0);
            Core.Inspector.ProtocolEntry pentry = null;           

            try
            {
                int akku = 0; 

                if (IntOperands)
                {
                    akku = ((Data.ConstInt)EvaluatedElements.First().ResultTerm).Value;

                    foreach (Data.ConstInt cint in EvaluatedElements.Skip(1).Select(e => e.ResultTerm))
                    {
                        akku = opInt(akku, cint.Value);
                    }
                }
                else if (DblOperands)
                {
                    akku = (int)((Data.ConstDbl)EvaluatedElements.First().ResultTerm).Value;

                    foreach (Data.ConstDbl cdbl in EvaluatedElements.Skip(1).Select(e => e.ResultTerm))
                    {
                        akku = opInt(akku, (int)cdbl.Value);
                    }
                }
                else
                {
                    if (EvaluatedElements.First().ResultTerm is Data.ConstInt)
                        akku = ((Data.ConstInt)EvaluatedElements.First().ResultTerm).Value;
                    else
                        akku = (int)((Data.ConstDbl)EvaluatedElements.First().ResultTerm).Value;

                    foreach (var constVal in EvaluatedElements.Skip(1).Select(e => e.ResultTerm))
                    {
                        if (constVal is Data.ConstInt)
                            akku = opInt(akku, ((Data.ConstInt)constVal).Value);
                        else
                            akku = opInt(akku, (int)((Data.ConstInt)constVal).Value);
                    }
                }

                pentry = new Core.Inspector.ProtocolEntry(this, true, true, typeof(Data.ConstInt), ToString());
                result = new Data.ConstInt(akku);
            }
            catch (Exception ex)
            {
                pentry = new Core.Inspector.ProtocolEntry(this, false, true, typeof(Data.ConstInt), mko.TraceHlp.FormatErrMsg(this, Core.NameDir.Get(this).Name, "Eval", mko.ExceptionHelper.FlattenExceptionMessages(ex)));
            }

            pentry.ChildProtocolEntries = EvaluatedElements.Select(e => e.ResultProtocolEntry).ToArray();
            return new Core.Evaluator.Result(result, pentry);
        }

    }
}
