using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Calculator.Models.Calc
{
    public class CalcOpModel : CS.Calculator.CalcBase.ProtocolEntry
    {        

        public string Msg { get; set; }

        
        public CS.Calculator.CalcBase.ProtocolEntry[] Protocol { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> OpList
        {
            get
            {
                return new System.Web.Mvc.SelectListItem[] {
                    new System.Web.Mvc.SelectListItem() { Value = CS.Calculator.CalcBase.Operators.Add.ToString(), Text="+: ADD"},
                    new System.Web.Mvc.SelectListItem() { Value= CS.Calculator.CalcBase.Operators.Subtract.ToString(), Text="-: Subtract"},
                    new System.Web.Mvc.SelectListItem() { Value= CS.Calculator.CalcBase.Operators.Mul.ToString(), Text="*: Mul"},
                    new System.Web.Mvc.SelectListItem() { Value= CS.Calculator.CalcBase.Operators.Div.ToString(), Text="/: Div"}
                };
            }
        }
    }
}