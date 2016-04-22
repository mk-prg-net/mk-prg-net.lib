//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 15.3.2016
//
//  Projekt.......: mko.BI
//  Name..........: ISubmitChanges.cs
//  Aufgabe/Fkt...: Durch Interface- Segregation aus ICreateUpdate hervorgegangen.
//                  Bestätigen von Änderungen für die unteren Schichten.
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

namespace mko.BI.Repositories.Interfaces
{
    public interface ISubmitChanges
    {
        /// <summary>
        /// Aktualisierungen am ORMContext mit der Datenbank abgleichen
        /// </summary>
        void SubmitChanges();


    }
}
