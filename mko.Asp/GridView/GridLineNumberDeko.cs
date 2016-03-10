using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;


namespace mkoIt.Asp
{
    public class GridLineNumberDeko
    {
        System.Web.UI.WebControls.GridView _grd;
        int zeilennr = 0;
        int _ZeilenNrCol = 0;


        public GridLineNumberDeko(System.Web.UI.WebControls.GridView grd, int ZeilenNrCol)
        {
            _ZeilenNrCol = ZeilenNrCol;
            _grd = grd;
            _grd.RowDataBound += new GridViewRowEventHandler(_grd_RowDataBound);
        }

        void _grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var grd = (System.Web.UI.WebControls.GridView)sender;

            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    zeilennr = -1;
                    break;
                case DataControlRowType.DataRow:
                    zeilennr++;

                    Label lblZeilenNr = e.Row.Cells[_ZeilenNrCol].FindControl("lblZeilenNr") as Label;
                    Debug.Assert(lblZeilenNr != null, "Das Label lblZeilenNr wurde nicht gefunden");

                    lblZeilenNr.Text = String.Format("{0,3:N0}", zeilennr + grd.PageSize * grd.PageIndex);
                    break;
                default: ;
                    break;
            }

        }

    }
}
