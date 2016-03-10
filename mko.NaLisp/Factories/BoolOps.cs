using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    public partial class Bool : BoolOps.IFactory
    {
        public static Bool _ = new Bool();

        public BoolOps.AND AND(params Core.INaLisp[] Elements)
        {
            return new BoolOps.AND(Elements);
        }

        public BoolOps.OR OR(params Core.INaLisp[] Elements)
        {
            return new BoolOps.OR(Elements);
        }

        public BoolOps.NOT NOT(Core.INaLisp Element)
        {
            return new BoolOps.NOT(new Core.INaLisp[]{ Element});
        }
    }
}
