//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 5.5.2017
//
//  Projekt.......: mko.RPN.DateTime
//  Name..........: DateDataToken.cs
//  Aufgabe/Fkt...: Speichert ein Datum, das durch vorausgegangene Auswertung
//                  von RPN- Befehlen entstand.
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

namespace mko.RPN.DateTime
{
    /// <summary>
    /// Speichert ein Datum, das durch vorausgegangene Auswertung von RPN- Befehlen entstand.
    /// </summary>
    public class Date : FunctionNameToken
    {
        public Date(IFunctionNamesDateTime fn, System.DateTime date, int CountOfEvaluated = 1) : base(fn.DateTime, CountOfEvaluated)
        {
            this.DateTimeValue = date;
        }

        /// <summary>
        /// Datumswert, der durch vorausgegangene Auswertung von RPN- Ausdrücken ermittelt wurde.
        /// </summary>
        public System.DateTime DateTimeValue { get; }
    }
}
