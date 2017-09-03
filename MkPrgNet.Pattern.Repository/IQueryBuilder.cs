//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 5.7.2017
//
//  Projekt.......: MkPrgNet.Pattern.Repository
//  Name..........: IQueryBuilder.cs
//  Aufgabe/Fkt...: Allgemeine Struktur eines Builders,
//                  mit dem Filterkriterien auf einer Menge 
//                  definiert werden
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
    /// <summary>
    /// Abgeleitete Schnittstellen von Querybuilder können eine Menge
    /// von nur schreibbaren Eigenschaften definieren, über welche die 
    /// Filtereinschränkungen festgelegt werden
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryBuilder<T, TSortOrderBuilder>
        where TSortOrderBuilder : ISortOrderBuilder<T>
    {
        /// <summary>
        /// Wenn alle Filter in dem QueryBuilder definiert wurden,
        /// dann wird diese Methode aufgerufen, um einen SortOrderBuilder
        /// zu erhalten, mit dem man die Sortierreihenfolgen definieren kann.
        /// </summary>
        /// <returns></returns>
        TSortOrderBuilder GetSortOrderBuilder();
    }
}
