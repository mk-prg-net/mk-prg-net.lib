﻿//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.4.2014
//
//  Projekt.......: mko.Algo
//  Name..........: ITerminal.cs
//  Aufgabe/Fkt...: Schnittstelle von Ausdrücken, die nicht weiter aufgelöst werden können.
//                  Entspricht konstanten Funktionen (Parameterlos, liefern einen konstanten Wert)
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

namespace mko.NaLisp.Core
{
    public interface ITerminal
    {
        Inspector.ProtocolEntry Validate(NaLispStack Stack);

        INaLisp Eval(NaLispStack StackInstance, bool DebugOn);

    }
}
