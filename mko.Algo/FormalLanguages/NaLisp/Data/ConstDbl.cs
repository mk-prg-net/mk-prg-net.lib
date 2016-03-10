using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Data
{
    public class ConstDbl : ConstVal<double>
    {
        public ConstDbl(double Value) : base((int)Core.NameDir.Names.ConstDbl, Value) { }

        public override Core.NaLisp Clone(bool deep)
        {
            return new ConstDbl(Value);
        }

        protected override Type GetType()
        {
            return typeof(ConstDbl);
        }

    }
}
