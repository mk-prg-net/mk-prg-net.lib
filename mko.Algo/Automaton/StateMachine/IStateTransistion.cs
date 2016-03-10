
//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 12.2014
//
//  Projekt.......: mko.Algo
//  Name..........: IStateTransistion.cs
//  Aufgabe/Fkt...: Vertrag für Zustände, die Zustandsüberführungsfunktion anbieten
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

namespace mko.Algo.Automaton.StateMachine
{
    /// <summary>
    /// Schnittstalle kann von Klassen für Zustandsobjekte implementiert werden, um  eine Zustandsüberführungsfunktion
    /// zu definieren.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    public interface IStateTransistion<TState, TInput>
        where TState : State
    {
        /// <summary>
        /// Zustandsüberführungsfunktion. 
        /// </summary>
        /// <param name="ActiveState">aktueller Zustand</param>
        /// <param name="input">aktuelle Eingabe</param>
        /// <returns>Neuer Zustand</returns>
        State Transition(TState ActiveState, TInput input);
        
    }
}
