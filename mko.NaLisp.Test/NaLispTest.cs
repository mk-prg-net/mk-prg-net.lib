using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


using NL = mko.NaLisp;
using NLF = mko.NaLisp.Factory;
using System.Diagnostics;

namespace mko.NaLisp.Test
{

    [TestClass]
    public class NaLispTest
    {

        NL.Data.ConstValComp<int> Int1 = NLF.Int(1);
        NL.Data.ConstValComp<int> Int2 = NLF.Int(2);
        NL.Data.ConstValComp<int> Int3 = NLF.Int(3);
        NL.Data.ConstValComp<int> Int4 = NLF.Int(4);
        NL.Data.ConstValComp<int> Int5 = NLF.Int(5);

        NL.Data.ConstValComp<double> Dbl1 = NLF.Dbl(1.0);
        NL.Data.ConstValComp<double> Dbl2 = NLF.Dbl(2.0);
        NL.Data.ConstValComp<double> Dbl3 = NLF.Dbl(3.0);
        NL.Data.ConstValComp<double> Dbl4 = NLF.Dbl(4.0);
        NL.Data.ConstValComp<double> Dbl5 = NLF.Dbl(5.0);
        NL.Data.ConstValComp<double> DblPI = NLF.Dbl(Math.PI);


        NL.Core.NaLisp AddIntSimple = NLF.ADD_to_int(1, 2, 3, 4, 5);
        NL.Core.NaLisp AddDblSimple = NLF.ADD_to_dbl(1.0, 2.0, 3.0, 4.0, 5.0);

        NL.Core.NaLisp AddInt;
        NL.Core.NaLisp SubInt;
        NL.Core.NaLisp MulInt;
        NL.Core.NaLisp DivInt;

        NL.Core.NaLisp AddDbl;
        NL.Core.NaLisp SubDbl;
        NL.Core.NaLisp MulDbl;
        NL.Core.NaLisp DivDbl;


        NL.Core.NaLisp MulAddInt;
        NL.Core.NaLisp MulAddDbl;
        NL.Core.NaLisp Div1_2;

        NL.Core.NaLisp GT1;
        NL.Core.NaLisp GT2;

        NL.Core.NaLisp GE1;
        NL.Core.NaLisp GE2;
        NL.Core.NaLisp GE3;

        NL.Core.NaLisp LT1;
        NL.Core.NaLisp LT2;

        NL.Core.NaLisp LE1;
        NL.Core.NaLisp LE2;
        NL.Core.NaLisp LE3;


        NL.Core.NaLisp Rabatt1;
        NL.Core.NaLisp Rabatt2;

        NL.Core.NaLisp Pipe1;
        NL.Core.NaLisp Pipe2;

        NL.Core.NaLisp InvalidExpr1;
        NL.Core.NaLisp InvalidExpr2;
        NL.Core.NaLisp InvalidExpr3;
        NL.Core.NaLisp InvalidExpr4;


        NL.Core.NaLisp Variablen_Mw3;
        Dictionary<string, NL.Data.VarOfComp<double>> Variablen = new Dictionary<string, NL.Data.VarOfComp<double>>();
        
        const string a_dbl = "adbl";
        const string b_dbl = "bdbl";
        const string c_dbl = "cdbl";
        const string d_dbl = "ddbl";
        const string e_dbl = "edbl";
        

        NL.Core.NaLisp Tupel1;
        NL.Core.NaLisp TupelConcat;

        NL.Core.NaLisp Take3;
        NL.Core.NaLisp Skip3;

        NL.Core.NaLisp AddList;

        NL.Core.NaLisp PipeTupelAdd;


