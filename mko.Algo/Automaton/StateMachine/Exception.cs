using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.Automaton.StateMachine
{
    public class UnexcpectedStateException : Exception
    {
        public UnexcpectedStateException(State ActiveState)
            : base("Automat befindet sich in einem unerwarteten Zustand:" + ActiveState.Name)
        {

        }
    }
}
