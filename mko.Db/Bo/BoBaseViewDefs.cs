using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Db
{
    public class BoBaseViewDefs
    {
        /// <summary>
        /// Zustand einer View, wenn ihre Inhalte bearbeitet werden
        /// </summary>
        /// <remarks></remarks>
        public enum ViewState
        {
            // View wurde neu hinzugefügt
            Added,

            // View wurde nicht geändert und ist Member einer Collection
            Unchanged,

            // View wurde zum löschen Markiert
            Deleted
        }

        public enum ViewPropertyState
        {

            // Eigenschaften wurden nicht geändert
            Unchanged,

            // Eigenschaften der View wurden geändert
            Modified,

        }

    }
}
