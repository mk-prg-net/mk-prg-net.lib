//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 15.12.2016
//
//  Projekt.......: MkPrgNet.Pattern.StateMachine.Test
//  Name..........: CreateDraft\Automat.cs
//  Aufgabe/Fkt...: Endlicher Automat, mit dem ein Dokumentenentwurd aus einem Tokenstrom
//                  eingelesen werden kann.
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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.StateMachine.Test.CreateDraft
{
    public class Automat : Pattern.StateMachine.IStateMachine
    {

        Pattern.StateMachine.StateMachine _fsm = new StateMachine();

        public IList<IInput> Inputs
        {
            get { return _fsm.Inputs; }
        }

        public int GetInputIx(string Name)
        {
            return _fsm.GetInputIx(Name);
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

        public void Start()
        {
            _fsm.Start();
        }

        public void Start(int ixStartState)
        {
            _fsm.Start(ixStartState);
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

        public Automat()
        {
            // Zustände erzeugen
            var Start = _fsm.CreateState<Start.State>();
            var DefAuthor = _fsm.CreateState<DefAuthor.State>();
            var DefNode = _fsm.CreateState<DefNode.State>();
            var DefType = _fsm.CreateState<DefType.State>();
            var DocDefTheme = _fsm.CreateState<DocDefTheme.State>();
            var DocDefTocParent = _fsm.CreateState<DocDefTocParent.State>();
            var TocDefParent = _fsm.CreateState<TocDefParent.State>();
            var TocDefTheme = _fsm.CreateState<TocDefTheme.State>();
            var Error = _fsm.CreateState<Error.State>();
            var Seal = _fsm.CreateState<Seal.State>();

            // Zustandsüberführungen definieren
            var InputAuthorIX = _fsm.CreateInput<DefAuthor.InputAuthor>(
                Error,
                Start.To(DefAuthor),
                DefNode.To(DefType));

            _fsm.DefineOutputFunc(InputAuthorIX, Start.IX, input => Debug.WriteLine("Im Zustand " + Start.Name + " wurde der Author eingegeben"));


            var InputNodeIX = _fsm.CreateInput<DefNode.InputNode>(
                Error,
                Start.To(DefNode),
                DefAuthor.To(DefType));

            // Docs
            var InputTypeDocIX =  _fsm.CreateInput<DefType.InputTypeDoc>(
                Error,
                DefType.To(DocDefTheme));

            _fsm.DefineOutputFunc(InputTypeDocIX, DefType.IX, input => Debug.WriteLine("Im Zustand " + DefType.Name + " wurde der Typ Doc gewählt"));

            // Tocs
            _fsm.CreateInput<DefType.InputTypeToc>(
                Error,
                DefType.To(TocDefTheme));

            _fsm.CreateInput<DocDefTheme.InputTheme>(
                Error,
                DocDefTheme.To(DocDefTocParent),
                TocDefTheme.To(TocDefParent));


            _fsm.CreateInput<CreateDraft.Seal.InputSeal>(
                Error,
                DocDefTocParent.To(Seal),
                TocDefParent.To(Seal));
        }


    }
}
