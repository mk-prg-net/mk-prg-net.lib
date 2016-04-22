//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 28.9.2016
//
//  Projekt.......: mko.BI
//  Name..........: IGet.cs
//  Aufgabe/Fkt...: Schnittstelle von Repositories, die den Zugriff auf Geschäftsobjekte ermöglicht.
//                  Abgeleitet aus IGetBo
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
//  Datum.........: 21.4.2016
//  Änderungen....: Umgewandelt in die Schnittstelle IGet, welche 
//                  Get- Methoden anbietet, mit der auch Mengen von 
//                  Geschäftsobjekten gefiltert und sortiert werden 
//                  können.
//</unit_history>
//</unit_header>        
        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.Repositories.Interfaces
{
    interface IGet<TBo, TBoId>
    {

        /// <summary>
        /// 25.7.2014, mko    
        /// Zugriff auf einzelnes Entity mit der gegebenen id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TBo GetBo(TBoId id);

        /// <summary>
        /// 14.3.2016, mko
        /// Prüfen, ob zu einem gegebenen Schlüssel ein Eintrag existiert
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ExistsBo(TBoId id);

    }
}
