using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Math.Sets
{
    /// <summary>
    /// mko, 21.1.2019
    /// 
    /// </summary>
    public class SequenceOfIntervalsOfDifferentSize : ISequenceOfIntervalsOfDifferentSize<long>
    {
        /// <summary>
        /// Erzeug eine neue Intervallsequenz
        /// </summary>
        /// <param name="start">Beginn des ersten Intervalls, welches die Sequenz liefert</param>
        /// <param name="width">Breite der von der Sequenz ausgelieferten Intervalle</param>
        public SequenceOfIntervalsOfDifferentSize(long start, long end = long.MaxValue)
        {
            _start = start;
            _nextStart = start;
            _end = end;
        }

        /// <summary>
        /// Beginn des Bereiches, der in Intervalle aufgeteilt wird.
        /// </summary>
        long _start;

        // Ende des Bereiches, welcher in Intervalle aufgeteilt wird.
        long _end;

        long _nextStart;

        public Interval<long> Range =>new Interval<long>(_start, _end);

        /// <summary>
        /// Liefert das nächste disjunkte Intervall der Sequenz
        /// </summary>
        /// <returns></returns>
        public Interval<long>? NextInterval(long Size)
        {
            if (_nextStart + Size > _end || Size <= 0)
                return new Interval<long>?();

            var newI = Interval.Create<long>(_nextStart, _nextStart + Size - 1);
            _nextStart = _nextStart + Size;
            return newI;
        }
    }
}
