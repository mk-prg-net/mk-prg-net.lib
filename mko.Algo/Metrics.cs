using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo
{
    public class Metrics
    {
        /// <summary>
        /// Abstand zweier Punkte auf einem Zahlenstrahl
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static decimal Distance(decimal a, decimal b)
        {
            decimal diff = a - b;
            return diff < 0 ? -1m * diff : diff;
        }

    }
}
