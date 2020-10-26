using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;
using static mko.Algo.Listprocessing.Fn;


namespace ATMO.mko.Logging.PNDocuTerms.Parser
{
    /// <summary>
    /// mko, 26.3.2018
    /// 
    /// </summary>
    public class DateEval : EvalBase
    {
        public DateEval(IFn fn)
        {
            this.fn = fn;
        }

        IFn fn;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stack">Main.Sub.Build #ver</param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            var tok = stack.Peek();
            TraceHlp.ThrowArgExIfNot(StringToken.Test(tok), "string token expected");

            var strTok = (StringToken)stack.Pop();

            stack.Push(new DocuEntities.DocuEntity(fn, DocuEntities.DocuEntityTypes.Date, L(new DocuEntities.String(strTok.Value))));
        }
    }
}
