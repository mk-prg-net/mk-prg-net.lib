//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 20.5.2017
//
//  Projekt.......: mko.RPN
//  Name..........: FNameEvalMapper.cs
//  Aufgabe/Fkt...: BAsisimplementierung. Ordnet den Funktionsnamen
//                  aus mko.RPN.IFunctionNames die Basisimplementireung von 
//                  Evaluatoren zu
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
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN
{
    public class FnameEvalMapper : IFnameEvalMapper
    {
        public FnameEvalMapper(IFunctionNames fn)
        {
            this.fn = fn;
            this.pfmt = new System.Globalization.CultureInfo("en-US");
        }


        public FnameEvalMapper(IFunctionNames fn, IFormatProvider pfmt)
        {
            this.fn = fn;
            this.pfmt = pfmt;
        }

        IFunctionNames fn;
        IFormatProvider pfmt;

        public void MapFnameToEvalIn(Dictionary<string, IEval> dict)
        {
            dict[fn.ListEnd] = new ListEndEval(fn);
            dict[fn.constDbl] = new DoubleEval(pfmt);
            dict[fn.constInt] = new IntEval();
            dict[fn.constBool] = new BoolEval();

        }
    }
}
