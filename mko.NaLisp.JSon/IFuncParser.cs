using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Base = mko.NaLisp;


namespace mko.NaLisp.JSon
{
    public interface IFuncParser
    {
        /// <summary>
        /// Name der Funktion
        /// </summary>
        string FunctionName { 
            get; 
        }


        /// <summary>
        /// List eine JSon Darstellung einer Funktion ein und erzeugt aus dieser Funktionsdefinition einen NaLisp- Term
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="NaExp"></param>
        /// <returns></returns>
        bool TryParse(Newtonsoft.Json.JsonTextReader reader, Dictionary<string, IFuncParser> FuncParsers, out Base.Core.INaLisp NaExp);
    }
}
