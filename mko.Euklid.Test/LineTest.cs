using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.Euklid.UnitTest
{
    [TestClass]
    public class LineTest
    {
        [TestMethod]
        public void TestSchnittpunkteLinien()
        {
            mko.Euklid.Line l1 = new Line(2);

            l1.P1.Set(0.0, 1.0);
            l1.P2.Set(1.0, 0.0);

            Assert.IsTrue(l1.Contains(l1.P1, 100 * double.Epsilon));
            Assert.IsTrue(l1.Contains(l1.P2, 100 * double.Epsilon));

            mko.Euklid.Line l2 = new Line(2);

            l2.P1.Set(0.0, 0.0);
            l2.P2.Set(1.0, 1.0);

            mko.Euklid.Vector Intersection;
            Assert.IsTrue(l2.IntersectionWith(l1, out Intersection));

        }
    }
}
