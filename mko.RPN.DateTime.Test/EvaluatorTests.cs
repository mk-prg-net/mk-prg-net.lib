using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.RPN.DateTime.Test
{
    [TestClass]
    public class EvaluatorTests
    {

        mko.RPN.IFunctionNames fnBase;
        FunctionNameDateTimeBasic fn;
        FunctionEvaluatorTable etab;

        public EvaluatorTests()
        {

            fnBase = new mko.RPN.FunctionNamesLight();
            fn = new FunctionNameDateTimeBasic(fnBase);            
            etab = new FunctionEvaluatorTable(new FnameEvalMapperFunctor(fn));
        }

        mko.RPN.Parser parser;
        Composer comp;

        [TestInitialize]
        public void Init()
        {
            comp = new Composer(fnBase, fn);
            parser = new mko.RPN.Parser(etab.FuncEvaluators);
        }

        [TestMethod]
        public void Evaluator_Date()
        {

            var weihnachten = comp.rDate(2017, 12, 24);


            parser.Parse(weihnachten);

            Assert.IsTrue(parser.Succsessful);
            Assert.AreEqual(1, parser.Stack.Count);
            Assert.IsInstanceOfType(parser.Stack.Peek(), typeof(Date));

            var weihnachtenDate = (Date)parser.Stack.Peek();

            Assert.AreEqual(2017, weihnachtenDate.DateTimeValue.Year);
            Assert.AreEqual(12, weihnachtenDate.DateTimeValue.Month);
            Assert.AreEqual(24, weihnachtenDate.DateTimeValue.Day);

        }
    }
}
