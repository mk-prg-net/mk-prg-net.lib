using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using mkx = mkoIt.Xhtml.Xhtml;


namespace mkoIt.Asp.GridView
{
    public class Utils
    {
        public static TemplateField FindTemplateField(System.Web.UI.WebControls.GridView grd, string headerText)
        {
            TemplateField tfMatch = null;
            foreach (var col in grd.Columns)
            {
                if (col is TemplateField)
                {
                    var tf = col as TemplateField;
                    if (tf.HeaderText == headerText)
                    {
                        tfMatch = tf;
                        break;
                    }
                }
            }

            return tfMatch;
        }

    }
}
