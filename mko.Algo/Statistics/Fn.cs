using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using lisp = mko.Algo.Listprocessing;
using Lint = System.Collections.Generic.IEnumerable<int>;
using Ldouble = System.Collections.Generic.IEnumerable<double>;

namespace mko.Algo.Statistics
{
    public static class Fn
    {
        /// <summary>
        /// Summenbildung
        /// Summe({a1, a2, …, aN}): {a1, a2, …, aN} → a1 + a2 + … + aN
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static double Sum(Ldouble list)
        {
            if (lisp.Fn.Count(list) == 0)
                return 0;
            else if (lisp.Fn.Count(list) == 1)
                return lisp.Fn.First(list);
            else
                return lisp.Fn.First(list) + Sum(lisp.Fn.Skip(list, 1));
        }


        public static double Sum(Lint list)
        {
            if (lisp.Fn.Count(list) == 0)
                return 0;
            else if (lisp.Fn.Count(list) == 1)
                return lisp.Fn.First(list);
            else
                return lisp.Fn.First(list) + Sum(lisp.Fn.Skip(list, 1));
        }
        
        /// <summary>
        ///  Minima finden
        ///  Min({…}): {…} → min mit min ϵ {…} & Für alle x ϵ {…} gilt: min <= x
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static double Min(Ldouble list)
        {
            if (lisp.Fn.Count(list) == 0)
                throw new Exception();
            else if (lisp.Fn.Count(list) == 1)
                return lisp.Fn.First(list);
            else if (lisp.Fn.First(list) < Min(lisp.Fn.Skip(list, 1)))
                return lisp.Fn.First(list);
            else
                return Min(lisp.Fn.Skip(list, 1));
        }

        public static double Min(Lint list)
        {
            if (lisp.Fn.Count(list) == 0)
                throw new Exception();
            else if (lisp.Fn.Count(list) == 1)
                return lisp.Fn.First(list);
            else if (lisp.Fn.First(list) < Min(lisp.Fn.Skip(list, 1)))
                return lisp.Fn.First(list);
            else
                return Min(lisp.Fn.Skip(list, 1));
        }

    }
}
