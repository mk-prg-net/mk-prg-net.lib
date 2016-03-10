using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using mkx = mkoIt.Xhtml.Xhtml;


namespace mkoIt.Asp.HtmlCtrl
{
    public class DateBox : SPAN
    {
        TextBox tbx;
        AjaxControlToolkit.CalendarExtender calExt;        

        public DateBox(string ID)            
        {
            CreateDateBox(ID);
        }

        public DateBox(string ID, out DateBox dateBoxRef)
        {
            dateBoxRef = this;
            CreateDateBox(ID);
        }

        private void CreateDateBox(string ID)
        {
            this.ID = ID;

            var ParentCssBld = CssStyleBld;

            tbx = new TextBox(ID + "Tbx");

            calExt = new AjaxControlToolkit.CalendarExtender()
            {
                ID = ID + "CalExt",
                TargetControlID = tbx.ID,
                PopupPosition = AjaxControlToolkit.CalendarPosition.BottomLeft,
                Format = "dd.MM.yyyy"
            };

            Content = new Control[] { tbx, calExt };
        }        

        public override void PreRenderReworking(object sender)
        {
            var tbxWeb = ((System.Web.UI.WebControls.TextBox)tbx);
            if (CssStyleBld != null && CssStyleBld.Width != null)
                tbxWeb.Attributes.Add("style", "width: " + CssStyleBld.Width.length);
        }

        public string DateFormat
        {
            set
            {                
                calExt.Format = value;
            }
        }

        public EventHandler SetLoad
        {
            set
            {
                tbx.Load += new EventHandler(value);
            }
        }

        public EventHandler SetTextChanged
        {
            set
            {
                ((System.Web.UI.WebControls.TextBox)tbx).TextChanged += new EventHandler(value);
            }
        }

    }
}
