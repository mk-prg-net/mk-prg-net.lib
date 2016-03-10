//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 26.5.2014
//
//  Projekt.......: mko.Algo
//  Name..........: mko.Algo.Automaton.FiniteStateMachine.cs
//  Aufgabe/Fkt...: Basisklasse für die Implementierung von endlichen Zustandsautomaten
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2012
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

namespace mko.Algo.Automaton
{
    public abstract class FiniteStateMachine<TName, TState, TInput>
        where TState : StateBase<TName>
    {
        /// <summary>
        /// Zugriff auf den Zustandsgraph als Dictionary
        /// </summary>
        public Dictionary<TName, TState> States = new Dictionary<TName, TState>();

        public FiniteStateMachine()
        {
            CreateStates();
        }

        /// <summary>
        /// Fügt dem Automaten einen Neuen Zustand hinzu
        /// </summary>
        /// <param name="NewState"></param>
        protected void AddState(TState NewState)
        {
            States[NewState.Name] = NewState;
        }

        /// <summary>
        /// Erzeugt den Zustandsgraphen
        /// </summary>
        /// <returns></returns>
        protected abstract void CreateStates();

        /// <summary>
        /// Auflisten aller Startzustände
        /// </summary>
        public IEnumerable<StateBase<TName>> GetInitialStates
        {
            get
            {
                return States.Where(p => p.Value.KindOfState == StateBase<TName>.KindsOfStates.initial).Select(p => p.Value);
            }
        }

        public IEnumerable<StateBase<TName>> GetFinalStates
        {
            get
            {
                return States.Where(p => p.Value.KindOfState == StateBase<TName>.KindsOfStates.final).Select(p => p.Value);
            }
        }


        /// <summary>
        /// Alle mit dem Zustand verbundenen Aktionen starten
        /// </summary>
        /// <param name="State"></param>
        public abstract void InitateActionsWhenTransitioningTo(StateBase<TName> State);


        /// <summary>
        /// Liefert Folgezustand, in den der Automat bei gegebenen aktiven Zustand und nach Eingbabe wechselt
        /// </summary>
        /// <param name="State"></param>
        /// <param name="Input"></param>
        /// <returns></returns>
        public abstract StateBase<TName> StateTransistionFunction(StateBase<TName> ActiveState, TInput Input);


    }
}
