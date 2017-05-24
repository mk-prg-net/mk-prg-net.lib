//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 18.5.2017
//
//  Projekt.......: mko.RPN.Arithmetik
//  Name..........: SumatEval.cs
//  Aufgabe/Fkt...: Evaluator für Aufsummation
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

using mko.RPN;
namespace mko.RPN.Arithmetik
{
    /// <summary>
    /// Evaluator für Aufsummation
    /// </summary>
    public class SumatEval : mko.RPN.EvalBase        
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListEndSymbol">Symbol, welches das Ende einer variablen Parameterliste anzeigt</param>
        public SumatEval(string ListEndSymbol)
        {
            this.ListEndSymbol = ListEndSymbol;
        }

        string ListEndSymbol;

        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            double sum = 0.0;
            int CountEvaluated = 0;

            stack.ParseVariadicParameters(ListEndSymbol, (stackP, iParam) =>
            {
                mko.TraceHlp.ThrowArgExIfNot(stackP.Peek().IsNummeric, string.Format(Properties.Resources.EvalErrSumat_ParameterAsDoubleReqiured, stackP.Peek().Value));
                var tok = stack.Pop();
                CountEvaluated += tok.CountOfEvaluatedTokens;
                if (tok.IsInteger)
                {
                    sum += (IntToken)tok;
                } else
                {
                    sum += (DoubleToken)tok;
                }
                
            });

            // Ergebnis auf dem Stapel speichern
            stack.Push(new DoubleToken(sum, CountEvaluated+1));

        }
    }
}
