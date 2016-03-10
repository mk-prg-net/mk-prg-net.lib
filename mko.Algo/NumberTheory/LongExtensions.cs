using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.CompilerServices;

namespace mko.Algo.NumberTheory
{
    
    public static class LongExtensions
    {
        /// <summary>
        /// Liefert true, wenn z ungerade
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public static bool IsOdd(this long z)
        {
            return z % 2 == 1;
        }
        
        /// <summary>
        /// Liefert true, wenn z gerade
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public static bool IsEven(this long z)
        {
            return z % 2 == 0;
        }
        
        /// <summary>
        /// Liefert den Absoluten Betrag von z
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public static long ABS(this long z)
        {
            if (z < 0)
                return -z;
            else if (z > 0)
                return z;
            else
                return 0;
        }
        
        /// <summary>
        /// Liefert true, wenn z eine Primzahl ist
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public static bool IsPrime(this long z)
        {
            long primefactor;
            return PrimeFactors.PrimeTest(z, out primefactor);
        }
    }
}
