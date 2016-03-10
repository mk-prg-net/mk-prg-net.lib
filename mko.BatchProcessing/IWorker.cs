//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 12.5.2008
//
//  Projekt.......: Stapelverarbeitung
//  Name..........: IWorker.cs
//  Aufgabe/Fkt...: Schnittstelle für Objekte, die Jobs in einen Stapelverarbeitungsprozess
//                  durchführen
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
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
//  Version.......: 1.3
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 25.3.2013
//  Änderungen....: Aus dem Namensraum DMS in den Namensraum mko.BatchProcessing verschoben
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Text;

namespace mko.BatchProcessing
{
    public interface IWorker
    {
        // Ausführung eines Jobs vorbereiten
        bool setup(Job currentJob);
    
        // Einen Job ausführen
        void doIt(Job currentJob);

        // Aktuellen Bearbeitungszustand bestimmen und als Ableitung von 
        // JobProgressInfo zurückgeben
        JobProgressInfo GetProgressInfo(Job job);      

    }

    // Delegate zum asynchronen Start der bearbeitung eines Jobs
    public delegate void DGworkerdoIt(Job currentJob);
}
