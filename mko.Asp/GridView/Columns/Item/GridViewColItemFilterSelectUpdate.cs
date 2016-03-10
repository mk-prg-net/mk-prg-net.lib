using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using mkx = mkoIt.Xhtml.Xhtml;

namespace mkoIt.Asp.GridView
{
    public class ColItemFilterSelectUpdate : ColTemplateBase
    {
        int zeilennr = 0;

        Label lblZeilenNr;

        public ColItemFilterSelectUpdate(System.Web.UI.WebControls.GridView grd)
        {
            grd.RowDataBound += new GridViewRowEventHandler(grd_RowDataBound);
        }

        void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var grd = (System.Web.UI.WebControls.GridView)sender;           

            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    zeilennr = -1;
                    break;
                case DataControlRowType.DataRow:
                    zeilennr = e.Row.RowIndex;
                    
                    Debug.Assert(lblZeilenNr != null, "Das Label lblZeilenNr wurde nicht gefunden");

                    lblZeilenNr.Text = String.Format("{0,3:N0}", zeilennr + grd.PageSize * grd.PageIndex);
                    break;
                default: ;
                    break;
            }
        }

        protected override void CreateContent(Control NamingContainer, ControlCollection content)
        {
            var cell1 = new TableCell()
            {
                Width = new Unit(50, UnitType.Percentage)
            };

            lblZeilenNr = new Label() { 
                ID="lblZeilenNr",
                Text="0"
            };

            cell1.Controls.Add(lblZeilenNr);

            var cell2 = new TableCell()
            {
                Width = new Unit(50, UnitType.Percentage)
            };

            cell2.Controls.Add(new Button()
            {
                ID = "btnEdit",
                Text = ">_",
                Width = new Unit(30, UnitType.Pixel),
                ToolTip = "Daten ändern",
                CommandName="Edit"
            });


            var row = new TableRow()
            {
                Width = new Unit(100, UnitType.Percentage)                
            };
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);

            var tab = new Table()
            {
                Width = new Unit(100, UnitType.Percentage)
            };
            tab.Rows.Add(row);

            content.Add(tab);
            
        }
    }
}
