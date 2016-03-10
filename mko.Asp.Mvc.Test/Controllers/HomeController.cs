using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mko.Asp.Mvc.Test.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
            
        }

        public ActionResult About()
        {

            // Die ViewBag ist ein dynamisches Objekt, dem zur Laufzeit AdHoc Eigenschaften zugewiesen werden
            // können. Auf diese kann in der View dann zugegriffen werden.
            ViewBag.Message = "Einführung in ASP.NET MVC";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "(c)Matrin Korneffel: www.mkoit.de";

            return View();
        }

        /// <summary>
        /// Diese Aktion verknüpft das Model mit einer Partiellen View, die schließlich zu einem String gerendert 
        /// wird. Dieser wird dann am Aufrufpunkt für diese Aktion in die Hauptview eingebettet.
        /// </summary>
        /// <returns></returns>
        public ActionResult GetContactData()
        {

            return PartialView("ContactData", mko.Asp.Mvc.Test.Models.ContactData.Instance);
        }


    }
}
