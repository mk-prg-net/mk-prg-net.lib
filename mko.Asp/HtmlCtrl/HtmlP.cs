using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mkoIt.Asp.HtmlCtrl
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:P runat=server></{0}:P>")]
    public class P : HtmlContainerCtrlBase
    {
        public P()
            : base("p") { }

        public P(string ID)
            : base("p") 
        {
            this.ID = ID;
        }

        public P(string ID, out P pRef)
            : base("p")
        {
            pRef = this;
            this.ID = ID;
        }

        public P(params Control[] controls)
            : base("p") 
        {
            Content = controls;
        }

        public P(string ID, Control[] controls)
            : base("p")
        {
            this.ID = ID;
            Content = controls;
        }

        public P(string ID, out P pRef, Control[] controls)
            : base("p")
        {
            pRef = this;
            this.ID = ID;
            Content = controls;
        }
    }
}
