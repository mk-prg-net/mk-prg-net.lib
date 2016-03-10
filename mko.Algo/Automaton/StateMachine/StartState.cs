using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.Automaton.StateMachine
{
    public class StartState : BehaviorOfState
    {
        public override bool IsStart
        {
            get { return true; }
        }

        public override bool IsFinal
        {
            get { return false; }
        }
    }
}
