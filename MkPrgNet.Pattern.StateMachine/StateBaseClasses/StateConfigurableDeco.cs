//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 16.12.2016
//
//  Projekt.......: Projektkontext
//  Name..........: Dateiname
//  Aufgabe/Fkt...: Kurzbeschreibung
//                  
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

namespace MkPrgNet.Pattern.StateMachine
{
    /// <summary>
    /// Decorates a state with configuration- functions
    /// </summary>
    public class StateConfigurableDeco : IState
    {
        /// <summary>
        /// Index of state in the machine state collection
        /// </summary>
        public int IX
        {
            get
            {
                return _IX;
            }
        }
        int _IX;

        IState _State;
        Dictionary<Tuple<IInput, IState>, Tuple<IState, IOutput>> _Transistions;
        IList<IState> _States;

        /// <summary>
        /// Funktional, das eine Funktion liefert, welche diesen Zustand für eine gegebene Eingabe in den Zustand mit dem Index ixToState
        /// überführt.
        /// </summary>
        /// <param name="ixToState"></param>
        /// <returns></returns>
        public Func<IInput, int> To(StateConfigurableDeco ToState)
        {
            return (input) => { _Transistions[Tuple.Create(input, _States[IX])] = Tuple.Create(_States[ToState.IX], (IOutput)NullOutput.Instance.Value); return IX; };
        }

        internal StateConfigurableDeco(int ix, Dictionary<Tuple<IInput, IState>, Tuple<IState, IOutput>> Transistions, IList<IState> States)
        {
            _IX = ix;
            _State = States[ix];
            _Transistions = Transistions;
            _States = States;
        }

        /// <summary>
        /// Name of state
        /// </summary>
        public string Name
        {
            get { return _State.Name; }
        }

        /// <summary>
        /// true, if start state
        /// </summary>
        public bool IsStart
        {
            get { return _State.IsStart; }
        }

        /// <summary>
        /// true, if final state
        /// </summary>
        public bool IsFinal
        {
            get { return _State.IsFinal; }
        }
    }
}
