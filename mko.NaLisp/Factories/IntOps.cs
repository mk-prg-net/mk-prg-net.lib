using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.Factories
{
    public partial class Int : MathOps.IFactory<int>
    {
        public static Int _ = new Int(); 

        static MathOps.ArithmetikOpsInt AOpsInt = new MathOps.ArithmetikOpsInt();

        public MathOps.ADD<int> ADD(params Core.INaLisp[] Elements)
        {
            return new MathOps.ADD<int>(AOpsInt, Elements);
        }

        public MathOps.SUB<int> SUB(params Core.INaLisp[] Elements)
        {
            return new MathOps.SUB<int>(AOpsInt, Elements);
        }

        public MathOps.MUL<int> MUL(params Core.INaLisp[] Elements)
        {
            return new MathOps.MUL<int>(AOpsInt, Elements);
        }

        public MathOps.DIV<int> DIV(params Core.INaLisp[] Elements)
        {
            return new MathOps.DIV<int>(AOpsInt, Elements);
        }
    }
}
