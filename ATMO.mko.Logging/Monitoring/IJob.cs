using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.Monitoring
{
    /// <summary>
    /// mko, 8.11.2018
    /// Zulässige Zustandsübergänge:
    /// 
    ///   +--: running --+--------------------------------------+--+--: completed
    ///   |              +-----------------+--+--: aborted --+--+  |
    ///   |              |                 |  +--------------+     |
    ///   |              +--: stopped --+--+-----------------------+  
    ///   |                             |
    ///   +-----------------------------+
    /// </summary>
    public enum JobState
    {
        none,
        running,
        completed,
        aborted,
        stopped
    }

    /// <summary>
    /// mko, 8.11.2018
    /// Beschreibt Zustand eines aktuell in Bearbeitung befindlichen DFC- Auftrages
    /// </summary>
    public interface IJob
    {
        long JobId { get; }

        JobState State { get; }

        /// <summary>
        /// Informelle Beschreibung des Jobs
        /// </summary>
        string Title { get; }

        /// <summary>
        /// voraussichtlicher Arbeitsaufwand
        /// </summary>
        long EstimatedEffort { get; }

        /// <summary>
        /// bereits bewältigter Arbeitsaufwand
        /// </summary>
        long CurrentProgress { get; }

        int CurrentProgressInPercent { get; }


        /// <summary>
        /// Zeitpunkt, zu dem der Job erstellt wurde
        /// </summary>
        DateTime Created { get; }
    }
}
