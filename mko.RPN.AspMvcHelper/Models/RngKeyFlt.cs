//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 12.4.2017
//
//  Projekt.......: KeplerBI.MVC
//  Name..........: RpnRngFlt.c
//  Aufgabe/Fkt...: Modell für Views von Key- Filtern.
//                  Key Filter wählen aus einer Menge mit großer Mächtigkeit einen einzelnen
//                  Wert aus. Beim einstellen ist die Unterstützung mittels Autocomplete 
//                  sinvoll.
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
using System.Web;

using mko.RPN;

namespace mko.RPN.AspMvcHelper.Models
{
    public class RpnKeyFlt : Rpn
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="tokens"></param>
        /// <param name="fSubtree"></param>
        /// <param name="GetEnumsSvcName">Name des Webdienstes, der alle Werte aufzählt</param>
        /// <param name="ParameterNames"></param>
        public RpnKeyFlt(string ControllerName, IFunctionNames fn, IToken[] tokens, IToken[] fSubtree, string NameOfAutocompleteSvc, params string[] ParameterNames)
            : base(ControllerName, fn, tokens, fSubtree, ParameterNames)
        {
            this.NameOfAutocompleteScv = NameOfAutocompleteScv;
        }

        /// <summary>
        /// Name des Dienstes, der alle Werte einer kleinen endlichen Menge (enum) afzählt.
        /// </summary>
        public string NameOfAutocompleteScv { get; }
    }
}