using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Data
{
    public class ConstString: ConstVal<string>
    {
        public ConstString(string Value) : base((int)Core.NameDir.Names.ConstString, Value) { }

        public override Core.NaLisp Clone(bool deep)
        {
            return new ConstString(Value);
        }

        protected override Type GetType()
        {
            return typeof(ConstString);
        }

    }
}
