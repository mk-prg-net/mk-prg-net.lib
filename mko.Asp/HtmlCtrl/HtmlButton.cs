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
    public class Button : msWebCtrl.Button, ICss

    {
        public Button(string ID)           
        {
            this.ID = ID;
            CssOutputGenerator.config(this);
            
        }

        public Button(string ID, out Button buttonRef)
        {
            buttonRef = this;
            this.ID = ID;
            CssOutputGenerator.config(this);
        }        

        public string Text
        {
            set
            {
                base.Text = value;
            }
        }

        public EventHandler SetClick
        {
            set
            {
                base.Click += new EventHandler(value);
            }
        }


        public string SetClientClick
        {
            set
            {
                base.OnClientClick = value;
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
