using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Woc
{
    /// <summary>
    /// mko, 24.1.2019
    /// WocId's als hirarchisch verschachtelte Intervalle/Schlüsselmengen
    /// </summary>
    public interface IIdSet
    {
        /// <summary>
        /// Informelle Kurzbeschreibung der Inhalte, die hinter dem IdSet stehen
        /// </summary>
        string Title { get; }

        long Begin { get; }

        long End { get; }

        bool Contains(IIdSet set);        

        // Setter für ein IdSet
        void SetTo(long Begin, long End, string Title);

    }
}
