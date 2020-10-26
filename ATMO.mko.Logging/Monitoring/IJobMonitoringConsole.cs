using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.Monitoring
{
    public interface IJobMonitoringConsole
    {
        /// <summary>
        /// liefert alle aktuell aktiven Jobs/Arbeitsaufträge
        /// </summary>
        RCV3sV<IEnumerable<IJob>> Jobs { get; }

        /// <summary>
        /// Unterbricht die ausführung eines Jobs
        /// </summary>
        /// <param name="JobId"></param>
        RCV3sV<JobState> stopJob(long JobId);

        /// <summary>
        /// setzt die Ausführung eines Jobs fort
        /// </summary>
        /// <param name="JobId"></param>
        RCV3sV<JobState> continueJob(long JobId);

        /// <summary>
        /// Bricht einen Job vorzeitig ab
        /// </summary>
        /// <param name="JobId"></param>
        RCV3 abortJob(long JobId);

        /// <summary>
        /// Der zuvor fertiggestellte oder abgebrochene Job wird aus Überwachung entfernt.
        /// Der Rückgabewert zeigt den aktuellen Zustand des Jobs an. War der Job abgebrchen worden,
        /// dann verbleibt sein Zustand in aborted. Sonst wird er nach Ausführung dieser Funktion den 
        /// Zustand completed haben.
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        RCV3sV<JobState> deregisterJob(long JobId);


    }
}
