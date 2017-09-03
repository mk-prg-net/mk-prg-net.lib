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

namespace MkPrgNet.Pattern.StateMachine
{
    public interface IStateMachine
    {
        /// <summary>
        /// All inputs of the finit state machine
        /// </summary>
        IList<IInput> Inputs
        {
            get;
        }

        /// <summary>
        /// Maps Input with Name to an index.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        int GetInputIx(string Name);


        /// <summary>
        /// Alle states of the finit state machine
        /// </summary>
        IList<IState> States
        {
            get;
        }

        /// <summary>
        /// Maps state with Name to an index.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        int GetStateIx(string Name);

        /// <summary>
        /// Defines the output for a given input*state transition 
        /// </summary>
        /// <param name="IndexOfInput"></param>
        /// <param name="IndexOfState"></param>
        /// <param name="output"></param>
        void DefineOutput(int IndexOfInput, int IndexOfState, IOutput output);

        /// <summary>
        /// Defines the output- function for a given input*state transition 
        /// </summary>
        /// <param name="IndexOfInput"></param>
        /// <param name="IndexOfState"></param>
        /// <param name="outputFunction"></param>
        void DefineOutputFunc(int IndexOfInput, int IndexOfState, Action<IInput> outputFunction);




        /// <summary>
        /// Current active state of the machine
        /// </summary>
        IState CurrentState
        {
            get;
        }


        /// <summary>
        /// Starts the machine. 
        /// If only one start state exists, then this start state will become CurrentState.
        /// If more then on start  state exists, a exception will be thrown. 
        /// </summary>
        void Start();

        /// <summary>
        /// Starts the machine. 
        /// The state with defined index will be set as the current state.        
        /// </summary>
        void Start(int ixStartState);


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
        IEnumerable<IState> PossibleSubsequentStatesOf(int ixState);

        /// <summary>
        /// Query all possible subsequent states of curren state
        /// </summary>
        /// <returns>List of all possible subsequent states</returns>
        IEnumerable<IState> PossibleSubsequentStatesOfCurrent();



    }
}
