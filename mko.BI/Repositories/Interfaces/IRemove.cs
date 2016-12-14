//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 13.3.2016
//
//  Projekt.......: mko.BI
//  Name..........: IRemove
//  Aufgabe/Fkt...: Schnittstelle zum Löschen von Einträgen in einem Repository
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
//  Datum.........: 30.11.2016
//  Änderungen....: Contravarianz zugelassen
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
    public interface IRemove<in TBoId>
    {
        /// <summary>
        /// Löschen des durch die ID definierten Entity
        /// </summary>
        /// <param name="id"></param>
        void RemoveBo(TBoId id);

        /// <summary>
        /// Löschen aller Entities
        /// </summary>
        void RemoveAllBo();

    }
}
