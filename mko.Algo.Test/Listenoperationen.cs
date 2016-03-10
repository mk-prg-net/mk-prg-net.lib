using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using mko.Algo.Listprocessing;

// Mittels using wird ein Typalias für dan langen Typnamen der hier betrachteten Listen vereinbart
using Lint = System.Collections.Generic.IEnumerable<int>;

namespace mko.Algo.Test
{    
    [TestClass]
    public class Listenoperationen
    {
        Lint PrimListe = Fn.L(2, 3, 5, 7, 11, 13, 17, 19);

        [TestMethod]
        public void ListprocessingFunctionalTest()
        {
            // Clonen testen
            // - gleiche Listen
            Assert.IsTrue(Fn.Equal(Fn.Clone(Fn.L(2, 3, 5, 7)), Fn.L(2, 3, 5, 7)));
            // - verschiedene Instanzen
            Assert.IsTrue(!Fn.Clone(Fn.L(2, 3, 5, 7)).Equals(Fn.L(2, 3, 5, 7)));

            // - Element auslesen
            Assert.IsTrue(5 == Fn.Get(Fn.L(2, 3, 5, 7), 2));

            // - Element setzen
            Assert.IsTrue(Fn.Equal(Fn.Set(Fn.L(2, 3, 5, 7), 2, 13), Fn.L(2, 3, 13, 7)));

            // Swap Testen
            // - Anfang und Ende tauschen
            var l = Fn.Swap(Fn.L(2, 3, 5, 7), 0, 3);
            Assert.IsTrue(Fn.Equal(l, Fn.L(7, 3, 5, 2)));
            // - in der Mitte tauschen
            l = Fn.SwapComplex(Fn.L(2, 3, 5, 7), 1, 2);
            Assert.IsTrue(Fn.Equal(l, Fn.L(2, 5, 3, 7)));
            // - ersten gegen Element aus Liste tauschen
            l = Fn.SwapComplex(Fn.L(2, 3, 5, 7), 0, 2);
            Assert.IsTrue(Fn.Equal(l, Fn.L(5, 3, 2, 7)));
            // - letzten gegen Element aus Liste tauschen
            l = Fn.SwapComplex(Fn.L(2, 3, 5, 7), 1, 3);
            Assert.IsTrue(Fn.Equal(l, Fn.L(2, 7, 5, 3)));
            // -alle Test mit jetzt mit vertauschten Indizes
            l = Fn.SwapComplex(Fn.L(2, 3, 5, 7), 3, 0);
            Assert.IsTrue(Fn.Equal(l, Fn.L(7, 3, 5, 2)));
            l = Fn.SwapComplex(Fn.L(2, 3, 5, 7), 2, 1);
            Assert.IsTrue(Fn.Equal(l, Fn.L(2, 5, 3, 7)));
            l = Fn.SwapComplex(Fn.L(2, 3, 5, 7), 2, 0);
            Assert.IsTrue(Fn.Equal(l, Fn.L(5, 3, 2, 7)));
            l = Fn.SwapComplex(Fn.L(2, 3, 5, 7), 3, 1);
            Assert.IsTrue(Fn.Equal(l, Fn.L(2, 7, 5, 3)));

            // SwapOpt Testen
            // - Anfang und Ende tauschen
            l = Fn.Swap(Fn.L(2, 3, 5, 7), 0, 3);
            Assert.IsTrue(Fn.Equal(l, Fn.L(7, 3, 5, 2)));
            // - in der Mitte tauschen
            l = Fn.Swap(Fn.L(2, 3, 5, 7), 1, 2);
            Assert.IsTrue(Fn.Equal(l, Fn.L(2, 5, 3, 7)));
            // - ersten gegen Element aus Liste tauschen
            l = Fn.Swap(Fn.L(2, 3, 5, 7), 0, 2);
            Assert.IsTrue(Fn.Equal(l, Fn.L(5, 3, 2, 7)));
            // - letzten gegen Element aus Liste tauschen
            l = Fn.Swap(Fn.L(2, 3, 5, 7), 1, 3);
            Assert.IsTrue(Fn.Equal(l, Fn.L(2, 7, 5, 3)));
            // -alle Test mit jetzt mit vertauschten Indizes
            l = Fn.Swap(Fn.L(2, 3, 5, 7), 3, 0);
            Assert.IsTrue(Fn.Equal(l, Fn.L(7, 3, 5, 2)));
            l = Fn.Swap(Fn.L(2, 3, 5, 7), 2, 1);
            Assert.IsTrue(Fn.Equal(l, Fn.L(2, 5, 3, 7)));
            l = Fn.Swap(Fn.L(2, 3, 5, 7), 2, 0);
            Assert.IsTrue(Fn.Equal(l, Fn.L(5, 3, 2, 7)));
            l = Fn.Swap(Fn.L(2, 3, 5, 7), 3, 1);
            Assert.IsTrue(Fn.Equal(l, Fn.L(2, 7, 5, 3)));

        }
    }
}
