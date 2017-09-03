//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.12.2016
//
//  Projekt.......: MkPrgNet.Pattern.StateMachine
//  Name..........: StateMachine.cs
//  Aufgabe/Fkt...: Implementierung des endlichen Zustandsautomaten
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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.StateMachine
{
    public class StateMachine : IStateMachine
    {

        public class StateMachineException : Exception
        {
            public enum EnumReason
            {
                generalFailure,
                keyNotFound,
                moreThanOneInputOn,
                stateNotExists,
                inputNotExists,
                tryAddingStateThatAlreadyExists,
                tryAddingInputThatAlreadyExists,
                countSubsequentStatesMustBeEqualToCountOfInputs,
                countSubsequentStatesMustBeEqualToCountOfStates,
                moreThanOneStartStateExists
            }

            public StateMachineException(string MethodName, EnumReason reason)
                : base(reason.ToString())
            {
                _MethodName = MethodName;
                _Reason = reason;
            }

            public string MethodName
            {
                get
                {
                    return _MethodName;
                }
            }
            string _MethodName;

            public EnumReason Reason
            {
                get
                {
                    return _Reason;
                }
            }
            EnumReason _Reason;
        }


        public StateMachine()
        {
            //_Inputs.Add(NullInput.Instance.Value);
        }

        /// <summary>
        /// All inputs of the finit state machine
        /// </summary>
        public IList<IInput> Inputs
        {
            get
            {
                return _Inputs;
            }
        }
        protected List<IInput> _Inputs = new List<IInput>();
        protected Dictionary<string, int> _InputIndex = new Dictionary<string, int>();

        /// <summary>
        /// Maps input name to index.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public int GetInputIx(string Name)
        {
            var input = _Inputs.FirstOrDefault(r => r.Name == Name);
            if (input != null)
            {
                return _Inputs.IndexOf(input);
            }
            else
            {

                throw new StateMachineException("GetInputIx", StateMachineException.EnumReason.inputNotExists);
            }
        }



        /// <summary>
        /// All states of the finit state machine
        /// </summary>
        public IList<IState> States
        {
            get
            {
                return _States;
            }
        }
        protected List<IState> _States = new List<IState>();
        protected Dictionary<string, int> _StateIndex = new Dictionary<string, int>();

        /// <summary>
        /// Maps state with Name to index.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public int GetStateIx(string Name)
        {
            var state = _States.FirstOrDefault(r => r.Name == Name);
            if (state != null)
            {
                return _States.IndexOf(state);
            }
            else
            {

                throw new StateMachineException("GetStateIx", StateMachineException.EnumReason.stateNotExists);
            }
        }


        /// <summary>
        /// Abbildung des Zustandsautomatennetzes 
        /// </summary>
        protected Dictionary<Tuple<IInput, IState>, Tuple<IState, IOutput>> Transistions = new Dictionary<Tuple<IInput, IState>, Tuple<IState, IOutput>>();

        protected Tuple<IInput, IState> GetKey(IInput input, IState state)
        {
            try
            {
                return Transistions.Keys.First(r => r.Item1 == input && r.Item2 == state);
            }                
            catch (Exception)
            {
                throw new StateMachineException("GetKey", StateMachineException.EnumReason.keyNotFound);
            }

        } 

        /// <summary>
        /// Current state of state machine
        /// </summary>
        public IState CurrentState
        {
            get
            {
                return _CurrentState;
            }
        }
        protected IState _CurrentState;

        /// <summary>
        /// State transistion function. Fires the associated output function. Ends with input reset.
        /// </summary>
        /// <param name="ActiveState"></param>
        /// <returns></returns>
        public virtual void Transistion()
        {
            // Sicherstellen, daß nur eine Eingabe vollständig ist 
            int aktiveInputCount = Inputs.Count(r => r.On);

            if (aktiveInputCount > 0)
            {
                if (aktiveInputCount > 1)
                {
                    throw new StateMachineException("Transistion", StateMachineException.EnumReason.moreThanOneInputOn);
                }
                else
                {
                    var input = Inputs.First(r => r.On);
                    var key = GetKey(input, CurrentState);
                    var val = Transistions[key];
                    _CurrentState = val.Item1;
                    val.Item2.Write(input);

                    // Eingang wieder zurücksetzen
                    input.Reset();

                }
            }
        }

        /// <summary>
        /// Query all possible subsequent states of state with name StateName
        /// </summary>
        /// <param name="StateName">Name od State</param>
        /// <returns>List of all possible subsequent states</returns>
        public IEnumerable<IState> PossibleSubsequentStatesOf(int ixState)
        {
            var State = _States[ixState];

            // Zu allen möglichen Eingaben die Folgezustände berechnen
            return GetPossibleSubsequentStates(State);
        }
        

        /// <summary>
        /// Query all possible subsequent states of curren state
        /// </summary>
        /// <returns>List of all possible subsequent states</returns>
        public IEnumerable<IState> PossibleSubsequentStatesOfCurrent()
        {
            return GetPossibleSubsequentStates(CurrentState);
        }

        protected IEnumerable<IState> GetPossibleSubsequentStates(IState State)
        {
            var SubsequentStates = new List<IState>();
            foreach (var input in Inputs)
            {
                var key = GetKey(input, State);
                SubsequentStates.Add(Transistions[key].Item1);
            }

            return SubsequentStates;
        }


        /// <summary>
        /// Adds a new state to the state machine. 
        /// The second parameter defines all subsequent states for currently all possible inputs.
        /// Define the subsequent state by it's index.
        /// </summary>
        /// <param name="NewState">New state, that enhances the state machine</param>
        /// <param name="SubsequentStateIndexList">List of indizes of (Input 1 * NewState), (Input 2 * NewState), ..., (Input n * NewState) </param>
        public StateConfigurableDeco AddState(IState NewState, params int[] SubsequentStateIndexList)
        {

            if (_StateIndex.ContainsKey(NewState.Name))
            {
                throw new StateMachineException("AddState", StateMachineException.EnumReason.tryAddingStateThatAlreadyExists);
            }

            if (SubsequentStateIndexList.Length != _Inputs.Count)
            {
                throw new StateMachineException("AddState", StateMachineException.EnumReason.countSubsequentStatesMustBeEqualToCountOfInputs);
            }

            // Zustand im Verzeichnis aufnehmen
            _States.Add(NewState);
            _StateIndex[NewState.Name] = _States.Count() - 1;


            // Zustandsübergangsfunktionen implementieren
            for (int i = 0; i < _Inputs.Count(); i++)
            {
                Transistions[Tuple.Create(_Inputs[i], NewState)] = Tuple.Create(_States[SubsequentStateIndexList[i]], (IOutput)NullOutput.Instance.Value);
            }

            //return _States.Count() - 1;
            return new StateConfigurableDeco(_States.Count() - 1, Transistions, States);
        }


        /// <summary>
        /// Class factory for machine states
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <returns></returns>
        public StateConfigurableDeco CreateState<TState>()  where TState: IState, new()
        {
            return AddState(new TState());
        }

        /// <summary>
        /// Defines the output functor for a given input*state transition 
        /// </summary>
        /// <param name="IndexOfInput"></param>
        /// <param name="IndexOfState"></param>
        /// <param name="output"></param>
        public void DefineOutput(int IndexOfInput, int IndexOfState, IOutput output)
        {
            Debug.Assert(IndexOfInput >= 0 && IndexOfInput < _Inputs.Count());
            Debug.Assert(IndexOfState >= 0 && IndexOfState < _States.Count());

            var key = GetKey(_Inputs[IndexOfInput], _States[IndexOfState]);
            Transistions[key] = Tuple.Create(Transistions[key].Item1, output);
        }

        /// <summary>
        /// Defines the output- function for a given input*state transition 
        /// </summary>
        /// <param name="IndexOfInput"></param>
        /// <param name="IndexOfState"></param>
        /// <param name="outputFunction"></param>
        public void DefineOutputFunc(int IndexOfInput, int IndexOfState, Action<IInput> outputFunction)
        {
            Debug.Assert(IndexOfInput >= 0 && IndexOfInput < _Inputs.Count());
            Debug.Assert(IndexOfState >= 0 && IndexOfState < _States.Count());

            var key = GetKey(_Inputs[IndexOfInput], _States[IndexOfState]);
            Transistions[key] = Tuple.Create(Transistions[key].Item1, (IOutput)new Outputs.OutputFunctionWrapper(outputFunction));
        }


        /// <summary>
        /// Adds a new Input to the state machine. 
        /// The second parameter defines all subsequent states for currently all possible states.
        /// Define the subsequent state by it's index.
        /// </summary>
        /// <param name="NewState">New state, that enhances the state machine</param>
        /// <param name="SubsequentStateIndexList">List of indizes of (Input 1 * NewState), (Input 2 * NewState), ..., (Input n * NewState) </param>
        public int AddInput(IInput NewInput, params int[] SubsequentStateIndexList)
        {
            if (_InputIndex.ContainsKey(NewInput.Name))
            {
                throw new StateMachineException("AddInput", StateMachineException.EnumReason.tryAddingInputThatAlreadyExists);
            }

            if (SubsequentStateIndexList.Length != _States.Count)
            {
                throw new StateMachineException("AddInput", StateMachineException.EnumReason.countSubsequentStatesMustBeEqualToCountOfInputs);
            }

            // Eingang im Verzeichnis aufnehmen
            _Inputs.Add(NewInput);
            _InputIndex[NewInput.Name] = _Inputs.Count() - 1;

            // Zustandsübergangsfunktionen implementieren
            for (int i = 0; i < _States.Count(); i++)
            {
                Transistions[Tuple.Create(NewInput, _States[i])] = Tuple.Create(_States[SubsequentStateIndexList[i]], (IOutput)NullOutput.Instance.Value);
            }

            return _Inputs.Count() - 1;
        }

        public int AddInput(IInput NewInput, StateConfigurableDeco DefaultTransitionToState, params Func<IInput, int>[] StateTransitionFactories)
        {
            if (_InputIndex.ContainsKey(NewInput.Name))
            {
                throw new StateMachineException("AddInput", StateMachineException.EnumReason.tryAddingInputThatAlreadyExists);
            }            

            // Eingang im Verzeichnis aufnehmen
            _Inputs.Add(NewInput);
            _InputIndex[NewInput.Name] = _Inputs.Count() - 1;

            var ixOfStateWithDefinedTransition = new List<int>(StateTransitionFactories.Length);

            // Alle speziellen Zustandsüberführungsfunktionen definieren
            foreach(var stfactory in StateTransitionFactories){
                ixOfStateWithDefinedTransition.Add(stfactory(NewInput));
            }

            // Rest mit der Default- Zustandsüberführung initialisieren
            var defaultTransist = Tuple.Create(_States[DefaultTransitionToState.IX], (IOutput)NullOutput.Instance.Value);
            for (int i = 0; i < _States.Count(); i++)
            {
                if (!ixOfStateWithDefinedTransition.Any(r => r == i))
                {
                    Transistions[Tuple.Create(NewInput, _States[i])] = defaultTransist;
                }
                
            }

            return _Inputs.Count() - 1;
        }


        public int CreateInput<TInput>(StateConfigurableDeco DefaultTransitionToState, params Func<IInput, int>[] StateTransitionFactories)
            where TInput : IInput, new()
        {
            return AddInput(new TInput(), DefaultTransitionToState, StateTransitionFactories);
        }



        /// <summary>
        /// Starts the machine. 
        /// If only one start state exists, then this start state will become CurrentState.
        /// If more then on start  state exists, a exception will be thrown. 
        /// </summary>
        public void Start()
        {
            if (_States.Count(r => r.IsStart) != 1)
            {
                throw new StateMachineException("Start", StateMachineException.EnumReason.moreThanOneStartStateExists);
            }
            else
            {
                _CurrentState = _States.First(r => r.IsStart);
            }
        }

        /// <summary>
        /// Starts the machine. 
        /// The state with defined index will be set as the current state.        
        /// </summary>
        public void Start(int ixStartState)
        {
            if (ixStartState >= 0 && ixStartState < _States.Count && _States[ixStartState].IsStart)
            {
                _CurrentState = _States[ixStartState];
            }
            else
            {
                throw new StateMachineException("Start", StateMachineException.EnumReason.stateNotExists);
            }
        }
    }
}
