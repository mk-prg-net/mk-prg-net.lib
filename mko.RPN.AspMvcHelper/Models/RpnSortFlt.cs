//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 12.4.2017
//
//  Projekt.......: KeplerBI.MVC
//  Name..........: RpnRngFlt.c
//  Aufgabe/Fkt...: Modell für Views von Sortier- Ausdrücke
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
    public class RpnSortFlt : Rpn
    {
        public RpnSortFlt(string ControllerName, IFunctionNames fn, IToken[] tokens, IToken[] fSubtree, mko.RPN.Parser Parser, Func<IToken, bool> GetDirection, params string[] ParameterNames)
            : base(ControllerName, fn, tokens, fSubtree, ParameterNames)
        {
            var Key = ParameterSubTrees[0].Key;
            ParamTag =  Key;            

            Parser.Parse(fSubtree);
            mko.TraceHlp.ThrowExIfNot(Parser.Succsessful, string.Format(Properties.Resources.PNParseFailed, fSubtree.ToPNString(), Parser.Stack.ToArray().ToPNString()), Parser.LastException);
            mko.TraceHlp.ThrowArgExIfNot(Parser.Stack.Count == 1, Properties.Resources.PNParseFailed);            

            SortDescending = GetDirection(Parser.Stack.Peek());
        }

        public string ParamTag { get; set; }
        public bool SortDescending { get; set; }
    }
}