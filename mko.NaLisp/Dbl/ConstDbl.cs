using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Dbl
{
    public class ConstDbl : Dbl, Core.ITerminal, Core.IConstValueProp<double>
    {
        ImplHelper.ConstVal<ConstDbl, double> hlp;

        public ConstDbl(long Value)
            : base((int)Core.NameDir.Names.ConstDbl)
        {
            hlp = new ImplHelper.ConstVal<ConstDbl, double>(this, Value);
        }

        //public override bool IsTerminal
        //{
        //    get { return true; }
        //}

        public double Value {
            get
            {
                return hlp.ConstValue;
            }
        }

        public Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack)
        {
            return hlp.Validate(Stack);
        }

        public Core.Evaluator.Result Eval(Core.NaLispStack StackInstance, bool DebugOn)
        {
            return hlp.Eval(StackInstance, DebugOn);
        }

        public override Core.NaLisp Clone(bool deep = true)
        {
            throw new NotImplementedException();
        }
    }
}
