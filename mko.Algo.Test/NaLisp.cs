using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


using NL = mko.Algo.FormalLanguages.NaLisp;
using NLF = mko.Algo.FormalLanguages.NaLisp.Factory;

namespace mko.Algo.Test
{

    [TestClass]
    public class NaLisp
    {

        NL.Data.ConstInt Int1 = NLF.Int(1);
        NL.Data.ConstInt Int2 = NLF.Int(2);
        NL.Data.ConstInt Int3 = NLF.Int(3);
        NL.Data.ConstInt Int4 = NLF.Int(4);
        NL.Data.ConstInt Int5 = NLF.Int(5);

        NL.Data.ConstDbl Dbl1 = NLF.Dbl(1.0);
        NL.Data.ConstDbl Dbl2 = NLF.Dbl(2.0);
        NL.Data.ConstDbl Dbl3 = NLF.Dbl(3.0);
        NL.Data.ConstDbl Dbl4 = NLF.Dbl(4.0);
        NL.Data.ConstDbl Dbl5 = NLF.Dbl(5.0);
        NL.Data.ConstDbl DblPI = NLF.Dbl(Math.PI);


        NL.Core.NaLisp AddIntSimple = new NL.MathOps.ADDtoInt(1, 2, 3, 4, 5);
        NL.Core.NaLisp AddDblSimple = new NL.MathOps.ADDtoDbl(1.0, 2.0, 3.0, 4.0, 5.0);

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
        Dictionary<VarName, NL.Data.VariableBase> Variablen = new Dictionary<VarName, NL.Data.VariableBase>();
        enum VarName
        {
            a_dbl,
            b_dbl,
            c_dbl,
            d_dbl,
            e_dbl
        }


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
            AddInt = NLF.ADD_to_Int(Int1, Int2, Int3, Int4, Int5);

            // SubInt <=> 1-2-3-4-5
            SubInt = NLF.SUB_to_Int(Int1, Int2, Int3, Int4, Int5);

            // MulInt <=> 1*2*3*4*5
            MulInt = NLF.MUL_to_Int(Int1, Int2, Int3, Int4, Int5);

            // MulInt <=> 1/2/3/4/5
            DivInt = NLF.DIV_to_Int(Int1, Int2, Int3, Int4, Int5);

            // Doubleops
            // AddDbl <=> 1.0+2.0+3.0+4.0+5.0
            AddDbl = NLF.ADD_to_Dbl(Dbl1, Dbl2, Dbl3, Dbl4, Dbl5);

            // SubInt <=> 1-2-3-4-5
            SubDbl = NLF.SUB_to_Dbl(Dbl1, Dbl2, Dbl3, Dbl4, Dbl5);

            // MulInt <=> 1*2*3*4*5
            MulDbl = NLF.MUL_to_Dbl(Dbl1, Dbl2, Dbl3, Dbl4, Dbl5);

            // MulInt <=> 1/2/3/4/5
            DivDbl = NLF.DIV_to_Dbl(Dbl1, Dbl2, Dbl3, Dbl4, Dbl5);

            // Div1_2 = 1.0/2.0
            Div1_2 = NLF.DIV_to_Dbl(Dbl1, Dbl2);


            // Kombinierte Integer und Doubleops
            // MulAddInt <=> 2*(1+2+3+4+5)
            MulAddInt = NLF.MUL_to_Int(Int2, AddInt);
            NLF.MUL_to_Int(NLF.Int(2), NLF.ADD_to_Int(NLF.Int(1), NLF.Int(2), NLF.Int(3), NLF.Int(4), NLF.Int(5)));
            // MulAddDbl <=> 2*(1.0+2.0+3.0+4.0+5.0)
            MulAddDbl = NLF.MUL_to_Dbl(Dbl2, AddDbl);


            // VErgleichsoperatoren

            GT1 = NLF.GT(Int1, Int2);
            GT2 = NLF.GT(Int2, Int1);

            GE1 = NLF.GE(Int1, Int2);
            GE2 = NLF.GE(Int2, Int1);
            GE3 = NLF.GE(Int1, Int1);

