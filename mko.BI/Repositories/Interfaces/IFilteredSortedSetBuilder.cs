//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 21.4.2016
//
//  Projekt.......: mko.BI
//  Name..........: IFilteredSortedSetBuilder.cs
//  Aufgabe/Fkt...: Schnittstelle für Objekte, die ein IFilteredSortedSet- Objekt
//                  Erzeugen. Builder- Designpattern
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
//  Datum.........: 30.11.2016
//  Änderungen....: Erweitert um asynchrones GetSet
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
    /// Über Eigenschaften und Konfigurationskommandos werden Einschränkungen durch Filterbedingungen und Sortierkriterien
    /// auf einer Menge definiert. Schließlich kann die so definierte Menge mittels GetSet() abgerufen werden.
    /// Die Konfigurationskommandos sollten mit dem Präfix def... beginnnen.
    /// </summary>
    /// <typeparam name="TBo"></typeparam>
    public interface IFilteredSortedSetBuilder<out TBo>
    {
        IFilteredSortedSet<TBo> GetSet();
    }
}
