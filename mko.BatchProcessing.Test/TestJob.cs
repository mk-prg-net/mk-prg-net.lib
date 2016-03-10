//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: Stapelverarbeitung
//  Name..........: TestJob.cs
//  Aufgabe/Fkt...: Klassendeklaration für einen Job zum testen der Stapelverarbetung
//                  Der Job definiert eine Verweildauer in der Arbeitsstation.
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
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Text;

namespace mko.BatchProcessing.Test
{
    [Serializable]
    public class TestJob : mko.BatchProcessing.Job
    {
        // Festgelegte Verweildauer in der Bearbeitungsstation in ms
        public readonly int processingDuration;

        // Aktuelle Verweildauer
        public int elapsedProcessTime;

        // Privater Konstruktor. Kann aus Memberfunktionen aufgerufen werden wie z.B. die stat. 
        // Create- Methode
        private TestJob(int JobId, int processingDuration): base(JobId)
        {
            this.processingDuration = processingDuration;
        }

        // Create kapselt die Prozedur zur erstellung eines neuen Jobs auf einer Stapelverarbeitungsanlage
        public static TestJob Create(mko.BatchProcessing.IBatchProcessing ibp, int processingDuration, out int JobId)
        {
            // Neue Job- ID generieren und Job erzeugen
            JobId = ibp.NewJobId();
            return new TestJob(JobId, processingDuration);
        }
    }
}