        [TestInitialize]
        public void CreateNaLispTerms()
        {
            // Integerops
            // AddInt <=> 1+2+3+4+5
            AddInt = NLF.ADD_to_int(Int1, Int2, Int3, Int4, Int5);

            // SubInt <=> 1-2-3-4-5
            SubInt = NLF.SUB_to_int(Int1, Int2, Int3, Int4, Int5);

            // MulInt <=> 1*2*3*4*5
            MulInt = NLF.MUL_to_int(Int1, Int2, Int3, Int4, Int5);

            // MulInt <=> 1/2/3/4/5
            DivInt = NLF.DIV_to_int(Int1, Int2, Int3, Int4, Int5);

            // Doubleops
            // AddDbl <=> 1.0+2.0+3.0+4.0+5.0
            AddDbl = NLF.ADD_to_dbl(Dbl1, Dbl2, Dbl3, Dbl4, Dbl5);

            // SubInt <=> 1-2-3-4-5
            SubDbl = NLF.SUB_to_dbl(Dbl1, Dbl2, Dbl3, Dbl4, Dbl5);

            // MulInt <=> 1*2*3*4*5
            MulDbl = NLF.MUL_to_dbl(Dbl1, Dbl2, Dbl3, Dbl4, Dbl5);

            // MulInt <=> 1/2/3/4/5
            DivDbl = NLF.DIV_to_dbl(Dbl1, Dbl2, Dbl3, Dbl4, Dbl5);

            // Div1_2 = 1.0/2.0
            Div1_2 = NLF.DIV_to_dbl(Dbl1, Dbl2);


            // Kombinierte Integer und Doubleops
            // MulAddInt <=> 2*(1+2+3+4+5)
            MulAddInt = NLF.MUL_to_int(Int2, AddInt);
            NLF.MUL_to_int(NLF.Int(2), NLF.ADD_to_int(NLF.Int(1), NLF.Int(2), NLF.Int(3), NLF.Int(4), NLF.Int(5)));
            // MulAddDbl <=> 2*(1.0+2.0+3.0+4.0+5.0)
            MulAddDbl = NLF.MUL_to_dbl(Dbl2, AddDbl);


            // VErgleichsoperatoren

            GT1 = NLF.GT<int>(Int1, Int2);
            GT2 = NLF.GT<int>(Int2, Int1);

            GE1 = NLF.Instance.GE<int>(Int1, Int2);
            GE2 = NLF.GE<int>(Int2, Int1);
            GE3 = NLF.GE<int>(Int1, Int1);

            LT1 = NLF.LT<int>(Int1, Int2);
            LT2 = NLF.LT<int>(Int2, Int1);

            LE1 = NLF.LE<int>(Int1, Int2);
            LE2 = NLF.LE<int>(Int2, Int1);
            LE3 = NLF.LE<int>(Int1, Int1);


            // Rabatt1 
            Rabatt1 = NLF.IfThen(NLF.GT<double>(AddDbl, NLF.Dbl(10)),
                NLF.SUB_to_dbl(AddDbl, NLF.MUL_to_dbl(AddDbl, NLF.Dbl(0.10))),
                AddDbl);

            Rabatt2 = NLF.IfThen(NLF.GT<double>(AddDbl, NLF.Dbl(20)),
                NLF.SUB_to_dbl(AddDbl, NLF.MUL_to_dbl(AddDbl, NLF.Dbl(0.10))),
                AddDbl);

            // Pipe(2.0, Add(13), Mul(2)) -> Mul(Add(2, 13), 2)
            Pipe1 = NLF.Pipe(NLF.Dbl(2.0), NLF.ADD_to_dbl(NLF.Dbl(13)), NLF.MUL_to_dbl(NLF.Dbl(2)));

            // 
            Pipe2 = NLF.Pipe(NLF.Dbl(2.0), NLF.Pipe(NLF.ADD_to_dbl(NLF.Dbl(13)), NLF.MUL_to_dbl(NLF.Dbl(2))));

            //// Fehler: Operanden verschiedenen Typs
            //InvalidExpr1 = new NL.MathOps.ADDtoDbl(Int1, Dbl2);

            // Fehler: Ganzzahldivision durch 0
            InvalidExpr2 = NLF.DIV_to_int(NLF.Int(1), NLF.Int(0));

            // Fehler: Operand mit inkompatiblen Typ
            InvalidExpr3 = NLF.DIV_to_int(NLF.Bool(true), Int2);

            // Falscher Subausdruck
            InvalidExpr4 = NLF.DIV_to_int(Int2, InvalidExpr3);

            
            Variablen[a_dbl] = NLF.VarDbl(a_dbl);
            Variablen[b_dbl] = NLF.VarDbl(b_dbl);
            Variablen[c_dbl] = NLF.VarDbl(c_dbl);
            Variablen[d_dbl] = NLF.VarDbl(d_dbl);
            Variablen[e_dbl] = NLF.VarDbl(e_dbl);

            Variablen_Mw3 = NLF.DIV_to_dbl(NLF.ADD_to_dbl(Variablen[a_dbl], Variablen[b_dbl], Variablen[c_dbl]), NLF.Dbl(3.0));

            Tupel1 = NLF.Tuple(2, 3, 4);
            TupelConcat = NLF.Tuple(NLF.Tuple(1, 2, 3, 4, 5), NLF.Tuple(6, 7, 8, 9, 10));

            Take3 = NLF.Take(3, TupelConcat);
            Skip3 = NLF.Skip(3, TupelConcat);

            AddList = NLF.ADD_to_int(Int1, Tupel1, Int5);

            PipeTupelAdd = NLF.Pipe(TupelConcat, NLF.ADD_to_int(NLF.Int(0)));


        }

