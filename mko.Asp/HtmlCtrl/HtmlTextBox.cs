using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using msWebUi = System.Web.UI;
using msWebCtrl = System.Web.UI.WebControls;

using mkx = mkoIt.Xhtml.Xhtml;



namespace mkoIt.Asp.HtmlCtrl 
{
    public class TextBox : msWebCtrl.TextBox, ICss
    {
        public TextBox(string ID)            
        {
            CssOutputGenerator.config(this);
            this.ID = ID;
        }

        public TextBox(string ID, out TextBox tbxRef)
        {
            CssOutputGenerator.config(this);
            tbxRef = this;
            this.ID = ID;
        }          


        public EventHandler SetLoad
        {
            set
            {
                Load += new EventHandler(value);
            }
        }

        public EventHandler SetTextChanged
        {
            set
            {
                TextChanged += new EventHandler(value);
            }
        }

        public string CssClassName
        {
            get;
            set;
        }

        public Xhtml.Css.StyleBuilder CssStyleBld
        {
            get;
            set;
        }


        public void PreRenderReworking()
        {
            if (!string.IsNullOrWhiteSpace(CssClassName))
                CssClass = CssClassName;

            if(CssStyleBld != null)
                Attributes.Add("style", CssStyleBld.ToString());
        }


        public msWebUi.AttributeCollection CtrlAttributes
        {
            get { return this.Attributes; }
        }


        public msWebUi.Control Ctrl
        {
            get { return this; }
        }
    }
}
