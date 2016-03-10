//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 7.3.2012
//
//  Projekt.......: mkoItAsp
//  Name..........: GridViewEmptyDataTemplate.cs
//  Aufgabe/Fkt...: GridView- Vorlage für den Fall einer leeren Datenmenge
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        
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
    public class EmptyDataTemplate<TEntity> : TemplateBase
        where TEntity : class, new()
    {
        public string RemoveButtonCaption { get; set; }
        public css.StyleBuilder RemoveButtonStyle { get; set; }

        public string EmptyDataText { get; set; }
        public css.StyleBuilder EmptyDataTextStyle { get; set; }

        public css.StyleBuilder CurrentlyActiveFiltersTabStyle { get; set; }
        public css.StyleBuilder CurrentlyActiveFiltersTabCellDescription { get; set; }
        public css.StyleBuilder CurrentlyActiveFiltersTabCellAction { get; set; }

        SessionStateFilterAndSortEntities<TEntity> sessVar;
        HttpResponse response;
        
        public EmptyDataTemplate(SessionStateFilterAndSortEntities<TEntity> sessVar, HttpResponse response)
        {
            Debug.Assert(sessVar != null && response != null, "EmptyDataTemplate: Konstruktorparameter unvollständig");
            this.sessVar = sessVar;
            this.response = response;

            RemoveButtonCaption = "Alle Filter entfernen";
            RemoveButtonStyle = new css.StyleBuilder()
            {
                BackgroundColor = css.Color.Red,
                ForeColor = css.Color.White
            };
            EmptyDataText = "Keine Datensätze vorhanden, die den aktuellen Filtereinstellungen genügen.";
            EmptyDataTextStyle = new css.StyleBuilder()
            {
                ForeColor = css.Color.Black
            };

            CurrentlyActiveFiltersTabStyle = new css.StyleBuilder()
            {
                Width = new css.LengthRealtive() { Value = 100.0 },
                TableBorderCollapse = new css.BorderCollapse() { Value = css.BorderCollapse.Unit.collapse }                
            };

            CurrentlyActiveFiltersTabCellDescription = new css.StyleBuilder()
            {
                Width = new css.LengthPixel() { Value=400 },
                ForeColor = css.Color.Black,
                BorderStyle = new css.BorderStyle() { Value = css.BorderStyle.Unit.solid },
                BorderWidth = new css.LengthPixel() { Value = 1 },
            };

            CurrentlyActiveFiltersTabCellAction = new css.StyleBuilder()
            {               
                ForeColor = css.Color.Black,
                BorderStyle = new css.BorderStyle() { Value = css.BorderStyle.Unit.solid },
                BorderWidth = new css.LengthPixel() { Value = 1 },
            };
        }

        protected override void CreateContent(System.Web.UI.Control NamingContainer, System.Web.UI.ControlCollection content)
        {
            var hinweis = new HtmlCtrl.P()
            {                
                InnerText = EmptyDataText,
                CssStyleBld = EmptyDataTextStyle
            };
            content.Add(hinweis);
            content.Add(new HtmlCtrl.BR());

            var btnRemove = new Button()
            {
                Text = RemoveButtonCaption
            };
            btnRemove.Attributes.Add("style", RemoveButtonStyle.ToString());
            btnRemove.Click += new EventHandler(btnRemove_Click);
            content.Add(btnRemove);

            content.Add(new HtmlCtrl.BR());

            // Alle aktuell gültigen Filter auflisten
            var fltTab = new System.Web.UI.HtmlControls.HtmlTable();
            content.Add(fltTab);
            fltTab.Attributes.Add("style", CurrentlyActiveFiltersTabStyle.ToString());

            var header = new System.Web.UI.HtmlControls.HtmlTableRow();
            fltTab.Rows.Add(header);

            var col1Header = new System.Web.UI.HtmlControls.HtmlTableCell()
            {
                InnerText = "Filter"
            };
            header.Cells.Add(col1Header);
            var col1HeaderCssBld = new css.StyleBuilder(CurrentlyActiveFiltersTabCellDescription);
            col1HeaderCssBld.FontWeight = new css.FontWeight()
            {
                Value = css.FontWeight.Unit.bold
            };
            col1Header.Attributes.Add("style", col1HeaderCssBld.ToString());

            var col2Header = new System.Web.UI.HtmlControls.HtmlTableCell()
            {
                InnerText = "entfernen ja/nein"
            };
            header.Cells.Add(col2Header);
            var col2HeaderCssBld = new css.StyleBuilder(CurrentlyActiveFiltersTabCellAction);
            col2HeaderCssBld.FontWeight = new css.FontWeight()
            {
                Value = css.FontWeight.Unit.bold
            };
            col2Header.Attributes.Add("style", col2HeaderCssBld.ToString());
            


            int line = 0;
            foreach (var flt in sessVar.Filters)
            {
                var row = new System.Web.UI.HtmlControls.HtmlTableRow();
                fltTab.Rows.Add(row);

                var descr = new System.Web.UI.HtmlControls.HtmlTableCell()
                {
                    InnerText = flt.Value.Description
                };
                descr.Attributes.Add("style", CurrentlyActiveFiltersTabCellDescription.ToString());
                row.Cells.Add(descr);

                var action = new System.Web.UI.HtmlControls.HtmlTableCell();
                action.Attributes.Add("style", CurrentlyActiveFiltersTabCellAction.ToString());
                action.Controls.Add(new CheckBox() { ID="cbxCtrl" + line++, ClientIDMode=ClientIDMode.Static, Checked=true});
                row.Cells.Add(action);
            }

        }

        void btnRemove_Click(object sender, EventArgs e)
        {
            var btn = (Button) sender;
            //sessVar.RemoveAllFilters();

            Queue<Type> FiltersToRemove = new Queue<Type>();
            int line = 0;
            foreach(var flt in sessVar.Filters) {
                var cbx = btn.Parent.FindControl("cbxCtrl" + line++) as CheckBox;
                if (cbx.Checked)
                    FiltersToRemove.Enqueue(flt.Key);
            }

            while (FiltersToRemove.Count > 0)
                sessVar.RemoveFilter(FiltersToRemove.Dequeue());

            // Erneuter Aufruf der Webseite ohne Filter- Restriktionen
            response.Redirect(SiteMap.CurrentNode.Url, true);
        }

    }
}
