//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: INonTerminal.cs
//  Aufgabe/Fkt...: Schnittstelle von Objekte, die Ausdrücke sind,
//                  deren Elemente ausschließlich NaLisp sind (hier immer in
//                  runde Klammern eingeschlossene Listen).
//                  Entspricht Funktionen mit n- Parametern. Die Parameter
//                  sind wiederum Funktionen.
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

namespace mko.NaLisp.Core
{
    public interface INonTerminal : INaLisp
    {
        /// <summary>
        /// Parameterliste der NaLisp- Funktion
        /// </summary>
        INaLisp[] Elements { get; }

        Inspector.ProtocolEntry Validate(NaLispStack Stack, Inspector.ProtocolEntry[] ElemValidationResult);

        INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn);
    }
}
