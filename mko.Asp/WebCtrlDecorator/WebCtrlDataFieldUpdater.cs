using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using msWebUi = System.Web.UI;
using msWebCtrl = System.Web.UI.WebControls;

namespace mkoIt.Asp.DataBind
{
    public class WebCtrlDataFieldUpdater<TWebCtrl, TFieldCollection, TField> : IDataFieldUpdater
        where TWebCtrl : msWebCtrl.WebControl        
    {
        public WebCtrlDataFieldUpdater(TFieldCollection Fields, Action<TFieldCollection, TField> SetFieldOperator, Func<TWebCtrl, TField> MapToFieldValue)
        {
            this.Fields = Fields;
            this.SetFieldOperator = SetFieldOperator;
            this.MapToFieldValue = MapToFieldValue;            
        }


        public void SetField(msWebCtrl.WebControl Ctrl)
        {
            SetFieldOperator((TFieldCollection)Fields, MapToFieldValue((TWebCtrl) Ctrl));
        }

        /// <summary>
        /// Objekt mit den zu aktualisierenden Datenfeldern. 
        /// </summary>
        TFieldCollection Fields;

        /// <summary>
        /// Wählt ein Feld aus dem Objekt aus und weist ihm einen neuen Feldwert zu
        /// </summary>
        Action<TFieldCollection, TField> SetFieldOperator;

        /// <summary>
        /// Berechnet aus den Eigenschaften eines WebControls den neuen Wert für ein Feld aus einem Datenobjekt
        /// </summary>
        Func<TWebCtrl, TField> MapToFieldValue;
    }
}
