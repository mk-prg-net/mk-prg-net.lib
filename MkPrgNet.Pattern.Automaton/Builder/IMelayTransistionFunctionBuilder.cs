using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.Automaton
{
    /// <summary>
    /// Builder, that defines for every Input Type all subsequent States
    /// input x state -> state
    /// </summary>
    /// <typeparam name="TState">Enum, that defines all states of automaton</typeparam>
    public interface IMelayTransistionFunctionBuilder<TState> : IStateTransitionsBuilder<TState>
        where TState : struct
    {
        /// <summary>
        /// Defines the output functor for a given transistion (state x input -> state)
        /// </summary>
        /// <returns></returns>
        IMelayOutputFunctionBuilder<TState> CreateOutputFunctionBuilder();

    }
}
