using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Data
{
    public class ConstBool : ConstVal<bool>
    {
        public ConstBool(bool Value) : base((int)Core.NameDir.Names.ConstBool, Value) { }

        public override Core.NaLisp Clone(bool deep)
        {
            return new ConstBool(Value);
        }

        protected override Type GetType()
        {
            return typeof(ConstBool);
        }
    }
}
