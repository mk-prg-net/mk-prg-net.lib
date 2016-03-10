using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Algo.fml
{
    public class FunctorUnary<T> : FunctorBase<T>
    {
        public FunctorBase<T> A { get; set; }
        public Func<T, T> op { get; set; }

        public override T map()
        {
            return op(A.map());
        }
    }
}
