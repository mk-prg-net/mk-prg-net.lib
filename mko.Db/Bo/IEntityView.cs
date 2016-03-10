using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Db
{
    public interface IEntityView<TEntity, TEntityId>
        where TEntity : class
    {
        /// <summary>
        /// Widerspiegelt den Bearbeitungszustand einer View
        /// </summary>
        BoBaseViewDefs.ViewState State { get; set; }

        BoBaseViewDefs.ViewPropertyState PropertiesState { get; set; }


        /// <summary>
        /// Liefert die ID eines Entitys / Datensatzes
        /// </summary>
        TEntityId Id
        {
            get;
            set;
        }

        /// <summary>
        /// Liefert alle Komponenten des Schlüssels eines Entitys / Datensatzes
        /// </summary>
        object[] Keys { get; }


        /// <summary>
        /// Liefert das Entity, dessen Inhalt die View darstellt
        /// </summary>
        TEntity Entity
        {
            get;
        }

        /// <summary>
        /// Alle auf am lokalen Entity durchgeführten Änderungen an einem externen Entity ebenfalls durchfürhen
        /// Das externe Entity kann z.B. Teil einer Datenbank sein.
        /// </summary>
        /// <param name="entity"></param>
        void UpdateExternalEntity(TEntity Entity);

        /// <summary>
        /// Liste mit den aufgezeichneten Änderungen an internen Entity löschen
        /// </summary>
        void DeleteAllChangeTrackingEntries();

    }
}
