using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.FormalLanguages.NaLisp
{
    partial class Factory
    {
        public static MathOps.ADDtoInt ADD_to_Int(params Core.NaLisp[] Elements)
        {
            return new MathOps.ADDtoInt(Elements);
        }

        public static MathOps.SUBtoInt SUB_to_Int(params Core.NaLisp[] Elements)
        {
            return new MathOps.SUBtoInt(Elements);
        }

        public static MathOps.MULtoInt MUL_to_Int(params Core.NaLisp[] Elements)
        {
            return new MathOps.MULtoInt(Elements);
        }

        public static MathOps.DIVtoInt DIV_to_Int(params Core.NaLisp[] Elements)
        {
            return new MathOps.DIVtoInt(Elements);
        }


    }
}
