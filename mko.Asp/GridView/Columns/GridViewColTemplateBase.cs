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
    public abstract class ColTemplateBase : ITemplate
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
        protected abstract void CreateContent(Control NamingContainer, ControlCollection content);


        /// <summary>
        /// Liste aller Datenbindungen von Steuerelementen in der Spalte an den Feldwert
        /// </summary>
        //protected List<DataBind.BindingBase> DataBindings = new List<DataBind.BindingBase>();

        //protected void AddBinding(DataBind.BindingBase binding)
        //{
        //    DataBindings.Add(binding);
        //}

        //public DataBind.BindingBase[] Bindings
        //{
        //    set
        //    {
        //        DataBindings.AddRange(value);
        //    }
        //}

        public void InstantiateIn(Control container)
        {
            if (container is WebControl)
            {
                var webCtrl = container as WebControl;
                //webCtrl.Style.Add(HtmlTextWriterStyle.Padding, "0");
                //webCtrl.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
                //webCtrl.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");

                webCtrl.CssClass = CssClassFrame;
                if(StyleFrame != null)
                    webCtrl.Attributes.Add("style", StyleFrame.ToString());

                CreateContent(container.NamingContainer, container.Controls);
            }
            else
            {
                // Rahmen definieren
                var divFrame = new mkoIt.Asp.HtmlCtrl.DIV();
                divFrame.CssClassName = CssClassFrame;
                divFrame.CssStyleBld = StyleFrame;
                container.Controls.Add(divFrame);

                // Inhalt aufbauen
                CreateContent(container.NamingContainer, divFrame.Controls);
            }
        }       

    }
}
