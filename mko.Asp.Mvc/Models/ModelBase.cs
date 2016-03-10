
//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 19.5.2014
//
//  Projekt.......: mko.Asp.Mvc
//  Name..........: ModelBase.cs
//  Aufgabe/Fkt...: Basisklasse aller Modelle in der MVC- Architektur.
//                  Enthält Eigenschaften, die für die Implementierung von 
//                  Workflows notwendig sind.
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2012
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
    public class ModelBase
    {

        /// <summary>
        /// ID, über die im Sitzungszustand auf den Zustand des Workflows zugegriffen wird,
        /// innerhalb dessen das Model zwischen Controller und View ausgetauscht wird.
        /// </summary>
        public string WorkflowID { get; set; }

        /// <summary>
        /// Liste mit der Beschreibung aller aktiver Filter
        /// </summary>
        public string[] FilterDescriptions { get; set; }

        /// <summary>
        /// Liste mit der Beschreibung aller aktiven Sortierregeln
        /// </summary>
        public string[] GradingRulesDescriptions { get; set; }

        /// <summary>
        /// Zeile im gefilterten Resultset, ab der das Ausgabefenster startet
        /// </summary>
        public int StartsAt { get; set; }

        /// <summary>
        /// Anzahl der Auszugebenden Zeilen
        /// </summary>
        public int PageSize { get; set; }


    }
}
