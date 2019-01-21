using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Math.Sets
{
    /// <summary>
    /// mko, 21.1.2019
    /// Erzeugt Folge gleichgroßer Intervalle
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISequenceOfIntervalsOfEqualSize<T>: ISequenceOfIntervals<T>
         where T: IComparable<T>, new()
    {
        /// <summary>
        /// Creates a new interval, that is disjunct to all predecessors.
        /// </summary>
        /// <returns></returns>
        Interval<T>? NextInterval { get; }

    }
}
