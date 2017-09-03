using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.StateMachine.StateMachines
{
    /// <summary>
    /// Zweiter Schritt beim Anlegen eines Zustandsautomaten:
    /// Definieren der Zustandsüberführungsfunktionen
    /// </summary>
    public interface IStateTransitionBuilder
    {
        /// <summary>
        /// Fügt einen neue Eingabe, und definiert die Zustandsübergänge, die für 
        /// jeden möglichen Zustand des Automaten stattfinden können.
        /// </summary>
        /// <typeparam name="TInput">Klasse des Eingabefunktors</typeparam>
        /// <param name="subsequentStates">Liste </param>
        void AddNewTransitionsForInput<TInput>(params IState[] subsequentStates) where TInput : IInput, new();

        /// <summary>
        /// Erzeugt bezüglich der Vorausgeganenen Definitionen für Zustände und 
        /// Zustandsüberführungsfunktionen einen neuen Automaten
        /// </summary>
        /// <returns></returns>
        IStateMachine CreateStateMachine();
    }
}
