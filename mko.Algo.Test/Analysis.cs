using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.Algo.Test
{
    [TestClass]
    public class Analysis
    {
        const decimal MinDeltaDeci = 0.000000000000001m;

        [TestMethod]
        public void AnalysisSinTest()
        {
            var res = Radian.RadToDeg(Radian.decPI);
            Assert.IsTrue(Metrics.Distance(res, 180m) < MinDeltaDeci);

            res = Radian.RadToDeg(Radian.decPI / 2m);
            Assert.IsTrue(Metrics.Distance(res, 90m) < MinDeltaDeci);

            res = Radian.DegToRad(180);
            Assert.IsTrue(Metrics.Distance(res, Radian.decPI) < MinDeltaDeci);

            res = Radian.DegToRad(90);
            Assert.IsTrue(Metrics.Distance(res, Radian.decPI/2) < MinDeltaDeci);

            res = mko.Algo.Analysis.InfiniteSeries.Sin(0);
            Assert.IsTrue(Metrics.Distance(res, 0m) < MinDeltaDeci);

            res = mko.Algo.Analysis.InfiniteSeries.Sin(0.1745329m);

            res = mko.Algo.Analysis.InfiniteSeries.Sin(Radian.decPI/2m);
            Assert.IsTrue(Metrics.Distance(res, 1m) < MinDeltaDeci);

            res = mko.Algo.Analysis.InfiniteSeries.Sin(Radian.decPI / 4m);
            Assert.IsTrue(Metrics.Distance(res, 0.70710678118654752440084436210m) < MinDeltaDeci);         

        }
    }
}
