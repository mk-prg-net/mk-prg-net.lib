using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.IdSet
{
    /// <summary>
    /// mko, 18.2.2019
    /// Implementierung hierarchicher ID's. Dabei werden Hierarchien auf verschachtelte Intervalle abgebildet.
    /// </summary>
    /// <typeparam name="TDescriptor"></typeparam>
    public static class IdSet
    {

        /// <summary>
        /// mko, 18.2.2019
        /// Erzeugt das erste ID- Set
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="deskriptor"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static (TDescriptor Deskriptor, long idBeg, long idEnd)  FirstIdSet<TDescriptor>(TDescriptor deskriptor, (TDescriptor Deskriptor, long idBeg, long idEnd) parent, long width)
        {
            return (deskriptor, parent.idBeg, parent.idBeg + width);
        }

        public static (TDescriptor Deskriptor, long idBeg, long idEnd) NextIdSet<TDescriptor>(TDescriptor deskriptor, (TDescriptor Deskriptor, long idBeg, long idEnd) parent, (TDescriptor Deskriptor, long idBeg, long idEnd) last)
        {
            (TDescriptor Descriptor, long idBeg, long idEnd) next = (deskriptor, parent.idBeg, last.idEnd + 1 + last.idEnd - last.idBeg);
            TraceHlp.ThrowArgExIfNot(next.idEnd > parent.idEnd, "ParentId Set is exhausted");
            return next;
        }

        public static IEnumerable<(TDescriptor Deskriptor, long idBeg, long idEnd)> PathTo<TDescriptor>((TDescriptor Deskriptor, long idBeg, long idEnd) idSet, IEnumerable<(TDescriptor Deskriptor, long idBeg, long idEnd)> AllIds)
        {
            return AllIds.Where(r => r.idBeg <= idSet.idBeg && r.idEnd >= idSet.idEnd).OrderByDescending(r => r.idEnd - r.idBeg);
        }
    }
}
