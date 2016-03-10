using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Algo.fml
{
    public class FunctorConvert<T, TSource> : FunctorBase<T>
    {
        public TSource A { get; set; }

        public Func<TSource, T> converter { get; set; }

        public override T map()
        {
            return converter(A);
        }
    }
}
