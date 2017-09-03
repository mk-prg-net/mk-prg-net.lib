using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.Automaton.Builder.Impl
{
    class TransistionFunctionBuilder<TStateEnum> : IStateTransitionsBuilder<TStateEnum>
        where TStateEnum : struct
    {

        public HashSet<IInput> Inputs = new HashSet<IInput>();

        /// <summary>
        /// Abbildung des Zustandsautomatennetzes 
        /// </summary>        
        public Dictionary<int, TStateEnum> Transistions = new Dictionary<int, TStateEnum>();

        public void DefNewTransistionFor(IInput input, params TStateEnum[] subsequentStates)
        {
            Inputs.Add(input);
            TStateEnum[] states = (TStateEnum[])Enum.GetValues(typeof(TStateEnum));
            for (int i = 0; i < states.Length; i++)
            {
                Transistions[Tuple.Create(input, states[i]).GetHashCode()] = subsequentStates[i];
            }
        }
    }
}
