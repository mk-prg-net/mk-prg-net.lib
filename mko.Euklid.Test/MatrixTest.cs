using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.Euklid.UnitTest
{
    [TestClass]
    public class MatrixTest
    {
        const int RoundDigits = 6;

        [TestMethod]
        public void TestInvertieren()
        {

            Transformations.Matrix M = new Transformations.Matrix(2, 2);
            M.SetLine(0, 1, 0);
            M.SetLine(1, 0, 1);

            Transformations.Matrix Iinv;
            if (!M.Invert(out Iinv))
                Assert.Fail("Einheitsmatrix konnte nicht invertiert werden");

            if(!Iinv.Round(RoundDigits).Equals(Transformations.Matrix.CreateIdentityMatrix(2).Round(RoundDigits)))
                Assert.Fail("Invertieren der Einheitsmatrix liefert falsches Ergebnis");

            Transformations.Matrix M3 = new Transformations.Matrix(3, 3);
            M3.SetLine(0, 1, 2, 1);
            M3.SetLine(1, 1, 0, 2);
            M3.SetLine(2, 1, -1, 1);

            Transformations.Matrix M3inv;
            if(!M3.Invert(out M3inv))
                Assert.Fail("Die reguläre Testmatrix konnte nicht invertiert werden");

            Transformations.Matrix M3invSoll = new Transformations.Matrix(3, 3);
            M3invSoll.SetLine(0, 2.0/3.0, -1.0, 4.0/3.0);
            M3invSoll.SetLine(1, 1.0/3.0, 0.0, -1.0/3.0);
            M3invSoll.SetLine(2, -1.0/3.0, 1.0, -2.0/3.0);

            if(!M3inv.Round(RoundDigits).Equals(M3invSoll.Round(RoundDigits)))
                Assert.Fail("Invertierte Testmatrix stimmt nicht mit dem Sollergebnis überein");

        }
    }
}
