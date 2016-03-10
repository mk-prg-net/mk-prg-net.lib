//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 28.4.2015
//
//  Projekt.......: GKStatReportViewer
//  Name..........: LastErrorDescriptor.cs
//  Aufgabe/Fkt...: Modell, speicher Informationen zu einem Fehler ab.
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
    public class LastErrorDescriptor
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public Exception Ex { get; set; }
        public string ShortErrorDescription { get; set; }
    }
}
