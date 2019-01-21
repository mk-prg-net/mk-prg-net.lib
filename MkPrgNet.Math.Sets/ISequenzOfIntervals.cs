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
    /// 
    /// mko, 21.1.2019
    /// IsEmpty entfernt, da Komplikationen in multithreading Umgebung befürchtet.
    /// Eigenschaft Range hinzugefügt
    /// </summary>
    public interface ISequenceOfIntervals<T>
        where T : IComparable<T>, new()
    {        
        /// <summary>
        /// Definiert den Bereich, welcher durch die sequenz der Intervalle überdeckt wird.
        /// </summary>
        Interval<T> Range { get; }
    }
}
