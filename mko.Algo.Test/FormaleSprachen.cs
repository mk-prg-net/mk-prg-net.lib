using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

using mko.Algo.fml;

namespace mko.Algo.Test
{
    [TestClass]
    public class FormaleSprachen
    {
        [TestMethod]
        public void fmlTyp2GrammatikInterpreterTest()
        {
            // Test der Funktoren
            var constA = new FunctorConst<Double>() { A = 2 };
            var constB = new FunctorConst<Double>() { A = 5 };
            var constC = new FunctorConst<Double>() { A = 7 };

            var AddOp = new FunctorBinary<Double>() { A = constB, B = constC, op = (a, b) => a + b };
            var MulOp = new FunctorBinary<Double>() { A = constA, B = AddOp, op = (a, b) => a * b };

            var result = MulOp.map();
            Debug.WriteLine("2*(5+7)= " + result);

            // Test des Interpreters
            var symbole = new List<FunctorBase<Double>>();

            //  Ausdruck 2*(5+7) in umgekehrt polnischer Notation: 5 7 + 2 *
            symbole.Add(new FunctorConst<Double>() { A = 5 });
            symbole.Add(new FunctorConst<Double>() { A = 7 });
            symbole.Add(new FunctorBinary<Double>() { op = (a, b) => a + b });
            symbole.Add(new FunctorConst<Double>() { A = 2 });
            symbole.Add(new FunctorBinary<Double>() { op = (a, b) => a * b });

            var interpreter = new Interpreter<Double>();
            result = interpreter.Eval(symbole);
            Debug.WriteLine("2*(5+7)-> 5 7 + 2 * -> Interpreter = " + result);

            //  Ausdruck (5+7)*(2 + 8) in umgekehrt polnischer Notation: 5 7 + 2 8 + *

            symbole.Clear();
            symbole.Add(new FunctorConst<Double>() { A = 5 });
            symbole.Add(new FunctorConst<Double>() { A = 7 });
            symbole.Add(new FunctorBinary<Double>() { op = (a, b) => a + b });
            symbole.Add(new FunctorConst<Double>() { A = 2 });
            symbole.Add(new FunctorConst<Double>() { A = 8 });
            symbole.Add(new FunctorBinary<Double>() { op = (a, b) => a + b });
            symbole.Add(new FunctorBinary<Double>() { op = (a, b) => a * b });

            result = interpreter.Eval(symbole);
            Debug.WriteLine("(5+7)*(2 + 8)-> 5 7 + 2 8 + * -> Interpreter = " + result);

        }
    }
}
