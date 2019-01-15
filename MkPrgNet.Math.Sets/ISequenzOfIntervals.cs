using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Math.Sets
{
    /// <summary>
    /// mko, 10.12.2018
    /// Generiert ein Intervall, welches disjunkt zu allen Intervallen ist,
    /// die zuvor mit dieser Sequenz generiert wurden
    /// 
    /// mko, 15.1.2019
    /// IsEmpty hinzugefügt.
    /// </summary>
    public interface ISequenzOfIntervals<T>
        where T : IComparable<T>, new()
    {
        /// <summary>
        /// Creates a new interval, that is disjunct to all predecessors.
        /// </summary>
        /// <returns></returns>
        Interval<T> NextInterval { get; }

        /// <summary>
        /// If true NextInterval will return with an new valid interval. 
        /// If false NextInterval will throw an exception,
        /// </summary>
        bool IsNotEmpty
        {
            get;
        }
    }
}
