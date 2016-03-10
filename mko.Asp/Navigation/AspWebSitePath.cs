using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Asp
{
    public class AspWebSitePath
    {
        public static string MapUrl(System.Web.UI.Page Page, string relativePath)
        {
            string ap = Page.Request.Url.Scheme + "://" + Page.Request.Url.Authority;
            if (Page.Request.ApplicationPath != "/")
            {
                if(relativePath.StartsWith("~"))
                    relativePath = relativePath.Substring(1);
                if(relativePath.StartsWith("/"))
                    relativePath = relativePath.Substring(1);

                if (!Page.Request.ApplicationPath.EndsWith("/"))
                    relativePath = "/" + relativePath;

                ap += Page.Request.ApplicationPath + relativePath;
            }
            else
            {
                if (relativePath.StartsWith("~"))
                    relativePath = relativePath.Substring(1);
                if (relativePath.StartsWith("/"))
                    relativePath = relativePath.Substring(1);

                ap += "/" + relativePath;
            }

            return ap;                
        }
    }
}
