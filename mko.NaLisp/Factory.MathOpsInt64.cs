using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp
{
    partial class Factory
    {
        static MathOps.ArithmetikOpsInt64 AOpsInt64 = new MathOps.ArithmetikOpsInt64();

        public static MathOps.ADD<long> ADD_to_int64(params Core.NaLisp[] Elements)
        {
            return new MathOps.ADD<long>(AOpsInt64, Elements);
        }

        public static MathOps.SUB<long> SUB_to_int64(params Core.NaLisp[] Elements)
        {
            return new MathOps.SUB<long>(AOpsInt64, Elements);
        }

        public static MathOps.MUL<long> MUL_to_int64(params Core.NaLisp[] Elements)
        {
            return new MathOps.MUL<long>(AOpsInt64, Elements);
        }

        public static MathOps.DIV<long> DIV_to_int64(params Core.NaLisp[] Elements)
        {
            return new MathOps.DIV<long>(AOpsInt64, Elements);
        }

        // elementare Datentypen

        public static MathOps.ADD<long> ADD_to_int64(params long[] Elements)
        {
            return new MathOps.ADD<long>(AOpsInt64, Elements);
        }

        public static MathOps.SUB<long> SUB_to_int64(params long[] Elements)
        {
            return new MathOps.SUB<long>(AOpsInt64, Elements);
        }

        public static MathOps.MUL<long> MUL_to_int64(params long[] Elements)
        {
            return new MathOps.MUL<long>(AOpsInt64, Elements);
        }

        public static MathOps.DIV<long> DIV_to_int64(params long[] Elements)
        {
            return new MathOps.DIV<long>(AOpsInt64, Elements);
        }

    }
}
