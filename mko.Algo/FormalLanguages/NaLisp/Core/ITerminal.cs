using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Core
{
    public interface ITerminal
    {
        Inspector.ProtocolEntry Validate(NaLispStack Stack);

        Evaluator.Result Eval(NaLispStack StackInstance, bool DebugOn);

    }
}
