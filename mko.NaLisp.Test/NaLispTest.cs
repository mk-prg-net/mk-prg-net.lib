using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using mko.NaLisp.Factories;
using NL = mko.NaLisp;
using System.Diagnostics;

namespace mko.NaLisp.Test
{

    [TestClass]
    public class NaLispTest
    {


        [TestInitialize]
        public void CreateNaLispTerms()
        {

        }

        [TestMethod]
        public void NaLisp_ExprTest()
        {
            try
            {

                var constInt199 = Int._.Create(99);
                var constInt1 = Int._.Create(1);
                var sum = NL.Core.Evaluator._.Eval(Int._.ADD(constInt199, constInt1));

                Assert.IsInstanceOfType(sum, typeof(NL.Data.ConstValComp<int>), "Konstante int erwartet");
                Assert.AreEqual(((NL.Data.ConstValComp<int>)sum).Value, 100, "Das Ergebnis aus (1+99) sollte 100 sein");

                var resMulSum = NL.Core.Evaluator._.Eval(Int._.MUL(Int._.ADD(Int._.Create(1), Int._.Create(99)), Int._.Create(3)));
                Assert.IsInstanceOfType(resMulSum, typeof(NL.Data.ConstValComp<int>), "konstanter int erwartet");
                Assert.AreEqual(((NL.Data.ConstValComp<int>)resMulSum).Value, 300, "Das Ergebnis aus (1+99)*3 sollte 300 sein");


                var ich_habe_einen_fahrschein = Bool._.Create(false);
                var ich_fahre_bahn = Bool._.Create(true);

                var ich_bin_ein_Schwarzfahrer = NL.Core.Evaluator._.Eval(Bool._.AND(Bool._.NOT(ich_habe_einen_fahrschein), ich_fahre_bahn));
                Assert.IsTrue(((NL.Data.ConstVal<bool>)ich_bin_ein_Schwarzfahrer).Value, "Fahren ohne Fahrschein sollt nicht ok sein");

                var erlebnisse =  NL.Core.Evaluator._.Eval(Ctrl._.IfThen(ich_bin_ein_Schwarzfahrer, Txt._.Create("Bitte 40 Euro zahlen"), Txt._.Create("Gute Fahrt")));
                Assert.AreEqual(((NL.Data.ConstVal<string>)erlebnisse).Value, "Bitte 40 Euro zahlen", "Alles sollte was koten");


            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }        

        [TestMethod]
        public async System.Threading.Tasks.Task NaLisp_InspectorTest()
        {

            var nl = Int._.MUL(Int._.ADD(Int._.Create(1), Int._.Create(99)), Int._.Create(3));
            var res = await NL.Core.Inspector._.ValidateAsync(nl);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<int>), res.TypeOfEvaluated);
        }



    }

}
