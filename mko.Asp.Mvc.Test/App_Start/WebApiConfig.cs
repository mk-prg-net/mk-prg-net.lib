using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace mko.Asp.Mvc.Test
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            // mko: S 99.
            var jf = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jf.UseDataContractJsonSerializer = false;
            jf.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            jf.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
        }
    }
}
