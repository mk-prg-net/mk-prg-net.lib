using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.Monitoring
{
    /// <summary>
    /// mko, 8.11.2018
    /// Überwachung von nebenläufigen Prozessen in DFC.
    /// Diese Schnittstelle wird vom Auftraggeber benutzt. Der Auftraggeber gibt Jobs in Auftrag,
    /// und meldet, wann diese Fertig sind.
    /// 
    /// mko, 8.3.2019
    /// completeJob hinzugefügt, dafür deregisterJob in die Schnittstelle IJobMonitoringConsole verschoben.
    /// Wann ein fertiggestellter Job aus der Anzeige verschwindet, entscheidet jetzt die Konsole, und nicht der 
    /// Auftraggeber eines Jobs.
    /// </summary>
    public interface IJobMonitoring
    {
        /// <summary>
        /// Ein neuer nebenläufiger Prozess (Job) wird registiert. Wenn erfolgreich, dann liefert die 
        /// Funktion eine eindeutige ID für den Job zurück
        /// </summary>
        /// <param name="title">Informelle Beschreibung des Jobs (ird benutzt in Anzeigen des Prozessfortschrittes)</param>
        /// <param name="estimatedEffort">prognostizierte Ausführungsdauer in ms</param>        
        /// <returns></returns>
        RCV3sV<long> registerJob(string title, long estimatedEffort);


        /// <summary>
        /// Meldet den aktuellen Prozessfortschritt.
        /// Wenn erfolgreich, dann wird im Rückgabewert mitgeteilt, ob der Überwacher einen 
        /// vorzeitigen Abbruch des Jobs wünscht.
        /// Der Rückgabewert zeigt den aktuellen Zustand des Jobs an. Wenn aborted angezeigt wird, dann 
        /// sollte der Job dies durch deregister Job quittieren.
        /// </summary>
        /// <param name="JobId"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        RCV3sV<JobState> reportProgess(long JobId, long progress);


        /// <summary>
        /// mko, 8.3.2019
        /// Meldet, das ein Job fertiggestellt ist.
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        RCV3sV<JobState> completeJob(long JobId);

    }
}
