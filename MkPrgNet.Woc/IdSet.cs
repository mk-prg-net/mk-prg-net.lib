using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Woc
{

    /// <summary>
    /// mko, 24.1.2019
    /// Ist ein Intervall, welches ein Themengebit identifiziert analog einem Schlüssel.
    /// Alle subthemen werden durch Intervalle präsentiert, die in diesem enthalten sind.
    /// </summary>
    public struct IdSet : IIdSet
    {
        MkPrgNet.Math.Sets.Interval<long> Interval;

        public void SetTo(long Begin, long End, string Title)
        {
            Interval = new Math.Sets.Interval<long>(Begin, End);
            _Title = Title;
        }

        public string Title => _Title;
        string _Title;

        public long Begin => Interval.Begin;

        public long End => Interval.End;

        public bool Contains(IIdSet set)
        {
            var i2 = Math.Sets.Interval.Create(set.Begin, set.End);
            return Interval.Intersect(i2) == i2;            
        }

    }
}
