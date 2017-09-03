//<babaros_unit_header>
//----------------------------------------------------------------
//
// TRACS - Optische Computer Sensorik
// Stuttgart, den 8.5.2017
//
//  Projekt.......: mko.RPN.DateTime
//  Name..........: IFunctionNameDateTime
//  Aufgabe/Fkt...: Symboltabelle der Datumsfunktionen
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
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
//</babaros_unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN.DateTime
{
    /// <summary>
    /// Symboltabelle der Datumsfunktionen
    /// </summary>
    public interface IFunctionNamesDateTime 
    {
        /// <summary>
        /// Name der Funktion, die einen Tag im Monat definiert
        /// </summary>
        string Day { get; }

        /// <summary>
        /// Name einer Funktion, die einen Monat im Jahr definiert
        /// </summary>
        string Month { get; }

        /// <summary>
        /// Name der der Funktion, die das Jahr definiert
        /// </summary>
        string Year { get; }

        /// <summary>
        /// Name der Funktion, die einen Zeitstempel liefert
        /// </summary>
        string DateTime { get; }
    }
}
