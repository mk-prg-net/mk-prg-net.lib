//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 12.4.2017
//
//  Projekt.......: KeplerBI.MVC
//  Name..........: RpnRngFlt.c
//  Aufgabe/Fkt...: Modell für Views von Like- Filtern
//                  
//
//
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
    public class RpnLikeFlt : Rpn
    {
        public RpnLikeFlt(string ControllerName, IFunctionNames fn, IToken[] tokens, IToken[] fSubtree, params string[] ParameterNames)
            : base(ControllerName, fn, tokens, fSubtree, ParameterNames)
        { }
    }
}