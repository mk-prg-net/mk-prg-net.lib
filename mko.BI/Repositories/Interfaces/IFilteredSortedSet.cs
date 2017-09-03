//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 21.04.2016
//
//  Projekt.......: mko.BI
//  Name..........: IFilteredSotedSet.cs
//  Aufgabe/Fkt...: Schnittstelle für Objekte, welche Mengen darstellen, die 
//                  durch Filter- und Sortierkriterien eingeschränkt wurden.
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

namespace mko.BI.Repositories.Interfaces
{
    /// <summary>
    /// Wird von Objekten implementiert, die Mengen darstellen, welche durch Filterbedingungen und Sortierkriterien 
    /// eingeschränkt wurden.
    /// </summary>
    /// <typeparam name="TBo"></typeparam>
    public interface IFilteredSortedSet<out TBo>
    {
        /// <summary>
        /// True, wenn die Menge Elemente enthält
        /// </summary>
        /// <returns></returns>
        bool Any();

        /// <summary>
        /// Anzahl der Elemente in der Menge
        /// </summary>
        /// <returns></returns>
        long Count();


        /// <summary>
        /// Liefert alle Elemente der Menge
        /// </summary>
        /// <returns></returns>
        IEnumerable<TBo> Get();
        
    }


    
}