        [TestMethod]
        public void NaLisp_ExprTest()
        {
            string Name = AddInt.Name;
            string Name2 = AddDbl.Name;
        }        

        [TestMethod]
        public async System.Threading.Tasks.Task NaLisp_InspectorTest()
        {
            var Inspect = new NL.Core.Inspector();

            var res = Inspect.Validate(AddInt);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<int>), res.TypeOfEvaluated);

            res = null;
            res = await Inspect.ValidateAsync(AddInt);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<int>), res.TypeOfEvaluated);


            res = Inspect.Validate(SubInt);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<int>), res.TypeOfEvaluated);

            res = Inspect.Validate(MulInt);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<int>), res.TypeOfEvaluated);

            res = Inspect.Validate(DivInt);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<int>), res.TypeOfEvaluated);


            res = Inspect.Validate(AddDbl);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<double>), res.TypeOfEvaluated);

            res = Inspect.Validate(SubDbl);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<double>), res.TypeOfEvaluated);

            res = Inspect.Validate(MulDbl);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<double>), res.TypeOfEvaluated);

            res = Inspect.Validate(DivDbl);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<double>), res.TypeOfEvaluated);


            res = Inspect.Validate(AddIntSimple);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);

            res = Inspect.Validate(AddDblSimple);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<double>), res.TypeOfEvaluated);

            res = Inspect.Validate(MulAddInt);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);

            res = Inspect.Validate(MulAddDbl);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<double>), res.TypeOfEvaluated);

            res = Inspect.Validate(Div1_2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<double>), res.TypeOfEvaluated);

            //res = Inspect.Validate(InvalidExpr1);
            //Assert.IsFalse(res.IsCurrentValid);
            //Assert.IsTrue(res.IsTreeValid);

            res = Inspect.Validate(InvalidExpr2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<int>), res.TypeOfEvaluated);

            res = Inspect.Validate(InvalidExpr3);
            Assert.IsFalse(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);

            res = Inspect.Validate(InvalidExpr4);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsFalse(res.IsTreeValid);

            res = Inspect.Validate(GT1);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstVal<bool>), res.TypeOfEvaluated);

            res = Inspect.Validate(GT2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstVal<bool>), res.TypeOfEvaluated);

            res = Inspect.Validate(GE1);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstVal<bool>), res.TypeOfEvaluated);

            res = Inspect.Validate(GE2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstVal<bool>), res.TypeOfEvaluated);

            res = Inspect.Validate(GE3);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstVal<bool>), res.TypeOfEvaluated);

            res = Inspect.Validate(LT1);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstVal<bool>), res.TypeOfEvaluated);

            res = Inspect.Validate(LT2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstVal<bool>), res.TypeOfEvaluated);

            res = Inspect.Validate(LE1);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstVal<bool>), res.TypeOfEvaluated);

            res = Inspect.Validate(LE2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstVal<bool>), res.TypeOfEvaluated);

            res = Inspect.Validate(LE3);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstVal<bool>), res.TypeOfEvaluated);

            res = Inspect.Validate(Rabatt1);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<double>), res.TypeOfEvaluated);

            res = Inspect.Validate(Rabatt2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<double>), res.TypeOfEvaluated);

            res = Inspect.Validate(Pipe1);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<double>), res.TypeOfEvaluated);

            res = Inspect.Validate(Pipe2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<double>), res.TypeOfEvaluated);

            res = Inspect.Validate(Tupel1);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Lisp.Tuple), res.TypeOfEvaluated);

            res = Inspect.Validate(TupelConcat);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Lisp.Tuple), res.TypeOfEvaluated);

            res = Inspect.Validate(Take3);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Lisp.Tuple), res.TypeOfEvaluated);

            res = Inspect.Validate(Skip3);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Lisp.Tuple), res.TypeOfEvaluated);


            res = Inspect.Validate(AddList);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<int>), res.TypeOfEvaluated);

            res = Inspect.Validate(PipeTupelAdd);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<int>), res.TypeOfEvaluated);


            res = Inspect.Validate(Variablen_Mw3);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstValComp<double>), res.TypeOfEvaluated);            

        }

        [TestMethod]
        public void NaLisp_EvaluatorDebugTest()
        {
            var Inspect = new NL.Core.Inspector();
            var Evaluator = new NL.Core.Evaluator();

            try
            {
                // Korrekte Terme werden ausgewertet
                Assert.IsTrue(Inspect.Validate(AddInt).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resAddInt = Evaluator.Eval(AddInt);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(SubInt).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resSubInt = Evaluator.Eval(SubInt);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(MulInt).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resMulInt = Evaluator.Eval(MulInt);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(DivInt).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resSubInt = Evaluator.Eval(DivInt);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(AddDbl).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resAddDbl = Evaluator.Eval(AddDbl);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(SubDbl).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resSubDbl = Evaluator.Eval(SubDbl);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(MulDbl).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resMulDbl = Evaluator.Eval(MulDbl);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(DivDbl).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resDivDbl = Evaluator.Eval(DivDbl);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(MulAddInt).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resMulAddInt = Evaluator.Eval(MulAddInt);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                // Terme mit Laufzeitfehlern werden ausgewertet (Division durch 2)
                Assert.IsTrue(Inspect.Validate(InvalidExpr2).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resInvalidExpr2 = Evaluator.Eval(InvalidExpr2);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try
            {
                // Unkorrekt aufgebauter Term wird versucht, auszuwerten
                Assert.IsFalse(Inspect.Validate(InvalidExpr4).IsTreeValid);
                Evaluator.DebugOn = true;
                var resInvalidExpr4 = Evaluator.Eval(InvalidExpr2);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(GT1).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resGT1 = Evaluator.Eval(GT1);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(GT2).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resGT2 = Evaluator.Eval(GT2);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(LT1).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resLT1 = Evaluator.Eval(LT1);
                Assert.IsTrue(((NL.Data.ConstVal<bool>)resLT1).Value);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(LT2).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resLT2 = Evaluator.Eval(LT2);
                Assert.IsFalse(((NL.Data.ConstVal<bool>)resLT2).Value);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(GE1).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resGE1 = Evaluator.Eval(GE1);
                Assert.IsFalse(((NL.Data.ConstVal<bool>)resGE1).Value);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(GE2).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resGE2 = Evaluator.Eval(GE2);
                Assert.IsTrue(((NL.Data.ConstVal<bool>)resGE2).Value);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(GE3).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resGE3 = Evaluator.Eval(GE3);
                Assert.IsTrue(((NL.Data.ConstVal<bool>)resGE3).Value);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(LE1).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resLE1 = Evaluator.Eval(LE1);
                Assert.IsTrue(((NL.Data.ConstVal<bool>)resLE1).Value);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(LE2).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resLE2 = Evaluator.Eval(LE2);
                Assert.IsFalse(((NL.Data.ConstVal<bool>)resLE2).Value);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(LE3).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resLE3 = Evaluator.Eval(LE3);
                Assert.IsTrue(((NL.Data.ConstVal<bool>)resLE3).Value);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(Rabatt1).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resRabatt1 = Evaluator.Eval(Rabatt1);
                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Assert.IsTrue(Inspect.Validate(Rabatt2).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resRabatt2 = Evaluator.Eval(Rabatt2);                
                Assert.AreEqual(15.0, ((NL.Data.ConstValComp<double>)resRabatt2).Value);
                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Debug.WriteLine(Pipe1.ToString());
                Assert.IsTrue(Inspect.Validate(Pipe1).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(Pipe1);
                Assert.AreEqual(30.0, ((NL.Data.ConstValComp<double>)res).Value);                                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Debug.WriteLine(Pipe2.ToString());
                Assert.IsTrue(Inspect.Validate(Pipe2).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(Pipe2);
                Assert.AreEqual(30.0, ((NL.Data.ConstValComp<double>)res).Value);                

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }


            try
            {
                Debug.WriteLine(Tupel1.ToString());
                Assert.IsTrue(Inspect.Validate(Tupel1).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(Tupel1);
                Assert.IsTrue(res is NL.Lisp.Tuple);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }


            try
            {
                Debug.WriteLine(TupelConcat.ToString());
                Assert.IsTrue(Inspect.Validate(TupelConcat).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(TupelConcat);
                Assert.IsTrue(res is NL.Lisp.Tuple);
                Assert.AreEqual(10, ((NL.Lisp.Tuple)res).Elements.Length);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }


            try
            {
                Debug.WriteLine(Take3.ToString());
                Assert.IsTrue(Inspect.Validate(Take3).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(Take3);
                Assert.IsTrue(res is NL.Lisp.Tuple);
                Assert.AreEqual(3, ((NL.Lisp.Tuple)res).Elements.Length);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Debug.WriteLine(Skip3.ToString());
                Assert.IsTrue(Inspect.Validate(Skip3).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(Skip3);                
                Assert.IsTrue(res is NL.Lisp.Tuple);
                Assert.AreEqual(7, ((NL.Lisp.Tuple)res).Elements.Length);                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Debug.WriteLine(PipeTupelAdd.ToString());
                Assert.IsTrue(Inspect.Validate(PipeTupelAdd).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(PipeTupelAdd);                
                Assert.IsTrue(res is NL.Data.ConstValComp<int>);
                Assert.AreEqual(55, ((NL.Data.ConstValComp<int>)res).Value);                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            
            try
            {
                Debug.WriteLine(AddList.ToString());
                Assert.IsTrue(Inspect.Validate(AddList).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(AddList);                
                Assert.IsTrue(res is NL.Data.ConstValComp<int>);
                Assert.AreEqual(15, ((NL.Data.ConstValComp<int>)res).Value);                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {
                Variablen[a_dbl].Value = 1;
                Variablen[b_dbl].Value = 2;
                Variablen[c_dbl].Value = 3;

                Debug.WriteLine(Variablen_Mw3.ToString());
                Assert.IsTrue(Inspect.Validate(Variablen_Mw3).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(Variablen_Mw3);                
                Assert.IsTrue(res is NL.Data.ConstValComp<double>);
                Assert.AreEqual(2.0, ((NL.Data.ConstValComp<double>)res).Value);                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            try
            {

                Variablen[a_dbl].Value = 10;
                Variablen[b_dbl].Value =20;
                Variablen[c_dbl].Value = 30;

                Debug.WriteLine(Variablen_Mw3.ToString());
                Assert.IsTrue(Inspect.Validate(Variablen_Mw3).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(Variablen_Mw3);
                Assert.IsTrue(res is NL.Data.ConstValComp<double>);
                Assert.AreEqual(20.0, ((NL.Data.ConstValComp<double>)res).Value);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }



        }


    }

}
