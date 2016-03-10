using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.BoolOps
{
    public abstract class OpBaseBin : Core.NaLispNonTerminal
    {

        protected OpBaseBin(Core.NameDir.Names Name, Core.NaLisp[] Elements)
            : base((int)Name)
        {
            this.Elements = Elements;
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            bool IsSubTreeValid = SubTreeValid(ElemValidationResult);

            if (Stack.ParentIs((int)Core.NameDir.Names.Pipe))
            {
                if (ElemValidationResult.Count() < 1)
                    return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstBool), Core.NameDir.Get(this).Name + ": innerhalb eines Pipe- Opearators muss die Anzahl der Operanden muss mindestens 1 sein");
            }
            else
            {
                if (ElemValidationResult.Count() < 2)
                    return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstBool), Core.NameDir.Get(this).Name + ": Anzahl der Operanden muss mindestens 2 sein");
            }

            if (ElemValidationResult.All(e => e.TypeOfEvaluated == typeof(Data.ConstBool)))
                return new Core.Inspector.ProtocolEntry(this, true, IsSubTreeValid, typeof(Data.ConstBool), "");
            else
                return new Core.Inspector.ProtocolEntry(this, false, IsSubTreeValid, typeof(Data.ConstBool), Core.NameDir.Get(this).Name + " darf nur Parameter haben, die zu ConstBool ausgewertet werden.");

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

        protected Core.Evaluator.Result EvalImpl(Core.Evaluator.Result[] EvaluatedElements, Func<bool, bool, bool> opBool, bool DebugOn)
        {
            Data.ConstBool result = new Data.ConstBool(false);
            Core.Inspector.ProtocolEntry pentry = null;

            try
            {
                bool akku = ((Data.ConstBool)EvaluatedElements.First().ResultTerm).Value;

                foreach (var constVal in EvaluatedElements.Skip(1).Select(e => e.ResultTerm))
                {
                    akku = opBool(akku, ((Data.ConstBool)constVal).Value);
                }


                pentry = new Core.Inspector.ProtocolEntry(this, true, true, typeof(Data.ConstBool), ToString());
                result = new Data.ConstBool(akku);
            }
            catch (Exception ex)
            {
                pentry = new Core.Inspector.ProtocolEntry(this, false, true, typeof(Data.ConstBool), mko.TraceHlp.FormatErrMsg(this, "Eval", mko.ExceptionHelper.FlattenExceptionMessages(ex)));
            }

            pentry.ChildProtocolEntries = EvaluatedElements.Select(e => e.ResultProtocolEntry).ToArray();
            return new Core.Evaluator.Result(result, pentry);
        }

    }
}
