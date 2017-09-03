//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 3.5.2017
//
//  Projekt.......: mko.RPN
//  Name..........: FunctionNamesBase.cs
//  Aufgabe/Fkt...: Basisimplementierung der IFunctionNames- Schnittstelle
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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 23.5.2017
//  Änderungen....: verschoben aus mko.RPN.Test in mko.RPN und damit verallgemeinert
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
    public class FunctionNamesStrong : mko.RPN.IFunctionNames
    {

        public string constBool
        {
            get
            {
                return ".bool";
            }
        }

        public string constDbl
        {
            get
            {
                return ".dbl";
            }
        }

        public string constInt
        {
            get
            {
                return ".int";
            }
        }

        public string constStr
        {
            get
            {
                return ".str";
            }
        }

        public string DerivedTokenPrefix
        {
            get
            {
                return ".derv";
            }
        }

        public string ListEnd
        {
            get
            {
                return ".Lend";
            }
        }

        public string NamePrefix
        {
            get
            {
                return ".";
            }
        }

        public string ParamNamePrefix
        {
            get
            {
                return "..";
            }
        }

        public bool IsSemanticDescriptor(string FunctionName)
        {
            return FunctionName.StartsWith(ParamNamePrefix);
        }
    }
}