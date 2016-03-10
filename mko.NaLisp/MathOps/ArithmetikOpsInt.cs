using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.MathOps
{
    public class ArithmetikOpsInt: IArithmetikOps<int>
    {
        public Func<int, int, int> ADD()
        {
            return (a, b) => a + b;
        }

        public Func<int, int, int> SUB()
        {
            return (a, b) => a - b;
        }

        public Func<int, int, int> DIV()
        {
            return (a, b) => a / b;
        }

        public Func<int, int, int> MUL()
        {
            return (a, b) => a * b;
        }
    }
}