            LT1 = NLF.LT(Int1, Int2);
            LT2 = NLF.LT(Int2, Int1);

            LE1 = NLF.LE(Int1, Int2);
            LE2 = NLF.LE(Int2, Int1);
            LE3 = NLF.LE(Int1, Int1);


            // Rabatt1 
            Rabatt1 = NLF.IfThen(NLF.GT(AddDbl, NLF.Dbl(10)),
                NLF.SUB_to_Dbl(AddDbl, NLF.MUL_to_Dbl(AddDbl, NLF.Dbl(0.10))),
                AddDbl);

            Rabatt2 = NLF.IfThen(NLF.GT(AddDbl, NLF.Dbl(20)),
                NLF.SUB_to_Dbl(AddDbl, NLF.MUL_to_Dbl(AddDbl, NLF.Dbl(0.10))),
                AddDbl);

            // Pipe(2.0, Add(13), Mul(2)) -> Mul(Add(2, 13), 2)
            Pipe1 = NLF.Pipe(NLF.Dbl(2.0), NLF.ADD_to_Dbl(NLF.Dbl(13)), NLF.MUL_to_Dbl(NLF.Dbl(2)));

            // 
            Pipe2 = NLF.Pipe(NLF.Dbl(2.0), NLF.Pipe(NLF.ADD_to_Dbl(NLF.Dbl(13)), NLF.MUL_to_Dbl(NLF.Dbl(2))));

            //// Fehler: Operanden verschiedenen Typs
            //InvalidExpr1 = new NL.MathOps.ADDtoDbl(Int1, Dbl2);

            // Fehler: Ganzzahldivision durch 0
            InvalidExpr2 = NLF.DIV_to_Int(NLF.Int(1), NLF.Int(0));

            // Fehler: Operand mit inkompatiblen Typ
            InvalidExpr3 = NLF.DIV_to_Int(NLF.Bool(true), Int2);

            // Falscher Subausdruck
            InvalidExpr4 = NLF.DIV_to_Int(Int2, InvalidExpr3);

            
            Variablen[VarName.a_dbl] = NLF.VarDbl((int)VarName.a_dbl);
            Variablen[VarName.b_dbl] = NLF.VarDbl((int)VarName.b_dbl);
            Variablen[VarName.c_dbl] = NLF.VarDbl((int)VarName.c_dbl);
            Variablen[VarName.d_dbl] = NLF.VarDbl((int)VarName.d_dbl);
            Variablen[VarName.e_dbl] = NLF.VarDbl((int)VarName.e_dbl);

            Variablen_Mw3 = NLF.DIV_to_Dbl(NLF.ADD_to_Dbl(Variablen[VarName.a_dbl], Variablen[VarName.b_dbl], Variablen[VarName.c_dbl]), NLF.Int(3));

            Tupel1 = NLF.Tuple(2, 3, 4);
            TupelConcat = NLF.Tuple(NLF.Tuple(1, 2, 3, 4, 5), NLF.Tuple(6, 7, 8, 9, 10));

            Take3 = NLF.Take(3, TupelConcat);
            Skip3 = NLF.Skip(3, TupelConcat);

            AddList = NLF.ADD_to_Int(Int1, Tupel1, Int5);

            PipeTupelAdd = NLF.Pipe(TupelConcat, NLF.ADD_to_Int(NLF.Int(0)));


        }


