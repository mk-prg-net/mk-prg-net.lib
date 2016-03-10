using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Data
{
    public class ConstInt : ConstVal<int>
    {
        public ConstInt(int Value) : base((int)Core.NameDir.Names.ConstInt, Value) { }

        public override Core.NaLisp Clone(bool deep)
        {
            return new ConstInt(Value);
        }

        protected override Type GetType()
        {
            return typeof(ConstInt);
        }

    }
}
