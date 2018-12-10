//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.6.2016
//
//  Projekt.......: mko.RPN.Arithmetik
//  Name..........: NumBinOps.cs
//  Aufgabe/Fkt...: Basisklasse der Evaluatoren für arithmetische Funktionen
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 6.4.2017
//  Änderungen....: Tokenbuffer entfernt, da diese Funktion nun im Parser
//                  enthalten ist
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN.Arithmetik
{
    public abstract class NumBinOp : BasicEvaluator
    {
        IFormatProvider pfmt;

        public NumBinOp(IFormatProvider pfmt) : base(2) 
        {
            this.pfmt = pfmt;
        }

        public NumBinOp() : this(new System.Globalization.CultureInfo("en-US"))
        {            
        }
        protected abstract double Func(double a, double b);

        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            var a = PopNummeric(stack);
            var b = PopNummeric(stack);

            var res = new DoubleToken(Func(a.Item1, b.Item1), pfmt, a.Item2.CountOfEvaluatedTokens + b.Item2.CountOfEvaluatedTokens + 1);
            stack.Push(res);
        }
    }
}
