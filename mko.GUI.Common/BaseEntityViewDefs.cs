using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.GUI.Common
{
    public sealed class BaseEntityViewDefs
    {

        /// <summary>
        /// Zustand einer View, wenn ihre Inhalte bearbeitet werden
        /// </summary>
        /// <remarks></remarks>
        public enum ViewState
        {
            // View wurde neu angelegt
            Detached,

            // View wurde einer Collection hinzugefügt
            Added,

            // View wurde nicht geändert und ist Member einer Collection
            Unchanged,

            // Eigenschaften der View wurden geändert
            Modified,

            // View wurde zum löschen Markiert
            Deleted
        }
    }
}
