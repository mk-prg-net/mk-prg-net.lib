using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;

namespace mkoIt.Asp
{
    public abstract class GridNewLineDeko
    {
        mko.Log.LogServer _log;
        System.Web.UI.WebControls.GridView _grd;
        Button _btnNew;
        public bool showDummyRow = false;

        public GridNewLineDeko(System.Web.UI.WebControls.GridView grd, Button btnNew, mko.Log.LogServer log)
        {
            _log = log;
            _btnNew = btnNew;
            _btnNew.Click += new EventHandler(_btnNew_Click);
            _grd = grd;
            _grd.RowDataBound += new GridViewRowEventHandler(_grd_RowDataBound);
        }

        void _btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                // Dummy- Zeile sichbar machen
                showDummyRow = true;

                // 1. Zeile in den Edit- Modus umschalten
                _grd.EditIndex = 0;

                _grd.DataBind();
            }
            catch (Exception ex)
            {
                _log.Log(mko.Log.RC.CreateError("Beim Anlegen eines neuen Datensatzes: ", ex));
            }

        }

        public abstract bool IsNewLine(object DataItem);

        void _grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var grd = (System.Web.UI.WebControls.GridView)sender;

            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:                   
                    break;
                case DataControlRowType.DataRow:
                    if (IsNewLine(e.Row.DataItem) && !showDummyRow)
                        e.Row.Visible = false;
                    break;
                default: ;
                    break;
            }

        }



    }
}
