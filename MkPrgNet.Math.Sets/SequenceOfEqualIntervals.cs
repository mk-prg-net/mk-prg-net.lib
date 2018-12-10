using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Math.Sets
{
    public class SequenceOfEqualIntervalsOverLong : ISequenzOfIntervals<long>
    {
        /// <summary>
        /// Erzeug eine neue Intervallsequenz
        /// </summary>
        /// <param name="start">Beginn des ersten Intervalls, welches die Sequenz liefert</param>
        /// <param name="width">Breite der von der Sequenz ausgelieferten Intervalle</param>
        public SequenceOfEqualIntervalsOverLong(long start, long width)
        {
            _nextStart = start;
            _width = width;
        }

        /// <summary>
        /// Intervallbreite
        /// </summary>
        long _width;

        long _nextStart;

        /// <summary>
        /// Liefert das nächste disjunkte Intervall der Sequenz
        /// </summary>
        /// <returns></returns>
        public Interval<long> NewIntervall()
        {
            var newI = Interval.Create<long>(_nextStart, _nextStart + _width - 1);
            _nextStart = _nextStart + _width;
            return newI;
        }
    }
}
