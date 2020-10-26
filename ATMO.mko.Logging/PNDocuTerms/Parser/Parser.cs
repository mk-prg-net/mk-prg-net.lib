using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

namespace ATMO.mko.Logging.PNDocuTerms.Parser
{
    /// <summary>
    /// mko, 27.3.2018
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// mko, 16.4.2018
        /// </summary>
        public static PNDocuTerms.DocuEntities.Composer Composer = new DocuEntities.Composer();


        /// <summary>
        /// mko, 27.3.2018
        /// Specialiazed parser for DocEntity strings
        /// 
        /// mko, 26.4.2018
        /// Added Parameter fn to inject functionnames
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        public static RCV2<DocuEntities.IDocuEntity> Parse(string pn, IFn fn, bool doRPNUrlDecode = true)
        {
            RCV2<DocuEntities.IDocuEntity> rc = null;
            DocuEntities.IDocuEntity NullEntity = null;

            var evalTab = new FunctionEvaluatorTable(new FunctionEvalMapperFunctor(fn));
            var _parser = new ParserV2(evalTab.FuncEvaluators);

            var rcT = BasicTokenizer.TokenizePN(pn, doRPNUrlDecode, evalTab.FuncEvaluators.Keys.ToArray());

            if (rcT.Succeeded)
            {
                var rcp = _parser.Parse(rcT.Value);

                PNDocuTerms.DocuEntities.IDocuEntity val = rcp.Value.Stack.Peek() is PNDocuTerms.DocuEntities.IDocuEntity  
                                                                ? (DocuEntities.IDocuEntity)rcp.Value.Stack.Peek() 
                                                                : Composer.txt(rcp.Value.Stack.Peek().ToString());

                if (rcp.Succeeded && rcp.Value.Stack.Count == 1)
                {
                    rc = RCV2<DocuEntities.IDocuEntity>.Ok(value: val);
                }
                else
                {
                    rc = RCV2<DocuEntities.IDocuEntity>.Failed(NullEntity, ErrorDescription: "Parse failed", inner: new RCV2<IToken>(rcp));
                }
            }
            else
            {
                rc = RCV2<DocuEntities.IDocuEntity>.Failed(NullEntity, ErrorDescription: "Tokenizer failed");
            }

            return rc;

        }

        /// <summary>
        /// mko, 15.11.2018
        /// Neuer Parserfunktion mit leistungsfähigeren Rückgabewert.
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="fn"></param>
        /// <param name="pnL"></param>
        /// <returns></returns>
        public static RCV3sV<DocuEntities.IDocuEntity> Parse18_11(string pn, IFn fn, PNDocuTerms.DocuEntities.IComposer pnL, bool doRPNUrlDecode = true)
        {
            RCV3sV<DocuEntities.IDocuEntity> rc = null;
            DocuEntities.IDocuEntity NullEntity = null;

            try
            {

                var evalTab = new FunctionEvaluatorTable(new FunctionEvalMapperFunctor(fn));
                var _parser = new ParserV2(evalTab.FuncEvaluators);

                var rcT = BasicTokenizer.TokenizePN(pn, doRPNUrlDecode, evalTab.FuncEvaluators.Keys.ToArray());

                if (rcT.Succeeded)
                {
                    var rcp = _parser.Parse(rcT.Value);

                    PNDocuTerms.DocuEntities.IDocuEntity val = rcp.Value.Stack.Peek() is PNDocuTerms.DocuEntities.IDocuEntity
                                                                    ? (DocuEntities.IDocuEntity)rcp.Value.Stack.Peek()
                                                                    : Composer.txt(rcp.Value.Stack.Peek().ToString());

                    if (rcp.Succeeded && rcp.Value.Stack.Count == 1)
                    {
                        rc = RCV3sV<DocuEntities.IDocuEntity>.Ok(value: val);
                    }
                    else
                    {
                        rc = RCV3sV<DocuEntities.IDocuEntity>.Failed(NullEntity, ErrorDescription: pnL.m("Parse", pnL.ret(pnL.eFails())), inner: RCV3.TranformToRCV3(rcp));
                    }
                }
                else
                {
                    rc = RCV3sV<DocuEntities.IDocuEntity>.Failed(NullEntity, ErrorDescription: pnL.i("Tokenizer", pnL.m("Tokenize", pnL.ret(pnL.eFails()))), inner: RCV3.TranformToRCV3(rcT));
                }
            }catch(Exception ex)
            {
                rc = RCV3sV<DocuEntities.IDocuEntity>.Failed(NullEntity, ErrorDescription: pnL.i("Parser", pnL.eFails(TraceHlp.FlattenExceptionMessagesPN(ex))));
            }

            return rc;
        }


    }
}
