//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 5.7.2017
//
//  Projekt.......: MkPrgNet.Pattern.Repository
//  Name..........: ISortOrderBuilder.cs
//  Aufgabe/Fkt...: Allgemeine Struktur eines Builders,
//                  mit dem die Sortierreihenfolge definiert wird
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

namespace MkPrgNet.Pattern.Repository
{
    public interface ISortOrderBuilder<T>
    {
        /// <summary>
        /// Liefert eine Menge von Asteroiden, die bezüglich  der zuvor
        /// eingestellten Filter- und Sortierkriterien gefiltert ist.
        /// </summary>
        /// <returns></returns>
        IFilteredSortedSet<T> GetFilteredSortedSet();
    }
}
