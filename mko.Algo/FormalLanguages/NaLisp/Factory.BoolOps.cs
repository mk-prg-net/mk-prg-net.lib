using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp
{
    partial class Factory
    {
        public static BoolOps.AND AND(params Core.NaLisp[] Elements)
        {
            return new BoolOps.AND(Elements);
        }

        public static BoolOps.OR OR(params Core.NaLisp[] Elements)
        {
            return new BoolOps.OR(Elements);
        }

        public static BoolOps.NOT NOT(Core.NaLisp Element)
        {
            return new BoolOps.NOT(new Core.NaLisp[]{ Element});
        }


    }
}
