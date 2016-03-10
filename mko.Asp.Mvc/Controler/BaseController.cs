//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 28.4.2015
//
//  Projekt.......: GKStatReportViewer
//  Name..........: BaseController.cs
//  Aufgabe/Fkt...: Basisklasse der Controller. Stellt Infrastruktur wie Log und Ausnahmebehandlung bereit
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 4.8.2015
//  Änderungen....: Verallgemeinert in mko.Asp.Mvc.Controler
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace mko.Asp.Mvc.Controler
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Zentraler Verteiler für Fehler und Statusmeldungen in einem Controller
        /// </summary>
        protected mko.Log.LogServer log = new mko.Log.LogServer();

        /// <summary>
        /// Default- Protokollmedium von Fehler und Statusmeldungen
        /// </summary>
        protected mko.Log.MemLogHandler MemLogHnd;

        const string IDMemLogHandler = "mkoItlog04082015";

        HttpApplicationStateBase app;

        /// <summary>
        /// Liste aller Logeinträge
        /// </summary>
        protected mko.Log.MemLogHandler.Entry[] AllLogs
        {
            get
            {
                try
                {
                    app.Lock();
                    // Einlesen aus dem Anwendungszustand
                    return MemLogHnd.ToArray();

                }
                finally
                {
                    app.UnLock();
                }

            }
        }

        /// <summary>
        /// Kann in Controler überschrieben werden, um den Anwendungszustand zu konfigurieren.
        /// app.lock muss nicht mehr aufgerufen werden, wird bereits im Context getan.
        /// </summary>
        /// <param name="app"></param>
        protected abstract void ConfigAppState(HttpApplicationStateBase app);
        

        /// <summary>
        /// Richtet allg. Umgebung eines Controllers ein 
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            // Anlegen des MemLogHandlers oder einlesen dieses aus dem Anwendungszustand 
            app = requestContext.HttpContext.Application;
            try
            {
                app.Lock();
                if (app[IDMemLogHandler] == null)
                {
                    // Anlegen, falls er noch nicht existiert
                    MemLogHnd = new mko.Log.MemLogHandler();
                    app[IDMemLogHandler] = MemLogHnd;
                }
                else
                {
                    // Einlesen aus dem Anwendungszustand
                    MemLogHnd = (mko.Log.MemLogHandler)app[IDMemLogHandler];
                }

                ConfigAppState(app);


            }
            finally
            {
                app.UnLock();
            }

            log.registerLogHnd(MemLogHnd);
            base.Initialize(requestContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            // Ausnahme dokumentieren            

            var LastError = new Models.LastErrorDescriptor();

            LastError.ControllerName = (string)filterContext.RouteData.Values["controller"];
            LastError.ActionName = (string)RouteData.Values["action"];
            LastError.Ex = filterContext.Exception;
            LastError.ShortErrorDescription = LastError.Ex.Message;

            try
            {
                // Protokollieren in der DB
                log.Log(mko.Log.RC.CreateError(mko.TraceHlp.FormatErrMsg(this, LastError.ActionName, LastError.ShortErrorDescription), LastError.Ex));
            }
            catch (Exception ex)
            {
                LastError.ShortErrorDescription += "; Fehler beim Loggen: " + ex.Message;
            }

            // View mit Fehlerbeschreibung
            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Views/Error/Index.cshtml",
                ViewData = new ViewDataDictionary<Models.LastErrorDescriptor>(LastError),
                TempData = filterContext.Controller.TempData
            };

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        /// <summary>
        /// Freischaltung von TryUpdateModel für FilterSortPagingViewsModelBase
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="Model"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        internal bool TryUpdateModelIntern<TModel>(TModel Model, FormCollection fc)
            where TModel : class
        {
            return TryUpdateModel(Model, fc);
        }

        /// <summary>
        /// Freischaltung von RedirectToRoute für FilterSortPagingViewsModelBase
        /// </summary>
        /// <param name="RouteName"></param>
        /// <param name="RouteParameter"></param>
        /// <returns></returns>
        internal ActionResult RedirectToRouteIntern(string RouteName, object RouteParameter)
        {
            return RedirectToRoute(RouteName, RouteParameter);
        }

    }
}
