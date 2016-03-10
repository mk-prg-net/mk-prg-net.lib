//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den Nov. 2006
//
//  Projekt.......: Stapelverarbeitung
//  Name..........: IBatchProcessing.cs
//  Aufgabe/Fkt...: Schnittstelle eines Stapelverarbeitungsprozesses
//                  
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
//  Datum.........: 14.5.2008
//  Änderungen....: Pause und Resume hinzugefügt
//
//  Version.......: 1.2
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 09.5.2011
//  Änderungen....: RemoveFinishedOrAbortedJob
//
//  Version.......: 1.3
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 25.3.2013
//  Änderungen....: Aus dem Namensraum DMS in den Namensraum mko.BatchProcessing verschoben
//
//</unit_history>
//</unit_header>        

using System;

namespace mko.BatchProcessing
{
    public interface IBatchProcessing
    {
        /// <summary>
        /// Erzeugt eine, für die die Schnittstelle implementierende Komponente eindeutige ID
        /// </summary>
        /// <returns></returns>
        int NewJobId();

        /// <summary>
        /// Stellt einen neuen Job in die Warteschlange der unbearbeiteten Jobs
        /// </summary>
        /// <param name="job"></param>
        void pushJob(Job job);

        /// <summary>
        /// Liefert Informationen über den aktuellen Bearbetiungszustand eines Jobs
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        JobProgressInfo GetProgressInfo(int JobId);


        /// <summary>
        /// Bei gültiger JobId blockiert der Aufruf, bis der Job fertiggestellt, oder 
        /// das Timeout abgelaufen ist. true wird nur zurückgeliefert, wenn der Job
        /// fertiggestellt wurde, sont immer false 
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        bool WaitUntilJobFinished(int jobId, int timeout);

        /// <summary>
        /// Für eine gültige JobId und einen fertiggestellten Job wird dieser ausgegeben und aus dem 
        /// internen JobStorage gelöscht. In diesem Falle gibt die Methode true zurück und die JobId ist
        /// zukünftig nicht mehr gültig.
        /// Sonst wird immer false zurück-, und ein  undefinierte Job ausgegeben
        /// </summary>
        /// <param name="JobId"></param>
        /// <param name="job"></param>
        /// <returns></returns>
        bool DeliverFinishedJob(int JobId, out Job job);

        /// <summary>
        /// Ein Job wird aus der Stapelverarbeitun entfernt und gelöscht. Dabei muss er sich im 
        /// Zustand finished oder abroted befinden. Sonst gibt die Funktion false zurück, und der 
        /// Job verbleibt in der Stapelverarbeitung.
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        bool RemoveFinishedOrAbortedJob(int JobId);

        /// <summary>
        /// Die verarbeitung des Jobs mit der übergebenen JobId wird abgebrochen und der
        /// Job verworfen
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        bool Abort(int JobId);

        /// <summary>
        /// Die Jobverarbeitung wird angehalten
        /// </summary>
        /// <returns></returns>
        bool Pause();

        /// <summary>
        /// Die Jobverarbeitung wird fortgestzt
        /// </summary>
        /// <returns></returns>
        bool Resume();

        /// <summary>
        /// Wenn keine Jobs zu verarbeiten sind, dann wird true zurückgegeben
        /// </summary>
        /// <returns></returns>
        bool Idle();

        /// <summary>
        /// Liste aller Jobs im Processor
        /// </summary>
        /// <returns></returns>
        int[] AllJobs();
    }
}
