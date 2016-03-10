using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Core
{
    public interface INonTerminal
    {
        NaLisp[] Elements { get; }

        Inspector.ProtocolEntry Validate(NaLispStack Stack, Inspector.ProtocolEntry[] ElemValidationResult);

        Evaluator.Result Eval(Evaluator.Result[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn);
    }
}
