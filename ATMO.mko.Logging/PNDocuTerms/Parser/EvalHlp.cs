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
    public static class EvalHlp
    {
        public static DocuEntities.String EvaluateName(Stack<IToken> stack, string errorMsg)
        {
            var tokName = stack.Peek();
            
            //TraceHlp.ThrowArgExIfNot(!tokName.IsFunctionName && StringToken.Test(tokName), errorMsg);

            // 9.5.2018: relaxed restrictions for property names: also int or numeric literals are allowed.
            TraceHlp.ThrowArgExIfNot(!tokName.IsFunctionName, errorMsg);

            var name = stack.Pop().Value;
            return new DocuEntities.String(name);
        }

        public static void EvalNameValuePair(
            IFn fn, DocuEntities.DocuEntityTypes type, 
            Stack<IToken> stack, 
            Func<IToken, bool> restrictions, 
            bool violateRestrictionsAllowed)
        {
            var name = EvalHlp.EvaluateName(stack, $"{type} without name and value");

            //TraceHlp.ThrowArgExIfNot(stack.Any(), $"{type} {name} without value");

            if (stack.Any() && restrictions(stack.Peek()))
            {
                var tok = stack.Peek();

                DocuEntities.IDocuEntity Value = null;

                if (tok is DocuEntities.IDocuEntity)
                {
                    Value = (DocuEntities.IDocuEntity)stack.Pop();
                }
                else
                {
                    stack.Pop();
                    Value = new DocuEntities.String(tok.Value);
                }
                var p = new DocuEntities.DocuEntity(fn, type, L(name, Value));
                stack.Push(p);
            }
            else if(violateRestrictionsAllowed)
            {
                stack.Push(new DocuEntities.DocuEntity(fn, type,  L(name)));
            } else
            {
                TraceHlp.ThrowArgEx("Restrictions violated");
            }
        }

        public static void EvalValue(
            IFn fn, DocuEntities.DocuEntityTypes type,
            Stack<IToken> stack,
            Func<IToken, bool> restrictions)
        {
            if (stack.Any() && restrictions(stack.Peek()))
            {
                var tok = stack.Peek();

                DocuEntities.IDocuEntity Value = null;

                if (tok is DocuEntities.IDocuEntity)
                {
                    Value = (DocuEntities.IDocuEntity)stack.Pop();
                }
                else
                {
                    // mko, 21.12.2018
                    // An Verhalten von EvalNameValuePair angeglichen
                    stack.Pop();

                    Value = new DocuEntities.String(tok.Value);
                }
                var p = new DocuEntities.DocuEntity(fn, type, L(Value));
                stack.Push(p);
            }
            else
            {
                TraceHlp.ThrowArgEx("Restrictions violated");
            }
        }

        /// <summary>
        /// mko, 21.12.2018
        /// Zur separaten Verarbeitung von Name und Parameter beim Evaluieren von Methoden entwickelt
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="fn"></param>
        /// <param name="type"></param>
        /// <param name="stack"></param>
        /// <param name="restrictions"></param>
        public static void EvalValue(
            DocuEntities.String Name,
            IFn fn, DocuEntities.DocuEntityTypes type,
            Stack<IToken> stack,
            Func<IToken, bool> restrictions)
        {
            if (stack.Any() && restrictions(stack.Peek()))
            {
                var tok = stack.Peek();

                DocuEntities.IDocuEntity Value = null;

                if (tok is DocuEntities.IDocuEntity)
                {
                    Value = (DocuEntities.IDocuEntity)stack.Pop();
                }
                else
                {
                    // mko, 21.12.2018
                    // An Verhalten von EvalNameValuePair angeglichen
                    stack.Pop();

                    Value = new DocuEntities.String(tok.Value);
                }
                var p = new DocuEntities.DocuEntity(fn, type, L(Name, Value));
                stack.Push(p);
            }
            else
            {
                TraceHlp.ThrowArgEx("Restrictions violated");
            }
        }



    }
}
