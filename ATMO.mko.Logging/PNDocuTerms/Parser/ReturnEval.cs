using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

namespace ATMO.mko.Logging.PNDocuTerms.Parser
{
    public class ReturnEval : EvalBase
    {

        public ReturnEval(IFn fn)
        {
            this.fn = fn;
        }

        IFn fn;

        /// <summary>
        /// Reads [value name #p] from stack and evaluates
        /// name is a string
        /// value can be a basic type like string, bool, num or as DocuEntiy
        /// </summary>
        /// <param name="stack">value name #p</param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            EvalHlp.EvalValue(
                fn,
                DocuEntities.DocuEntityTypes.ReturnValue,
                stack,
                tok => tok is DocuEntities.DocuEntity
                        && DocuEntities.DocuEntityHlp.IsValidPropertyValue(((DocuEntities.DocuEntity)tok).EntityType)                
                );
        }

    }
}
