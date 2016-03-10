using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using msWebUi = System.Web.UI;
using msWebCtrl = System.Web.UI.WebControls;

namespace mkoIt.Asp.DataBind
{
    public interface IDataFieldUpdater
    {        
        void SetField(msWebCtrl.WebControl Ctrl);
    }
}
