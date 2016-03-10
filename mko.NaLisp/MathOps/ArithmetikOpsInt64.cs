using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.MathOps
{
    public class ArithmetikOpsInt64 : IArithmetikOps<long>
    {
        public Func<long, long, long> ADD()
        {
            return (a, b) => a + b;
        }

        public Func<long, long, long> SUB()
        {
            return (a, b) => a - b;
        }

        public Func<long, long, long> DIV()
        {
            return (a, b) => a / b;
        }

        public Func<long, long, long> MUL()
        {
            return (a, b) => a * b;
        }
    }
}
