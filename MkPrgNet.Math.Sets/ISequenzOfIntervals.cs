using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Math.Sets
{
    /// <summary>
    /// mko, 10.12.2018
    /// Generiert ein Intervall, welches disjunkt zu allen Intervallen ist,
    /// die zuvor mit dieser Sequenz generiert wurden
    /// </summary>
    public interface ISequenzOfIntervals<T>
        where T : IComparable<T>, new()
    {
        /// <summary>
        /// Creates a new interval, that is disjunct to all predecessors.
        /// </summary>
        /// <returns></returns>
        MkPrgNet.Math.Sets.Interval<T> NewIntervall();
    }
}
