using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Cb = mko.Algo.Combinatorics.Permutations;
using Lisp = mko.Algo.Listprocessing;
using System.Diagnostics;

namespace mko.Algo.Test
{
    [TestClass]
    public class Kombinatorik
    {
        [TestMethod]
        public void FakultaetTest()
        {
            Assert.AreEqual(Cb.Fn.Fact(0), 1);
            Assert.AreEqual(Cb.Fn.Fact(1), 1);
            Assert.AreEqual(Cb.Fn.Fact(2), 2);
            Assert.AreEqual(Cb.Fn.Fact(3), 6);
            Assert.AreEqual(Cb.Fn.Fact(6), 720);
        }

        [TestMethod]
        public void PermutationenTest()
        {
            long[] set1 = {1, 2};

            IEnumerable<long> l = Cb.Fn.FirstPermutation(2);
            Assert.IsTrue(Lisp.Fn.Equal(set1, l));

            l = Cb.Fn.NextPermutation(set1, 1);
            Assert.IsTrue(Lisp.Fn.Equal(l, new long[]{2, 1}));

            //l = Cb.Fn.NextPermutation(set1, 3);
            //Assert.IsTrue(Lisp.Fn.Equal(l, new long[] { 1, 2 }));

            var set2 = Cb.Fn.FirstPermutation(3);
            Assert.IsTrue(Lisp.Fn.Equal(set2, new long[] { 1, 2, 3 }));

            var all = Cb.Fn.AllPermutations(3);
            var soll =  new IEnumerable<long>[]{
                new long[]{1, 2, 3}, new long[]{2, 1, 3}, new long[]{2, 3, 1},
                new long[]{3, 2, 1}, new long[]{3, 1, 2}, new long[]{1, 3, 2}
            };

            int i = 0;
            foreach (var perm in all)
            {
                Debug.Write("{");
                foreach (var elem in perm)
                {
                    Debug.Write("" + elem + ", ");
                }
                Debug.Write("}");
                Assert.IsTrue(Lisp.Fn.Equal(perm, soll[i]));
                i++;
            }
        }
    }
}
