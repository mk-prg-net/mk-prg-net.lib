//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 9.5.2017
//
//  Projekt.......: mko.RPN.Arithmetik
//  Name..........: FnameEvalMapperFunctor.cs
//  Aufgabe/Fkt...: Orden Funtkionsnamen Evaluatoren zu.
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

namespace mko.RPN.Arithmetik
{
    public class FnameEvalMapperFunctor: IFnameEvalMapper        
    {
        public FnameEvalMapperFunctor(IFunctionNames fn, string ListEndSymbol, IFormatProvider pfmt)
        {
            this.fn = fn;
            this.ListEndSymbol = ListEndSymbol;
            this.pfmt = pfmt;
        }

        IFunctionNames fn;
        string ListEndSymbol;
        IFormatProvider pfmt;

        public void MapFnameToEvalIn(Dictionary<string, IEval> dict)
        {
            dict[fn.ADD] = new Add();
            dict[fn.DIV] = new Div();
            dict[fn.MUL] = new Mul();
            dict[fn.SUB] = new Sub();
            dict[fn.SUMAT] = new SumatEval(ListEndSymbol, pfmt);

        }
    }
}
