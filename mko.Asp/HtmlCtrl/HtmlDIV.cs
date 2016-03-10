using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using mkx = mkoIt.Xhtml.Xhtml;


namespace mkoIt.Asp.HtmlCtrl 
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:HtmlDIV runat=server></{0}:HtmlDIV>")]
    public class DIV : HtmlContainerCtrlBase
    {
        public DIV()
            : base("div") {             
        }

        public DIV(string ID)
            : base("div")
        {
            this.ID = ID;
        }

        public DIV(string ID, out DIV divRef)
            : base("div")
        {
            this.ID = ID;
            divRef = this;
        }

        public DIV(params System.Web.UI.Control[] ctrl)
            : base("div")
        {
            Content = ctrl;
        }

        public DIV(string ID, System.Web.UI.Control[] ctrl)
            : base("div")
        {
            this.ID = ID;
            Content = ctrl;
        }

        public DIV(string ID, out DIV divRef, System.Web.UI.Control[] ctrl)
            : base("div")
        {
            this.ID = ID;
            Content = ctrl;
            divRef = this;
        }

    }
}
