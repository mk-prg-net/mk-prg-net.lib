//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 18.5.2017
//
//  Projekt.......: mko.RPN
//  Name..........: LendToken.cs
//  Aufgabe/Fkt...: Token, das das Ende einer variadischen Parameterliste darstellt
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

namespace mko.RPN
{
    public class ListEndToken : FunctionNameToken        
    {
        public ListEndToken(IFunctionNames fn, int CountOfEvaluated = 1) : base(fn.ListEnd, CountOfEvaluated) { }
    }
}
