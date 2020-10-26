﻿//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.8.2017
//
//  Projekt.......: MkPrgNet.Pattern.Automaton
//  Name..........: IMooreOutputFunctionBuilder.cs
//  Aufgabe/Fkt...: Definition von Ausgabefunktoren für die Zustände eines
//                  Moore- Automaten.
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
//  Version.......: 19.3.2
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 22.3.2019
//  Änderungen....: Neue Schnittstelle der Ausgabefunktoren: Write hat den 
//                  zusätzlichen Parameter prevState.
//                  Name von DefineOutputFunctorFor gekürtzt auf DefOutputFor
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.Automaton
{
    public interface IMooreOutputFunctionBuilder<TStateEnum>
        where TStateEnum : struct
    {
        /// <summary>
        /// Defines the output for a given state (Moore- behavior)
        /// </summary>        
        /// <param name="state">state as enum value</param>
        /// /// <param name="output">Output functor</param>
        void DefOutputFor(TStateEnum state, IOutput<TStateEnum> output);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IAutomaton<TStateEnum> CreateMooreAutomaton();
    }
}
