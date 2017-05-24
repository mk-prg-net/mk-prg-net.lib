//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.5.2017
//
//  Projekt.......: mko.RPN.DateTime 
//  Name..........: YearEval.cs
//  Aufgabe/Fkt...: Evaluierung einer Jahres- Funktion
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
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN.DateTime.Eval
{
    public class YearEval : EvalFunctionWithFixedParamCount
    {
        public YearEval(IFunctionNamesDateTime fn) : base(fn.Year, 1)
        {
            this.fn = fn;
        }

        IFunctionNamesDateTime fn;


        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            mko.TraceHlp.ThrowArgExIfNot(stack.Peek().IsInteger, "");
            var tok = (mko.RPN.IntToken)stack.Pop();

            stack.Push(new Year(fn, tok.ValueAsInt));
        }
    }
}
