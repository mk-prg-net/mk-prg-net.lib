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
    public class ListEval : EvalBase
    {
        public ListEval(IFn fn)
        {
            this.fn = fn;            
        }

        IFn fn;        
        int CountEvaluated = 0;

        List<DocuEntities.IDocuEntity> parts = new List<DocuEntities.IDocuEntity>();

        /// <summary>
        /// #li P1 P2 ... PN #pl
        /// Pi are DocuEnties
        /// </summary>
        /// <param name="stack"></param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            parts.Clear();
            stack.ParseVariadicParameters(fn.ListEnd, (stackP, iParam) => {
                var tok = stack.Pop();

                //TraceHlp.ThrowArgExIfNot(tok.IsFunctionName, $"{tok.ToString()} is not a parameter");
                TraceHlp.ThrowArgExIfNot(tok is DocuEntities.IDocuEntity, $"{tok.ToString()} is not a List part");

                var dokE = (DocuEntities.IDocuEntity)tok;
                TraceHlp.ThrowArgExIfNot(DocuEntities.DocuEntityHlp.IsValidListMember(dokE.EntityType), $"list element expected, but got {dokE.EntityType}");
                
                CountEvaluated += tok.CountOfEvaluatedTokens;

                parts.Add(dokE);
            });

            stack.Push(new DocuEntities.DocuEntity(fn, DocuEntities.DocuEntityTypes.List, parts.ToArray()));
        }
    }
}
