using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.RPN.Test
{
    [TestClass]
    public class mkoRPN_Tokenizer
    {

        IFunctionNames fn;
        Composer compB;

        [TestInitialize]
        public void Init()
        {
            fn = new FunctionNamesLight();
            compB = new Composer(fn);
        }


        [TestMethod]
        public void mkoRPN_Tokenizer_Delimited_Strings()
        {
            var pn = compB.combinePn(
                        compB.Dbl(3.14), compB.Str("km"), 
                        compB.Dbl(290), compB.Str("Miles per Hour"), 
                        compB.Str("'Hallo Welt'"), 
                        compB.Bool(true));

            var toknzr = new BasicTokenizer(pn);

            try
            {
                toknzr.Read();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.IsTrue(toknzr.Token.IsNummeric);

            try
            {
                toknzr.Read();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.IsTrue(StringToken.Test(toknzr.Token));

            try
            {
                toknzr.Read();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.IsTrue(toknzr.Token.IsNummeric);

            try
            {
                toknzr.Read();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.IsTrue(StringToken.Test(toknzr.Token));
            Assert.AreEqual("Miles per Hour", toknzr.Token.Value);

            try
            {
                toknzr.Read();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.IsTrue(StringToken.Test(toknzr.Token));
            Assert.AreEqual("Hallo Welt", toknzr.Token.Value);

            try
            {
                toknzr.Read();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            Assert.IsTrue(toknzr.Token.IsBoolean);
            Assert.IsFalse(toknzr.EOF);

            try
            {
                toknzr.Read();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            Assert.IsTrue(toknzr.EOF);


        }
    }
}
