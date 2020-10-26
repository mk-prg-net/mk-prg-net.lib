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
    public class MethodEval : EvalBase
    {
        public MethodEval(IFn fn)
        {
            this.fn = fn;
        }

        IFn fn;

        List<DocuEntities.String> parts = new List<DocuEntities.String>();

        /// <summary>
        /// mko
        /// </summary>
        /// <param name="stack"></param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            // mko, 21.12.2018
            // Bestimmung von Namen und Parameteliste separiert, um auch Methoden mit leeren 
            // Parameterlisten zu ermöglichen

            var name = EvalHlp.EvaluateName(stack, "Method without Methodname");

            // gibt es eine Parameterliste ?
            if (stack.Any() 
                && stack.Peek() is DocuEntities.DocuEntity
                && ((DocuEntities.DocuEntity)stack.Peek()).EntityType == DocuEntities.DocuEntityTypes.List)
            {
                // Parameterliste und Rückgabewerte der Methode auswerten

                EvalHlp.EvalValue(
                    name,
                    fn,
                    DocuEntities.DocuEntityTypes.Method,
                    stack,
                    tok => tok is DocuEntities.DocuEntity
                            && DocuEntities.DocuEntityHlp.IsValidMethodParameterType(((DocuEntities.DocuEntity)tok).EntityType)                    
                    );

            } else
            {
                // Für Methode mit leerer Parameterliste DokuEntity vom Typ Methode mit gegebenen Namen auf dem Stapel ablegen
                var p = new DocuEntities.DocuEntity(fn, DocuEntities.DocuEntityTypes.Method, L(name));
                stack.Push(p);

            }

            //EvalHlp.EvalNameValuePair(
            //    fn, 
            //    DocuEntities.DocuEntityTypes.Method, 
            //    stack,
            //    tok => tok is DocuEntities.DocuEntity
            //            && DocuEntities.DocuEntityHlp.IsValidMethodParameterType(((DocuEntities.DocuEntity)tok).EntityType),
            //    false
            //    );
        }

    }
}
