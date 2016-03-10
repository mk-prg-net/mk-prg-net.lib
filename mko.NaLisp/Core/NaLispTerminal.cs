//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: NaLispTerminal.cs
//  Aufgabe/Fkt...: Basisklasse von Ausdrücken, die nicht weiter aufgelöst werden können. 
//                  Sie stellen benannte Listen dar, wobei alle Elemente String, Double oder
//                  Integerkonstanten sind. Beim Evaluieren werden diese direkt auf einen 
//                  NaLisp Ausdruck abgebildet.
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2012
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
    public abstract class NaLispTerminal : NaLisp, ITerminal
    {

        public abstract Inspector.ProtocolEntry Validate(NaLispStack Stack);


        /// <summary>
        /// Evaluierungsfunktion
        /// </summary>
        /// <param name="StackInstance"></param>
        /// <param name="Term"></param>
        /// <returns></returns>
        public abstract INaLisp Eval(NaLispStack StackInstance, bool DebugOn);

        public override string ToString()
        {
            return Name;
        }
    }
}
