using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.StateMachine.StateMachines
{
    /// <summary>
    /// Erster Schritt beim Aufbau eines neuen Zustandsautomaten:
    /// Anlegen der Zusände
    /// </summary>
    public interface IStateBuilder
    {
        void AddNewState<TState>() where TState: IState, new();

        /// <summary>
        /// Nachdem alle Zusände angelegt wurden, kann mit dieser 
        /// Funktion ein Builder für die Zustandsüberführungsfunktionen 
        /// erzeugt werden
        /// </summary>
        /// <returns></returns>
        IStateTransitionBuilder CreateStateTransitionBuilder();
    }
}
