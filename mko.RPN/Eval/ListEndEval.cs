//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 18.5.2017
//
//  Projekt.......: mko.RPN
//  Name..........: EvalNop.cs
//  Aufgabe/Fkt...: Evaluator der eine NOP evaluiert. Wird eingesetzt für 
//                  Funktionen wie Lend oder If, die zwar Funktionen sind,
//                  die aber nicht durch einen eignen Evaluator, sondern    
//                  im Rahmen der EWvaluierung anderer Funktionen ausgewertet werden.
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

namespace mko.RPN
{
    public class ListEndEval : IEval
    {
        public ListEndEval(IFunctionNames fn)
        {
            this.fn = fn;
        }
        IFunctionNames fn;

        public Exception EvalException
        {
            get
            {
                return null;
            }
        }

        public bool Succesful
        {
            get
            {
                return true;
            }
        }

        public void Eval(Stack<IToken> stack)
        {
            stack.Push(new ListEndToken(fn));
        }
    }
}
