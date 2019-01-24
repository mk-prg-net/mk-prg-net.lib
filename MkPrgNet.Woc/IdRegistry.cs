using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Woc
{
    /// <summary>
    /// mko, 29.1.2019
    /// Repository hierarschisch verschachtetlter IdSets
    /// </summary>
    /// <typeparam name="TIdSet"></typeparam>
    public interface IIdRegistry<TIdSet>
        where TIdSet : IIdSet
    {
        /// <summary>
        /// True, wenn das IdSet bereits registriert ist
        /// </summary>
        /// <param name="idSet"></param>
        /// <returns></returns>
        bool Contains(TIdSet idSet);

        /// <summary>
        /// Das IdSet wird registriert
        /// </summary>
        /// <param name="idSet"></param>
        void Register(TIdSet idSet);

        /// <summary>
        /// Bestimmt alle IdSets, die das gegebene IdSet enthalten. 
        /// Das mächtigste IdSet steht am Anfang, das gegebene idSet selbst
        /// am Ende der zurückgegebenen Liste
        /// </summary>
        /// <param name="idSet"></param>
        /// <returns></returns>
        IEnumerable<TIdSet> GetAncestorsOf(TIdSet idSet);

        /// <summary>
        /// Liefert alle IdSets, die über diesen mittels einer Sequenz erzeugt wurden 
        /// und jeweils in dem gegebenen enthalten sind.
        /// </summary>
        /// <param name="idSet"></param>
        /// <returns></returns>
        IEnumerable<TIdSet> GetChilds(TIdSet idSet);


    }
}
