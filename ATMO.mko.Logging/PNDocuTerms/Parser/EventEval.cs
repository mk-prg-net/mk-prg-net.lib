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
    public class EventEval : EvalBase
    {
        public EventEval(IFn fn)
        {
            this.fn = fn;
        }

        IFn fn;
        /// <summary>
        /// #li p1 p2 ... pN #pl name #e
        /// </summary>
        /// <param name="stack"></param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            EvalHlp.EvalNameValuePair(
                fn,
                DocuEntities.DocuEntityTypes.Event,
                stack,
                tok => tok is DocuEntities.DocuEntity
                       && DocuEntities.DocuEntityHlp.IsValidPropertyValue(((DocuEntities.DocuEntity)tok).EntityType),
                true
                );
        }
    }
}
