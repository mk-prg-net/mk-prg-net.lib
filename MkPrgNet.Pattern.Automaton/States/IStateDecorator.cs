using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.Automaton.States
{
    public interface IStateDecorator<TStateEnum>
        where TStateEnum : struct
    {
        bool IsStart(TStateEnum state);
        bool IsFinal(TStateEnum state);
    }
}
