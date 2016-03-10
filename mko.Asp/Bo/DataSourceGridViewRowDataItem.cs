using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using msWebUi = System.Web.UI;
using msWebCtrl = System.Web.UI.WebControls;


namespace mkoIt.Asp.DataBinding.DataSource
{
    public class GridView
    {
        public static TDataItem RowDataItem<TDataItem>(msWebCtrl.WebControl ctrl)
        {
            msWebCtrl.GridViewRow row = (msWebCtrl.GridViewRow)ctrl.NamingContainer;
            return (TDataItem)row.DataItem;
        }        
    }
}
