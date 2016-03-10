using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp
{
    public partial class Int64 : MathOps.IFactory<long>
    {
        /// <summary>
        /// Singleton
        /// </summary>
        public static Int64 _ = new Int64();

        static MathOps.ArithmetikOpsInt64 AOpsInt64 = new MathOps.ArithmetikOpsInt64();

        public MathOps.ADD<long> ADD(params Core.INaLisp[] Elements)
        {
            return new MathOps.ADD<long>(AOpsInt64, Elements);
        }

        public  MathOps.SUB<long> SUB(params Core.INaLisp[] Elements)
        {
            return new MathOps.SUB<long>(AOpsInt64, Elements);
        }

        public  MathOps.MUL<long> MUL(params Core.INaLisp[] Elements)
        {
            return new MathOps.MUL<long>(AOpsInt64, Elements);
        }

        public  MathOps.DIV<long> DIV(params Core.INaLisp[] Elements)
        {
            return new MathOps.DIV<long>(AOpsInt64, Elements);
        }

    }
}
