using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.RPN.Test
{
    [TestClass]
    public class mkoRPN_Composer
    {
        [TestMethod]
        public void mkoRPN_Composer_pn()
        {
            var composer = new Composer(new mko.RPN.FunctionNamesLight());

            var trio = composer.Trio(1, 2, 3); //.Trim();
            Assert.AreEqual(".trio 1 2 3", trio);

            var quattro = composer.Quattro(1, 2, 3, 4); //.Trim();
            Assert.AreEqual(".quattro 1 2 3 4", quattro);

            var prod = composer.VectorProdukt(
                                composer.Trio(1, 2, 3),
                                composer.Trio(10, 20, 30));

            Assert.AreEqual(".vec.prod .trio 1 2 3 .trio 10 20 30", prod);


            var liste = composer.IntList(1, 2, 3, 4, 5); //.Trim();
            Assert.AreEqual(".int.list 1 2 3 4 5 .Lend", liste);
        }

        [TestMethod]
        public void mkoRPN_ComposerStrong_pn()
        {
            var composer = new ComposerStrong(new FunctionNamesStrong());

            var trio = composer.Trio(1, 2, 3); //.Trim();
            Assert.AreEqual(".trio .int 1 .int 2 .int 3", trio);

            var quattro = composer.Quattro(1, 2, 3, 4); //.Trim();
            Assert.AreEqual(".quattro .int 1 .int 2 .int 3 .int 4", quattro);

            var prod = composer.VectorProdukt(
                                composer.Trio(1, 2, 3),
                                composer.Trio(10, 20, 30));

            Assert.AreEqual(".vec.prod .trio .int 1 .int 2 .int 3 .trio .int 10 .int 20 .int 30", prod);


            var liste = composer.IntList(1, 2, 3, 4, 5); //.Trim();
            Assert.AreEqual(".int.list .int 1 .int 2 .int 3 .int 4 .int 5 .Lend", liste);
        }

        [TestMethod]
        public void mkoRPN_Composer_rpn()
        {
            var composer = new Composer(new FunctionNamesLight());

            var trio = composer.rTrio(1, 2, 3);
            Assert.AreEqual("3 2 1 .trio", trio);

            var quattro = composer.rQuattro(1, 2, 3, 4);
            Assert.AreEqual("4 3 2 1 .quattro", quattro);

            var prod = composer.rVectorProdukt(
                                    composer.rTrio(1, 2, 3),
                                    composer.rTrio(10, 20, 30));

            Assert.AreEqual("30 20 10 .trio 3 2 1 .trio .vec.prod", prod);


            var liste = composer.rIntList(1, 2, 3, 4, 5);
            Assert.AreEqual(".Lend 5 4 3 2 1 .int.list", liste);
        }


        public void mkoRPN_ComposerStrong_rpn()
        {
            var composer = new ComposerStrong(new FunctionNamesStrong());

            var trio = composer.rTrio(1, 2, 3);
            Assert.AreEqual("3 .int 2 .int 1 .int .trio", trio);

            var quattro = composer.rQuattro(1, 2, 3, 4);
            Assert.AreEqual("4 .int 3 .int 2 .int 1 .int .quattro", quattro);

            var prod = composer.rVectorProdukt(
                                    composer.rTrio(1, 2, 3),
                                    composer.rTrio(10, 20, 30));

            Assert.AreEqual("30 .int 20 .int 10 .int .trio 3 .int 2 .int 1 .int .trio .vec.prod", prod);


            var liste = composer.rIntList(1, 2, 3, 4, 5);
            Assert.AreEqual(".Lend 5 .int 4 .int 3 .int 2 .int 1 .int .int.list", liste);
        }


    }
}
