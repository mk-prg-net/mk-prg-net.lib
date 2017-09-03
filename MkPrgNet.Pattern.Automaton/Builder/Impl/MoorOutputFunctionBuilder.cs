using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.Automaton.Builder.Impl
{
    public class MoorOutputFunctionBuilder<TStateEnum> : IMooreOutputFunctionBuilder<TStateEnum>
        where TStateEnum : struct
    {
        Tuple<TStateEnum, HashSet<TStateEnum>> stateDeco;
        HashSet<IInput> Inputs;
        Dictionary<int, TStateEnum> Transistions;
        Dictionary<TStateEnum, IOutput> outputs = new Dictionary<TStateEnum, IOutput>();

        public MoorOutputFunctionBuilder(Tuple<TStateEnum, HashSet<TStateEnum>> stateDeco, HashSet<IInput> Inputs, Dictionary<int, TStateEnum> Transistions)
        {
            this.stateDeco = stateDeco;
            this.Inputs = Inputs;
            this.Transistions = Transistions;
        }       

        public IAutomaton<TStateEnum> CreateMooreAutomaton()
        {
            return new global::MkPrgNet.Pattern.Automaton.MooreAutomaton<TStateEnum>(stateDeco, Inputs, Transistions, outputs);
        }

        public void DefineOutputFunctorFor(TStateEnum state, IOutput output)
        {
            outputs[state] = output;
        }
    }
}
