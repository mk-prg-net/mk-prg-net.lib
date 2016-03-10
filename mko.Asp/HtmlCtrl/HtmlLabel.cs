using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using msWebUi = System.Web.UI;
using msWebCtrl = System.Web.UI.WebControls;
using mkx = mkoIt.Xhtml.Xhtml;
using css = mkoIt.Xhtml.Css;

namespace mkoIt.Asp.HtmlCtrl
{
    public class Label : msWebCtrl.Label, ICss
    {
        public Label(string ID)            
        {
            this.ID = ID;
            CssOutputGenerator.config(this);

        }

        public Label(string ID, out Label labelRef)
        {
            labelRef = this;
            this.ID = ID;
            CssOutputGenerator.config(this);
        }

        public EventHandler SetLoad
        {
            set
            {
                Load += new EventHandler(value);
            }
        }

        public EventHandler SetDataBinding
        {
            set
            {
                DataBinding += new EventHandler(value);
            }
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

        public msWebUi.Control Ctrl
        {
            get { return this; }
        }

        public msWebUi.AttributeCollection CtrlAttributes
        {
            get { return this.Attributes; }
        }

        public void PreRenderReworking()
        {
            
        }
    }
}
