//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.5.2017
//
//  Projekt.......: mko.RPN.DateTime
//  Name..........: FnameEvalMapperFunctor.cs
//  Aufgabe/Fkt...: Ordnet in  einem Dictionary Funktionsnamen Evaluatoren von DateTime- Funktionen zu.
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
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN.DateTime
{
    public class FnameEvalMapperFunctor : IFnameEvalMapper
    {
        public FnameEvalMapperFunctor(IFunctionNamesDateTime fn)
        {
            this.fn = fn;
        }

        IFunctionNamesDateTime fn;

        public void MapFnameToEvalIn(Dictionary<string, IEval> dict)
        {
            dict[fn.Day] = new Eval.DayEval(fn);
            dict[fn.Month] = new Eval.MonthEval(fn);
            dict[fn.Year] = new Eval.YearEval(fn);
            dict[fn.DateTime] = new Eval.DateEval(fn);
        }
    }
}
