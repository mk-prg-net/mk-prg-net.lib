using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mko.Asp.Mvc.Test.Models
{
    /// <summary>
    /// Beispiele für Erweiterungsmethoden (neu ab .NET 3.5)
    /// </summary>
    public static class BinOpModelExtension
    {
        /// <summary>
        /// Der erste 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static double executeOp(this BinOpModel model)
        {   
            switch (model.Operator)
            {
                case Models.BinOpModel.Operators.add:
                    model.Result = model.A + model.B;
                    break;
                case Models.BinOpModel.Operators.sub:
                    model.Result = model.A - model.B;
                    break;
                case Models.BinOpModel.Operators.mul:
                    model.Result = model.A * model.B;
                    break;
                case Models.BinOpModel.Operators.div:
                    model.Result = model.A / model.B;
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return model.Result;
        }

    }
}