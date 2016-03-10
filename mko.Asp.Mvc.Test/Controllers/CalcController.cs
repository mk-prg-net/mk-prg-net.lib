using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Calculator.Controllers
{
    public class CalcController : Controller
    {


        const string ProcessorKey = "{18E8BDB2-2DB4-4B18-90BE-9B71F2EC6029}";

        /// <summary>
        /// Liefert die im Sitzungszustand gespeicherte Instanz des Calculators
        /// </summary>
        public CS.Calculator.CalcBase CalcInstance
        {
            get
            {
                if (Session[ProcessorKey] == null)
                {
                    // Es wurde noch kein Eintrag angelegt
                    // Eintrag anlegen
                    Session[ProcessorKey] = new CS.Calculator.CalcDerived_Protocol_as_ListCollection();
                }

                return (CS.Calculator.CalcBase)Session[ProcessorKey];

            }
        }

        //
        // GET: /Calc/

        public ActionResult Index()
        {
            var Model = new Models.Calc.CalcOpModel();

            Model.OpA = 0;
            Model.OpB = 0;
            Model.Protocol = CalcInstance.Protocol;
            Model.Op = CS.Calculator.CalcBase.Operators.Add;
            return View(Model);
        }


        public ActionResult ExeOp(Models.Calc.CalcOpModel Model)
        {

            if (ViewData.ModelState.IsValid)
            {
                try
                {
                    Model.Res = CalcInstance.EXE(Model.Op, Model.OpA, Model.OpB);
                 
                }
                catch (Exception ex)
                {
                    Model.Msg = ex.Message;
                }
            }

            Model.Protocol = CalcInstance.Protocol;
            return View("Index", Model);
        }


    }
}
