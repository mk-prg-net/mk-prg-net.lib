using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;

namespace mkoIt.Asp.DataBind
{
    public abstract class MultiBinding<TWebCtrl, TFieldValue> : BindingBase
        where TWebCtrl : Control
    {
        TWebCtrl _webCtrl;
        
        /// <summary>
        /// Führt die Zuweisung eines Feldwertes aus einem Datensatz an die Eigenschaft eines Webcontrols durch.
        /// Dabei können Konvertierungen und Formatierungen stattfinden
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="value"></param>
        public delegate void WebCtrlPropertySetter(TWebCtrl ctrl, TFieldValue[] values);
        WebCtrlPropertySetter _webCtrlPropertySetter;

        /// <summary>
        /// Name des Datenfeldes im Datensatz, auf das zugegriffen wir
        /// </summary>
        string[] _fieldNames { get; set; }


        public MultiBinding(TWebCtrl webCtrl, string[] fieldNames, WebCtrlPropertySetter webCtrlPoropertySetter)
        {
            _webCtrl = webCtrl;
            _webCtrlPropertySetter = webCtrlPoropertySetter;

            _fieldNames = fieldNames;           

            _webCtrl.DataBinding += new EventHandler(DataBindHandler);           
        }

        void  DataBindHandler(object sender, EventArgs e)
        {
            var ctrl = (TWebCtrl)sender;

            // Sammeln aller Feldwerte, die an ein Steuerelement zu binden sind
            var values = new TFieldValue[_fieldNames.Length];
            for (int i = 0; i < _fieldNames.Length; i++)
            {
                values[i] = (TFieldValue)DataBinder.Eval((GetDataItem(ctrl), _fieldNames[i]);
            }

            _webCtrlPropertySetter(ctrl, values);
        }

        /// <summary>
        /// In Abgeleiteten Klassen wird hier der Zugriff zu dem
        /// mit dem Control assoziierten Datensatz beschrieben
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        protected abstract object GetDataItem(TWebCtrl ctrl);

    }
}
