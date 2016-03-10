using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.BoolOps
{
    public abstract class OpBaseBin : Core.NaLispNonTerminal
    {

        protected OpBaseBin(Core.INaLisp[] Elements)            
        {
            this.Elements = Elements;
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            return ValidateAndDescribeResults(Stack, typeof(Data.IConstValue<bool>), ElemValidationResult, typeof(Data.IConstValue<bool>), 2);
        }

        ///// <summary>
        ///// Hilffunktion für Clone- Implementierung
        ///// </summary>
        ///// <param name="Elements"></param>
        ///// <returns></returns>
        //protected abstract Core.NaLisp Create(Core.NaLisp[] Elements);

        //public override Core.NaLisp Clone(bool deep)
        //{
        //    if (deep)
        //        return Create(Elements.Select(e => e.Clone()).ToArray());
        //    else
        //        return Create(Elements);
        //}

        protected Core.NaLisp EvalImpl(Core.INaLisp[] EvaluatedElements, Func<bool, bool, bool> opBool, bool DebugOn)
        {
            var result = new Data.ConstVal<bool>(false);

            try
            {
                bool akku = ((Data.IConstValue<bool>)EvaluatedElements.First()).Value;

                foreach (var constVal in EvaluatedElements.Skip(1))
                {
                    akku = opBool(akku, ((Data.IConstValue<bool>)constVal).Value);
                }

                result = new Data.ConstVal<bool>(akku);
            }
            catch (Exception ex)
            {
                throw new Core.Evaluator.EvalException(this, mko.TraceHlp.FormatErrMsg(this, "Eval", mko.ExceptionHelper.FlattenExceptionMessages(ex)), ex);
            }

            return result;
        }

    }
}
