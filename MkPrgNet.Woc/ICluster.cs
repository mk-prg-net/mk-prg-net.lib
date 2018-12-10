using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Woc
{
    /// <summary>
    /// mko, 10.12.2018
    /// Cluster sind Mengen von ID's, mit denen Nodes und Authoren gekennzeichnet 
    /// werden können.
    /// Cluster werden in disjunkte Subcluster aufgeteilt, die wiederum weiter aufgeteilt 
    /// werden können, bis schließlich kleine Cluster entstehen, aus denen die ID's direkt 
    /// hintereinander vergeben werden.
    /// </summary>
    public interface ICluster
    {
        /// <summary>
        /// Kurze, informelle Beschreibung des Clusters
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Ebene/Tiefe im Baum, an der sich dieses Cluster befindet
        /// </summary>
        long Level { get; }

        /// <summary>
        /// Repräsentiert das aktuelle Cluster
        /// </summary>
        Math.Sets.Interval<long> Cluster { get; }

        /// <summary>
        /// Liefert das nächste, noch nicht belegte Subcluster.
        /// </summary>
        Math.Sets.ISequenzOfIntervals<long> SubClusterSequenz { get; }
    }
}
