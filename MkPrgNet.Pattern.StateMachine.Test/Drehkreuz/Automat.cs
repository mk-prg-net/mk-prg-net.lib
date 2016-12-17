//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 15.12.2016
//
//  Projekt.......: MkPerNet.Pattern.StateMachine.Test
//  Name..........: Drehkreuz\Automat.cs
//  Aufgabe/Fkt...: Implementiert den Drehkreuz- Automaten
//                  
//                         O
//                         |
//                         |
//                 O-------X--------O
//                         |    | 
//                         |  <-/
//                         O
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

namespace MkPrgNet.Pattern.StateMachine.Test.Drehkreuz
{
    public class Automat : IStateMachine
    {
        StateMachine _fsm = new StateMachine();

        // Identitätsfunktionen zur Dokumentation der Zustandsübergänge
        int Zu_to(int ix) { return ix; }
        int Auf_to(int ix) { return ix; }


        // Indizes der Eingabesysteme
        readonly int ixMuenzeinwurf;
        readonly int ixDrehkreuz;

        public Automat()
        {
            // Alle Zustände definieren
            var _Zu = _fsm.AddState(new Zu.State());
            var _Auf = _fsm.AddState(new Auf.State());
          
            // Eingaben, und die mit Ihnen verbundenen Zustandsübergänge definieren
            ixMuenzeinwurf = _fsm.AddInput(new Inputs.Muenzeinwurf(), Zu_to(_Auf.IX), Auf_to(_Auf.IX));
            ixDrehkreuz = _fsm.AddInput(new Inputs.Drehkreuz(), Zu_to(_Zu.IX), Auf_to(_Zu.IX));
        }


        public IList<IInput> Inputs
        {
            get { return _fsm.Inputs; }
        }

        public int GetInputIx(string Name)
        {
            return _fsm.GetInputIx(Name);
        }

        /// <summary>
        /// Hier können Münzen eingeworfen werden (Eingabe)
        /// </summary>
        public Inputs.Muenzeinwurf Muenzeinwurf
        {
            get
            {
                return (Test.Drehkreuz.Inputs.Muenzeinwurf)Inputs[ixMuenzeinwurf];
            }
        }

        /// <summary>
        /// Hier kann versucht werden, das Drehkreuz zu passieren
        /// </summary>
        public Inputs.Drehkreuz Drehkreuz
        {
            get
            {
                return (Test.Drehkreuz.Inputs.Drehkreuz)Inputs[ixDrehkreuz];
            }
        }

        public IList<IState> States
        {
            get { return _fsm.States; }
        }

        public int GetStateIx(string Name)
        {
            return _fsm.GetStateIx(Name);
        }

        public void DefineOutput(int IndexOfInput, int IndexOfState, IOutput output)
        {
            _fsm.DefineOutput(IndexOfInput, IndexOfState, output);
        }

        public void DefineOutputFunc(int IndexOfInput, int IndexOfState, Action<IInput> outputFunction)
        {
            _fsm.DefineOutputFunc(IndexOfInput, IndexOfState, outputFunction);
        }


        public IState CurrentState
        {
            get { return _fsm.CurrentState; }
        }

        public void Transistion()
        {
            _fsm.Transistion();
        }

        public IEnumerable<IState> PossibleSubsequentStatesOf(int ixState)
        {
            return _fsm.PossibleSubsequentStatesOf(ixState);
        }

        public IEnumerable<IState> PossibleSubsequentStatesOfCurrent()
        {
            return _fsm.PossibleSubsequentStatesOfCurrent();
        }


        public void Start()
        {
            _fsm.Start();
        }

        public void Start(int ixStartState)
        {
            _fsm.Start(ixStartState);
        }


    }
}
