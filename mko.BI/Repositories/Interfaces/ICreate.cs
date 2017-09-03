//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 13.3.2016
//
//  Projekt.......: mko.BI
//  Name..........: ICreateUpdate.cs
//  Aufgabe/Fkt...: Schnitstelle zum Anlegen und Aktualisieren innerhalb von Repositories
//                  Hervorgegangen aus ICrud vom 18.9.2015
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
    public interface ICreate<in TBoId>
    {
        /// <summary>
        /// Ein neues Geschäftsobjekt wird unter der Id angelegt und der vom Repository verwalteten Collection hinzugefügt.
        /// Durch Aufruf von SubmitChanges (siehe unten) werden die Änerungen schließlich übernommen und das
        /// neue Objekt permanen in der Collection aufgenommen. 
        /// </summary>
        /// <returns></returns>
        void CreateBoAndAdd(TBoId id);


    }
}
