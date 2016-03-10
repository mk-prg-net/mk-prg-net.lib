using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;


namespace mkoIt.Asp
{
    public class GridFilterSetFilterButton
    {
        System.Web.UI.WebControls.GridView _grdAll;
        Button _btnSetFilter;
        Page _Page;
        public GridFilterSetFilterButton(Page page, System.Web.UI.WebControls.GridView grdAll, string IdBtnSetFilter)
        {
            _Page = page;
            _grdAll = grdAll;
            if (_grdAll.HeaderRow != null)
            {
                _btnSetFilter = _grdAll.HeaderRow.FindControl(IdBtnSetFilter) as Button;
                Debug.Assert(_btnSetFilter != null);
                _btnSetFilter.Click += new EventHandler(OnButtonClick);
            }
        }

        void OnButtonClick(object sender, EventArgs e)
        {
            if (_Page.IsValid && _grdAll.HeaderRow != null)
            {
                _grdAll.PageIndex = 0;
                _grdAll.DataBind();
            }

        }
    }
}
