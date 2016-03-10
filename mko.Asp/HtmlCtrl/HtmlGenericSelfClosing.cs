using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mkoIt.Asp
{
    /// <summary>
    /// Html generic control class that displays as self-closing tag
    /// </summary>
    public class HtmlGenericSelfClosing : System.Web.UI.HtmlControls.HtmlGenericControl
    {
        public HtmlGenericSelfClosing()
            : base()
        {
        }

        public HtmlGenericSelfClosing(string tag)
            : base(tag)
        {
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(HtmlTextWriter.TagLeftChar + this.TagName);
            Attributes.Render(writer);
            writer.Write(HtmlTextWriter.SelfClosingTagEnd);
        }

        //public override ControlCollection Controls
        //{
        //    get {
        //        // Leere ControlColletion zurückgeben, da manchmal externer Code 
        //        // die 
        //        return new ControlCollection(this);
        //        //throw new Exception("Self-closing tag cannot have child controls"); 
        //    }
        //}

        public override string InnerHtml
        {
            get { return String.Empty; }
            set { throw new Exception("Self-closing tag cannot have inner content"); }
        }

        public override string InnerText
        {
            get { return String.Empty; }
            set { throw new Exception("Self-closing tag cannot have inner content"); }
        }
    }

}
