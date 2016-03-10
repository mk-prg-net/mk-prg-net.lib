//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 4.2.2015
//
//  Projekt.......: Gbl.Lab.Concrete:PahmImport.Web
//  Name..........: LogModel.cs
//  Aufgabe/Fkt...: Model des LogControllers.
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

namespace mko.Asp.Mvc.Models
{
    public class LogModel
    {
        public System.Collections.Generic.IEnumerable<mko.Log.MemLogHandler.Entry> LogEntries { get; set; }

    }
}
