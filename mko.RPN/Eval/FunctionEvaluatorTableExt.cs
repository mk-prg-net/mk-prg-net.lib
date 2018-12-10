using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN
{
    /// <summary>
    /// mko, 11.3.2018
    /// Erweiter Schnittstelle IFunctionEvaluatorTable nachträglich um nützliche Funktionen
    /// </summary>
    public static class FunctionEvaluatorTableExt
    {
        /// <summary>
        /// mko, 11.3.2018
        /// </summary>
        public static string[] FunctionNames(this IFunctionEvaluatorTable ft)
        {
            return ft.FuncEvaluators.Keys.ToArray();                           
        }

    }
}
