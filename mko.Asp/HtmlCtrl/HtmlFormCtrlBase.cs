using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using css = mkoIt.Xhtml.Css;

namespace mkoIt.Asp.HtmlCtrl
{
    public class FormCtrlBase<TWebCtrl>
        where TWebCtrl : System.Web.UI.WebControls.WebControl, new()
    {
        protected TWebCtrl ctrl = new TWebCtrl();

        public FormCtrlBase(string ID)            
        {
            ctrl.ID = ID;
            ctrl.PreRender += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(CssClassName)) ctrl.Attributes.Add("class", CssClassName);
                if (CssStyleBld != null) ctrl.Attributes.Add("style", CssStyleBld.ToString());

                // Überarbeitung des Html- Elementes vor der Auslieferung in abgeleiteten Klassen
                PreRenderReworking(sender);
            };
        }

        /// <summary>
        /// Wird in abgeleiteten Klassen überschrieben, um die Ausgabe für das Html- Element anzupassen
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void PreRenderReworking(object sender)
        {
        }

        public string CssClassName
        {
            get;
            set;
        }

        public css.StyleBuilder CssStyleBld
        {
            get;
            set;
        }

        public TWebCtrl ToWebControl()
        {
            return ctrl;
        }
    }
}
