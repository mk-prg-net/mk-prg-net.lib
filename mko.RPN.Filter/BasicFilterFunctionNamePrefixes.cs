//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 11.4.2017
//
//  Projekt.......: mko.RPN.Filter
//  Name..........: BasicFilterFunctionNamePrefixes.cs
//  Aufgabe/Fkt...: Standard- Implementierung von Namenspräfixen für RPN- Filterfunktionen.
//                  Für die Implementierung von RPN- Sprachen zur Definition
//                  von Filter auf Mengen wird ein Namensschema vorgegeben, 
//                  welches die Klassifizierung von Filtern aus Mengen aus 
//                  mko.BI.Repositories wiederspiegelt.
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FltClass = mko.BI.Repositories.FilterClassification;

namespace mko.RPN.Filter
{
    /// <summary>
    /// Standard- Implementierung von Namenspräfixen für RPN- Filterfunktionen.
    /// Für die Implementierung von RPN- Sprachen zur Definition
    /// von Filter auf Mengen wird ein Namensschema vorgegeben, 
    /// welches die Klassifizierung von Filtern aus Mengen aus 
    /// mko.BI.Repositories wiederspiegelt.
    /// </summary>
    public class BasicFilterFunctionNamePrefixes : IFilterFunctionNamePrefixes
    {
        public const string Prefix = ".";

        public string keyFltPrefix
        {
            get
            {
                return Prefix + FltClass.key.ToString();
            }
        }

        public string likeFltPrefix
        {
            get
            {
                return Prefix + FltClass.like.ToString();
            }
        }

        public string predFltPrefix
        {
            get
            {
                return Prefix + "pred";
            }
        }

        public string rngFltPrefix
        {
            get
            {
                return Prefix + FltClass.rng.ToString();
            }
        }

        public string setFltPrefix
        {
            get
            {
                return Prefix + FltClass.set.ToString();
            }
        }

        public string sortFltPrefix
        {
            get
            {
                return Prefix + FltClass.sort.ToString();
            }
        }
    }
}
