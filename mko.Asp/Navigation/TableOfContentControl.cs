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
    [DefaultProperty("Text")]
    
    [ToolboxData("<{0}:TableOfContentControl runat=server></{0}:TableOfContentControl>")]
    public class TableOfContentControl : WebControl, INamingContainer
    {
        mkoIt.Asp.SessionHistory _SessionHistory;

        protected override void CreateChildControls()
        {
            // Ab dieser Seite beginnt einer neuer History- Path
            _SessionHistory = mkoIt.Asp.SessionHistory.GetInstance(Page.Session);
            _SessionHistory.reset();

            Table TabLinksToPages = new Table();
            TabLinksToPages.Page = Page;
            TabLinksToPages.Width = Width;
            TabLinksToPages.CssClass = CssClass;
            mkoIt.Asp.TableOfContent.NewTOC(TabLinksToPages);

            Controls.Add(TabLinksToPages);
        }    

        
    }
}
