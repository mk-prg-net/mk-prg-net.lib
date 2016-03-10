using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Long
{
    public class ConstLong : Long, Core.ITerminal, Core.IConstValueProp<long>
    {
        ImplHelper.ConstVal<ConstLong, long> hlp;
        public ConstLong(long Value)
            : base((int)Core.NameDir.Names.ConstLong)
        {
            hlp = new ImplHelper.ConstVal<ConstLong, long>(this, Value);
        }

        //public override bool IsTerminal
        //{
        //    get { return true; }
        //}

        public long Value { 
            get{
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
