using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp
{
    partial class Factory
    {
        static MathOps.ArithmetikOpsInt AOpsInt = new MathOps.ArithmetikOpsInt();

        public static MathOps.ADD<int> ADD_to_int(params Core.NaLisp[] Elements)
        {
            return new MathOps.ADD<int>(AOpsInt, Elements);
        }

        public static MathOps.SUB<int> SUB_to_int(params Core.NaLisp[] Elements)
        {
            return new MathOps.SUB<int>(AOpsInt, Elements);
        }

        public static MathOps.MUL<int> MUL_to_int(params Core.NaLisp[] Elements)
        {
            return new MathOps.MUL<int>(AOpsInt, Elements);
        }

        public static MathOps.DIV<int> DIV_to_int(params Core.NaLisp[] Elements)
        {
            return new MathOps.DIV<int>(AOpsInt, Elements);
        }


        // mit elementaren Datentypen

        public static MathOps.ADD<int> ADD_to_int(params int[] Elements)
        {
            return new MathOps.ADD<int>(AOpsInt, Elements);
        }

        public static MathOps.SUB<int> SUB_to_int(params int[] Elements)
        {
            return new MathOps.SUB<int>(AOpsInt, Elements);
        }

        public static MathOps.MUL<int> MUL_to_int(params int[] Elements)
        {
            return new MathOps.MUL<int>(AOpsInt, Elements);
        }

        public static MathOps.DIV<int> DIV_to_int(params int[] Elements)
        {
            return new MathOps.DIV<int>(AOpsInt, Elements);
        }



    }
}
