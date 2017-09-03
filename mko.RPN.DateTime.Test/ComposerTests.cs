using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.RPN.DateTime.Test
{
    [TestClass]
    public class ComposerTests
    {
        mko.RPN.IFunctionNames fnBase;
        DateTime.Composer comp;

        [TestInitialize]
        public void TestInit()
        {

            fnBase = new mko.RPN.FunctionNamesLight();
            var fn = new FunctionNameDateTimeBasic(fnBase);
            comp = new Composer(fnBase, fn);
        }

        [TestMethod]
        public void Composer_Date()
        {
            {
                var d = comp.Date(2017, 12, 24);
                Assert.AreEqual(".dat .y 2017 .mon 12 .day 24", d.Trim());

                var rd = comp.rDate(2017, 12, 24);
                Assert.AreEqual("24 .day 12 .mon 2017 .y .dat", rd.Trim());
            }

        }
    }
}
