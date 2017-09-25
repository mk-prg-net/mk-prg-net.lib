using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MkPrgNet.Tools.Async.Test
{
    [TestClass]
    public class ProgressorWithStatisticalEstimationTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var progressor = new ProgressorStatisticallyBasedPrediction(2000);
            progressor.WorkDone += wd => Debug.WriteLine(wd + "%");

            Debug.WriteLine("1. mit 1500");
            await progressor.StartAsync(() => System.Threading.Thread.Sleep(1500));

            Debug.WriteLine("2. mit 1500");
            await progressor.StartAsync(() => System.Threading.Thread.Sleep(1500));

            Debug.WriteLine("3. mit 2500");
            await progressor.StartAsync(() => System.Threading.Thread.Sleep(2500));

            Debug.WriteLine("4. mit 2500");
            await progressor.StartAsync(() => System.Threading.Thread.Sleep(2500));

            Debug.WriteLine("5. mit 2700");
            await progressor.StartAsync(() => System.Threading.Thread.Sleep(2700));


        }


    }
}
