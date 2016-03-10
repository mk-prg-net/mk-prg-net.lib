using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Data
{
    public class VarOfComp<T> : VarOf<T>, IVarOfComp<T>
        where T : IComparable<T>
    {
        public VarOfComp(string Name)
            : base(Name)
        { }


        public VarOfComp(string Name, T Value)
            : base(Name, Value)
        { }

        public int CompareTo(T other)
        {            
            return this.Value.CompareTo(other);
        }

        public override Core.Inspector.ProtocolEntry Validate(Core.NaLispStack Stack)
        {
            return new Core.Inspector.ProtocolEntry(this, true, true, typeof(ConstValComp<T>), "");
        }


        public override Core.INaLisp Eval(Core.NaLispStack StackInstance, bool DebugOn)
        {
            return new ConstValComp<T>(Value);
        }

        public override Core.INaLisp Clone(bool deep = true)
        {
            return new VarOfComp<T>(VarName);
        }

    }
}
