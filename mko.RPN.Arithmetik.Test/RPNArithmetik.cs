using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

using System.Linq;
using mko.RPN;

namespace mko.RPN.Arithmetik.Test
{
    [TestClass]
    public class RPNArithmetik
    {

        readonly Parser Parser;


        mko.RPN.IFunctionNames fnBase;
        IFunctionNames fn;
        IFunctionEvaluatorTable evalTab;
        Composer composer;

        /// <summary>
        /// Versuchsterm in revers, polnischer Notation
        /// </summary>
        readonly string rpn;

        readonly string rpnSumat;

        readonly string rpnCplxSumat;

        /// <summary>
        /// Versuchsterm in polnischer Notation
        /// </summary>
        readonly string pn;

        readonly string pnSumat;

        readonly string pnCplxSumat;


        //ITokenizer Tokenizer;

        public RPNArithmetik()
        {


            fnBase = new FunctionNamesLight();
            fn = new BasicFunctionNames(fnBase);

            evalTab = new FunctionEvaluatorTable(new mko.RPN.FnameEvalMapper(fnBase), new FnameEvalMapperFunctor(fn, fnBase.ListEnd));

            composer = new Composer(fnBase, fn);

            rpn = composer.rADD(
                composer.rMUL(
                    composer.rDbl(2),
                    composer.rSUB(
                        composer.rDbl(2.5),
                        composer.rDbl(0.5))),
                composer.rADD(
                    composer.rDbl(2.3),
                    composer.rDbl(4.7)));

            rpnSumat = composer.rSUMAT(composer.rDbl(1), composer.rDbl(2), composer.rDbl(3));

            rpnCplxSumat = composer.rADD(
                composer.rMUL(
                    composer.rSUMAT(composer.rDbl(1), composer.rDbl(2), composer.rDbl(3)),
                    composer.rSUB(
                        composer.rDbl(2.5),
                        composer.rDbl(0.5))),
                composer.rADD(
                    composer.rDbl(2.3),
                    composer.rDbl(4.7)));

            pn = composer.ADD(
                composer.MUL(
                    composer.Dbl(2),
                    composer.SUB(
                        composer.Dbl(2.5),
                        composer.Dbl(0.5))),
                composer.ADD(
                    composer.Dbl(2.3),
                    composer.Dbl(4.7)));

            pnSumat = composer.SUMAT(composer.Dbl(1), composer.Dbl(2), composer.Dbl(3));

            pnCplxSumat = composer.ADD(
                composer.MUL(
                    composer.SUMAT(composer.Dbl(1), composer.Dbl(2), composer.Dbl(3)),
                    composer.SUB(
                        composer.Dbl(2.5),
                        composer.Dbl(0.5))),
                composer.ADD(
                    composer.Dbl(2.3),
                    composer.Dbl(4.7)));


            //Tokenizer = new Tokenizer(TermReader);
            Parser = new Parser(evalTab.FuncEvaluators);
        }

        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void Composer_Test()
        {
            Assert.AreEqual("4.7 2.3 .add 0.5 2.5 .sub 2 .mul .add", rpn.Trim());
            Assert.AreEqual(".add .mul 2 .sub 2.5 0.5 .add 2.3 4.7", pn.Trim());
            Assert.AreEqual(".Lend 3 2 1 .sumat", rpnSumat);
            Assert.AreEqual(".sumat 1 2 3 .Lend", pnSumat);

            Assert.AreEqual("4.7 2.3 .add 0.5 2.5 .sub .Lend 3 2 1 .sumat .mul .add", rpnCplxSumat.Trim());
            Assert.AreEqual(".add .mul .sumat 1 2 3 .Lend .sub 2.5 0.5 .add 2.3 4.7", pnCplxSumat.Trim());
        }

        [TestMethod]
        public void RPNTools_ToString()
        {
            Parser.Parse(rpn);

            Assert.IsTrue(Parser.Succsessful);
            Assert.IsTrue(Parser.Stack.Peek().IsNummeric);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(mko.RPN.DoubleToken));
            Assert.AreEqual(11.0, ((mko.RPN.DoubleToken)Parser.Stack.Peek()).ValueAsDouble);

            var Tokens = Parser.TokenBuffer.Tokens.Copy();

