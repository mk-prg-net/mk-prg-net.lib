using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.Automaton.Builder.Impl
{
    public class MooreAutomatonBuilder<TStateEnum> : IMooreAutomatonBuilder<TStateEnum>
        where TStateEnum : struct
    {
        public IMooreTransitionFunctionBuilder<TStateEnum> CreateTransistionFunctionBuilder()
        {
            return new MooreTransistionFunctionBuilder<TStateEnum>(sd.Definitions);
        }


        StateDecoratorBuilder<TStateEnum> sd = new StateDecoratorBuilder<TStateEnum>();

        public void DefineStateAsFinal(TStateEnum state)
        {
            sd.DefineStateAsFinal(state);
        }

        public void DefineStateAsStart(TStateEnum state)
        {
            sd.DefineStateAsStart(state);
        }
    }
}
