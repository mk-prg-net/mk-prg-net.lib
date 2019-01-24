using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Woc
{

    /// <summary>
    /// mko, 24.1.2019
    /// 
    /// "Gesetz von Martin":
    /// Funktionen sollten immer einen konkreten Typ zurückliefern, nie jedoch eine Schnittstelle.
    /// 
    /// - deshalb ist der Rückgabetyp hier ein generischer PArameter mit Einschränkungen auf 
    ///   Schnittstellen
    /// 
    /// </summary>
    /// <typeparam name="TIdSet"></typeparam>
    public class IdGen<TIdSet>
        where TIdSet: struct, IIdSet
    {
        /// <summary>
        /// IdSet, für welches SubIdSets erzeugt werden
        /// </summary>
        TIdSet IdSet { get; }

        MkPrgNet.Math.Sets.SequenceOfIntervalsOfDifferentSize seq;

        public IdGen(TIdSet IdSet, long Begin, long End)
        {
            this.IdSet = IdSet;
            MkPrgNet.Base.TraceHlp.ThrowArgExIfNot(IdSet.Contains(new IdSet(Begin, End, "")), "");

            seq = new Math.Sets.SequenceOfIntervalsOfDifferentSize(Begin, End);            
        }

        TIdSet? NextIdSet(long maxCountSubIds, string Title)
        {
            var sub = seq.NextInterval(maxCountSubIds);

            if (sub.HasValue)
            {
                var tid = new TIdSet();
                tid.SetTo(sub.Value.Begin, sub.Value.End, Title);
                return tid;

            } else
            {
                return null;
            }
        }

    }
}
