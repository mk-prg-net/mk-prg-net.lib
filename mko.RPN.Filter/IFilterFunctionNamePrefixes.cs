//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 11.4.2017
//
//  Projekt.......: mko.RPN.Filter
//  Name..........: IfilterFunctionNamePrefixes.cs
//  Aufgabe/Fkt...: Kurzbeschreibung
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

namespace mko.RPN.Filter
{
    public interface IFilterFunctionNamePrefixes
    {
        /// <summary>
        /// Namenspräfix für Bereichsfilter
        /// </summary>
        string rngFltPrefix { get; }


        /// <summary>
        /// Namenspräfix für Sortierfilter
        /// </summary>
        string sortFltPrefix { get; }

        /// <summary>
        /// Namenspräfix für Auswahl aus endlichen Mengen Filter
        /// </summary>
        string setFltPrefix { get; }


        /// <summary>
        /// Namenspräfix für Zugriff auf Element über Schlüssel Filter
        /// </summary>
        string keyFltPrefix { get; }

        /// <summary>
        /// Namenspräfix für Ähnlichkeits/Mustervergleichs Filter
        /// </summary>
        string likeFltPrefix { get; }

        /// <summary>
        /// Namensprefix für Filter,  die Mengen bezüglich eines logischen Prädikates
        /// in zwei Teilmengen aufteilen.
        /// </summary>
        string predFltPrefix { get; }


    }
}
