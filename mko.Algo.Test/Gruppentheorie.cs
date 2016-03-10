using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.Algo.Test
{
    [TestClass]
    public class Gruppentheorie
    {
        [TestMethod]
        public void mko_Algo_Gruppentheorie_Z7()
        {
            var Z7 = new mko.Algo.Algebra.ZZGroup(7);

            Assert.AreEqual(7, Z7.Order);
            Assert.AreEqual(3, Z7.Combine(6, 4));
            Assert.AreEqual(0, Z7.Combine(6, 1));
            Assert.AreEqual(4, Z7.InverseOf(3));
            Assert.AreEqual(3, Z7.Span(1, 4));
            Assert.AreEqual(4, Z7.Span(4, 1));
            Assert.AreEqual(0, Z7.Span(4, 4));


        }
    }
}
