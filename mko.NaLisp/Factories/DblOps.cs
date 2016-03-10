using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    public partial class Dbl : MathOps.IFactory<double>
    {
        /// <summary>
        /// Singleton
        /// </summary>
        public static Dbl _ = new Dbl();

        static MathOps.ArithmetikOpsDbl AOpsDbl = new MathOps.ArithmetikOpsDbl();

        public MathOps.ADD<double> ADD(params Core.INaLisp[] Elements)
        {
            return new MathOps.ADD<double>(AOpsDbl, Elements);
        }

        public MathOps.SUB<double> SUB(params Core.INaLisp[] Elements)
        {
            return new MathOps.SUB<double>(AOpsDbl, Elements);
        }

        public MathOps.MUL<double> MUL(params Core.INaLisp[] Elements)
        {
            return new MathOps.MUL<double>(AOpsDbl, Elements);
        }

        public MathOps.DIV<double> DIV(params Core.INaLisp[] Elements)
        {
            return new MathOps.DIV<double>(AOpsDbl, Elements);
        }

    }
}
