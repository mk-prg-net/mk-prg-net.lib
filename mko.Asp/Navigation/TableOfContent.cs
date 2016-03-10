using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Zusammenfassungsbeschreibung für TableOfContent
/// </summary>
/// 
namespace mkoIt.Asp
{
    public class TableOfContent
    {
        public static void NewTOC(Table TabLinksToPages)
        {
            if (SiteMap.CurrentNode != null)
            {
                foreach (SiteMapNode node in SiteMap.CurrentNode.ChildNodes)
                {
                    TabLinksToPages.Rows.Add(MakeTableRow(node));
                }
            }
            else                
               TabLinksToPages.Page.Response.Redirect(SiteMap.RootNode.Url);

        }

        private static TableRow MakeTableRow(SiteMapNode node)
        {
            // Tabellenzeile mit einer Zelle anlegen
            TableRow row = new TableRow();
            row.CssClass = "SiteMapTabRow";

            TableCell cell = new TableCell();
            cell.CssClass = "SiteMapTabRowCell";
            row.Cells.Add(cell);

            // In der Zelle eine weitere Tabelle anlegen
            Table innerTab = new Table();
            innerTab.CssClass = "SiteMapTabCellInnerTab";
            cell.Controls.Add(innerTab);

            TableRow innerRow1 = new TableRow();
            innerRow1.CssClass = "SiteMapTabCellInnerTabRow1";
            innerTab.Rows.Add(innerRow1);

            TableRow innerRow2 = new TableRow();
            innerRow2.CssClass = "SiteMapTabCellInnerTabRow2";
            innerTab.Rows.Add(innerRow2);

            // Zellen anlegen und den Zeilen zuteilen
            TableCell innerRow1Cell1 = new TableCell();
            innerRow1Cell1.CssClass = "SiteMapTabCellInnerTabRow1Cell1";
            innerRow1.Cells.Add(innerRow1Cell1);

            TableCell innerRow1Cell2 = new TableCell();
            innerRow1Cell2.CssClass = "SiteMapTabCellInnerTabRow1Cell2";
            innerRow1.Cells.Add(innerRow1Cell2);

            TableCell innerRow2Cell1 = new TableCell();
            innerRow2Cell1.CssClass = "SiteMapTabCellInnerTabRow2Cell1";
            innerRow2.Cells.Add(innerRow2Cell1);

            TableCell innerRow2Cell2 = new TableCell();
            innerRow2Cell2.CssClass = "SiteMapTabCellInnerTabRow2Cell2";
            innerRow2.Cells.Add(innerRow2Cell2);


            // Inhalte der Zellen definieren
            LinkButton lkbTitle = new LinkButton();
            lkbTitle.CssClass = "SiteMapTabTitle";
            lkbTitle.Text = node.HasChildNodes ? "+" + node.Title : node.Title;
            lkbTitle.CommandArgument = node.Url;
            lkbTitle.Click += new EventHandler(ButtonGotoPage_Click);
            innerRow1Cell1.Controls.Add(lkbTitle);

            Label lblDescr = new Label();
            lblDescr.CssClass = "SiteMapTabDescr";
            lblDescr.Text = node.Description;
            innerRow2Cell1.Controls.Add(lblDescr);

            return row;
        }

        public static event EventHandler PrepareRedirect;
        public class PrepareRedirectEventArgs : EventArgs
        {
            public PrepareRedirectEventArgs(string SitemapNodeTitle)
            {
                this.SitemapNodeTitle = SitemapNodeTitle;
            }

            public string SitemapNodeTitle;
        }

        private static void ButtonGotoPage_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            if (PrepareRedirect != null)
                PrepareRedirect(null, new PrepareRedirectEventArgs(btn.ToolTip));

            btn.Page.Response.Redirect(btn.CommandArgument);
        }


    }
}