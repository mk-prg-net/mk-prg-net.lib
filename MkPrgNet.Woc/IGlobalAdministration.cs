using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Woc
{
    /// <summary>
    /// mko, 10.12.2018
    /// Stellt die globalen Verwaltung des Woc- Systems bereit
    /// </summary>
    public interface IGlobalAdministration
    {
        /// <summary>
        /// Erzeugt die allgemeinsten Teilmengen von Nodes
        /// </summary>
        Math.Sets.ISequenzOfIntervals<long> NodeClusterSequnce { get; }

    }
}
