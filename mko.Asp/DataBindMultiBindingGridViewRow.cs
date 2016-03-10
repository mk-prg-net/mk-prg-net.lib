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
    public class MultiBindingGridViewRow<TWebCtrl, TFieldValue> : MultiBinding<TWebCtrl, TFieldValue>
        where TWebCtrl : Control
    {
        public MultiBindingGridViewRow(TWebCtrl webCtrl, string[] fieldNames, WebCtrlPropertySetter webCtrlPoropertySetter)
            : base(webCtrl, fieldNames, webCtrlPoropertySetter)
        { }

        protected override object GetDataItem(TWebCtrl ctrl)
        {
            GridViewRow row = (GridViewRow)ctrl.NamingContainer;
            return row.DataItem;
        }
    }
}
