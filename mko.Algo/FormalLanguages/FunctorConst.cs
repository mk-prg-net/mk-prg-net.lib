using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Algo.fml
{    
    public class FunctorConst<T> : FunctorBase<T>
    {
        public T A { get; set; }

        public override T map()
        {
            return A;
        }
    }
}
