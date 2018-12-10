//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 15.12.2016
//
//  Projekt.......: MkPrgNet.Pattern.StateMachine
//  Name..........: IStateMachine.cs
//  Aufgabe/Fkt...: Allgemeine Schnittstelle von Zustandsautomaten.
//                  
//                  
//                            +-------------------+
//                 Input 1 ---| Automat           |
//                            |-------->\         |
//                            |    +---> STF----+ |     
//                            |    |            | |     
//                 Input 2 ---|  CurrentState <-+ |     
//                            |    |              |     
//                            |    +--->\         |
//                 Input n ---|--------> Output---|---> Ausgabe
//                            +-------------------+
//
//                 Achtung: Die Eingänge können mit Prioritäten verknüpft sein.
//                          Ohne Beschränkung der Allgemeinheit kann wie im Bild
//                          angenommen werden, das Input 1 die höchste und Input n die 
//                          niedrigste Priorität hat. Sind mehrere Eingänge gleichzeitig aktiv,
//                          dann wird der Eingang mit der höchsten Priorität innerhalb einer
//                          Zustandsüberführungsfunktion verarbeitet.
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

namespace MkPrgNet.Pattern.Automaton
{
    public interface IAutomaton<TStateEnum>
        where TStateEnum : struct
    {
        /// <summary>
        /// All inputs of the finit state machine
        /// </summary>
        IEnumerable<IInput> Inputs
        {
            get;
        }


        /// <summary>
        /// Current active state of the machine
        /// </summary>
        TStateEnum CurrentState
        {
            get;
        }

        /// <summary>
        /// State property getter
        /// </summary>
        States.IStateDecorator<TStateEnum> StateProperties { get; }


        /// <summary>
        /// Starts the machine. 
        /// If only one start state exists, then this start state will become CurrentState.
        /// If more then on start  state exists, a exception will be thrown. 
        /// </summary>
        void Start();


        /// <summary>
        /// State transistion function. Fires the associated output function. Ends with input reset.
        /// </summary>
        /// <param name="ActiveState"></param>
        /// <returns></returns>
        void Transistion();

        /// <summary>
        /// Query all possible subsequent states of state with index.
        /// </summary>
        /// <param name="StateName">Index of state</param>
        /// <returns>List of all possible subsequent states</returns>
        IEnumerable<TStateEnum> PossibleSubsequentStatesOf(TStateEnum State);

        /// <summary>
        /// Query all possible subsequent states of curren state
        /// </summary>
        /// <returns>List of all possible subsequent states</returns>
        IEnumerable<TStateEnum> PossibleSubsequentStatesOfCurrent();


    }
}
