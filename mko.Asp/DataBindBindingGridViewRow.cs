using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;

namespace mkoIt.Asp.DataBind
{
    /// <summary>
    /// Bindet ein WebControl aus einer Zeile in einem GridView an ein Datenfeld aus einer Datenquelle
    /// </summary>
    /// <typeparam name="TWebCtrl"></typeparam>
    /// <typeparam name="TFieldValue"></typeparam>
    public class BindingWebCtrlInGridViewRow<TWebCtrl, TDataItem> : BindingWebCtrl<TWebCtrl, TDataItem>
        where TWebCtrl : Control
    {
        public BindingWebCtrlInGridViewRow(TWebCtrl webCtrl, WebCtrlPropertySetter webCtrlPoropertySetter)
            : base(webCtrl, webCtrlPoropertySetter)
        { }

        protected override TDataItem DataItem
        {
            get
            {
                GridViewRow row = (GridViewRow)WebCtrl.NamingContainer;
                return (TDataItem)row.DataItem;
            }
        }
    }
}
