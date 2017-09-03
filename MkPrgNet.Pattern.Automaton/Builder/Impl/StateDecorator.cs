using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.Automaton.Builder.Impl
{
    public class StateDecoratorBuilder<TStateEnum> : IStateDecoratorBuilder<TStateEnum>
        where TStateEnum : struct
    {
        HashSet<TStateEnum> finals = new HashSet<TStateEnum>();
        TStateEnum start;

        public void DefineStateAsFinal(TStateEnum state)
        {
            finals.Add(state);
        }

        public void DefineStateAsStart(TStateEnum state)
        {
            start = state;
        }

        public Tuple<TStateEnum, HashSet<TStateEnum>> Definitions
        {
            get
            {
                return Tuple.Create(start, finals);
            }
        }

    }
}
