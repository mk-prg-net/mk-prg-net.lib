//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 12.4.2017
//
//  Projekt.......: KeplerBI.MVC
//  Name..........: RpnRngFlt.c
//  Aufgabe/Fkt...: Modell für Views von BEreichsfiltern
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
    public class RpnRngFlt : Rpn
    {
        public RpnRngFlt(string ControllerName, IFunctionNames fn, IToken[] tokens, IToken[] fSubtree, params string[] ParameterNames) 
            : base(ControllerName, fn, tokens, fSubtree, ParameterNames)
        {
            mko.TraceHlp.ThrowArgExIfNot(ParameterCount == 2, Properties.Resources.RpnRngFlt_ParameterCount_2_expected);

            var Key = ParameterSubTrees[0].Key;
            BeginName = Key;
            Begin = ParameterSubTrees[0].Value.ToPNString();

            Key = ParameterSubTrees[1].Key;
            EndName = Key;
            End = ParameterSubTrees[1].Value.ToPNString();

        }

        public string BeginName { get; set; }
        public string Begin { get; set; }

        public string EndName { get; set; }
        public string End { get; set; }
    }
}