//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 4.3.2012
//
//  Projekt.......: mkoItAps
//  Name..........: ReportBld.cs
//  Aufgabe/Fkt...: Basisklasse für Klassen zum Erzeugen von Datenbankberichten.
//                  Berichte zu erzeugen kann ein langwieriger Prozess sein.
//                  Dazu bietet die Klasse Events an, über die der Prozessfortschritt
//                  signalisiert wird.
//                  Zudem werden allgemeine Berichtseigenschaften implementiert, wie 
//                  Autor, Erstellungsdatm etc.
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.0
//  Werkzeuge.....: Visual Studio 2010
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

namespace mkoIt.Db
{
    public abstract class ReportBld
    {

        /// <summary>
        /// Author des Berichtes
        /// </summary>
        public string ReportAuthor { get; set; }

        /// <summary>
        /// Anmerkungen zum erstellten Bericht
        /// </summary>
        public string ReportNotes { get; set; }

        /// <summary>
        /// Datum der Berichtserstellung
        /// </summary>
        public DateTime ReportCreated { get; set; }

        /// <summary>
        /// Anstossen der Berichtserstellung
        /// </summary>
        public abstract void CreateReport();

        /// <summary>
        /// Event wird zu Beginn der Berechnung eines Berichtes gefeuert und 
        /// informiert über die Anzahl der zu verrechnenden Datensätze im Bericht
        /// </summary>
        public event Action<int> SetWorkloadEvent;
        protected void RaiseWorkloadEvent(int workload)
        {
            if (SetWorkloadEvent != null)
                SetWorkloadEvent(workload);
        }

        /// <summary>
        /// Event wird regelmäßig gefeuert und informiert Umwelt über den Fortschritt der 
        /// Berechnung des Berichtes
        /// </summary>
        public event Action<int> SetProcessedEvent;
        protected void RaiseProcessedEvent(int processed)
        {
            if (SetProcessedEvent != null)
                SetProcessedEvent(processed);
        }
    }
}
