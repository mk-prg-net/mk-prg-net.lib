//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 4.5.2017
//
//  Projekt.......: mko.RPN
//  Name..........: IDateTimeFunctionNames
//  Aufgabe/Fkt...: Funktionsnamen- Tabelle für Datums- und Uhrzeitfunktionen
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
    /// <summary>
    /// Funktionsnamen- Tabelle für Datums- und Uhrzeitfunktionen
    /// </summary>
    public interface IDateTimeFunctionNames
    {
        /// <summary>
        /// Jahr
        /// </summary>
        string Year { get; }

        /// <summary>
        /// Monat
        /// </summary>
        string Month { get; }

        /// <summary>
        /// Tag
        /// </summary>
        string Day { get; }

        /// <summary>
        /// Stunde
        /// </summary>
        string Hour { get; }

        /// <summary>
        /// Minute
        /// </summary>
        string Minute { get; }

        /// <summary>
        /// Sekunde
        /// </summary>
        string Second { get; }

        /// <summary>
        /// Millisekunde
        /// </summary>
        string Millisecond { get; }
    }
}
