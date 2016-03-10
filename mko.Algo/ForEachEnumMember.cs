//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 2007
//
//  Projekt.......: mko.Algo
//  Name..........: ForEachEnumMember
//  Aufgabe/Fkt...: Zählt alle Member eines Enums auf und führt auf diesen eine Action aus
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
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
//  Datum.........: 13.12.2014
//  Änderungen....: Get Funktion hinzugefügt.
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace mko.Algo
{
    public delegate void DGOp(string EnumName, int EnumValue);
    public class ForEachEnumMember<TEnum>
    {        
        public static void execute(Action<string, int> op)
        {
            foreach (int member in System.Enum.GetValues(typeof(TEnum)))
            {
                string EnumName = System.Enum.GetName(typeof(TEnum), member);
                op(EnumName, member);
            }
        }

        public static TEnum[] Get()
        {
            return System.Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();
        }
    }
}
