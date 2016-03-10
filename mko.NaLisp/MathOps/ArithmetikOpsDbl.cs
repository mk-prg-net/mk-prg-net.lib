using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.NaLisp.MathOps
{
    public class ArithmetikOpsDbl : IArithmetikOps<double>
    {
        public Func<double, double, double> ADD()
        {
            return (a, b) => a + b;
        }

        public Func<double, double, double> SUB()
        {
            return (a, b) => a - b;
        }

        public Func<double, double, double> DIV()
        {
            return (a, b) => a / b;
        }

        public Func<double, double, double> MUL()
        {
            return (a, b) => a * b;
        }
    }
}
