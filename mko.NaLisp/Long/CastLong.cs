using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Long
{
    public class CastLong : Long, Core.INonTerminal
    {
        ImplHelper.Cast<ConstLong> hlp;

        public CastLong(Dbl.Dbl DblNaLisp)
            : base((int)Core.NameDir.Names.CastLong)
        {
            hlp = new ImplHelper.Cast<ConstLong>(DblNaLisp, na => new ConstLong((long)((Dbl.ConstDbl)na).Value));
        }

        //public override bool IsTerminal
        //{
        //    get { return false; }
        //}
        
        public Core.NaLisp[] Elements
        {
            get { return hlp.Elements; }
        }

        public Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack, Core.Inspector.ProtocolEntry[] ElemValidationResult)
        {
            throw new NotImplementedException();
        }

        public Core.Evaluator.Result Eval(Core.Evaluator.Result[] EvaluatedElements, Core.NaLispStack StackInstance, bool DebugOn)
        {
            return hlp.Eval(EvaluatedElements, StackInstance, DebugOn);            
        }

        public override Core.NaLisp Clone(bool deep = true)
        {
            throw new NotImplementedException();
        }
    }
}
