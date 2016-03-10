using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using G = mko.Algo.GraphTheory;

namespace mko.Algo.Test
{
    [TestClass]
    public class Grafentheorie
    {
        [TestMethod]
        public void PathfinderTest()
        {
            var pf = new G.Pathfinder();

            pf.MaxPathLength = 4;
            pf.MaxCountSolutions = 1;
            pf.Solve();

            //pf.MaxPathLength = 5;
            //pf.SolveSerial();

        }
    }
}
