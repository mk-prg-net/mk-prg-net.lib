//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 18.10.2015
//
//  Projekt.......: mko.NaLisp.JSon
//  Name..........: parser.base.cs
//  Aufgabe/Fkt...: Parser zum einlesen von NaLisp- Ausdrücken, die als 
//                  JSON dargestellt sind. 
//                  In einem Web- Client können NaLisp- Ausdrücke als JavaScript- Objekte interaktiv 
//                  erstellt werden. Über einbe Web- API kann man diese an den Server senden und mittels
//                  dieses Parsers als NaLisp- Tree aufbauen und evaluieren. So können komplexe Aufgaben auf dem 
//                  Client formuliert werden.
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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base = mko.NaLisp;


namespace mko.NaLisp.JSon
{
    public abstract class FuncParserBase : IFuncParser
    {
        public abstract string FunctionName
        {
            get;
        }

        public abstract bool TryParse(Newtonsoft.Json.JsonTextReader reader, Dictionary<string, IFuncParser> FuncParsers, out Base.Core.INaLisp NaExp);

    }
}
