using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using Base = mko.Algo.FormalLanguages.NaLisp;


namespace mko.NaLisp.JSon.Test
{
    [TestClass]
    public class NaLispParser
    {
        [TestMethod]
        public void NaLispJSon_baseExpressionsTest()
        {

            // 3 * (A + B)
            //var jsonString = @"{'mul_i': [{'const_i': 3}, {'add_i': [{'var_i': 'A'}, {'var_i': 'B'}]}]}";
            var jsonString = @"{'mul_i': [{'const_i': 3}, {'add_i': [{'const_i': 12}, {'const_i': 3}]}]}";

            // Liste der verfügbaren Parser aufbauen
            var FuncParser = new Dictionary<string, IFuncParser>() 
            {
                { add_i.Name, new add_i()},
                { sub_i.Name, new sub_i()},
                { mul_i.Name, new mul_i()},
                { const_i.Name, new const_i()},
                { var_i.Name, new var_i()}
            };

            var Parser = new Parser();

            Base.Core.NaLisp expr;
            if (Parser.TryParse(jsonString, FuncParser, out expr))            {

                var inspector = new Base.Core.Inspector();

                var protocol = inspector.Validate(expr);

                var eval = new Base.Core.Evaluator();

                eval.DebugOn = true;
                var res = eval.Eval(expr);
            }
            else
            {
                Assert.Fail();
            }

        }
    }
}
