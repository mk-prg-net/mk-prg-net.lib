using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Text
{
    public class Utils
    {
        /// <summary>
        /// Prüft, ob txt einem GUID entspricht 
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static bool IsGUID(string txt)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(txt, @"\{[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9|a-f|A-F]{12}\}");

        }
    }
}