        [TestMethod]
        public void NaLisp_InspectorTest()
        {
            var Inspect = new NL.Core.Inspector();

            var res = Inspect.Validate(AddInt);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstInt), res.TypeOfEvaluated);

            res = Inspect.Validate(SubInt);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstInt), res.TypeOfEvaluated);

            res = Inspect.Validate(MulInt);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstInt), res.TypeOfEvaluated);

            res = Inspect.Validate(DivInt);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstInt), res.TypeOfEvaluated);


            res = Inspect.Validate(AddDbl);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstDbl), res.TypeOfEvaluated);

            res = Inspect.Validate(SubDbl);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstDbl), res.TypeOfEvaluated);

            res = Inspect.Validate(MulDbl);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstDbl), res.TypeOfEvaluated);

            res = Inspect.Validate(DivDbl);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstDbl), res.TypeOfEvaluated);


            res = Inspect.Validate(AddIntSimple);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);

            res = Inspect.Validate(AddDblSimple);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstDbl), res.TypeOfEvaluated);

            res = Inspect.Validate(MulAddInt);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);

            res = Inspect.Validate(MulAddDbl);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstDbl), res.TypeOfEvaluated);

            res = Inspect.Validate(Div1_2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstDbl), res.TypeOfEvaluated);

            //res = Inspect.Validate(InvalidExpr1);
            //Assert.IsFalse(res.IsCurrentValid);
            //Assert.IsTrue(res.IsTreeValid);

            res = Inspect.Validate(InvalidExpr2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstInt), res.TypeOfEvaluated);

            res = Inspect.Validate(InvalidExpr3);
            Assert.IsFalse(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);

            res = Inspect.Validate(InvalidExpr4);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsFalse(res.IsTreeValid);

            res = Inspect.Validate(GT1);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstBool), res.TypeOfEvaluated);

            res = Inspect.Validate(GT2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstBool), res.TypeOfEvaluated);

            res = Inspect.Validate(GE1);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstBool), res.TypeOfEvaluated);

            res = Inspect.Validate(GE2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstBool), res.TypeOfEvaluated);

            res = Inspect.Validate(GE3);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstBool), res.TypeOfEvaluated);

            res = Inspect.Validate(LT1);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstBool), res.TypeOfEvaluated);

            res = Inspect.Validate(LT2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstBool), res.TypeOfEvaluated);

            res = Inspect.Validate(LE1);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstBool), res.TypeOfEvaluated);

            res = Inspect.Validate(LE2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstBool), res.TypeOfEvaluated);

            res = Inspect.Validate(LE3);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstBool), res.TypeOfEvaluated);

            res = Inspect.Validate(Rabatt1);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstDbl), res.TypeOfEvaluated);

            res = Inspect.Validate(Rabatt2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstDbl), res.TypeOfEvaluated);

            res = Inspect.Validate(Pipe1);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstDbl), res.TypeOfEvaluated);

            res = Inspect.Validate(Pipe2);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstDbl), res.TypeOfEvaluated);

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
            Assert.AreEqual(typeof(NL.Data.ConstInt), res.TypeOfEvaluated);

            res = Inspect.Validate(PipeTupelAdd);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstInt), res.TypeOfEvaluated);


            res = Inspect.Validate(Variablen_Mw3);
            Assert.IsTrue(res.IsCurrentValid);
            Assert.IsTrue(res.IsTreeValid);
            Assert.AreEqual(typeof(NL.Data.ConstDbl), res.TypeOfEvaluated);



            

        }

        [TestMethod]
        public void NaLisp_EvaluatorDebugTest()
        {
            var Inspect = new NL.Core.Inspector();
            var Evaluator = new NL.Core.Evaluator();

            {
                // Korrekte Terme werden ausgewertet
                Assert.IsTrue(Inspect.Validate(AddInt).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resAddInt = Evaluator.Eval(AddInt);
                Assert.IsTrue(resAddInt.Valid);
                //Assert.IsTrue(resAddInt.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resAddInt.ResultTerm, typeof(NL.Data.ConstInt));
                Assert.AreEqual(15, ((NL.Data.ConstInt)resAddInt.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(SubInt).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resSubInt = Evaluator.Eval(SubInt);
                Assert.IsTrue(resSubInt.Valid);
                //Assert.IsTrue(resSubInt.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resSubInt.ResultTerm, typeof(NL.Data.ConstInt));
                Assert.AreEqual(-13, ((NL.Data.ConstInt)resSubInt.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(MulInt).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resMulInt = Evaluator.Eval(MulInt);
                Assert.IsTrue(resMulInt.Valid);
                //Assert.IsTrue(resMulInt.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resMulInt.ResultTerm, typeof(NL.Data.ConstInt));
                Assert.AreEqual(120, ((NL.Data.ConstInt)resMulInt.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(DivInt).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resSubInt = Evaluator.Eval(DivInt);
                Assert.IsTrue(resSubInt.Valid);
                //Assert.IsTrue(resSubInt.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resSubInt.ResultTerm, typeof(NL.Data.ConstInt));
                Assert.AreEqual(0, ((NL.Data.ConstInt)resSubInt.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(AddDbl).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resAddDbl = Evaluator.Eval(AddDbl);
                Assert.IsTrue(resAddDbl.Valid);
                //Assert.IsTrue(resAddDbl.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resAddDbl.ResultTerm, typeof(NL.Data.ConstDbl));
                Assert.AreEqual(15, ((NL.Data.ConstDbl)resAddDbl.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(SubDbl).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resSubDbl = Evaluator.Eval(SubDbl);
                Assert.IsTrue(resSubDbl.Valid);
                //Assert.IsTrue(resSubDbl.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resSubDbl.ResultTerm, typeof(NL.Data.ConstDbl));
                Assert.AreEqual(-13, ((NL.Data.ConstDbl)resSubDbl.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(MulDbl).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resMulDbl = Evaluator.Eval(MulDbl);
                Assert.IsTrue(resMulDbl.Valid);
                //Assert.IsTrue(resMulDbl.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resMulDbl.ResultTerm, typeof(NL.Data.ConstDbl));
                Assert.AreEqual(120, ((NL.Data.ConstDbl)resMulDbl.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(DivDbl).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resDivDbl = Evaluator.Eval(DivDbl);
                Assert.IsTrue(resDivDbl.Valid);
                //Assert.IsTrue(resDivDbl.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resDivDbl.ResultTerm, typeof(NL.Data.ConstDbl));
                Assert.AreEqual(0.00833, Math.Round(((NL.Data.ConstDbl)resDivDbl.ResultTerm).Value, 5));
            }

            {
                Assert.IsTrue(Inspect.Validate(MulAddInt).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resMulAddInt = Evaluator.Eval(MulAddInt);
                Assert.IsTrue(resMulAddInt.Valid);
                //Assert.IsTrue(resMulAddInt.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resMulAddInt.ResultTerm, typeof(NL.Data.ConstInt));
                Assert.AreEqual(30, ((NL.Data.ConstInt)resMulAddInt.ResultTerm).Value);
            }

            {
                // Terme mit Laufzeitfehlern werden ausgewertet (Division durch 2)
                Assert.IsTrue(Inspect.Validate(InvalidExpr2).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resInvalidExpr2 = Evaluator.Eval(InvalidExpr2);
                Assert.IsFalse(resInvalidExpr2.Valid);
                Debug.WriteLine(resInvalidExpr2.ResultProtocolEntry.Description);
            }

            {
                // Unkorrekt aufgebauter Term wird versucht, auszuwerten
                Assert.IsFalse(Inspect.Validate(InvalidExpr4).IsTreeValid);
                Evaluator.DebugOn = true;
                var resInvalidExpr4 = Evaluator.Eval(InvalidExpr2);
                Assert.IsFalse(resInvalidExpr4.Valid);
                Debug.WriteLine(resInvalidExpr4.ResultProtocolEntry.Description);
            }

            {
                Assert.IsTrue(Inspect.Validate(GT1).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resGT1 = Evaluator.Eval(GT1);
                Assert.IsTrue(resGT1.Valid);
                //Assert.IsTrue(resGT1.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resGT1.ResultTerm, typeof(NL.Data.ConstBool));
                Assert.IsFalse(((NL.Data.ConstBool)resGT1.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(GT2).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resGT2 = Evaluator.Eval(GT2);
                Assert.IsTrue(resGT2.Valid);
                //Assert.IsTrue(resGT2.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resGT2.ResultTerm, typeof(NL.Data.ConstBool));
                Assert.IsTrue(((NL.Data.ConstBool)resGT2.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(LT1).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resLT1 = Evaluator.Eval(LT1);
                Assert.IsTrue(resLT1.Valid);
                //Assert.IsTrue(resLT1.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resLT1.ResultTerm, typeof(NL.Data.ConstBool));
                Assert.IsTrue(((NL.Data.ConstBool)resLT1.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(LT2).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resLT2 = Evaluator.Eval(LT2);
                Assert.IsTrue(resLT2.Valid);
                //Assert.IsTrue(resLT2.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resLT2.ResultTerm, typeof(NL.Data.ConstBool));
                Assert.IsFalse(((NL.Data.ConstBool)resLT2.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(GE1).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resGE1 = Evaluator.Eval(GE1);
                Assert.IsTrue(resGE1.Valid);
                //Assert.IsTrue(resGE1.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resGE1.ResultTerm, typeof(NL.Data.ConstBool));
                Assert.IsFalse(((NL.Data.ConstBool)resGE1.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(GE2).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resGE2 = Evaluator.Eval(GE2);
                Assert.IsTrue(resGE2.Valid);
                //Assert.IsTrue(resGE2.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resGE2.ResultTerm, typeof(NL.Data.ConstBool));
                Assert.IsTrue(((NL.Data.ConstBool)resGE2.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(GE3).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resGE3 = Evaluator.Eval(GE3);
                Assert.IsTrue(resGE3.Valid);
                //Assert.IsTrue(resGE3.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resGE3.ResultTerm, typeof(NL.Data.ConstBool));
                Assert.IsTrue(((NL.Data.ConstBool)resGE3.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(LE1).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resLE1 = Evaluator.Eval(LE1);
                Assert.IsTrue(resLE1.Valid);
                //Assert.IsTrue(resLE1.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resLE1.ResultTerm, typeof(NL.Data.ConstBool));
                Assert.IsTrue(((NL.Data.ConstBool)resLE1.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(LE2).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resLE2 = Evaluator.Eval(LE2);
                Assert.IsTrue(resLE2.Valid);
                //Assert.IsTrue(resLE2.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resLE2.ResultTerm, typeof(NL.Data.ConstBool));
                Assert.IsFalse(((NL.Data.ConstBool)resLE2.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(LE3).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resLE3 = Evaluator.Eval(LE3);
                Assert.IsTrue(resLE3.Valid);
                //Assert.IsTrue(resLE3.ResultTerm.IsTerminal);
                Assert.IsInstanceOfType(resLE3.ResultTerm, typeof(NL.Data.ConstBool));
                Assert.IsTrue(((NL.Data.ConstBool)resLE3.ResultTerm).Value);
            }

            {
                Assert.IsTrue(Inspect.Validate(Rabatt1).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resRabatt1 = Evaluator.Eval(Rabatt1);
                Assert.IsTrue(resRabatt1.Valid);
                Assert.AreEqual(13.5, ((NL.Data.ConstDbl)resRabatt1.ResultTerm).Value);
                Debug.WriteLine(resRabatt1.ResultProtocolEntry.Description);
            }

            {
                Assert.IsTrue(Inspect.Validate(Rabatt2).IsCurrentValid);
                Evaluator.DebugOn = true;
                var resRabatt2 = Evaluator.Eval(Rabatt2);
                Assert.IsTrue(resRabatt2.Valid);
                Assert.AreEqual(15.0, ((NL.Data.ConstDbl)resRabatt2.ResultTerm).Value);
                Debug.WriteLine(resRabatt2.ResultProtocolEntry.Description);
            }


            {
                Debug.WriteLine(Pipe1.ToString());
                Assert.IsTrue(Inspect.Validate(Pipe1).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(Pipe1);
                Assert.IsTrue(res.Valid);
                Assert.IsTrue(res.ResultTerm is NL.Data.ConstDbl);
                Assert.AreEqual(30.0, ((NL.Data.ConstDbl)res.ResultTerm).Value);                
                Debug.WriteLine(res.ResultProtocolEntry.Description);
            }


            {
                Debug.WriteLine(Pipe2.ToString());
                Assert.IsTrue(Inspect.Validate(Pipe2).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(Pipe2);
                Assert.IsTrue(res.Valid);
                Assert.IsTrue(res.ResultTerm is NL.Data.ConstDbl);
                Assert.AreEqual(30.0, ((NL.Data.ConstDbl)res.ResultTerm).Value);                
                Debug.WriteLine(res.ResultProtocolEntry.Description);
            }


            {
                Debug.WriteLine(Tupel1.ToString());
                Assert.IsTrue(Inspect.Validate(Tupel1).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(Tupel1);
                Assert.IsTrue(res.Valid);
                Assert.IsTrue(res.ResultTerm is NL.Lisp.Tuple);
                Debug.WriteLine(res.ResultProtocolEntry.Description);
            }

            {
                Debug.WriteLine(TupelConcat.ToString());
                Assert.IsTrue(Inspect.Validate(TupelConcat).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(TupelConcat);
                Assert.IsTrue(res.Valid);
                Assert.IsTrue(res.ResultTerm is NL.Lisp.Tuple);
                Assert.AreEqual(10, ((NL.Lisp.Tuple)res.ResultTerm).Elements.Length);
                Debug.WriteLine(res.ResultProtocolEntry.Description);
            }


            {
                Debug.WriteLine(Take3.ToString());
                Assert.IsTrue(Inspect.Validate(Take3).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(Take3);
                Assert.IsTrue(res.Valid);
                Assert.IsTrue(res.ResultTerm is NL.Lisp.Tuple);
                Assert.AreEqual(3, ((NL.Lisp.Tuple)res.ResultTerm).Elements.Length);
                Debug.WriteLine(res.ResultProtocolEntry.Description);
            }

            {
                Debug.WriteLine(Skip3.ToString());
                Assert.IsTrue(Inspect.Validate(Skip3).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(Skip3);
                Assert.IsTrue(res.Valid);
                Assert.IsTrue(res.ResultTerm is NL.Lisp.Tuple);
                Assert.AreEqual(7, ((NL.Lisp.Tuple)res.ResultTerm).Elements.Length);
                Debug.WriteLine(res.ResultProtocolEntry.Description);
            }

            {
                Debug.WriteLine(PipeTupelAdd.ToString());
                Assert.IsTrue(Inspect.Validate(PipeTupelAdd).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(PipeTupelAdd);
                Assert.IsTrue(res.Valid);
                Assert.IsTrue(res.ResultTerm is NL.Data.ConstInt);
                Assert.AreEqual(55, ((NL.Data.ConstInt)res.ResultTerm).Value);
                Debug.WriteLine(res.ResultProtocolEntry.Description);
            }





            {
                Debug.WriteLine(AddList.ToString());
                Assert.IsTrue(Inspect.Validate(AddList).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(AddList);
                Assert.IsTrue(res.Valid);
                Assert.IsTrue(res.ResultTerm is NL.Data.ConstInt);
                Assert.AreEqual(15, ((NL.Data.ConstInt)res.ResultTerm).Value);
                Debug.WriteLine(res.ResultProtocolEntry.Description);
            }

            {
                Variablen[VarName.a_dbl].SetValue(1);
                Variablen[VarName.b_dbl].SetValue(2);
                Variablen[VarName.c_dbl].SetValue(3);

                Debug.WriteLine(Variablen_Mw3.ToString());
                Assert.IsTrue(Inspect.Validate(Variablen_Mw3).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(Variablen_Mw3);
                Assert.IsTrue(res.Valid);
                Assert.IsTrue(res.ResultTerm is NL.Data.ConstDbl);
                Assert.AreEqual(2.0, ((NL.Data.ConstDbl)res.ResultTerm).Value);
                Debug.WriteLine(res.ResultProtocolEntry.Description);
            }


            {

                Variablen[VarName.a_dbl].SetValue(10);
                Variablen[VarName.b_dbl].SetValue(20);
                Variablen[VarName.c_dbl].SetValue(30);

                Debug.WriteLine(Variablen_Mw3.ToString());
                Assert.IsTrue(Inspect.Validate(Variablen_Mw3).IsCurrentValid);
                Evaluator.DebugOn = true;
                var res = Evaluator.Eval(Variablen_Mw3);
                Assert.IsTrue(res.Valid);
                Assert.IsTrue(res.ResultTerm is NL.Data.ConstDbl);
                Assert.AreEqual(20.0, ((NL.Data.ConstDbl)res.ResultTerm).Value);
                Debug.WriteLine(res.ResultProtocolEntry.Description);
            }



        }


    }
}
