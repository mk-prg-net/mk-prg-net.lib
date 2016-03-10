using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.Algebra
{
    public class ZZGroup : FiniteGroups<int>
    {
        public ZZGroup(int Order)
        {
            _Order = Order;
        }

        public long Span(int a, int b)
        {
            if (b >= a)
                return b - a;
            else
                return b + (_Order - a);
        }

        public int Combine(int a, int b)
        {
            return (a + b) % _Order;
        }

        public int Unity
        {
            get { return 0; }
        }

        public int InverseOf(int a)
        {
            Debug.Assert(IsElement(a));
            return _Order - a;
        }

        public long Order
        {
            get { return _Order; }
        }

        int _Order;


        public bool IsElement(int a)
        {
            return (a >= 0 && a < Order);                
        }
    }
}
