using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using mko.Asp.Mvc.Test.Models;

namespace mko.Asp.Mvc.Test.Controllers
{
    public class MathController : Controller
    {

        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            View("Error").ExecuteResult(ControllerContext);
        }

        //
        // GET: /Math/

        public ActionResult Index()
        {
            return View("IndexView");
        }

        public ActionResult BinOp()
        {
            return View("BinOpView", new Models.BinOpModel());
        }


        [HttpPost]        
        public ActionResult BinOp(Models.BinOpModel binOpModel)
        {
            if (ModelState.IsValid)
            {
                switch (binOpModel.Operator)
                {
                    case Models.BinOpModel.Operators.add:
                        binOpModel.Result = binOpModel.A + binOpModel.B;
                        break;
                    case Models.BinOpModel.Operators.sub:
                        binOpModel.Result = binOpModel.A - binOpModel.B;
                        break;
                    case Models.BinOpModel.Operators.mul:
                        binOpModel.Result = binOpModel.A * binOpModel.B;
                        break;
                    //case Models.BinOpModel.Operators.div:
                    //    binOpModel.Result = binOpModel.A / binOpModel.B;
                    //    break;
                    default:
                        throw new InvalidOperationException();
                }

                //// Der Compiler wandelt den folgenden Aufruf in die Form
                //// binOpModel.Result = BinOpModelExtensions.executeOp(binOpModel);
                //// automatisch um.
                //binOpModel.Result = binOpModel.executeOp();

                return View("BinOpView", binOpModel);
            } return View("BinOpView", binOpModel);
        }

    }
}
