using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.Automaton.States.Impl
{
    public class StateDecorator<TStateEnum> : IStateDecorator<TStateEnum>
        where TStateEnum : struct
    {

        internal StateDecorator(Tuple<TStateEnum, HashSet<TStateEnum>> stateDeco)
        {
            this.stateDeco = stateDeco;
        }

        Tuple<TStateEnum, HashSet<TStateEnum>> stateDeco;

        public bool IsFinal(TStateEnum state)
        {
            return stateDeco.Item1.Equals(state);
        }

        public bool IsStart(TStateEnum state)
        {
            return stateDeco.Item2.Contains(state);
        }
    }
}
