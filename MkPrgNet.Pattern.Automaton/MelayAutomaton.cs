using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MkPrgNet.Pattern.Automaton.States;

namespace MkPrgNet.Pattern.Automaton
{
    public class MelayAutomaton<TStateEnum> : IAutomaton<TStateEnum>
        where TStateEnum : struct
    {
        public TStateEnum CurrentState
        {
            get
            {
                return _currentState;
            }
        }
        TStateEnum _currentState;

        public IEnumerable<IInput> Inputs
        {
            get;
        }

        public IStateDecorator<TStateEnum> StateProperties
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<TStateEnum> PossibleSubsequentStatesOf(TStateEnum State)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TStateEnum> PossibleSubsequentStatesOfCurrent()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Transistion()
        {
            throw new NotImplementedException();
        }
    }
}
