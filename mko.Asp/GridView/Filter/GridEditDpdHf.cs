using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;


namespace mkoIt.Asp
{
    public class GridEditDpdHf
    {
        HiddenField _hf;
        DropDownList _dpd;

        public GridEditDpdHf(DropDownList dpd)
        {            
            _dpd = dpd;
            // Das zugehörige HiddenField befindet sich in der gleichen Tabellenzeile und hat den gleichen
            // Namen. Der Unterschied besteht im Präfix "hf"
            string hfId = "hf" + _dpd.ID.Substring(3);
            _hf = _dpd.Parent.FindControl(hfId) as HiddenField;
            Debug.Assert(_hf != null);

            _dpd.DataBound += new EventHandler(_dpd_DataBound);
            _dpd.SelectedIndexChanged += new EventHandler(_dpd_SelectedIndexChanged);
        }

        public void SyncHfWithDpdSelectedValue()
        {
            _hf.Value = _dpd.SelectedValue;
        }

        void _dpd_SelectedIndexChanged(object sender, EventArgs e)
        {
            _hf.Value = _dpd.SelectedValue;
        }

        void _dpd_DataBound(object sender, EventArgs e)
        {
            _dpd.SelectedValue = _hf.Value;
        }
    }
}
