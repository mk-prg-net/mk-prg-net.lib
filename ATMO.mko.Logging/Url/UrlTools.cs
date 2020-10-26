using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.Url
{
    /// <summary>
    /// mko, 29.11.2018
    /// Hilfsfunktionen für spezielle Url- Manipulationen 
    /// </summary>
    public class UrlTools
    {
        public class WebApi
        {
            /// <summary>
            /// mko, 29.11.2018
            /// Werden im Pfad Parameterwerte kodiert, die \ oder . enthalten, dann 
            /// schlägt das Routing auf der Webservice- Seite fehl. 
            /// Die kritischen Zeichen werden hier durch Codes ersetzt
            /// </summary>
            /// <param name="pathFragment"></param>
            /// <returns></returns>
            public static string RoutingSaveEncodePath(string pathFragment)
            {
                return pathFragment.Replace(" ", "~0~").Replace(@"\", "~1~").Replace(".", "~2~").Replace(",", "~3~");
            }

            /// <summary>
            /// mko, 29.11.2018
            /// Macht die Kodierungen aus RoutingSaveEncodePath rückgängig. Kann z.B. auf Parameter in der 
            /// Serverseite angewendet werden.
            /// </summary>
            /// <param name="pathFragment"></param>
            /// <returns></returns>
            public static string RoutingSaveDecodePath(string pathFragment)
            {
                return pathFragment.Replace("~0~", " ").Replace("~1~", @"\").Replace("~2~", ".").Replace(",", "~3~"); ;
            }

        }
        


    }
}
