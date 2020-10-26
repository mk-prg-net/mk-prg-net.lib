using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;
using pnL = ATMO.mko.Logging.PNDocuTerms.Composer;

using static mko.Algo.Listprocessing.Fn;


namespace ATMO.mko.Logging.PNDocuTerms.Parser
{
    /// <summary>
    /// mko, 7.3.2018
    /// </summary>
    public class PropertyEval : EvalBase
    {

        public PropertyEval(IFn fn)
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
            EvalHlp.EvalNameValuePair(
                fn, 
                DocuEntities.DocuEntityTypes.Property, 
                stack,
                tok =>  (tok is DocuEntities.DocuEntity
                        && DocuEntities.DocuEntityHlp.IsValidPropertyValue(((DocuEntities.DocuEntity)tok).EntityType)) 
                        || ! (tok is global::mko.RPN.FunctionNameToken),
                false
                );
        }

    }
}
