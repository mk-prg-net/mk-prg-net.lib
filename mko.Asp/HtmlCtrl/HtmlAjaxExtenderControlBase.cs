using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mkoIt.Asp.HtmlCtrl

{
    public class AjaxExtenderControlBase<TAjaxExtenderCtrl>
        where TAjaxExtenderCtrl : AjaxControlToolkit.ExtenderControlBase, new()
    {
        protected TAjaxExtenderCtrl ctrl = new TAjaxExtenderCtrl();

        public AjaxExtenderControlBase(string ID)            
        {
            ctrl.ID = ID;            
        }       

        public string TargetControlID
        {
            set
            {
                ctrl.TargetControlID = value;
            }
        }

        public System.Web.UI.Control ToWebControl()
        {
            return ctrl;
        }

    }
}
