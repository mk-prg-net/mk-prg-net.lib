using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp
{
    public partial class Factory
    {
        static MathOps.ArithmetikOpsDbl AOpsDbl = new MathOps.ArithmetikOpsDbl();

        public static MathOps.ADD<double> ADD_to_dbl(params Core.NaLisp[] Elements)
        {
            return new MathOps.ADD<double>(AOpsDbl, Elements);
        }

        public static MathOps.SUB<double> SUB_to_dbl(params Core.NaLisp[] Elements)
        {
            return new MathOps.SUB<double>(AOpsDbl, Elements);
        }

        public static MathOps.MUL<double> MUL_to_dbl(params Core.NaLisp[] Elements)
        {
            return new MathOps.MUL<double>(AOpsDbl, Elements);
        }

        public static MathOps.DIV<double> DIV_to_dbl(params Core.NaLisp[] Elements)
        {
            return new MathOps.DIV<double>(AOpsDbl, Elements);
        }

        // mit elementaren Datentypen


        public static MathOps.ADD<double> ADD_to_dbl(params double[] Elements)
        {
            return new MathOps.ADD<double>(AOpsDbl, Elements);
        }

        public static MathOps.SUB<double> SUB_to_dbl(params double[] Elements)
        {
            return new MathOps.SUB<double>(AOpsDbl, Elements);
        }

        public static MathOps.MUL<double> MUL_to_dbl(params double[] Elements)
        {
            return new MathOps.MUL<double>(AOpsDbl, Elements);
        }

        public static MathOps.DIV<double> DIV_to_dbl(params double[] Elements)
        {
            return new MathOps.DIV<double>(AOpsDbl, Elements);
        }
    }
}
