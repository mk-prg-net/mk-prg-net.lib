using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Asp.DataBind
{
    /// <summary>
    /// Definiert die Bindung eines Datenfeldes an die Inhalte von Websteuerelementen in der Zeile einer Gridview
    /// </summary>
    /// <typeparam name="TField">Typ des Datenfeldes</typeparam>
    public class BindingDataFieldToGridViewRow<TField> : BindingDataField<TField>
    {

        /// <summary>
        /// Name des Datenfeldes
        /// </summary>
        protected string FieldName;

        public BindingDataFieldToGridViewRow(System.Web.UI.WebControls.GridView grd, string FieldName, Func<TField> FieldPropertySetter) 
            : base(FieldPropertySetter)            
        {
            this.FieldName = FieldName;
            grd.RowUpdating += new System.Web.UI.WebControls.GridViewUpdateEventHandler(_grd_RowUpdating);
            
        }
        
        /// <summary>
        /// Implementierung des RowUpdating- Ereignisses einer Gridview. 
        /// Mittels FieldPropertySetter wird der neue Wert bestimmt, und damit die Zuordnugstabelle Feldnamen/Neue Werte aktualisiert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _grd_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {           

            var grd = sender as System.Web.UI.WebControls.GridView;
            if (e.NewValues.Contains(FieldName))
                e.NewValues[FieldName] = CalculateFieldValuesFromInputs();
            else 
                e.NewValues.Add(FieldName, CalculateFieldValuesFromInputs());             
        }
    }
}
