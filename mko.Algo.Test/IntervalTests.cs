using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.Algo.Test
{
    [TestClass]
    public class IntervalTests
    {
        [TestMethod]
        public void mko_Algo_Interval_int_Test()
        {
            // Leeres Intervall
            var iEmpty = new mko.Algo.Interval<int>();

            Assert.IsTrue(iEmpty.Empty);

            iEmpty.End = 10;

            Assert.IsFalse(iEmpty.Empty);

            iEmpty.End = -10;

            Assert.IsTrue(iEmpty.Empty);

            // Einelementiges Intervall
            var i1 = new mko.Algo.Interval<int>(1, 1);

            Assert.IsFalse(i1.Empty);

            // Intervall von 0-10
            var i0_10 = new mko.Algo.Interval<int>(0, 10);

            Assert.IsFalse(i0_10.Empty);
            Assert.IsTrue(i0_10.Contains(0));
            Assert.IsTrue(i0_10.Contains(5));
            Assert.IsTrue(i0_10.Contains(10));

            // Intervalle schneiden
            var iInter1 = i0_10.IntersectWith(i1);

            Assert.IsFalse(iInter1.Empty);
            Assert.AreEqual(1, iInter1.Begin);
            Assert.AreEqual(1, iInter1.End);

            // Intervalle schneiden
            var iInter2 = i0_10.IntersectWith(new mko.Algo.Interval<int>(3, 6));

            Assert.IsFalse(iInter2.Empty);
            Assert.AreEqual(3, iInter2.Begin);
            Assert.AreEqual(6, iInter2.End);

            // Intervalle schneiden
            var iInter3 = i0_10.IntersectWith(new mko.Algo.Interval<int>(-1, 6));

            Assert.IsFalse(iInter3.Empty);
            Assert.AreEqual(0, iInter3.Begin);
            Assert.AreEqual(6, iInter3.End);

            // Intervalle schneiden
            var iInter4 = i0_10.IntersectWith(new mko.Algo.Interval<int>(3, 12));

            Assert.IsFalse(iInter4.Empty);
            Assert.AreEqual(3, iInter4.Begin);
            Assert.AreEqual(10, iInter4.End);


            // Intervalle schneiden
            var iInter5 = i0_10.IntersectWith(new mko.Algo.Interval<int>(11, 12));

            Assert.IsTrue(iInter5.Empty);
            Assert.AreEqual(11, iInter5.Begin);
            Assert.AreEqual(10, iInter5.End);

        }

        [TestMethod]
        public void mko_Algo_Interval_DateTime_Test()
        {
            // Leeres Intervall
            var iEmpty = new mko.Algo.Interval<DateTime>();

            Assert.IsTrue(iEmpty.Empty);

            iEmpty.End = DateTime.Now;

            Assert.IsFalse(iEmpty.Empty);

            iEmpty.Begin = DateTime.Now.AddSeconds(1);

            Assert.IsTrue(iEmpty.Empty);

            // Einelementiges Intervall
            var Now = DateTime.Now;
            var i1 = new mko.Algo.Interval<DateTime>(Now, Now);

            Assert.IsFalse(i1.Empty);

            // Intervall von 0-10
            var i0_10 = new mko.Algo.Interval<DateTime>(Now, Now.AddHours(10));

            Assert.IsFalse(i0_10.Empty);
            Assert.IsTrue(i0_10.Contains(Now));
            Assert.IsTrue(i0_10.Contains(Now.AddHours(5)));
            Assert.IsTrue(i0_10.Contains(Now.AddHours(10)));

            // Intervalle schneiden
            var iInter1 = i0_10.IntersectWith(i1);

            Assert.IsFalse(iInter1.Empty);
            Assert.AreEqual(Now, iInter1.Begin);
            Assert.AreEqual(Now, iInter1.End);

            // Intervalle schneiden
            var iInter2 = i0_10.IntersectWith(new mko.Algo.Interval<DateTime>(Now.AddHours(3), Now.AddHours(6)));

            Assert.IsFalse(iInter2.Empty);
            Assert.AreEqual(Now.AddHours(3), iInter2.Begin);
            Assert.AreEqual(Now.AddHours(6), iInter2.End);

            // Intervalle schneiden
            var iInter3 = i0_10.IntersectWith(new mko.Algo.Interval<DateTime>(Now.AddHours(-1), Now.AddHours(6)));

            Assert.IsFalse(iInter3.Empty);
            Assert.AreEqual(Now.AddHours(0), iInter3.Begin);
            Assert.AreEqual(Now.AddHours(6), iInter3.End);

            // Intervalle schneiden
            var iInter4 = i0_10.IntersectWith(new mko.Algo.Interval<DateTime>(Now.AddHours(3), Now.AddHours(12)));

            Assert.IsFalse(iInter4.Empty);
            Assert.AreEqual(Now.AddHours(3), iInter4.Begin);
            Assert.AreEqual(Now.AddHours(10), iInter4.End);


            // Intervalle schneiden
            var iInter5 = i0_10.IntersectWith(new mko.Algo.Interval<DateTime>(Now.AddHours(11), Now.AddHours(12)));

            Assert.IsTrue(iInter5.Empty);
            Assert.AreEqual(Now.AddHours(11), iInter5.Begin);
            Assert.AreEqual(Now.AddHours(10), iInter5.End);

        }

    }
}
