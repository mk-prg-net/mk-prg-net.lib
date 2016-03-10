using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.Euklid.UnitTest
{
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void VectorTest()
        {
            // Dimension
            var v1 = new Vector(3);
            Assert.AreEqual(3, v1.Dimensions);

            // PhiCylindrical
            var v2 = new Vector(1.0, 1.0, 1.0);
            Assert.AreEqual(3, v2.Dimensions);
            Assert.AreEqual(Math.PI / 4.0, v2.PhiCylindrical(0, 1), 100 * double.Epsilon);

            // Length
            var v3 = new Vector(1.0, 1.0);
            Assert.AreEqual(Math.Sqrt(2), v3.Length, 100 * double.Epsilon);

            // Indexer[]
            var v4 = new Vector(1, 2, 3, 4);
            for(int i = 0; i < v4.Dimensions; i++)
                Assert.AreEqual((double)i+1, v4[i]);

            // UnitVector
            var v5 = new Vector(2, 2);
            var UnitV = new Vector(Math.Sqrt(0.5), Math.Sqrt(0.5));
            Assert.IsTrue(v5.UnitVector.Equals(UnitV));

            

        }
    }
}
