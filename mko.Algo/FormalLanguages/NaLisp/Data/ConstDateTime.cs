using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp.Data
{
    public class ConstDateTime : ConstVal<DateTime>
    {
        public ConstDateTime(DateTime Value)
            : base((int)Core.NameDir.Names.ConstDateTime, Value)
        { }

        protected override Type GetType()
        {
            return typeof(ConstDateTime);
        }

        public override Core.NaLisp Clone(bool deep = true)
        {
            return new ConstDateTime(Value);
        }
    }
}
