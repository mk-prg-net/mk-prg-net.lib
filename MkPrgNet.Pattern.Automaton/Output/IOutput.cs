//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.12.2016
//
//  Projekt.......: MkPrgNet.PAttern.StateMachine
//  Name..........: IOutput.cs
//  Aufgabe/Fkt...: Abstrahiert die die Berechnung einer Ausgabe zu einer
//                  Eingabe in einem bestimmten Zustand des Zustandsautomaten.
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

namespace MkPrgNet.Pattern.Automaton
{
    /// <summary>
    /// Output funktor. 
    /// </summary>
    public interface IOutput
    {
        /// <summary>
        /// Computes the output
        /// </summary>
        /// <param name="input"></param>
        void Write(IInput input);
    }
}
