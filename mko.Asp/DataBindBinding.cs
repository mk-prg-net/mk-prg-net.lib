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
    /// <summary>
    /// Definiert die Bindung eines WebControls an ein Datenfeld
    /// </summary>
    /// <typeparam name="TWebCtrl">Typ des WebControls</typeparam>
    /// <typeparam name="TFieldValue">Typ des Datenfeldes</typeparam>
    public abstract class BindingWebCtrl<TWebCtrl, TDataItem> : BindingBase
        where TWebCtrl : Control
    {
        protected TWebCtrl WebCtrl { get; set; }
        
        /// <summary>
        /// Führt die Zuweisung eines Feldwertes aus einem Datensatz an die Eigenschaft eines Webcontrols durch.
        /// Dabei können Konvertierungen und Formatierungen stattfinden
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="value"></param>
        public delegate void WebCtrlPropertySetter(TWebCtrl ctrl, TDataItem dataItem);
        WebCtrlPropertySetter _webCtrlPropertySetter;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="webCtrl">Web- Control, für das die Bindung definiert wird</param>        
        /// <param name="webCtrlPoropertySetter">Funktion, welche die Zuweisung einer DataItem- Eigenschaft an eine WebCtrl- Eigenschaft beschreibt</param>
        public BindingWebCtrl(TWebCtrl webCtrl, WebCtrlPropertySetter webCtrlPoropertySetter)
        {
            WebCtrl = webCtrl;
            _webCtrlPropertySetter = webCtrlPoropertySetter;                       

            WebCtrl.DataBinding += new EventHandler(DataBindHandler);           
        }

        void  DataBindHandler(object sender, EventArgs e)
        {
            var ctrl = (TWebCtrl)sender;
            Debug.Assert(ctrl == WebCtrl);
            _webCtrlPropertySetter(ctrl, DataItem);
        }

        /// <summary>
        /// In Abgeleiteten Klassen wird hier der Zugriff zu dem
        /// mit dem Control assoziierten Datensatz beschrieben
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        protected abstract TDataItem DataItem
        {
            get;
        }

    }
}
