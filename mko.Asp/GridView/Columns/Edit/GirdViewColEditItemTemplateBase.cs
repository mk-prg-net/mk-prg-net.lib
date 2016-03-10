using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using mkx = mkoIt.Xhtml.Xhtml;
using css = mkoIt.Xhtml.Css;

namespace mkoIt.Asp.GridView
{
    public abstract class ColEditItemTemplateBase : ITemplate
    {
        /// <summary>
        /// Css- Klasse für den Rahmen eines Item- Templates
        /// </summary>
        public string CssClassFrame { get; set; }

        /// <summary>
        /// Css- Stylesheets des Rahmens
        /// </summary>
        public css.StyleBuilder StyleFrame { get; set; }

        /// <summary>
        /// Erzeugt den Inhalt eines ItemTemplates
        /// </summary>
        /// <returns></returns>
        protected abstract System.Web.UI.Control CreateContent();
        

        public void InstantiateIn(Control container)
        {
            // Rahmen definieren
            var divFrame = new mkoIt.Asp.HtmlCtrl.DIV();
            divFrame.CssClassName = CssClassFrame;
            divFrame.CssStyleBld = StyleFrame;
            container.Controls.Add(divFrame);

            // Inhalt aufbauen
            divFrame.Controls.Add(CreateContent());
        }
    }
}
