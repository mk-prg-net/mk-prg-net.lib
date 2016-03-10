using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.Automaton.StateMachine
{
    public class NormalState : BehaviorOfState
    {
        public override bool IsFinal
        {
            get { return false; }
        }

        public override bool IsStart
        {
            get { return false; }
        }
    }
}
