using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Algo.FunctionalProgramming
{
    public static class Fn
    {
        /// <summary>
        /// Entnommen aus http://blogs.msdn.com/b/wesdyer/archive/2007/01/29/currying-and-partial-function-application.aspx
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="f"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Func<B, R> Curry<A, B, R>(this Func<A, B, R> f, A a)
        {
            return b => f(a, b);
        }

        public static Action<B> Curry<A, B>(this Action<A, B> f, A a)
        {
            return b => f(a, b);
        }
    }
}
