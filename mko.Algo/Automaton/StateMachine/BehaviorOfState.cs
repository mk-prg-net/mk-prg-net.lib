using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.Automaton.StateMachine
{
    public abstract class BehaviorOfState
    {
        public abstract bool IsStart
        {
            get;
        }

        public abstract bool IsFinal
        {
            get;
        }
    }
}
