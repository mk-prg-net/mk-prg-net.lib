using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Math.Sets
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISequenceOfIntervalsOfDifferentSize<T> : ISequenceOfIntervals<T>
        where T: IComparable<T>, new()
    {
        /// <summary>
        /// Liefert ein weiteres Intervall, disjunkt zu allen Vorläufern, mit der Breite Size.
        /// Falls das Intervall nicht erzeugt werden kann, wird eine OutOfRange Exception gefeuert.
        /// </summary>
        /// <param name="Size"></param>
        /// <returns></returns>
        Interval<T>? NextInterval(T Size);

    }
}
