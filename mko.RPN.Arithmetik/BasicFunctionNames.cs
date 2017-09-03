//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 6.4.2017
//
//  Projekt.......: mko.RPN.Arithmetik
//  Name..........: BasicFunctionNames.cs
//  Aufgabe/Fkt...: Implementierung eines Namensschemas für artihmetische Funktionen
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
    public class BasicFunctionNames : IFunctionNames
    {
        public BasicFunctionNames(mko.RPN.IFunctionNames fn)
        {
            this.fn = fn;
        }

        mko.RPN.IFunctionNames fn;

        public string ADD
        {
            get
            {
                return fn.NamePrefix+"add";
            }
        }


        public string DIV
        {
            get
            {
                return fn.NamePrefix + "div";
            }
        }


        public string MUL
        {
            get
            {
                return fn.NamePrefix + "mul";
            }
        }

        public string SUB
        {
            get
            {
                return fn.NamePrefix + "sub";
            }
        }

        public string SUMAT
        {
            get
            {
                return fn.NamePrefix+ "sumat";
            }
        }

    }
}
