using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Asp
{
    public class RestartApp
    {
        public static void exe(System.Web.UI.Page page)
        {
            System.Web.HttpRuntime.UnloadAppDomain();
            System.Net.WebClient myWebClient = null;
            try
            {
                string url = AspWebSitePath.MapUrl(page, ".");
                myWebClient = new System.Net.WebClient();
                byte[] stuff = myWebClient.DownloadData(url);
            }
            catch
            {
            }
            finally
            {
                myWebClient.Dispose();
            }
        }

    }
}
