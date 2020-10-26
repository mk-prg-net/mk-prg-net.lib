using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;
using pnL = ATMO.mko.Logging.PNDocuTerms.Composer;

namespace ATMO.mko.Logging.PNDocuTerms.Parser
{
    /// <summary>
    /// mko, 7.3.2018
    /// </summary>
    public class TextEval : EvalBase
    {
        public TextEval(IFn fn)
        {
            this.fn = fn;
        }

        IFn fn;
        int CountEvaluated = 0;

        List<DocuEntities.String> parts = new List<DocuEntities.String>();

        /// <summary>
        /// #li str1 str2 ... strN #txt
        /// str i are strings
        /// </summary>
        /// <param name="stack"></param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            parts.Clear();
            stack.ParseVariadicParameters(fn.ListEnd, (stackP, iParam) => {
                var tok = stack.Pop();

                TraceHlp.ThrowArgExIf(tok.IsFunctionName, $"{tok.ToString()} is not a string part");
                //TraceHlp.ThrowArgExIf(tok.IsFunctionName, $"{tok.ToString()} is not a string part");

                CountEvaluated += tok.CountOfEvaluatedTokens;

                parts.Add(new DocuEntities.String(tok.Value));
            });

            stack.Push(new DocuEntities.DocuEntity(fn, DocuEntities.DocuEntityTypes.Text, parts.ToArray()));
        }
    }
}
