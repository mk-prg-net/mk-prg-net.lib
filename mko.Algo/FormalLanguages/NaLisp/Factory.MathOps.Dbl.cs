using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp
{
    public partial class Factory
    {
        public static MathOps.ADDtoDbl ADD_to_Dbl(params Core.NaLisp[] Elements)
        {
            return new MathOps.ADDtoDbl(Elements);
        }

        public static MathOps.SUBtoDbl SUB_to_Dbl(params Core.NaLisp[] Elements)
        {
            return new MathOps.SUBtoDbl(Elements);
        }

        public static MathOps.MULtoDbl MUL_to_Dbl(params Core.NaLisp[] Elements)
        {
            return new MathOps.MULtoDbl(Elements);
        }

        public static MathOps.DIVtoDbl DIV_to_Dbl(params Core.NaLisp[] Elements)
        {
            return new MathOps.DIVtoDbl(Elements);
        }


    }
}
