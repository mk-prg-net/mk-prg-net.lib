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
        public SequenceOfEqualIntervalsOverLong(long start, long width, long end = long.MaxValue)
        {
            _start = start;
            _nextStart = start;
            _width = width;
            _end = end;
        }

        public SequenceOfEqualIntervalsOverLong()
        {
        }

        /// <summary>
        /// Beginn des Bereiches, der in Intervalle aufgeteilt wird.
        /// </summary>
        long _start;

        /// <summary>
        /// Intervallbreite
        /// </summary>
        long _width;

        // Ende des Bereiches, welcher in Intervalle aufgeteilt wird.
        long _end;

        long _nextStart;

        /// <summary>
        /// Liefert das nächste disjunkte Intervall der Sequenz
        /// </summary>
        /// <returns></returns>
        public Interval<long> NextInterval
        {
            get
            {
                if (!IsNotEmpty)
                    throw new IndexOutOfRangeException(Base.TraceHlp.FormatErrMsg(this, "NewInterval", "stock of intervals exhausted"));

                var newI = Interval.Create<long>(_nextStart, _nextStart + _width - 1);
                _nextStart = _nextStart + _width;
                return newI;
            }
        }

        public bool IsNotEmpty => _nextStart + _width - 1 <= _end;
    }
}
