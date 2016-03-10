using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.Euklid.UnitTest
{
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            mko.Euklid.Vector v1 = new Vector(3);
            Assert.AreEqual(3, v1.Dimensions);

            mko.Euklid.Vector v2 = new Vector(1.0, 1.0, 1.0);
            Assert.AreEqual(3, v2.Dimensions);
            Assert.AreEqual(Math.PI / 4.0, v2.PhiCylindrical(0, 1), 100 * double.Epsilon);

            mko.Euklid.Vector v3 = new Vector(1.0, 1.0);
            Assert.AreEqual(Math.Sqrt(2), v3.Length, 100 * double.Epsilon);
        }
    }
}
