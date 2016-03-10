﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mko.Asp.Test
{
    public partial class MkoTOC : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (null != bodyContentPlaceHolder.FindControl("hfBackgroundImage"))
            {
                var hfBackgroundImage = bodyContentPlaceHolder.FindControl("hfBackgroundImage") as HiddenField;
                bodyHtml.Style.Add(HtmlTextWriterStyle.BackgroundImage, mkoIt.Asp.AspWebSitePath.MapUrl(Page, "~/Bilder/" + hfBackgroundImage.Value));
                bodyHtml.Style.Add("background-repeat", "repeat");
            }

        }
    }
}