//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 17.5.2017
//
//  Projekt.......: Gbl.BI.LAb.Concrete.RPN
//  Name..........: EvalHelper.cs
//  Aufgabe/Fkt...: Hilfsfunktionen für das Evaluieren
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

namespace mko.RPN
{
    public static class EvalHelper
    {
        /// <summary>
        /// Liest alle Parameter einer variadischen Parameterliste ein, und führt für jeden die ReadParameter- Action aus
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="ReadParameter"></param>
        public static void ParseVariadicParameters(this Stack<IToken> stack, string ListEndSymbol, Action<Stack<IToken>, int> ReadParameter)
        {
            int i = 0;
            do
            {
                ReadParameter(stack, i);
                i++;
            } while (stack.Count > 0 && !(stack.Peek().IsFunctionName && stack.Peek().Value == ListEndSymbol));

            // Parameterlistenende entfernen
            mko.TraceHlp.ThrowArgExIfNot(stack.Count > 0 && (stack.Peek().IsFunctionName && stack.Peek().Value == ListEndSymbol), Properties.Resources.ErrEval_VariadicParameterlistNotTerminiated);
            stack.Pop();
        }
    }
}
