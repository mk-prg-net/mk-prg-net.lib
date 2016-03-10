using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mko.Asp.Mvc.Test.Controllers
{
    public class JavaScriptLernenController : Controller
    {
        //
        // GET: /JavaScriptLernen/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JsBasics()
        {
            return View("Basics");
        }

        public ActionResult StartRomToArab()
        {
            return View("RomToArab");
        }

        public ActionResult StartRussland()
        {
            return View("Russland");
        }

    }
}
