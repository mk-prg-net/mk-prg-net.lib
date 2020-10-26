using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN
{
    /// <summary>
    /// mko, 6.12.2018
    /// Methoden zur Kodierung und Dekodierung von Strings in/aus Formate,
    /// die sicher über URL's übertragen werden können.
    /// </summary>
    public static class UrlSaveStringEncoder
    {
        /// <summary>
        /// mko, 29.11.2018
        /// Werden im Pfad Parameterwerte kodiert, die \ oder . enthalten, dann 
        /// schlägt das Routing auf der Webservice- Seite fehl. 
        /// Die kritischen Zeichen werden hier durch Codes ersetzt.
        /// </summary>
        /// <param name="inStr"></param>
        /// <param name="condition">Wenn true, dann wird Dekodierung ausgeführt. Sonst wird originaler String zurückgegeben</param>
        /// <returns></returns>
        public static string RPNUrlSaveStringEncodeIf(this string inStr, bool condition)
        {
            return condition ? inStr
                        .Replace(" ", "~0~")
                        .Replace(@"\", "~1~")
                        .Replace(".", "~2~")
                        .Replace(",", "~3~")
                        .Replace("#", "~4~")
                        .Replace("$", "~5~")
                        .Replace("<", "~6~")
                        .Replace(">", "~7~") : inStr;
        }

        /// <summary>
        /// mko, 29.11.2018
        /// Macht die Kodierungen aus Encode rückgängig. Kann z.B. auf Parameter in der 
        /// Serverseite angewendet werden.
        /// </summary>
        /// <param name="inStr"></param>
        /// <param name="condition">Wenn true, dann wird Dekodierung ausgeführt. Sonst wird originaler String zurückgegeben</param>
        /// <returns></returns>
        public static string RPNUrlSaveStringDecodeIf(this string inStr, bool condition)
        {
            return condition ? inStr
                        .Replace("~0~", " ")
                        .Replace("~1~", @"\")
                        .Replace("~2~", ".")
                        .Replace("~3~", ",")
                        .Replace("~4~", "#")
                        .Replace("~5~", "$")
                        .Replace("~6~", "<")
                        .Replace("~7~", ">") : inStr;

        }


    }
}
