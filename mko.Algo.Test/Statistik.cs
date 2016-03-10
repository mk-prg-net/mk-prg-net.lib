using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using lisp = mko.Algo.Listprocessing;
using stat = mko.Algo.Statistics;

namespace mko.Algo.Test
{
    [TestClass]
    public class Statistik
    {
        [TestMethod]
        public void ElementareStatistikTest()
        {
            var list = lisp.Fn.L(3, 9, 7, -1, 8, 2, 4, 4, 6);

            Assert.AreEqual(stat.Fn.Sum(list), 42);
            Assert.AreEqual(stat.Fn.Min(list), -1);
        }
    }
}