            var rpnTxt = Tokens.ToRPNString();
            var pnText = Tokens.ToPNString();

            Parser.Parse(rpnTxt);
            Assert.AreEqual(1, Parser.Stack.Count);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(mko.RPN.DoubleToken));

            double resRpnTxt = ((mko.RPN.DoubleToken)Parser.Stack.Peek()).ValueAsDouble;

            Tokens = Parser.TokenizePN(pnText);
            Parser.Parse(Tokens);
            Assert.IsTrue(Parser.Succsessful);
            Assert.AreEqual(1, Parser.Stack.Count);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(mko.RPN.DoubleToken));

            double resPnTxt = ((mko.RPN.DoubleToken)Parser.Stack.Peek()).ValueAsDouble;

            Assert.AreEqual(resRpnTxt, resPnTxt);
        }

        [TestMethod]
        public void RPNTools_IndexOfFunction()
        {
            Parser.Parse(rpn);

            var Expr1Tokens = Parser.TokenBuffer.Tokens.Copy();
            var ixADD = Expr1Tokens.LastIndexOfFunction(fn.ADD);
            Assert.AreEqual(8, ixADD.IX);

            ixADD = Expr1Tokens.IndexOfFunction(fn.ADD);
            Assert.AreEqual(2, ixADD.IX);
        }

        [TestMethod]
        public void RPNTools_IndexOfFunction2()
        {
            Parser.Parse(rpnCplxSumat);

            var Expr1Tokens = Parser.TokenBuffer.Tokens.Copy();
            var ixADD = Expr1Tokens.LastIndexOfFunction(fn.ADD);
            Assert.AreEqual(12, ixADD.IX);

            ixADD = Expr1Tokens.IndexOfFunction(fn.ADD);
            Assert.AreEqual(2, ixADD.IX);
        }


        [TestMethod]
        public void RPNTools_Equals()
        {
            Parser.Parse(rpn);
            var left = Parser.TokenBuffer.Tokens;

            Assert.IsTrue(left.EQ(left));

            var rpn2 = composer.rADD(
                composer.rMUL(
                    composer.Dbl(3),
                    composer.rSUB(
                        composer.Dbl(2.5),
                        composer.Dbl(0.5))),
                composer.rADD(
                    composer.Dbl(2.3),
                    composer.Dbl(4.7)));

            Parser.Parse(rpn2);
            var right = Parser.TokenBuffer.Tokens;

            Assert.IsFalse(left.EQ(right));

            var rpn3 = composer.rADD(
                composer.rMUL(
                    composer.Dbl(3),
                    composer.rMUL(
                        composer.Dbl(2.5),
                        composer.Dbl(0.5))),
                composer.rADD(
                    composer.Dbl(2.3),
                    composer.Dbl(4.7)));

            Parser.Parse(rpn3);
            right = Parser.TokenBuffer.Tokens;

            Assert.IsFalse(left.EQ(right));

            left = new IToken[] { };
            right = new IToken[] { };

            bool res = left.EQ(right);
            Assert.IsTrue(res);
        }


        [TestMethod]
        public void RPNTools_RemoveFunction()
        {
            Parser.Parse(rpn);

            // Alggemeine Funktion entfernen
            var rpnTokens = Parser.TokenBuffer.Tokens.Copy();
            var withoutAdd = rpnTokens.RemoveFunction(fn.ADD);
            Assert.IsFalse(withoutAdd.Any());

            // Subfunktion am Anfang entfernen
            rpnTokens = Parser.TokenBuffer.Tokens.Copy();
            var withoutSubAdd = rpnTokens.RemoveSubFunction(fn.ADD);
            Assert.AreEqual(fn.SUB, withoutSubAdd[2].Value);

            // Subfunktion in der Mitte entfernen
            rpnTokens = Parser.TokenBuffer.Tokens.Copy();
            var withoutMul = rpnTokens.RemoveSubFunction(fn.MUL);
            Assert.AreEqual(fn.ADD, withoutMul[3].Value);

            rpnTokens = Parser.TokenBuffer.Tokens.Copy();
            var withoutMainMul = rpnTokens.RemoveFunction(fn.MUL);
            Assert.AreEqual(fn.ADD, withoutMainMul[3].Value);
        }


        [TestMethod]
        public void RPNTools_ReplaceFunction()
        {
            Parser.Parse(rpn);
            var rpnTokens = Parser.TokenBuffer.Tokens.Copy();

            var rpnDiv = composer.rDIV(composer.Dbl(8), composer.Dbl(2));
            Parser.Parse(rpnDiv);

            var DivTokens = Parser.TokenBuffer.Tokens.Copy();
            var DivInsteadAdd = rpnTokens.ReplaceFunction(fn.ADD, DivTokens);

            Assert.IsTrue(DivInsteadAdd.EQ(DivTokens));


            var DivInsteadSubAdd = rpnTokens.ReplaceSubFunction(fn.ADD, DivTokens);

            var rpnExpected = composer.rADD(
                    composer.rMUL(
                        composer.Dbl(2),
                        composer.rSUB(
                            composer.Dbl(2.5),
                            composer.Dbl(0.5))),
                    composer.rDIV(
                        composer.Dbl(8),
                        composer.Dbl(2)));

            Parser.Parse(rpnExpected);
            var expected = Parser.TokenBuffer.Tokens.Copy();

            Assert.IsTrue(expected.EQ(DivInsteadSubAdd));
        }

        [TestMethod]
        public void RPNTools_Parametercount()
        {
            Parser.Parse(rpn);

            var Tokens = Parser.TokenBuffer.Tokens.Copy();

            int pcount = Tokens.FunctionParameterCount(8);
            Assert.AreEqual(2, pcount);

            var ixSub = Tokens.IndexOfFunction(fn.SUB);
            pcount = Tokens.FunctionParameterCount(ixSub.IX);
            Assert.AreEqual(2, pcount);

            pcount = Tokens.FunctionParameterCount(fn.ADD);
            Assert.AreEqual(2, pcount);

            pcount = Tokens.SubFunctionParameterCount(fn.ADD);
            Assert.AreEqual(2, pcount);

            var MulSubTree = Tokens.GetSubFunctionSubtree(fn.MUL);
            pcount = MulSubTree.FunctionParameterCount();
            Assert.AreEqual(2, pcount);


        }

        [TestMethod]
        public void RPNTools_IndexOfFunctionParameter()
        {
            Parser.Parse(rpn);

            var Expr3Tokens = Parser.TokenBuffer.Tokens.Copy();
            var ixMUL = Expr3Tokens.IndexOfFunction(fn.MUL);

            var ixP1 = Expr3Tokens.IndexOfFunctionParameter(ixMUL.IX, 1);
            Assert.AreEqual(6, ixP1.IX);
            var P1Subtree = Expr3Tokens.GetSubtree(ixP1.IX);
            Assert.IsTrue(P1Subtree.Last().IsInteger);
            Assert.AreEqual(2, (IntToken)P1Subtree.Last());


            var ixP2 = Expr3Tokens.IndexOfFunctionParameter(ixMUL.IX, 2);
            Assert.AreEqual(5, ixP2.IX);
            var P2Subtree = Expr3Tokens.GetSubtree(ixP2.IX);
            Assert.IsTrue(P2Subtree.Last().IsFunctionName);
            Assert.AreEqual(fn.SUB, P2Subtree.Last().Value);
        }


        [TestMethod]
        public void RPNTools_Subtree()
        {

            Parser.Parse(rpn);

            var Expr2Tokens = Parser.TokenBuffer.Tokens.Copy();

            var ixMUL = Parser.TokenBuffer.Tokens.IndexOfFunction(fn.MUL);
            var MulTree = Parser.TokenBuffer.Tokens.GetSubtree(ixMUL.IX);
            Assert.AreEqual(5, MulTree.Count());

            Parser.Parse(MulTree);
            Assert.AreEqual(1, Parser.Stack.Count());
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(mko.RPN.DoubleToken));
            Assert.AreEqual(4.0, (DoubleToken)Parser.Stack.Peek());

            Assert.AreEqual(2, MulTree.IndexOfFunction(fn.SUB).IX);

        }

        [TestMethod]
        public void RPNTools_GetFunctionSubtree()
        {

            Assert.IsFalse(ParserHelper.IsFunctionSubtree(null));

            Parser.Parse(rpn);
            var rpnTokens = Parser.TokenBuffer.Tokens.Copy();

            // Zugriff auf allgemeine Funktion
            Assert.IsTrue(rpnTokens.IsFunctionSubtree(fn.ADD));
            Assert.AreEqual(fn.ADD, rpnTokens.FunctionName());

            var P1_Mul = rpnTokens.GetParameterSubtree(1);
            Assert.IsTrue(P1_Mul.IsFunctionSubtree(fn.MUL));

            var P2_Add = rpnTokens.GetParameterSubtree(2);
            Assert.IsTrue(P2_Add.IsFunctionSubtree(fn.ADD));


            var P1_Mul_P2 = P1_Mul.GetParameterSubtree(2);
            Assert.IsTrue(P1_Mul_P2.IsFunctionSubtree(fn.SUB));

            Parser.Parse(P1_Mul_P2);
            Assert.IsTrue(Parser.Succsessful);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(DoubleToken));
            Assert.AreEqual(2.0, ((DoubleToken)Parser.Stack.Peek()).ValueAsDouble);

            // Zugriff auf Subfunktion
            var SubAdd = rpnTokens.GetSubFunctionSubtree(fn.ADD);
            Assert.AreEqual(3, SubAdd.Count());
            var SubAdd_P1 = SubAdd.GetParameterSubtree(1);
            Assert.AreEqual(1, SubAdd_P1.Count());
            Assert.IsTrue(SubAdd_P1[0].IsNummeric);
            var SubAdd_P2 = SubAdd.GetParameterSubtree(2);
            Assert.AreEqual(1, SubAdd_P2.Count());
            Assert.IsTrue(SubAdd_P2[0].IsNummeric);
        }

        [TestMethod]
        public void RPNTools_GetFunctionSubtree2()
        {

            Assert.IsFalse(ParserHelper.IsFunctionSubtree(null));

            Parser.Parse(rpnCplxSumat);
            var rpnTokens = Parser.TokenBuffer.Tokens.Copy();

            // Zugriff auf allgemeine Funktion
            Assert.IsTrue(rpnTokens.IsFunctionSubtree(fn.ADD));
            Assert.AreEqual(fn.ADD, rpnTokens.FunctionName());

            var P1_Mul = rpnTokens.GetParameterSubtree(1);
            Assert.IsTrue(P1_Mul.IsFunctionSubtree(fn.MUL));

            var P2_Add = rpnTokens.GetParameterSubtree(2);
            Assert.IsTrue(P2_Add.IsFunctionSubtree(fn.ADD));

            var P1_Mul_P1 = P1_Mul.GetParameterSubtree(1);
            Assert.IsTrue(P1_Mul_P1.IsFunctionSubtree(fn.SUMAT));

            Parser.Parse(P1_Mul_P1);
            Assert.IsTrue(Parser.Succsessful);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(DoubleToken));
            Assert.AreEqual(6.0, ((DoubleToken)Parser.Stack.Peek()).ValueAsDouble);


            var P1_Mul_P2 = P1_Mul.GetParameterSubtree(2);
            Assert.IsTrue(P1_Mul_P2.IsFunctionSubtree(fn.SUB));

            Parser.Parse(P1_Mul_P2);
            Assert.IsTrue(Parser.Succsessful);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(DoubleToken));
            Assert.AreEqual(2.0, ((DoubleToken)Parser.Stack.Peek()).ValueAsDouble);

            // Zugriff auf Subfunktion
            var SubAdd = rpnTokens.GetSubFunctionSubtree(fn.ADD);
            Assert.AreEqual(3, SubAdd.Count());
            var SubAdd_P1 = SubAdd.GetParameterSubtree(1);
            Assert.AreEqual(1, SubAdd_P1.Count());
            Assert.IsTrue(SubAdd_P1[0].IsNummeric);
            var SubAdd_P2 = SubAdd.GetParameterSubtree(2);
            Assert.AreEqual(1, SubAdd_P2.Count());
            Assert.IsTrue(SubAdd_P2[0].IsNummeric);
        }



        [TestMethod]
        public void RPNArithmetik_Empty()
        {
            Parser.Parse("");

            Assert.IsTrue(Parser.Succsessful);
            Assert.AreEqual(0, Parser.Stack.Count);
            Assert.AreEqual(0, Parser.TokenBuffer.Count);

            Parser.Parse("      ");

            Assert.IsTrue(Parser.Succsessful);
            Assert.AreEqual(0, Parser.Stack.Count);
            Assert.AreEqual(0, Parser.TokenBuffer.Count);
        }


        [TestMethod]
        public void RPNArithmetik_ADD()
        {
            //TermWriter.Write();
            //TermWriter.Flush();
            //memStream.Seek(0, System.IO.SeekOrigin.Begin);

            //var myTokenizer = new Tokenizer();

            Parser.Parse(composer.rADD(composer.Dbl(4.7), composer.Dbl(2.3)));

            Assert.IsTrue(Parser.Succsessful);
            Assert.AreEqual(1, Parser.Stack.Count);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(DoubleToken));
            Assert.AreEqual(7.0, (DoubleToken)Parser.Stack.Peek());
        }

        [TestMethod]
        public void RPNArithmetik_SUMAT()
        {
            Parser.Parse(rpnSumat);

            Assert.IsTrue(Parser.Succsessful);
            Assert.AreEqual(1, Parser.Stack.Count);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(DoubleToken));
            Assert.AreEqual(6.0, (DoubleToken)Parser.Stack.Peek());

            Parser.Parse(rpnCplxSumat);

            Assert.IsTrue(Parser.Succsessful);
            Assert.AreEqual(1, Parser.Stack.Count);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(DoubleToken));
            Assert.AreEqual(19.0, (DoubleToken)Parser.Stack.Peek());


        }


        [TestMethod]
        public void RPNArithmetik_ADD_MUL()
        {
            Parser.Parse(composer.rMUL(composer.Dbl(3), composer.rADD(composer.Dbl(4.7), composer.Dbl(2.3))));

            Assert.IsTrue(Parser.Succsessful);
            Assert.AreEqual(1, Parser.Stack.Count);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(DoubleToken));
            Assert.AreEqual(21.0, (DoubleToken)Parser.Stack.Peek());
        }

        [TestMethod]
        public void RPNArithmetik_ADD_MUL_DIV()
        {
            Parser.Parse(composer.rDIV(
                            composer.rMUL(
                                composer.Dbl(3),
                                composer.rADD(composer.Dbl(4.7), composer.Dbl(2.3))),
                            composer.Dbl(2)));

            Assert.IsTrue(Parser.Succsessful);
            Assert.AreEqual(1, Parser.Stack.Count);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(DoubleToken));
            Assert.AreEqual(10.5, (DoubleToken)Parser.Stack.Peek());
        }

        [TestMethod]
        public void RPNArithmetik_ADD_SUB_MUL()
        {
            Parser.Parse(rpn);

            Assert.IsTrue(Parser.Succsessful);
            Assert.AreEqual(1, Parser.Stack.Count);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(DoubleToken));
            Assert.AreEqual(11, (Parser.Stack.Peek() as DoubleToken).ValueAsDouble);

            var tokens = Parser.TokenizeRPN(rpn);
            Assert.IsTrue(Parser.Succsessful);
            Assert.AreEqual(1, Parser.Stack.Count);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(DoubleToken));
            Assert.AreEqual(11.0, (DoubleToken)Parser.Stack.Peek());


        }

        [TestMethod]
        public void PNArithmetik_ADD_SUB_MUL()
        {
            var tokens = Parser.TokenizePN(pn);
            Assert.IsTrue(Parser.Succsessful);

            Parser.Parse(tokens);

            Assert.IsTrue(Parser.Succsessful);
            Assert.AreEqual(1, Parser.Stack.Count);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(DoubleToken));
            Assert.AreEqual(11.0, (DoubleToken)Parser.Stack.Peek());
        }


        [TestMethod]
        public void RPNArithmetik_ADD_SUB_MUL_DIV()
        {
            var rpnDiv = composer.rDIV(rpn, composer.Dbl(2));
            Parser.Parse(rpnDiv);

            Assert.IsTrue(Parser.Succsessful);
            Assert.AreEqual(1, Parser.Stack.Count);
            Assert.IsInstanceOfType(Parser.Stack.Peek(), typeof(DoubleToken));
            Assert.AreEqual(5.5, (DoubleToken)Parser.Stack.Peek());
        }




    }
}
