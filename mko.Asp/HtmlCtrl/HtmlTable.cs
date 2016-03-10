using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using msWebUi = System.Web.UI;
using msWebCtrl = System.Web.UI.WebControls;

using msHtml = System.Web.UI.HtmlControls;
using css = mkoIt.Xhtml.Css;

namespace mkoIt.Asp.HtmlCtrl
{
    public class Table : msHtml.HtmlTable, ICss
    {
        private msWebUi.Control[] control;
        private TableRow tableRow;
        private TableRow tableRow_2;

        public Table()            
        {
            CssOutputGenerator.config(this);
        }

        public Table(string ID)
        {
            this.ID = ID;
            CssOutputGenerator.config(this);

        }

        public Table(string ID, out Table tabRef)
        {
            tabRef = this;
            this.ID = ID;
            CssOutputGenerator.config(this);

        }



        public Table(params TableRow[] rows)
        {
            CssOutputGenerator.config(this);
            Rows = rows;
        }

        public Table(string ID, TableRow[] rows)
        {            
            this.ID = ID;
            CssOutputGenerator.config(this);
            Rows = rows;
        }

        public Table(string ID, out Table tabRef, TableRow[] rows)
        {
            tabRef = this;
            this.ID = ID;
            CssOutputGenerator.config(this);
            Rows = rows;
        }

        public Table(msWebUi.Control[] control, TableRow tableRow, TableRow tableRow_2)
        {
            // TODO: Complete member initialization
            this.control = control;
            this.tableRow = tableRow;
            this.tableRow_2 = tableRow_2;
        } 

        public new TableRow[] Rows {
            set
            {
                foreach (var row in value)
                {
                    base.Rows.Add(row);
                }
            }

            get
            {
                var rows = new TableRow[base.Rows.Count];
                int i = 0;
                foreach (var row in base.Rows)
                    rows[i++] = (TableRow)row;
                return rows;
            }
        }

        public void AddRow(TableRow newRow)
        {
            base.Rows.Add(newRow);
        }

        public void RemoveRow(TableRow row)
        {
            base.Rows.Remove(row);
        }

        public void ClearRows()
        {
            base.Rows.Clear();
        }        

        public string CssClassName { get; set; }

        public css.StyleBuilder CssStyleBld { get; set; }       

        public void PreRenderReworking()
        {            
        }

        public msWebUi.AttributeCollection CtrlAttributes
        {
            get { return this.Attributes; }
        }


        public msWebUi.Control Ctrl
        {
            get { return this; }
        }
    }


    public class TableRow : msHtml.HtmlTableRow, ICss
    {
        public TableRow() {
            CssOutputGenerator.config(this);
        }

        public TableRow(params TableCell[] cells)
        {
            CssOutputGenerator.config(this);
            Cells = cells;
        }


        public new TableCell[] Cells
        {
            set
            {
                foreach (var cell in value)
                {
                    base.Cells.Add(cell);
                }
            }
        }

        public string CssClassName { get; set; }

        public css.StyleBuilder CssStyleBld { get; set; }
               
        public void PreRenderReworking()
        {           
        }


        public msWebUi.AttributeCollection CtrlAttributes
        {
            get { return this.Attributes; }
        }


        public msWebUi.Control Ctrl
        {
            get { return this; }
        }
    }

    public class TableCell : msHtml.HtmlTableCell, ICss
    {
        public TableCell() {
            CssOutputGenerator.config(this);
        }

        public TableCell(params msWebUi.Control[] ctrls)
        {
            CssOutputGenerator.config(this);
            Content = ctrls;
        }

        public msWebUi.Control[] Content
        {
            set
            {
                foreach (var ctrl in value)
                {
                    base.Controls.Add(ctrl);
                }
            }
        }

        public string CssClassName { get; set; }

        public css.StyleBuilder CssStyleBld { get; set; }       

        public void PreRenderReworking()
        {            
        }

        public msWebUi.AttributeCollection CtrlAttributes
        {
            get { return this.Attributes; }
        }


        public msWebUi.Control Ctrl
        {
            get { return this; }
        }
    }
}
