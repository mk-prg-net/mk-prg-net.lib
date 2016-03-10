using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.ComparsionOps
{
    public abstract class OpBase<T> : Core.NaLispNonTerminal
        where T : IComparable<T>
    {
        public OpBase(Core.INaLisp[] Elements)
        {           
            this.Elements = Elements;
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            return ValidateAndDescribeResults(Stack, typeof(Data.ConstVal<bool>), ElemValidationResult, typeof(Data.ConstValComp<T>), typeof(Data.ConstValComp<T>));

        }

        protected Core.NaLisp EvalImpl(Core.INaLisp Currrent, Core.INaLisp[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn, Func<T, T, bool> op)
        {
            var left = (Data.ConstValComp<T>)EvaluatedElements[0];
            var right = (Data.ConstValComp<T>)EvaluatedElements[1];

            try
            {
                return new Data.ConstVal<bool>(op.Invoke(left.Value, right.Value));
            }
            catch (Exception ex)
            {
                throw new Core.Evaluator.EvalException(Currrent, mko.TraceHlp.FormatErrMsg(this, "EvalImpl", "es können nur Operanden gleichen Typs verglichen werden"), ex);
            }
        }

        ///// <summary>
        ///// Hilffunktion für Clone- Implementierung
        ///// </summary>
        ///// <param name="Elements"></param>
        ///// <returns></returns>
        //protected abstract Core.NaLisp Create(Core.NaLisp[] Elements);


        //public override Core.NaLisp Clone(bool deep = true)
        //{
        //    if (deep)
        //        return Create(Elements.Select(r => r.Clone()).ToArray());
        //    else
        //        return Create(Elements);
        //}

        protected abstract string GetOpName();

        public override string ToString()
        {
            var Ev = new Core.Evaluator();
            var bld = new StringBuilder();
            bld.Append("(" + GetOpName());
            foreach (var e in Elements.Select(e => Ev.Eval(e)))
            {
                bld.Append(e.ToString());
                bld.Append(" ");
            }

            bld.Remove(bld.Length - 1, 1);
            bld.Append(")");
            return bld.ToString();
        }

    }
}
