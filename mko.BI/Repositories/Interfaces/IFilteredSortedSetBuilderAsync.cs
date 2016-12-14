//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 30.11.2016
//
//  Projekt.......: mko.BI
//  Name..........: IFilteredSortedSetBuilderAsync.cs
//  Aufgabe/Fkt...: Schnittstelle für Objekte, die ein IFilteredSortedSet- Objekt
//                  Erzeugen. Builder- Designpattern. Asynchrone Implementierung
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
    public interface IFilteredSortedSetBuilderAsync<TBo>
    {
        Task<IFilteredSortedSet<TBo>> GetSetAsync();
    }

    //public interface IFilteredSortedSetBuilderAsync<TFSS, out TBo>
    //    where TFSS : IFilteredSortedSet<TBo>
    //{
    //    Task<TFSS> GetSetAsync();
    //}

}
