using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Models = mko.Asp.Mvc.Test.Models;

namespace mko.Asp.Mvc.Test.Controllers
{
    public class Stat2Controller : Controller
    {
        //const string ViewName = "InteractiveStatistic";
        const string ViewName = "Index";
        const string IdServerStatDataModel = "IdServerStatDataModel";
        public Models.Stat2Data ServerStatDataModel
        {
            get
            {
                var model = (Models.Stat2Data)Session[IdServerStatDataModel];
                if (model == null)
                {
                    model = new Models.Stat2Data();
                    Session[IdServerStatDataModel] = model;

#if(DEBUG)
                    model.Data = new double[] { 10, 20, 30, 40, 50 };

#endif
                }
                return model;
            }

            set
            {
                var model = (Models.Stat2Data)Session[IdServerStatDataModel];
                model.Data = value.Data;
            }
        }


        // Asynchroner Abruf von ausgwerteten, aktuellen Statistikdaten durch den Client 
        //public JsonResult GetMinMeanMax(string txt)
        //public JsonResult GetMinMeanMax(pccKursMVC.Models.Stat2Data model)
        [HttpGet]
        public JsonResult GetMinMeanMax(string Data)
        {

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Stat2Data>(Data);

            //pccKursMVC.Models.Stat2Data model = new Models.Stat2Data();
            //model.Data = Data;
            ValidateModel(model);
            if (ModelState.IsValid && model.Data.Any())
            {

                var statAnalysis = new Models.StatModelMinMeanMax();
                statAnalysis.Min = model.Data.Min();
                statAnalysis.Mean = model.Data.Average();
                statAnalysis.Max = model.Data.Max();

                return Json(statAnalysis, JsonRequestBehavior.AllowGet);
            }
            return Json(new Models.StatModelMinMeanMax(), JsonRequestBehavior.AllowGet);

        }


        //
        // GET: /Stat/
        //[AllowAnonymous]
        public ActionResult Index()
        {

            return View(ViewName, ServerStatDataModel);
        }

        // Die Aktualisierung erforder angemeldete Benutzer        
        public ActionResult Update(FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Debug.WriteLine("Aktueller Stand von Data: ");
                var Values = collection["Data"];
                foreach (var val in Values.Split(','))
                {
                    Debug.WriteLine(val);
                }

                if (ModelState.IsValid)
                {
                    TryUpdateModel(ServerStatDataModel);
                }
            }
            catch
            {
            }

            return View(ViewName, ServerStatDataModel);
        }

        // Update mit Model als PArameter
        // Die Aktualisierung erforder angemeldete Benutzer

        [HttpGet]
        public ActionResult Update2(Models.Stat2Data model)
        {
            try
            {
                // TODO: Add update logic here
                Debug.WriteLine("Aktueller Stand von Data: ");

                foreach (var val in model.Data)
                {
                    Debug.WriteLine(val);
                }

                if (ModelState.IsValid)
                {
                    ServerStatDataModel = model;
                }
            }
            catch
            {

            }

            return View(ViewName, model);
        }

    }
}
