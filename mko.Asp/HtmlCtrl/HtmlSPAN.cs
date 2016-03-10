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
    [ToolboxData("<{0}:HtmlSPAN runat=server></{0}:HtmlSPAN>")]
    public class SPAN : HtmlContainerCtrlBase
    {
        public SPAN()
            : base("span") { 
            
        }

        public SPAN(string ID)
            : base("span")
        {
            this.ID = ID;
        }

        public SPAN(string ID, out SPAN spanRef)
            : base("span")
        {
            spanRef = this;
            this.ID = ID;
        }

        public SPAN(params Control[] controls)
            : base("span")
        {
            Content = controls;
        }

        public SPAN(string ID, Control[] controls)
            : base("span")
        {
            this.ID = ID;
            Content = controls;
        }

        public SPAN(string ID, out SPAN spanRef, Control[] controls)
            : base("span")
        {
            spanRef = this;
            this.ID = ID;
            Content = controls;
        }
    }
}
