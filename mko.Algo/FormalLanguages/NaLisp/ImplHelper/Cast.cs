using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.ImplHelper
{
    public class Cast<TNaLispTarget>       
        where TNaLispTarget : Core.NaLisp
    {

        public Cast(Core.NaLisp Source, Func<Core.NaLisp, TNaLispTarget> CastOp)
        {
            this.Source = Source;
            this.CastOp = CastOp;
        }

        Func<Core.NaLisp, TNaLispTarget> CastOp;
        Core.NaLisp Source;
        public Core.NaLisp[] Elements
        {
            get { return new Core.NaLisp[]{Source}; }
        }

        public Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            throw new NotImplementedException();
        }

        public Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            var res = CastOp(EvaluatedElements.Single().ResultTerm);
            return new Core.Evaluator.Result(res, new Core.Inspector.ProtocolEntry(StackInstance.Peek(), true, true, typeof(TNaLispTarget), ToString()));
            
        }
    }
}
