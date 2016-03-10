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
    public abstract class ColHeaderTemplateBase : ColTemplateBase
    {

        /// <summary>
        /// Name des Feldes aus der Datenquelle, aus der die Daten in diesem 
        /// </summary>
        public string ColName { get; set; }

        /// <summary>
        /// Name des Feldes aus der Datenquelle, nach der die Datensätze zu sortieren sind
        /// </summary>
        public string SortColName { get; set; }

        /// <summary>
        /// Spaltenüberschrift + Tooltip
        /// </summary>
        public string HeadLine { get; set; }
        public string ToolTip { get; set; }

        public string CssClassHeadLine { get; set; }       
        public css.StyleBuilder StyleHeadLine {get; set;}

        protected abstract void CreateFilterCtrl(Control NamingContainer, ControlCollection content);

        protected override void CreateContent(Control NamingContainer, ControlCollection content)
        {
            var lbtHeadline = new LinkButton()
            {
                ID = "lbt" + ColName,
                Text = HeadLine,
                ToolTip = this.ToolTip,
                CommandArgument = string.IsNullOrWhiteSpace(SortColName) ? ColName: SortColName,
                CommandName = "Sort",
                CssClass = CssClassHeadLine
            };

            if(StyleHeadLine != null)
                lbtHeadline.Attributes["style"] = StyleHeadLine.ToString();

            content.Add(lbtHeadline);

            content.Add(new mkoIt.Asp.HtmlCtrl.BR());

            CreateFilterCtrl(NamingContainer, content);
        }
    }
}
