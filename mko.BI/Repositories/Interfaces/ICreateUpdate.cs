using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.Repositories.Interfaces
{
    public interface ICreateUpdate<TBo, TBoId>
    {
        /// <summary>
        /// Ein neues Geschäftsobjekt wird angelegt und der vom Repository verwalteten Collection hinzugefügt.
        /// Anschließend können die Eigenschaften des zurückgegebenen Reopsitories bearbeitet werden.
        /// Durch Aufruf von SubmitChanges (siehe unten) werden die Änerungen schließlich übernommen und das
        /// neue Objekt permanen in der Collection aufgenommen. 
        /// </summary>
        /// <returns></returns>
        TBo CreateBoAndAddToCollection(TBoId id);

        /// <summary>
        /// Aktualisierungen am ORMContext mit der Datenbank abgleichen
        /// </summary>
        void SubmitChanges();


    }
}
